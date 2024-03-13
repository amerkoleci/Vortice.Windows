// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XAudio2;

public partial class IXAudio2SourceVoice
{
    internal VoiceCallbackImpl? _callback = null;

    /// <summary>
    /// Occurs just before the processing pass for the voice begins.
    /// </summary>
    /// <remarks>
    /// In order to use this delegate, this instance must have been initialized with events delegate support.
    /// </remarks>
    public event Action<int> ProcessingPassStart;

    /// <summary>
    /// Occurs just after the processing pass for the voice ends.
    /// </summary>
    /// <remarks>
    /// In order to use this delegate, this instance must have been initialized with events delegate support.
    /// </remarks>
    public event Action ProcessingPassEnd;

    /// <summary>
    /// Occurs when the voice has just finished playing a contiguous audio stream.
    /// </summary>
    /// <remarks>
    /// In order to use this delegate, this instance must have been initialized with events delegate support.
    /// </remarks>
    public event Action StreamEnd;

    /// <summary>
    /// Occurs when the voice is about to start processing a new audio buffer.
    /// </summary>
    /// <remarks>
    /// In order to use this delegate, this instance must have been initialized with events delegate support.
    /// </remarks>
    public event Action<IntPtr> BufferStart;

    /// <summary>
    /// Occurs when the voice finishes processing a buffer.
    /// </summary>
    /// <remarks>
    /// In order to use this delegate, this instance must have been initialized with events delegate support.
    /// </remarks>
    public event Action<IntPtr> BufferEnd;

    /// <summary>
    /// Occurs when a critical error occurs during voice processing.
    /// </summary>
    /// <remarks>
    /// In order to use this delegate, this instance must have been initialized with events delegate support.
    /// </remarks>
    public event Action<IntPtr> LoopEnd;

    public readonly struct VoiceErrorArgs
    {
        public VoiceErrorArgs(IntPtr pointer, Result result)
        {
            Pointer = pointer;
            Result = result;
        }

        public IntPtr Pointer { get; }
        public Result Result { get; }
    }

    /// <summary>
    /// Occurs when [voice error].
    /// </summary>
    /// <remarks>
    /// In order to use this delegate, this instance must have been initialized with events delegate support.
    /// </remarks>
    public event Action<VoiceErrorArgs> VoiceError;

    /// <summary>
    /// Gets the voice state.
    /// </summary>
    public VoiceState State => GetState(0);

    /// <summary>
    /// Gets the voice state without samples played.
    /// </summary>
    public VoiceState StateNoSamplesPlayed => GetState(NoSamplesPlayed);

    /// <summary>
    /// Gets or Sets the frequency adjustment ratio of the voice.
    /// </summary>
    public float FrequencyRatio
    {
        get => GetFrequencyRatio();
        set => SetFrequencyRatio(value, 0);
    }

    /// <inheritdoc/>
    protected override void DisposeCore(IntPtr nativePointer, bool disposing)
    {
        if (disposing)
        {
            _callback?.Dispose();
            _callback = null;
        }

        base.DisposeCore(nativePointer, disposing);
    }

    /// <summary>	
    /// Starts consumption and processing of audio by the voice. Delivers the result to any connected submix or mastering voices, or to the output device, with CommitNow changes.
    /// </summary>	
    public void Start()
    {
        Start(0, 0);
    }

    /// <summary>	
    /// Starts consumption and processing of audio by the voice. Delivers the result to any connected submix or mastering voices, or to the output device.	
    /// </summary>	
    /// <param name="operationSet">Identifies this call as part of a deferred batch.</param>
    public void Start(int operationSet)
    {
        Start(0, operationSet);
    }

    public void Stop()
    {
        Stop(PlayFlags.None, 0);
    }

    public void Stop(int operationSet)
    {
        Stop(PlayFlags.None, operationSet);
    }

    /// <summary>
    /// Adds a new audio buffer to the voice queue.
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="decodedXMWAPacketInfo"></param>
    public void SubmitSourceBuffer(AudioBuffer buffer, uint[]? decodedXMWAPacketInfo = null)
    {
        unsafe
        {
            if (decodedXMWAPacketInfo != null)
            {
                fixed (void* pBuffer = decodedXMWAPacketInfo)
                {
                    var bufferWma = new BufferWma
                    {
                        PacketCount = decodedXMWAPacketInfo.Length,
                        DecodedPacketCumulativeBytesPointer = (IntPtr)pBuffer
                    };
                    SubmitSourceBuffer(buffer, bufferWma);
                }
            }
            else
            {
                SubmitSourceBuffer(buffer, (BufferWma?)null);
            }
        }
    }

    internal class VoiceCallbackImpl : CallbackBase, IXAudio2VoiceCallback
    {
        public IXAudio2SourceVoice Voice { get; set; }

        void IXAudio2VoiceCallback.OnVoiceProcessingPassStart(int bytesRequired)
        {
            Voice.ProcessingPassStart?.Invoke(bytesRequired);
        }

        void IXAudio2VoiceCallback.OnVoiceProcessingPassEnd()
        {
            Voice.ProcessingPassEnd?.Invoke();
        }

        void IXAudio2VoiceCallback.OnStreamEnd()
        {
            Voice.StreamEnd?.Invoke();
        }

        void IXAudio2VoiceCallback.OnBufferStart(IntPtr context)
        {
            Voice.BufferStart?.Invoke(context);
        }

        void IXAudio2VoiceCallback.OnBufferEnd(IntPtr context)
        {
            Voice.BufferEnd?.Invoke(context);
        }

        void IXAudio2VoiceCallback.OnLoopEnd(IntPtr context)
        {
            Voice.LoopEnd?.Invoke(context);
        }

        void IXAudio2VoiceCallback.OnVoiceError(IntPtr context, Result error)
        {
            Voice.VoiceError?.Invoke(new VoiceErrorArgs(context, error));
        }
    }
}
