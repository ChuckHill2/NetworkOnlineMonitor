using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace NetworkOnlineMonitor
{
    public static class Sound
    {
        /// <summary>
        /// List of file extensions supported by this audio player. This is retrieved from the registry.
        /// </summary>
        /// <returns>list of extensions or an empty array upon error.</returns>
        public static string[] SupportedTypes()
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Multimedia\WMPlayer\Extensions"))
                {
                    return key.GetSubKeyNames().Where(name =>
                    {
                        using (var subkey = key.OpenSubKey(name))
                        {
                            string MCIHandler = subkey.GetValue("MCIHandler", string.Empty) as string;
                            string PerceivedType = subkey.GetValue("PerceivedType", string.Empty) as string;
                            return (MCIHandler.Equals("MPEGVideo", StringComparison.OrdinalIgnoreCase)
                                  && PerceivedType.Equals("audio", StringComparison.OrdinalIgnoreCase));
                        }
                    }).OrderBy(m => m).ToArray();
                }
            }
            catch
            {
                return new string[0];
            }
        }

        [DllImport("winmm.dll")] private static extern int mciSendString(string command, StringBuilder szReturn, int cchReturn, int dummyCallback);
        [DllImport("winmm.dll")] private static extern int mciSendString(string command, int dummyRetstr, int dummyStrlen, int dummyCallback);
        [DllImport("winmm.dll")] private static extern bool mciGetErrorString(int fdwError, StringBuilder lpszErrorText, int cchErrorText);

        private static bool mciSendString(string command)
        {
            var status = mciSendString(command, 0, 0, 0);
            Debug.WriteLine($"SoundPlayAsync({(status == 0 ? "Success" : "Failed")}): \"{command}\" {(status == 0 ? "" : mciGetErrorString(status))}");
            return status == 0;
        }
        private static string mciSendStringResponse(string command)
        {
            var sb = new StringBuilder(128);
            var status = mciSendString(command, sb, 128, 0);
            Debug.WriteLine($"SoundPlayAsync({(status == 0 ? "Success" : "Failed")}): \"{command}\" {(status == 0 ? "" : mciGetErrorString(status))}");
            return status == 0 ? sb.ToString() : null;
        }
        private static string mciGetErrorString(int errorCode)
        {
            var sb = new StringBuilder(128);
            if (mciGetErrorString(errorCode, sb, 128)) return sb.ToString();
            return "0x" + errorCode.ToString("X8");
        }
        private static int FitRange(int value, int minvalue, int maxvalue) => value < minvalue ? minvalue : (value > maxvalue ? maxvalue : value);

        /// <summary>
        /// Play sound clip asynchronously. If the previous clip is still playing it will be stopped before this new one is played.  
        /// This is NOT thread-safe. In addition, it must run on the same thread as the application Forms UI.
        /// </summary>
        /// <param name="filename">Full filename of sound clip</param>
        /// <param name="volume">Volume to play the clip 0-1000</param>
        /// <returns>False if filename cannot be opened.</returns>
        public static bool Play(string filename, int volume = -1)
        {
            mciSendString($"stop MediaAlias wait");
            mciSendString($"close MediaAlias wait");
            if (!mciSendString($"open \"{filename}\" type mpegvideo alias MediaAlias wait")) return false;
            if (volume >= 0) mciSendString($"setaudio MediaAlias volume to {FitRange(volume, 0, 1000)} wait");
            mciSendString($"play MediaAlias");
            return true;
        }

        /// <summary>
        /// Stop a playing running sound clip.
        /// This is NOT thread-safe. In addition, it must run on the same thread as the application Forms UI.
        /// </summary>
        public static void Stop()
        {
            mciSendString($"stop MediaAlias wait");
            mciSendString($"close MediaAlias wait");
        }

        /// <summary>
        /// Get the media file duration in ms.
        /// This is NOT thread-safe. In addition, it must run on the same thread as the application Forms UI.
        /// Warning: MediaDuration() and WaveDuration() results may be mismatched by 1ms due to rounding.
        /// </summary>
        /// <param name="mediafile"></param>
        /// <returns>Duration in milliseconds or 0 upon error.</returns>
        public static int MediaDuration(string mediafile)
        {
            if (mediafile == null) throw new ArgumentNullException(nameof(mediafile));
            if (mciSendString($"open \"{mediafile}\" type mpegvideo alias MediaAlias2"))
            {
                var response = mciSendStringResponse($"status MediaAlias2 length");
                mciSendString($"close MediaAlias2");
                return int.TryParse(response, out var i) ? i : 0;
            }
            return 0;
        }

        /// <summary>
        /// Get the duration of a wav file in ms.
        /// This is completely thread-safe and may run on simultaneously on any thread.
        /// Warning: MediaDuration() and WaveDuration() results may be mismatched by 1ms due to rounding.
        /// </summary>
        /// <returns>ms duration or 0 upon error</returns>
        public static int WaveDuration(string wavFile)
        {
            return new WaveHeader(wavFile).AudioDuration();
        }

        private class WaveHeader
        {
            //http://soundfile.sapp.org/doc/WaveFormat/
            private const int MagicID = 0x46464952; //="RIFF"
            private const int WaveChunkHeaderID = 0x45564157; //=“WAVE”
            private const int FmtSubChunkHeaderID = 0x20746d66; //="fmt "
            private const int DataSubChunkHeaderID = 0x61746164; //="data " or  "atad" (0x64617461) big-endian form

            public readonly int Magic;             //="RIFF"
            public readonly int FileSize;          //Size of the overall file - 8 bytes, in bytes (32-bit integer). 
            public readonly int WaveChunkHeader;   //="wave". Type Header. For our purposes, it always equals “WAVE”.
            public readonly int FmtSubChunkHeader; //Format chunk marker == "fmt "
            public readonly int FmtSize;           //Length of 'fmt ' chunk
            public readonly short AudioFormat;     //Type of format (1 is PCM)
            public readonly short NumChannels;     //1=Mono, 2=Stereo, etc 
            public readonly int SampleRate;        //Common values are 44100 (CD), 48000 (DAT). Sample Rate = Number of Samples per second, or Hertz.
            public readonly int ByteRate;          //== SampleRate * NumChannels * BitsPerSample/8
            public readonly short BlockAlign;      //== NumChannels * BitsPerSample/8
            public readonly short BitsPerSample;   //8 bits = 8, 16 bits = 16, etc.
            public readonly int DataSubChunkHeader; //=“data” chunk header. Marks the beginning of the data section.
            public readonly int DataSize;           //Size of the following data.

            public int AudioDuration()
            {
                if (Magic != MagicID || WaveChunkHeader != WaveChunkHeaderID || FmtSubChunkHeader != FmtSubChunkHeaderID) return 0;
                return (int)(DataSize / (SampleRate * NumChannels * BitsPerSample / 8.0) * 1000 + 0.5);
            }

            public WaveHeader(string wavFile)
            {
                using (var fs = new FileStream(wavFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.SequentialScan))
                {
                    if (fs.Length < 44) return;  //sizeof(WaveHeader)
                    using (var br = new BinaryReader(fs))
                    {
                        Magic = br.ReadInt32();
                        FileSize = br.ReadInt32();
                        WaveChunkHeader = br.ReadInt32();
                        FmtSubChunkHeader = br.ReadInt32();
                        FmtSize = br.ReadInt32();
                        AudioFormat = br.ReadInt16();
                        NumChannels = br.ReadInt16();
                        SampleRate = br.ReadInt32();
                        ByteRate = br.ReadInt32();
                        BlockAlign = br.ReadInt16();
                        BitsPerSample = br.ReadInt16();
                        DataSubChunkHeader = br.ReadInt32();
                        DataSize = br.ReadInt32();
                    }
                }
            }
        }
    }
}
