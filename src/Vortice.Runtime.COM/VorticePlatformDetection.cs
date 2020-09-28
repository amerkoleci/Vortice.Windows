using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SharpGen.Runtime
{
    /// <summary>
	/// Provides methods to protect against invalid parameters.
	/// </summary>
    public static class VorticePlatformDetection
    {
        private static int s_isInAppContainer = -1;

        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsWindows7 => IsWindows && GetWindowsVersion() == 6 && GetWindowsMinorVersion() == 1;

        public static bool IsUAP
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
                        case 120:   // ERROR_CALL_NOT_IMPLEMENTED
                                    // This function is not supported on this system.
                                    // In example on Windows Nano Server
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

        private static unsafe uint GetWindowsVersion()
        {
            OSVERSIONINFOEX osvi = default;
            osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEX);
            bool result = GetVersionEx(ref osvi);
            Debug.Assert(result);
            return osvi.dwMajorVersion;
        }
        private static unsafe uint GetWindowsMinorVersion()
        {
            OSVERSIONINFOEX osvi = default;
            osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEX);
            bool result = GetVersionEx(ref osvi);
            Debug.Assert(result);
            return osvi.dwMinorVersion;
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern int GetCurrentApplicationUserModelId(ref uint applicationUserModelIdLength, byte[] applicationUserModelId);

        [DllImport("kernel32.dll", EntryPoint = "GetVersionExW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool GetVersionEx(ref OSVERSIONINFOEX ver);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private unsafe struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            public fixed char szCSDVersion[128];
            public short servicePackMajor;
            public short servicePackMinor;
            public short suiteMask;
            public byte productType;
            public byte reserved;
        }
    }
}
