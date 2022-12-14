using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChuckHill2.Forms;
using Microsoft.Win32;

namespace NetworkOnlineMonitor
{
    /// <summary>
    /// MAX_PATH and EnumerateFiles extracted from ChuckHill2.FileEx (ChuckHill2.Utilities\FileEx.cs)
    /// </summary>
    public static class StaticTools
    {
        /// <summary>
        /// The maximum size of a fully qualified (not relative) filename.
        /// </summary>
        /// <remarks>
        ///    The traditional maximum path has been 260 (256 + "\\?\") but as of Win10 this limit may be disabled and allow 
        ///    the true NTFS limit of 32767. To disable this limit within an application the registry flag HKEY_LOCAL_MACHINE
        ///    \SYSTEM\CurrentControlSet\Control\FileSystem\LongPathsEnabled [DWORD] must be set to 1, OS rebooted, 
        ///    AND enabled in the executable app manifest of all executables (not dlls) that use this variable.
        ///    <code>
        ///       <?xml version="1.0" encoding="utf-8"?>
        ///       <assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
        ///         <application xmlns="urn:schemas-microsoft-com:asm.v3">
        ///           <windowsSettings xmlns:ws2="http://schemas.microsoft.com/SMI/2016/WindowsSettings">
        ///             <ws2:longPathAware>true</ws2:longPathAware>
        ///           </windowsSettings>
        ///         </application>
        ///       </assembly>
        ///    </code>
        /// </remarks>
        public static readonly int MAX_PATH = (int)(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem", "LongPathsEnabled", 0) ?? 0) == 1 ? short.MaxValue : 256;

        /// <summary>
        /// A convenient pre-defined variable containing this executable's full path name. 
        /// </summary>
        public static readonly string ExecutableName = Process.GetCurrentProcess().MainModule.FileName;

        #region Win32
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        private struct WIN32_FIND_DATA
        {
            public FileAttributes dwFileAttributes;
            public ulong ftCreationTime;
            public ulong ftLastAccessTime;
            public ulong ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData);
        private const int ERROR_NO_MORE_FILES = 18;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FindClose(IntPtr hFindFile);

        [StructLayout(LayoutKind.Sequential)]
        private struct WIN32_FILE_ATTRIBUTE_DATA
        {
            public FileAttributes dwFileAttributes;
            public long ftCreationTime;
            public long ftLastAccessTime;
            public long ftLastWriteTime;
            public long nFileSize;
        }
        #endregion

        /// <summary>
        /// Returns an enumerable collection of file names in a specified path.
        /// Same as System.IO.Directory.EnumerateFiles(), except 42% faster on a SSD.
        /// To filter list, use this.Where(m=>m.something(m)) linq clause.
        /// </summary>
        /// <remarks>Does not throw exceptions.</remarks>
        /// <param name="folder">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories. The default value is System.IO.SearchOption.TopDirectoryOnly.</param>
        /// <returns>An enumerable collection of all the full names (including paths) for the files in the root directory specified by 'folder'.</returns>
        public static IEnumerable<string> EnumerateFiles(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly, CancellationToken cancel = default)
        {
            WIN32_FIND_DATA fd = new WIN32_FIND_DATA();
            IntPtr hFind = FindFirstFile(Path.Combine(folder, "*"), out fd);
            if (hFind == INVALID_HANDLE_VALUE)
            {
                yield break;
            }

            do
            {
                if (cancel.IsCancellationRequested) break;
                if (fd.cFileName == "." || fd.cFileName == "..") continue;   //pseudo-directory
                string path = Path.Combine(folder, fd.cFileName);
                if (path.Length > MAX_PATH) continue;
                if ((fd.dwFileAttributes & FileAttributes.Directory) != 0)
                {
                    if (searchOption == SearchOption.AllDirectories)
                    {
                        if ((fd.dwFileAttributes & FileAttributes.ReparsePoint) != 0) continue; //don't dive down into file links. they may be recursive!
                        foreach (var x in EnumerateFiles(path, searchOption, cancel))
                        {
                            yield return x;
                        }
                    }

                    continue;
                }

                yield return path;

            } while (FindNextFile(hFind, out fd));

            FindClose(hFind);

            yield break;
        }

        /// <summary>
        /// Enable/Disable this executable for autostart upon login.
        /// </summary>
        /// <param name="enable"></param>
        public static void SetAutoStart(bool enable)
        {
            // Key = HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
            // Name = NetworkOnlineMonitor
            // Value = full path to this executable.

            var value = ExecutableName;
            var name = Path.GetFileNameWithoutExtension(ExecutableName);
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (key == null) return; // Key doesn't exist.
                string oldvalue = key.GetValue(name, null) as string;
                var exists = oldvalue != null;
                if (!enable && exists) key.DeleteValue(name);
                else if (enable && oldvalue != value) key.SetValue(name, value);
            }
        }

