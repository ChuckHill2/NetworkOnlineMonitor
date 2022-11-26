//--------------------------------------------------------------------------
// <summary>
//   
// </summary>
// <copyright file="CaptionBarCloseButton.cs" company="Chuck Hill">
// Copyright (c) 2020 Chuck Hill.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public License
// as published by the Free Software Foundation; either version 2.1
// of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// The GNU Lesser General Public License can be viewed at
// http://www.opensource.org/licenses/lgpl-license.php. If
// you unfamiliar with this license or have questions about
// it, here is an http://www.gnu.org/licenses/gpl-faq.html.
//
// All code and executables are provided "as is" with no warranty
// either express or implied. The author accepts no liability for
// any damage or loss of business that this product may cause.
// </copyright>
// <repository>https://github.com/ChuckHill2/ChuckHill2.Utilities</repository>
// <author>Chuck Hill</author>
//--------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// =======================================================================
// This file has NO external dependencies as such this file may be reused anywhere.
// =======================================================================

namespace ChuckHill2.Forms
{
    /// <summary>
    /// Override the action and tooltip for the window caption bar "Close X button.
    /// This will self-dispose when the form closes.
    /// </summary>
    public class CaptionBarCloseButton : NativeWindow, IDisposable
    {
        private ToolTip tt;
        private Form ButtonOwner;
        private Action ButtonAction;
        private string ToolTipString;

        private bool HandleMouseMove = false;
        private bool IsActive = true; //Must be true because this is already active by the time this is created.
        private bool ttShow = true;   //The same.
        private Timer HTCloseTimer;   //how long the user has to be stationary over the close button before the tool tip is displayed.

        /// <summary>
        /// Override the action and tooltip for the window caption bar "Close X button.
        /// </summary>
        /// <param name="owner">The Form this is to operate upon.</param>
        /// <param name="action">The new action for the Close X button.</param>
        /// <param name="tooltip">The new tooltip that describes the new action.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="owner" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="action" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="tooltip" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="owner" />Form must have a border and the caption ControlBox must be enabled.</exception>
        public CaptionBarCloseButton(Form owner, Action action, string tooltip)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (owner.FormBorderStyle == FormBorderStyle.None || owner.ControlBox == false)
                throw new ArgumentException("Form must have a border and the caption ControlBox must be enabled.", nameof(owner));
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (string.IsNullOrWhiteSpace(tooltip)) throw new ArgumentNullException(nameof(tooltip));

            ButtonOwner = owner;
            ButtonAction = action;
            ToolTipString = tooltip;
            tt = new ToolTip();

            ButtonOwner.Shown += (s, e) =>
            {
                base.AssignHandle(ButtonOwner.Handle);
            };

            HTCloseTimer = new Timer();
            HTCloseTimer.Interval = 1500;
            HTCloseTimer.Tick += (s, e) =>
            {
                //Debug.WriteLine($"HTCloseTimer.Tick: Tooltip.Show");
                HTCloseTimer.Stop();
                ttShow = false;
                HandleMouseMove = true;
                tt.Show(ToolTipString, ButtonOwner, ButtonOwner.PointToClient(new Point(ButtonOwner.Right - 55, ButtonOwner.Top + 55)), 4000);
            };
        }

