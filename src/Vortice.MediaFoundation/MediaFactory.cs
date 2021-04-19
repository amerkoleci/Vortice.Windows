// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.MediaFoundation
{
    public partial class MediaFactory
    {
        private static bool s_started;

        public static void Startup(bool useLightVersion = false)
        {
            if (s_started)
                return;

            if (MFStartup(Version, useLightVersion ? 1 : 0).Success)
            {
                s_started = true;
            }
        }

        public static void Shutdown()
        {
            if (!s_started)
                return;

            MFShutdown().CheckError();
            s_started = false;
        }
    }
}