        /// <summary>
        /// Test if this app will be automatically started upon login
        /// </summary>
        /// <returns>True if this app is set to automatically start upon login</returns>
        public static bool AutostartExists()
        {
            var name = Path.GetFileNameWithoutExtension(ExecutableName);
            string oldvalue = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", name, null) as string;
            //The exe may have been moved so we not only test for null(existance) but also test for path equality.
            return ExecutableName == oldvalue;
        }

        #region public static long UtcTimerTicks
        [DllImport("Kernel32.dll")]
        private static extern void GetSystemTimeAsFileTime(out long SystemTimeAsFileTime);
        /// <summary>
        /// UTC time in ticks. Lowest overhead. Great for determining duration.
        /// </summary>
        public static long UtcTimerTicks
        {
            get
            {
                GetSystemTimeAsFileTime(out var ticks);
                return ticks + 0x0701ce1722770000; //offset from 1/1/1601 to 1/1/0001
            }
        }
        #endregion public static long UtcTimerTicks

        #region public static void AllowOnlyOneInstance()
        private const int SW_RESTORE = 9;
        [DllImport("User32.dll")] private static extern bool ShowWindow(IntPtr handle, int nCmdShow);
        [DllImport("User32.dll")] private static extern bool SetForegroundWindow(IntPtr handle);

        /// <summary>
        /// Allow only one instance of this executable to run.
        /// The other instance window is popped open to the foreground and this instance terminates.
        /// This should be the first line of Program.cs:Program.Main().
        /// </summary>
        /// <param name="caption">Window caption to search by if window not visible (e.g. not in the taskbar). This may occur when using the NotifyIcon control.</param>
        public static void AllowOnlyOneInstance(string caption)
        {
            var currentProcess = Process.GetCurrentProcess();
            var processes = Process.GetProcessesByName(currentProcess.ProcessName);
            if (processes.Length <= 1) return;  //should never be zero. 1 or 2 only.
            var currentPid = currentProcess.Id;
            var otherProcess = processes.FirstOrDefault(p => p.Id != currentPid);
            if (otherProcess == null) return; //should never occur.

            IntPtr handle = otherProcess.MainWindowHandle;
            //handle==zero occurs if not a Winforms process or if form is not visible such as used
            //within a NotifyIcon control. So we have to try harder searching by windows caption.
            if (handle == IntPtr.Zero) handle = FindWindow(otherProcess.Id, caption);
            if (handle == IntPtr.Zero) return; //should never occur.
            ShowWindow(handle, SW_RESTORE);
            SetForegroundWindow(handle);

            Environment.Exit(1);
        }

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll")] private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int processId);
        [DllImport("user32.dll")] private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        private static string GetWindowText(IntPtr hWnd)
        {
            var sb = new StringBuilder(512);
            int result = GetWindowText(hWnd, sb, 512);
            return sb.ToString();
        }

