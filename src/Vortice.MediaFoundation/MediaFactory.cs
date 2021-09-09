// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.MediaFoundation
{
    public partial class MediaFactory
    {
        public static Result MFStartup(bool useLightVersion = false) => MFStartup(Version, useLightVersion ? 1 : 0);

        public static IMFAttributes MFCreateAttributes(int initialSizeInBytes = 0)
        {
            MFCreateAttributes(out IMFAttributes attributes, initialSizeInBytes).CheckError();
            return attributes;
        }

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
    }
}
