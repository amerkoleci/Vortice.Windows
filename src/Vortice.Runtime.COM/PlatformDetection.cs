using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SharpGen.Runtime
{
    /// <summary>
	/// Provides methods to protect against invalid parameters.
	/// </summary>
    public static class PlatformDetection
    {
        private static int s_isInAppContainer = -1;
        public static readonly Version WindowsVersion;

        static PlatformDetection()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var result = RtlGetVersionEx(out var osvi);
                Debug.Assert(result == 0);
                WindowsVersion = new Version(osvi.dwMajorVersion, osvi.dwMinorVersion, osvi.dwBuildNumber);
            }
        }

        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsWindows7 => WindowsVersion.Major == 6 && WindowsVersion.Minor == 1;

        public static bool IsWindows8x => WindowsVersion.Major == 6 && (WindowsVersion.Minor == 2 || WindowsVersion.Minor == 3);
        public static bool IsWindows8xOrLater => WindowsVersion.Major >= 6 && WindowsVersion.Minor >= 2;

        public static bool IsNetCore => RuntimeInformation.FrameworkDescription.StartsWith(".NET Core", StringComparison.OrdinalIgnoreCase);

        public static bool IsNetNative => RuntimeInformation.FrameworkDescription.StartsWith(".NET Native", StringComparison.OrdinalIgnoreCase);

        public static bool IsUAP => IsInAppContainer || IsNetNative;

        public static bool IsWinRTSupported => IsWindows && !IsWindows7;

        public static bool IsInAppContainer
        {
            // This actually checks whether code is running in a modern app. 
            // Currently this is the only situation where we run in app container.
            // If we want to distinguish the two cases in future,
            // EnvironmentHelpers.IsAppContainerProcess in desktop code shows how to check for the AC token.
            get
            {
                if (s_isInAppContainer != -1)
                    return s_isInAppContainer == 1;

                if (!IsWindows || IsWindows7)
                {
                    s_isInAppContainer = 0;
                    return false;
                }

                byte[] buffer = Array.Empty<byte>();
                uint bufferSize = 0;
                try
                {
                    int result = GetCurrentApplicationUserModelId(ref bufferSize, buffer);
                    switch (result)
                    {
                        case 15703: // APPMODEL_ERROR_NO_APPLICATION
                            s_isInAppContainer = 0;
                            break;
                        case 0:     // ERROR_SUCCESS
                        case 122:   // ERROR_INSUFFICIENT_BUFFER
                                    // Success is actually insufficent buffer as we're really only looking for
                                    // not NO_APPLICATION and we're not actually giving a buffer here. The
                                    // API will always return NO_APPLICATION if we're not running under a
                                    // WinRT process, no matter what size the buffer is.
                            s_isInAppContainer = 1;
                            break;
                        default:
                            throw new InvalidOperationException($"Failed to get AppId, result was {result}.");
                    }
                }
                catch (Exception e)
                {
                    // We could catch this here, being friendly with older portable surface area should we
                    // desire to use this method elsewhere.
                    if (e.GetType().FullName.Equals("System.EntryPointNotFoundException", StringComparison.Ordinal))
                    {
                        // API doesn't exist, likely pre Win8
                        s_isInAppContainer = 0;
                    }
                    else
                    {
                        throw;
                    }
                }

                return s_isInAppContainer == 1;
            }
        }

        // >= Windows 10 Anniversary Update
        public static bool IsWindows10Version1607OrGreater =>
            WindowsVersion.Major == 10 && WindowsVersion.Minor == 0 && WindowsVersion.Build >= 14393;

        // >= Windows 10 Creators Update
        public static bool IsWindows10Version1703OrGreater =>
            WindowsVersion.Major == 10 && WindowsVersion.Minor == 0 && WindowsVersion.Build >= 15063;

        // >= Windows 10 Fall Creators Update
        public static bool IsWindows10Version1709OrGreater =>
            WindowsVersion.Major == 10 && WindowsVersion.Minor == 0 && WindowsVersion.Build >= 16299;

        // >= Windows 10 April 2018 Update
        public static bool IsWindows10Version1803OrGreater =>
            WindowsVersion.Major == 10 && WindowsVersion.Minor == 0 && WindowsVersion.Build >= 17134;

        // >= Windows 10 May 2019 Update (19H1)
        public static bool IsWindows10Version1903OrGreater =>
            WindowsVersion.Major == 10 && WindowsVersion.Minor == 0 && WindowsVersion.Build >= 18362;

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern int GetCurrentApplicationUserModelId(ref uint applicationUserModelIdLength, byte[] applicationUserModelId);

        [DllImport("ntdll.dll", ExactSpelling = true)]
        private static extern int RtlGetVersion(ref RTL_OSVERSIONINFOEX lpVersionInformation);

        private static unsafe int RtlGetVersionEx(out RTL_OSVERSIONINFOEX osvi)
        {
            osvi = new RTL_OSVERSIONINFOEX();
            osvi.dwOSVersionInfoSize = (uint)sizeof(RTL_OSVERSIONINFOEX);
            return RtlGetVersion(ref osvi);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private unsafe struct RTL_OSVERSIONINFOEX
        {
            internal uint dwOSVersionInfoSize;
            internal int dwMajorVersion;
            internal int dwMinorVersion;
            internal int dwBuildNumber;
            internal uint dwPlatformId;
            internal fixed char szCSDVersion[128];
        }
    }
}
