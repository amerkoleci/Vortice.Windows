// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;
using Vortice.Multimedia;
using static Vortice.XAudio2.XAudio2;

namespace Vortice.XAudio2;

public partial class IXAudio2
{
    private readonly EngineCallbackImpl? _engineCallback;

    #region Events
    /// <summary>
    /// Called by XAudio2 just before an audio processing pass begins.
    /// </summary>
    public event EventHandler? ProcessingPassStart;

    /// <summary>
    /// Called by XAudio2 just after an audio processing pass ends.
    /// </summary>
    public event EventHandler? ProcessingPassEnd;

    /// <summary>
    /// Called if a critical system error occurs that requires XAudio2 to be closed down and restarted.
    /// </summary>
    public event EventHandler<ErrorEventArgs>? CriticalError;
    #endregion Events

    internal IXAudio2(ProcessorSpecifier processorSpecifier, bool registerCallback)
        : base()
    {
        XAudio2Native.XAudio2Create(processorSpecifier, out IntPtr nativePtr).CheckError();
        NativePointer = nativePtr;

        // Register engine callback
        if (registerCallback)
        {
            _engineCallback = new EngineCallbackImpl(this);
            RegisterForCallbacks(_engineCallback);
        }
    }

    internal IXAudio2(IntPtr nativePtr, bool registerCallback)
        : base(nativePtr)
    {
        // Register engine callback
        if (registerCallback)
        {
            _engineCallback = new EngineCallbackImpl(this);
            RegisterForCallbacks(_engineCallback);
        }
    }

    protected override void DisposeCore(IntPtr nativePointer, bool disposing)
    {
        if (_engineCallback != null)
        {
            UnregisterForCallbacks(_engineCallback);
        }

        if (disposing)
        {
            _engineCallback?.Dispose();
        }

        base.DisposeCore(nativePointer, disposing);
    }

