// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.MediaFoundation;

/// <summary>
/// Delegate MediaEngineNotifyDelegate {CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
/// </summary>
/// <param name="mediaEvent">The media event.</param>
/// <param name="param1">The param1.</param>
/// <param name="param2">The param2.</param>
public delegate void MediaEngineNotifyDelegate(MediaEngineEvent mediaEvent, nuint param1, int param2);

public unsafe partial class IMFMediaEngine
{
    /// <summary>
    /// Media engine playback event.
    /// </summary>
    public event MediaEngineNotifyDelegate? PlaybackEvent;

    internal MediaEngineNotifyImpl? mediaEngineNotifyImpl;

    public MediaEngineReady ReadyState { get => (MediaEngineReady)GetReadyState(); }
    public MediaEngineNetwork NetworkState { get => (MediaEngineNetwork)GetNetworkState(); }

    public Size NativeVideoSize
    {
        get
        {
            GetNativeVideoSize(out int width, out int height);
            return new(width, height);
        }
    }

    public Size VideoAspectRatio
    {
        get
        {
            GetVideoAspectRatio(out int width, out int height);
            return new(width, height);
        }
    }

    protected override void DisposeCore(IntPtr nativePointer, bool disposing)
    {
        base.DisposeCore(nativePointer, disposing);

        if (disposing)
        {
            if (mediaEngineNotifyImpl != null)
            {
                mediaEngineNotifyImpl.Dispose();
                mediaEngineNotifyImpl = null;
            }
        }
    }

    public bool OnVideoStreamTick(out long presentationTime)
    {
        return OnVideoStreamTick_(out presentationTime).Success;
    }

    public void TransferVideoFrame(IUnknown destinationSurface, in RectI destinationRect)
    {
        RawRect dst = destinationRect;
        TransferVideoFrame(destinationSurface, null, dst, null);
    }

    public void TransferVideoFrame(IUnknown destinationSurface, VideoNormalizedRect sourceRect, in RectI destinationRect)
    {
        RawRect dst = destinationRect;
        TransferVideoFrame(destinationSurface, sourceRect, dst, null);
    }

    internal void OnPlaybackEvent(MediaEngineEvent @event, nuint param1, int param2)
    {
        PlaybackEvent?.Invoke(@event, param1, param2);
    }
}

internal class MediaEngineNotifyImpl : CallbackBase, IMFMediaEngineNotify
{
    public IMFMediaEngine? MediaEngine { get; internal set; }

    public void EventNotify(int @event, UIntPtr param1, int param2)
    {
        MediaEngine?.OnPlaybackEvent((MediaEngineEvent)@event, param1, param2);
    }
}
