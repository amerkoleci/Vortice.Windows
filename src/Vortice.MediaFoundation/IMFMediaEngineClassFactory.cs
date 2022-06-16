// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using static Vortice.MediaFoundation.MediaFactory;

namespace Vortice.MediaFoundation;

public partial class IMFMediaEngineClassFactory
{
    public IMFMediaEngineClassFactory()
    {
        ComUtilities.CreateComInstance(
            ClsidMFMediaEngineClassFactory,
            ComContext.InprocServer, typeof(IMFMediaEngineClassFactory).GUID,
            this);
    }

    public IMFMediaEngine CreateInstance(
        MediaEngineCreateFlags createFlags = MediaEngineCreateFlags.None,
        IMFAttributes? attributes = default,
        MediaEngineNotifyDelegate? playbackCallback = default)
    {
        attributes ??= MFCreateAttributes(1);

        MediaEngineNotifyImpl mediaEngineNotifyImpl = new();

        try
        {
            attributes.Set(MediaEngineAttributeKeys.Callback, mediaEngineNotifyImpl);
            CreateInstance(createFlags, attributes, out IMFMediaEngine engine).CheckError();

            mediaEngineNotifyImpl.MediaEngine = engine;
            engine.mediaEngineNotifyImpl = mediaEngineNotifyImpl;
            if (playbackCallback != null)
            {
                engine.PlaybackEvent += playbackCallback;
            }

            return engine;
        }
        catch
        {
            mediaEngineNotifyImpl.Dispose();
            throw;
        }
    }
}