        /// <summary>
        /// Closes and releases any resources used by this <see cref="T:ChuckHill2.Forms.CaptionBarCloseButton" />.
        /// This action is not necessary if used for the lifetime of the form as this automatically disposed upon form closure.
        /// </summary>
        public void Dispose()
        {
            base.ReleaseHandle();
            HTCloseTimer?.Dispose();
            HTCloseTimer = null;
            ButtonAction = null;
            tt?.Dispose();
            tt = null;
            ButtonOwner = null;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCMOUSEMOVE = 0x00A0;
            const int HTCLOSE = 20;
            const int WM_MOUSEMOVE = 0x0200;
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            const int SC_MINIMIZE = 0xF020;
            const int WM_DESTROY = 0x0002;
            const int WM_NCACTIVATE = 0x0086;

            if (ButtonOwner.Handle == IntPtr.Zero || m.HWnd != ButtonOwner.Handle) { base.WndProc(ref m); return; }
            //NativeMethods.DebugPrintMsg(m.HWnd, (int)m.Msg, m.WParam, m.LParam); //see Chuckhill2.Win32 (Forms.cs)

            if (m.Msg == WM_NCMOUSEMOVE && IsActive)
            {
                if ((m.WParam.ToInt32()&0xFFFF) == HTCLOSE)
                {
                    if (!HTCloseTimer.Enabled && ttShow)
                    {
                        //Debug.WriteLine($"--> WM_NCMOUSEMOVE (HTCLOSE): ttShow={ttShow}, HTCloseTimer.Start");
                        ttShow = false;
                        HTCloseTimer.Start();
                    }
                    //else Debug.WriteLine($"--> WM_NCMOUSEMOVE (HTCLOSE): ttShow={ttShow}");
                }
                else
                {
                    if (!ttShow)
                    {
                        //Debug.WriteLine($"WM_NCMOUSEMOVE ({(HT)(m.WParam.ToInt32() & 0xFFFF)}): ttShow={ttShow}, HTCloseTimer.Stop");
                        HTCloseTimer.Stop();
                        tt.Hide(ButtonOwner);
                        ttShow = true;
                    }
                }
            }
            else if (m.Msg == WM_MOUSEMOVE && HandleMouseMove)
            {
                //Debug.WriteLine($"WM_MOUSEMOVE: HTCloseTimer.Stop");
                HandleMouseMove = false;
                HTCloseTimer.Stop();
                tt.Hide(ButtonOwner);
                ttShow = true;
            }
            else if (m.Msg == WM_SYSCOMMAND)
            {
                var subcommand = m.WParam.ToInt32() & 0xFFFF;
                if (subcommand == SC_CLOSE || subcommand == SC_MINIMIZE)
                {
                    //Debug.WriteLine($"--> WM_SYSCOMMAND (SC_CLOSE): ButtonAction()");
                    try
                    {
                        ButtonAction();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"ButtonAction {ex.GetBaseException().GetType().Name}:{ex.GetBaseException().Message}");
                    }
                    return;
                }
            }
            else if (m.Msg == WM_NCACTIVATE)
            {

                IsActive = (int)m.WParam==0?false:true;
                HandleMouseMove = false;
                ttShow = IsActive;
                //Debug.WriteLine($"--> WM_NCACTIVATE: IsActive={IsActive}, ttShow={ttShow}");
            }
            else if (m.Msg == WM_DESTROY)
            {
                base.WndProc(ref m);
                this.Dispose();
                return;
            }

