// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;

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