    public unsafe IXAudio2MasteringVoice CreateMasteringVoice(
        int inputChannels = DefaultChannels,
        int inputSampleRate = DefaultSampleRate,
        AudioStreamCategory category = AudioStreamCategory.GameEffects,
        string deviceId = "",
        EffectDescriptor[]? effectDescriptors = null)
    {
        if (effectDescriptors != null)
        {
            var effectChain = new EffectChain();
            var effectDescriptorNatives = new EffectDescriptor.__Native[effectDescriptors.Length];
            for (int i = 0; i < effectDescriptorNatives.Length; i++)
            {
                effectDescriptors[i].__MarshalTo(ref effectDescriptorNatives[i]);
            }

            effectChain.EffectCount = effectDescriptorNatives.Length;
            fixed (void* pEffectDescriptors = &effectDescriptorNatives[0])
            {
                effectChain.EffectDescriptorPointer = (IntPtr)pEffectDescriptors;

                if (string.IsNullOrEmpty(deviceId))
                {
                    return CreateMasteringVoice(inputChannels, inputSampleRate, 0, null, effectChain, category);
                }
                else
                {
                    return CreateMasteringVoice(inputChannels, inputSampleRate, 0, deviceId, effectChain, category);
                }
            }
        }
        else
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                return CreateMasteringVoice(inputChannels, inputSampleRate, 0, null, null, category);
            }
            else
            {
                return CreateMasteringVoice(inputChannels, inputSampleRate, 0, deviceId, null, category);
            }
        }
    }

    public IXAudio2SourceVoice CreateSourceVoice(WaveFormat sourceFormat, bool enableCallbackEvents)
    {
        return CreateSourceVoice(sourceFormat, VoiceFlags.None, 1.0f, enableCallbackEvents, null);
    }

    public unsafe IXAudio2SourceVoice CreateSourceVoice(
        WaveFormat sourceFormat,
        VoiceFlags flags = VoiceFlags.None,
        float maxFrequencyRatio = 1.0f,
        bool enableCallbackEvents = false,
        EffectDescriptor[]? effectDescriptors = null)
    {
        IntPtr waveformatPtr = WaveFormat.MarshalToPtr(sourceFormat);
        IXAudio2SourceVoice.VoiceCallbackImpl? callback = enableCallbackEvents ? new IXAudio2SourceVoice.VoiceCallbackImpl() : default;

        try
        {
            if (effectDescriptors != null)
            {
                var effectChain = new EffectChain();
                var effectDescriptorNatives = new EffectDescriptor.__Native[effectDescriptors.Length];
                for (int i = 0; i < effectDescriptorNatives.Length; i++)
                {
                    effectDescriptors[i].__MarshalTo(ref effectDescriptorNatives[i]);
                }

                effectChain.EffectCount = effectDescriptorNatives.Length;
                fixed (void* pEffectDescriptors = &effectDescriptorNatives[0])
                {
                    effectChain.EffectDescriptorPointer = (IntPtr)pEffectDescriptors;
                    IXAudio2SourceVoice voice = CreateSourceVoice(waveformatPtr, flags, maxFrequencyRatio, callback, null, effectChain);
                    if (callback != null)
                    {
                        callback.Voice = voice;
                    }
                    voice._callback = callback;
                    return voice;
                }
            }
            else
            {
                IXAudio2SourceVoice voice = CreateSourceVoice(waveformatPtr, flags, maxFrequencyRatio, callback, null, null);
                if (callback != null)
                {
                    callback.Voice = voice;
                }
                voice._callback = callback;
                return voice;
            }
        }
        finally
        {
            Marshal.FreeHGlobal(waveformatPtr);
        }
    }

    public unsafe IXAudio2SubmixVoice CreateSubmixVoice(
        int inputChannels = 2,
        int inputSampleRate = 44100,
        SubmixVoiceFlags flags = SubmixVoiceFlags.None,
        int processingStage = 0,
        EffectDescriptor[]? effectDescriptors = null)
    {
        if (effectDescriptors != null)
        {
            var effectChain = new EffectChain();
            var effectDescriptorNatives = new EffectDescriptor.__Native[effectDescriptors.Length];
            for (int i = 0; i < effectDescriptorNatives.Length; i++)
            {
                effectDescriptors[i].__MarshalTo(ref effectDescriptorNatives[i]);
            }

            effectChain.EffectCount = effectDescriptorNatives.Length;
            fixed (void* pEffectDescriptors = &effectDescriptorNatives[0])
            {
                effectChain.EffectDescriptorPointer = (IntPtr)pEffectDescriptors;
                return CreateSubmixVoice(inputChannels, inputSampleRate, (int)flags, processingStage, null, effectChain);
            }
        }
        else
        {
            return CreateSubmixVoice(inputChannels, inputSampleRate, (int)flags, processingStage, null, null);
        }
    }

    public void SetDebugConfiguration(DebugConfiguration? debugConfiguration)
    {
        SetDebugConfiguration(debugConfiguration, IntPtr.Zero);
    }

    /// <summary>
    /// Calculate a decibel from a volume.
    /// </summary>
    /// <param name="volume">The volume.</param>
    /// <returns>a dB value</returns>
    public static float AmplitudeRatioToDecibels(float volume)
    {
        if (volume == 0f)
        {
            return float.MinValue;
        }

        return (float)(Math.Log10(volume) * 20);
    }

    /// <summary>
    /// Calculate radians from a cutoffs frequency.
    /// </summary>
    /// <param name="cutoffFrequency">The cutoff frequency.</param>
    /// <param name="sampleRate">The sample rate.</param>
    /// <returns>radian</returns>
    public static float CutoffFrequencyToRadians(float cutoffFrequency, int sampleRate)
    {
        if (((int)cutoffFrequency * 6.0) >= sampleRate)
            return 1f;
        return (float)(Math.Sin(cutoffFrequency * Math.PI / sampleRate) * 2);
    }

    /// <summary>
    /// Calculate a cutoff frequency from a radian.
    /// </summary>
    /// <param name="radians">The radians.</param>
    /// <param name="sampleRate">The sample rate.</param>
    /// <returns>cutoff frequency</returns>
    public static float RadiansToCutoffFrequency(float radians, float sampleRate)
    {
        return (float)((Math.Asin(radians * 0.5) * sampleRate) / Math.PI);
    }

    /// <summary>
    /// Calculate a volume from a decibel
    /// </summary>
    /// <param name="decibels">a dB value</param>
    /// <returns>an amplitude value</returns>
    public static float DecibelsToAmplitudeRatio(float decibels)
    {
        return (float)Math.Pow(10, decibels / 20);
    }


    /// <summary>
    /// Calculate semitones from a Frequency ratio
    /// </summary>
    /// <param name="frequencyRatio">The frequency ratio.</param>
    /// <returns>semitones</returns>
    public static float FrequencyRatioToSemitones(float frequencyRatio)
    {
        return (float)(Math.Log10(frequencyRatio) * 12 * Math.PI);
    }

    /// <summary>
    /// Calculate frequency from semitones.
    /// </summary>
    /// <param name="semitones">The semitones.</param>
    /// <returns>the frequency</returns>
    public static float SemitonesToFrequencyRatio(float semitones)
    {
        return (float)Math.Pow(2, semitones / 12);
    }

    /// <summary>
    /// Atomically applies a set of operations for all pending operations.
    /// </summary>
    public void CommitChanges()
    {
        CommitChanges(0);
    }


    #region Callback
    private class EngineCallbackImpl : CallbackBase, IXAudio2EngineCallback
    {
        public IXAudio2 XAudio2 { get; }

        public EngineCallbackImpl(IXAudio2 xAudio2)
        {
            XAudio2 = xAudio2;
        }

        public void OnProcessingPassStart()
        {
            XAudio2.ProcessingPassStart?.Invoke(this, EventArgs.Empty);
        }

        public void OnProcessingPassEnd()
        {
            XAudio2.ProcessingPassEnd?.Invoke(this, EventArgs.Empty);
        }

        public void OnCriticalError(Result error)
        {
            XAudio2.CriticalError?.Invoke(this, new ErrorEventArgs(error));
        }
    }
    #endregion
}