        private static IntPtr FindWindow(int pid, string caption)
        {
            IntPtr found = IntPtr.Zero;
            if (string.IsNullOrWhiteSpace(caption)) caption = null;

            EnumWindows(delegate (IntPtr hWnd, IntPtr param)
            {
                GetWindowThreadProcessId(hWnd, out int processId);
                //Limit search to win32 windows within this process. If more than one form in this process has the same caption/title, then we just pick the first one....
                if (processId==pid)
                {
                    if (caption != null)
                    {
                        if (caption.Equals(GetWindowText(hWnd).Trim(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            found = hWnd;
                            return false;
                        }
                    }
                    else
                    {
                        found = hWnd;
                        return false;
                    }
                }

                return true; // Return true here so that we iterate all windows
            }, IntPtr.Zero);

            return found;
        }
        #endregion public static void AllowOnlyOneInstance()

        #region public static LanInfo GetLanInfo()

        /// <summary>
        /// Get information for this LAN and this computer.
        /// </summary>
        /// <returns>Major active LAN properties </returns>
        public static LanInfo GetLanInfo()
        {
            // When App is started when the network is down an exception occurs.
            // When App is running and the network goes down, this allows this function to still get the initial valid LanInfo.
            if (_LanInfo.ComputerName != null) return _LanInfo;
            bool cacheResult = true;

            var myComputerName = Dns.GetHostName();

            var myAddresses = Dns.GetHostEntry(myComputerName).AddressList
                .Where(m => m.AddressFamily == AddressFamily.InterNetwork)
                .Select(m => new ValuePair<IPAddress, int>(m, 0))
                .ToArray();

            //https://stackoverflow.com/questions/13634868/get-the-default-gateway
            IPAddress gateway = NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up) //<--this is important!
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                .Select(g => g?.Address)
                .Where(a => a != null)
                .Where(a => a.AddressFamily == AddressFamily.InterNetwork)
                .Where(a => Array.FindIndex(a.GetAddressBytes(), b => b != 0) >= 0)
                .FirstOrDefault();

            if (gateway == null)
            {
                cacheResult = false;
                //We try again but without currently being in use
                gateway = NetworkInterface.GetAllNetworkInterfaces()
                                //.Where(n => n.OperationalStatus == OperationalStatus.Up)
                                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                                .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                                .Select(g => g?.Address)
                                .Where(a => a != null)
                                .Where(a => a.AddressFamily == AddressFamily.InterNetwork)
                                .Where(a => Array.FindIndex(a.GetAddressBytes(), b => b != 0) >= 0)
                                .FirstOrDefault();

                //This app is non-functional without knowledge of the gateway! We need to ping the router!
                if (gateway == null)
                {
                    MessageBox.Show(Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null, "Your Wi-Fi or internet cable is\r\ndisconnected. Check your settings.", "Network Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit(); //This essentially posts FormMain.Close(), so this app continues. Same as Application.OpenForms[0].Close();
                    return new LanInfo("[Gateway Unknown]", "127.0.0.1", "127.0.0.1");
                }
            }

            //Find my computer address that is closest match to gateway/router IP (e.g. same domain)
            var gatewaybytes = gateway?.GetAddressBytes() ?? new byte[0];
            foreach (var kv in myAddresses)
            {
                var b = kv.Key.GetAddressBytes();
                int i = 0;
                for (; i < 4; i++)
                {
                    if (gatewaybytes[i] != b[i]) break;
                }
                kv.Value = i;
            }

            var li = new LanInfo(myComputerName, myAddresses.OrderByDescending(m => m.Value).FirstOrDefault()?.Key.ToString() ?? "127.0.0.1", gateway?.ToString() ?? "127.0.0.1");
            if (cacheResult) _LanInfo = li;
            return li;
        }
        private static LanInfo _LanInfo;

        // Used internally by GetLanInfo().
        private class ValuePair<T, U>
        {
            public T Key { get; set; }
            public U Value { get; set; }
            public ValuePair(T key, U value) { Key = key; Value = value; }
            public override string ToString() => $"Key={Key},Value={Value}";
        }

        /// <summary>
        /// Properties retrieved from GetLanInfo()
        /// </summary>
        public struct LanInfo
        {
            public static readonly LanInfo Empty = new LanInfo();
            public readonly string ComputerName;
            public readonly string ComputerAddress;
            public readonly string GatewayAddess;
            public LanInfo(string cname, string caddr, string gaddr) { ComputerName = cname; ComputerAddress = caddr; GatewayAddess = gaddr; }
        }
        #endregion public static LanInfo GetLanInfo()
    }
}