            base.WndProc(ref m);
        }

        #region public void UpdateSysMenuCloseToMinimize()
        #region Win32
        [Flags]
        private enum MIIM
        {
            BITMAP = 0x00000080,     //Retrieves or sets the hbmpItem member.
            CHECKMARKS = 0x00000008, //Retrieves or sets the hbmpChecked and hbmpUnchecked members.
            DATA = 0x00000020,       //Retrieves or sets the dwItemData member.
            FTYPE = 0x00000100,      //Retrieves or sets the fType member.
            ID = 0x00000002,         //Retrieves or sets the wID member.
            STATE = 0x00000001,      //Retrieves or sets the fState member.
            STRING = 0x00000040,     //Retrieves or sets the dwTypeData member.
            SUBMENU = 0x00000004,    //Retrieves or sets the hSubMenu member.
            TYPE = 0x00000010        //Retrieves or sets the fType and dwTypeData members. MIIM_TYPE is replaced by MIIM_BITMAP, MIIM_FTYPE, and MIIM_STRING.
        }

        private enum MFT
        {
            BITMAP = 0x00000004,       //Displays the menu item using a bitmap. The low-order word of the dwTypeData member is the bitmap handle, and the cch member is ignored. MFT_BITMAP is replaced by MIIM_BITMAP and hbmpItem.
            MENUBARBREAK = 0x00000020, //Places the menu item on a new line (for a menu bar) or in a new column (for a drop-down menu, submenu, or shortcut menu). For a drop-down menu, submenu, or shortcut menu, a vertical line separates the new column from the old.
            MENUBREAK = 0x00000040,    //Places the menu item on a new line (for a menu bar) or in a new column (for a drop-down menu, submenu, or shortcut menu). For a drop-down menu, submenu, or shortcut menu, the columns are not separated by a vertical line.
            OWNERDRAW = 0x00000100,    //Assigns responsibility for drawing the menu item to the window that owns the menu. The window receives a WM_MEASUREITEM message before the menu is displayed for the first time, and a WM_DRAWITEM message whenever the appearance of the menu item must be updated. If this value is specified, the dwTypeData member contains an application-defined value.
            RADIOCHECK = 0x00000200,   //Displays selected menu items using a radio-button mark instead of a check mark if the hbmpChecked member is NULL.
            RIGHTJUSTIFY = 0x00004000, //Right-justifies the menu item and any subsequent items. This value is valid only if the menu item is in a menu bar.
            RIGHTORDER = 0x00002000,   //Specifies that menus cascade right-to-left (the default is left-to-right). This is used to support right-to-left languages, such as Arabic and Hebrew.
            SEPARATOR = 0x00000800,    //Specifies that the menu item is a separator. A menu item separator appears as a horizontal dividing line. The dwTypeData and cch members are ignored. This value is valid only in a drop-down menu, submenu, or shortcut menu.
            STRING = 0x00000000,       //Displays the menu item using a text string. The dwTypeData member is the pointer to a null-terminated string, and the cch member is the length of the string. MFT_STRING is replaced by MIIM_STRING.
            NotAvailable = -1          //MIIM fMask does not include the flag MIIM_FTYPE.
        }

        private enum MFS
        {
            CHECKED = 0x00000008,   //Checks the menu item. For more information about selected menu items, see the hbmpChecked member.
            DEFAULT = 0x00001000,   //Specifies that the menu item is the default. A menu can contain only one default menu item, which is displayed in bold.
            DISABLED = 0x00000003,  //Disables the menu item and grays it so that it cannot be selected. This is equivalent to MFS_GRAYED.
            ENABLED = 0x00000000,   //Enables the menu item so that it can be selected. This is the default state.
            GRAYED = 0x00000003,    //Disables the menu item and grays it so that it cannot be selected. This is equivalent to MFS_DISABLED.
            HILITE = 0x00000080,    //Highlights the menu item.
            UNCHECKED = 0x00000000, //Unchecks the menu item. For more information about clear menu items, see the hbmpChecked member.
            UNHILITE = 0x00000000,  //Removes the highlight from the menu item. This is the default state.
            NotAvailable = -1       //MIIM fMask does not include the flag MIIM_STATE.
        }

        private enum HBBM : long  //Named Built-in bitmaps
        {
            CALLBACK = -1,       //A bitmap that is drawn by the window that owns the menu. The application must process the WM_MEASUREITEM and WM_DRAWITEM messages.
            MBAR_CLOSE = 5,      //Close button for the menu bar.
            MBAR_CLOSE_D = 6,    //Disabled close button for the menu bar.
            MBAR_MINIMIZE = 3,   //Minimize button for the menu bar.
            MBAR_MINIMIZE_D = 7, //Disabled minimize button for the menu bar.
            MBAR_RESTORE = 2,    //Restore button for the menu bar.
            POPUP_CLOSE = 8,     //Close button for the submenu.
            POPUP_MAXIMIZE = 10, //Maximize button for the submenu.
            POPUP_MINIMIZE = 11, //Minimize button for the submenu.
            POPUP_RESTORE = 9,   //Restore button for the submenu.
            SYSTEM = 1,          //Windows icon or the icon of the window specified in dwItemData.
            NotAvailable = 0
            //Any other value is an HBITMAP IntPtr
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct MENUITEMINFO
        {
            public int cbSize;
            public MIIM fMask;
            public MFT fType;
            public MFS fState;
            public int wID;
            public IntPtr hSubMenu;
            public IntPtr hBmpChecked;
            public IntPtr hBmpUnchecked;
            public IntPtr dwItemData; //User data. 
            public String dwTypeData;
            public int cch;           // length of dwTypeData
            public HBBM hBmpItem;

            public static MENUITEMINFO Create(MIIM pfMask = 0)
            {
                var mii = new MENUITEMINFO();
                mii.cbSize = Marshal.SizeOf(typeof(MENUITEMINFO));
                mii.fMask = pfMask;
                mii.fType = MFT.NotAvailable;
                mii.fState = MFS.NotAvailable;
                mii.dwTypeData = new string('X', 256);
                mii.cch = 256;
                return mii;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetMenuItemInfo(IntPtr hMenu, uint item, bool fByPosition, ref MENUITEMINFO mii);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetMenuItemInfo(IntPtr hMenu, uint item, bool fByPositon, ref MENUITEMINFO mii);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DeleteMenu(IntPtr hMenu, int uPosition, int uFlags);
        #endregion Win32

        /// <summary>
        /// Assign label "Minimize\tAlt-F4" to "Close" system menuitem and remove existing "Minimize" and 'Maximize' menuitems if they are disabled.<br/>
        /// Warning: There is currently no 'restore' for this form's system menu.
        /// </summary>
        public void UpdateSysMenuCloseToMinimize()
        {
            const int SC_CLOSE = 0xF060;
            const int SC_MINIMIZE = 0xF020;
            const int SC_MAXIMIZE = 0xF030;

            Form form = ButtonOwner; 

            IntPtr hWnd = form.Handle;
            if (hWnd == null || hWnd == IntPtr.Zero) return;
            IntPtr hSysMenu = GetSystemMenu(hWnd, false);

            var minimizeItem = MENUITEMINFO.Create(MIIM.BITMAP | MIIM.STRING);
            bool success = GetMenuItemInfo(hSysMenu, SC_MINIMIZE, false, ref minimizeItem);
            if (!success) Debug.WriteLine($"GetMenuItemInfo: {new Win32Exception()}");

            minimizeItem.dwTypeData += "\tAlt-F4";
            minimizeItem.cch = minimizeItem.dwTypeData.Length;

            success = SetMenuItemInfo(hSysMenu, SC_CLOSE, false, ref minimizeItem);

            if (!form.MaximizeBox) success = DeleteMenu(hSysMenu, SC_MAXIMIZE, 0);
            if (!form.MinimizeBox) success = DeleteMenu(hSysMenu, SC_MINIMIZE, 0);
        }
        #endregion public void UpdateSysMenuCloseToMinimize()

        #region public static Rectangle CloseButtonBounds(Form form)
        #region Win32
        //https://learn.microsoft.com/en-us/windows/win32/menurc/wm-gettitlebarinfoex
        private const int WM_GETTITLEBARINFOEX = 0x033F;
        private const int CCHILDREN_TITLEBAR = 5;
        private const int INDEX_TITLEBAR = 0;
        private const int INDEX_RESERVED = 1;
        private const int INDEX_MINIMIZE = 2;
        private const int INDEX_MAXIMIZE = 3;
        private const int INDEX_HELP = 4;
        private const int INDEX_CLOSE = 5;

        private enum STATE_SYSTEM
        {
            FOCUSABLE = 0x00100000,   //The element can accept the focus.
            INVISIBLE = 0x00008000,   //The element is invisible.
            OFFSCREEN = 0x00010000,   //The element has no visible representation.
            UNAVAILABLE = 0x00000001, //The element is unavailable.
            PRESSED = 0x00000008,     //The element is in the pressed state.
            NORMAL = 0
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT { public int Left, Top, Right, Bottom; }

        [StructLayout(LayoutKind.Sequential)]
        private struct TITLEBARINFOEX
        {
            public int cbSize;
            public RECT rcTitleBar;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CCHILDREN_TITLEBAR + 1, ArraySubType = UnmanagedType.U4 )]
            public STATE_SYSTEM[] rgstate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CCHILDREN_TITLEBAR + 1, ArraySubType = UnmanagedType.Struct)]
            public RECT[] rgrect;
            public static TITLEBARINFOEX Create() => new TITLEBARINFOEX() { cbSize = Marshal.SizeOf(typeof(TITLEBARINFOEX)) };
        }

        [DllImport("User32.dll", SetLastError = true)] 
        private static extern int SendMessage(IntPtr hWnd, int Msg, int unused, ref TITLEBARINFOEX lParam);
        #endregion Win32

        /// <summary>
        /// Get caption close X button bounds. Queries position from 
        /// </summary>
        /// <returns>Bounds in screen coordinates.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="owner" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="owner" />Form must have a border and the caption ControlBox must be enabled.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="owner" />Form win32 handle has not yet been created.</exception>
        public static Rectangle CloseButtonBounds(Form owner)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (owner.FormBorderStyle == FormBorderStyle.None || owner.ControlBox == false)
                throw new ArgumentException("Form must have a border and the caption ControlBox must be enabled.", nameof(owner));
            if (!owner.IsHandleCreated)
                throw new ArgumentException("Form win32 handle has not yet been created.", nameof(owner));

            TITLEBARINFOEX tbi = TITLEBARINFOEX.Create();
            if (SendMessage(owner.Handle, WM_GETTITLEBARINFOEX, 0, ref tbi) != 1) return Rectangle.Empty;
            var rc = tbi.rgrect[INDEX_CLOSE];
            return Rectangle.FromLTRB(rc.Left,rc.Top,rc.Right,rc.Bottom);
        }

        /// <summary>
        /// HIDDEN
        /// Get caption close X button bounds. Computed from screen client.Bounds and window.Bounds. 
        /// Makes assumptions about border size (==1) and close button is rightmost in caption and takes the entire caption height.
        /// </summary>
        /// <returns>Bounds in screen coordinates.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="owner" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="owner" />Form must have a border and the caption ControlBox must be enabled.</exception>
        private static Rectangle CloseButtonBounds_X(Form owner)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (owner.FormBorderStyle == FormBorderStyle.None || owner.ControlBox == false)
                throw new ArgumentException("Form must have a border and the caption ControlBox must be enabled.", nameof(owner));

            Rectangle client = owner.RectangleToScreen(owner.ClientRectangle);
            int border = 1;
            int captionHeight = client.Top - owner.Top;
            return new Rectangle(owner.Right - captionHeight - border, owner.Top + border, captionHeight, captionHeight - border * 2);
        }
        #endregion public static Rectangle CloseButtonBounds(Form form)
    }
}
