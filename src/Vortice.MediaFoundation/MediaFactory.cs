// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.MediaFoundation
{
    public partial class MediaFactory
    {
        public static void MFStartup(bool useLightVersion = false) => MFStartup(Version, useLightVersion ? 1 : 0);

        public static IMFMediaSession MFCreateMediaSession(IMFAttributes configuration)
        {
            MFCreateMediaSession(configuration, out IMFMediaSession session).CheckError();
            return session;
        }

        public static IMFSourceResolver MFCreateSourceResolver()
        {
            MFCreateSourceResolver(out IMFSourceResolver sourceResolver).CheckError();
            return sourceResolver;
        }

        public static IMFMediaSource MFCreateDeviceSource(IMFAttributes attributes)
        {
            MFCreateDeviceSource(attributes, out IMFMediaSource mediaSource).CheckError();
            return mediaSource;
        }

        public static IMFDXGIDeviceManager MFCreateDXGIDeviceManager()
        {
            MFCreateDXGIDeviceManager(out int resetToken, out IMFDXGIDeviceManager deviceManager).CheckError();
            deviceManager.ResetToken = resetToken;
            return deviceManager;
        }
    }
}
