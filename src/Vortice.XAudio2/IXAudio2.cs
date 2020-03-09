// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using NativeLibraryLoader;
using SharpGen.Runtime;
using Vortice.Multimedia;

namespace Vortice.XAudio2
{
    public partial class IXAudio2
    {
        private readonly EngineCallback _engineCallback;
        private static readonly NativeLibrary s_xaudioLibrary = LoadXAudio2();
        private unsafe delegate int XAudio2CreateDelegate(void* arg0, int arg1, int arg2);

        private static NativeLibrary LoadXAudio2()
        {
            if (PlatformDetection.IsUAP)
            {
                return new NativeLibrary("xaudio2_9.dll");
            }

            return new NativeLibrary("xaudio2_9redist.dll");
        }

        public IXAudio2()
            : this(XAudio2Flags.None, ProcessorSpecifier.Processor1, null)
        {
        }

        public IXAudio2(EngineCallback engineCallback)
            : this(XAudio2Flags.None, ProcessorSpecifier.Processor1, engineCallback)
        {
        }

        public IXAudio2(
            XAudio2Flags flags,
            ProcessorSpecifier processorSpecifier,
            EngineCallback engineCallback = null)
            : base(IntPtr.Zero)
        {
            if (PlatformDetection.IsUAP)
            {
                NativePointer = XAudio29.XAudio2Create(0, processorSpecifier);
            }
            else
            {
                NativePointer = XAudio2CreateWithCallback(0, processorSpecifier);
            }

            // Register engine callback
            _engineCallback = engineCallback;
            if (engineCallback != null)
            {
                RegisterForCallbacks(engineCallback);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_engineCallback != null)
            {
                UnregisterForCallbacks(_engineCallback);
            }

            if (disposing)
            {
                _engineCallback?.Dispose();
            }

            base.Dispose(disposing);
        }

        public IXAudio2MasteringVoice CreateMasteringVoice(
            int inputChannels = DefaultChannels,
            int inputSampleRate = DefaultSampleRate,
            AudioStreamCategory category = AudioStreamCategory.GameEffects)
        {
            return CreateMasteringVoice(inputChannels, inputSampleRate, 0, null, null, category);
        }

        public unsafe IXAudio2SourceVoice CreateSourceVoice(
            WaveFormat sourceFormat,
            VoiceFlags flags = VoiceFlags.None,
            float maxFrequencyRatio = 1.0f,
            IXAudio2VoiceCallback callback = null,
            EffectDescriptor[] effectDescriptors = null)
        {
            var waveformatPtr = WaveFormat.MarshalToPtr(sourceFormat);
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
                        return CreateSourceVoice(waveformatPtr, flags, maxFrequencyRatio, callback, null, effectChain);
                    }
                }
                else
                {
                    return CreateSourceVoice(waveformatPtr, flags, maxFrequencyRatio, callback, null, null);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(waveformatPtr);
            }
        }

        /// <summary>
        /// Calculate a decibel from a volume.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <returns>a dB value</returns>
        public static float AmplitudeRatioToDecibels(float volume)
        {
            if (volume == 0f)
                return float.MinValue;
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

        private static unsafe IntPtr XAudio2CreateWithCallback(int flags, ProcessorSpecifier processorSpecifier)
        {
            var XAudio2CreateCallback = s_xaudioLibrary.LoadFunction<XAudio2CreateDelegate>("XAudio2Create");
            var nativePtr = IntPtr.Zero;
            Result result = XAudio2CreateCallback(&nativePtr, flags, (int)processorSpecifier);
            result.CheckError();
            return nativePtr;
        }
    }
}
