// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.MediaFoundation;

public delegate void CaptureEngineOnEventDelegate(IMFMediaEvent @event);

public unsafe partial class IMFCaptureEngine
{
    private readonly CaptureEngineOnEventImpl? _captureEngineOnEventImpl;

    public event CaptureEngineOnEventDelegate? CaptureEngineEvent;

    public IMFCaptureEngine(IMFCaptureEngineClassFactory factory)
    {
        NativePointer = factory.CreateInstance(ClsidMFCaptureEngine, typeof(IMFCaptureEngine).GUID);
        _captureEngineOnEventImpl = new CaptureEngineOnEventImpl(this);
    }

    private void OnEvent(IMFMediaEvent @event)
    {
        CaptureEngineEvent?.Invoke(@event);
    }

    internal class CaptureEngineOnEventImpl(IMFCaptureEngine engine) : CallbackBase, IMFCaptureEngineOnEventCallback
    {
        private readonly IMFCaptureEngine _engine = engine;

        public void OnEvent(IMFMediaEvent @event)
        {
            _engine.OnEvent(@event);
        }
    }
}
