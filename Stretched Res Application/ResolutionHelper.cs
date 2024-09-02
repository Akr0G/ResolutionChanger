using System;
using System.Runtime.InteropServices;

namespace ResolutionChanger
{
    public static class ResolutionHelper
    {
        // Constants for changing display settings
        private const int CDS_UPDATEREGISTRY = 0x01;
        private const int DISP_CHANGE_SUCCESSFUL = 0;
        private const int DM_PELSWIDTH = 0x00080000;
        private const int DM_PELSHEIGHT = 0x00100000;
        private const int DM_DISPLAYFREQUENCY = 0x00400000;

        [DllImport("user32.dll")]
        private static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);

        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        [StructLayout(LayoutKind.Sequential)]
        private struct DEVMODE
        {
            private const int CCHDEVICENAME = 32;
            private const int CCHFORMNAME = 32;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;

            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;

            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;  // Refresh rate

            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;

            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        public static void ChangeResolution(int width, int height)
        {
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));

            // Get current display settings to preserve the refresh rate
            if (!EnumDisplaySettings(null, -1, ref dm))
            {
                Console.WriteLine("Failed to retrieve current display settings.");
                return;
            }

            int currentRefreshRate = dm.dmDisplayFrequency;  // Preserve current refresh rate

            // Set new resolution but keep the refresh rate unchanged
            dm.dmPelsWidth = width;
            dm.dmPelsHeight = height;
            dm.dmFields = DM_PELSWIDTH | DM_PELSHEIGHT | DM_DISPLAYFREQUENCY;
            dm.dmDisplayFrequency = currentRefreshRate;  // Keep the refresh rate the same

            int result = ChangeDisplaySettings(ref dm, CDS_UPDATEREGISTRY);

            if (result == DISP_CHANGE_SUCCESSFUL)
            {
                Console.WriteLine($"Resolution changed to {width}x{height} with refresh rate {currentRefreshRate} Hz.");
            }
            else
            {
                Console.WriteLine($"Resolution change failed with error code {result}.");
            }
        }
    }
}
