// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Multimedia;

namespace Vortice.XAudio2
{
    public partial class IXAudio2
    {
        private static Guid CLSID_XAudio27 = new Guid("5a508685-a254-4fba-9b82-9a24b00306af");
        private static Guid CLSID_XAudio27_Debug = new Guid("db05ea35-0329-4d4b-a53a-6dead03d3852");
        private static Guid IID_IXAudio27 = new Guid("8bcf1f58-9fe7-4583-8ac6-e2adc465c8bb");

        private readonly EngineCallbackImpl _engineCallbackImpl;

        /// <summary>
        /// Get the running version.
        /// </summary>
        public XAudio2Version Version { get; }

        #region Events
        /// <summary>	
        /// Called by XAudio2 just before an audio processing pass begins.	
        /// </summary>	
        public event EventHandler ProcessingPassStart;

        /// <summary>	
        /// Called by XAudio2 just after an audio processing pass ends.	
        /// </summary>	
        public event EventHandler ProcessingPassEnd;

        /// <summary>
        /// Called if a critical system error occurs that requires XAudio2 to be closed down and restarted.
        /// </summary>
        public event EventHandler<ErrorEventArgs> CriticalError;
        #endregion Events

        public IXAudio2()
            : this(XAudio2Version.Default)
        {
        }


        public IXAudio2(XAudio2Version requestedVersion)
            : this(XAudio2Flags.None, ProcessorSpecifier.Processor1, requestedVersion)
        {
        }

        public IXAudio2(XAudio2Flags flags, ProcessorSpecifier processorSpecifier, XAudio2Version requestedVersion = XAudio2Version.Default)
            : base(IntPtr.Zero)
        {
            var tryVersions = requestedVersion == XAudio2Version.Default
                ? new[] { XAudio2Version.Version29, XAudio2Version.Version28, XAudio2Version.Version27 }
                : new[] { requestedVersion };

            foreach (var tryVersion in tryVersions)
            {
                switch (tryVersion)
                {
                    case XAudio2Version.Version27:
                        if (PlatformDetection.IsUAP)
                        {
                            throw new NotSupportedException("XAudio 2.7 is not supported on UAP platform");
                        }

                        Guid clsid = ((int)flags == 1) ? CLSID_XAudio27_Debug : CLSID_XAudio27;
                        if ((requestedVersion == XAudio2Version.Default || requestedVersion == XAudio2Version.Version27)
                            && ComUtilities.TryCreateComInstance(clsid, ComContext.InprocServer, IID_IXAudio27, this))
                        {
                            SetupVtblFor27();
                            // Initialize XAudio2
                            Initialize(0, processorSpecifier);
                            Version = XAudio2Version.Version27;
                        }
                        break;
                    case XAudio2Version.Version28:
                        try
                        {
                            NativePointer = XAudio28.XAudio2Create(0, processorSpecifier);
                            Version = XAudio2Version.Version28;
                        }
                        catch (DllNotFoundException) { }
                        break;
                    case XAudio2Version.Version29:
                        try
                        {
                            NativePointer = XAudio29.XAudio2Create(0, processorSpecifier);
                            Version = XAudio2Version.Version29;
                        }
                        catch (DllNotFoundException) { }
                        break;
                }

                // Early exit if we found a requestedVersion
                if (Version != XAudio2Version.Default)
                {
                    break;
                }
            }

            if (Version == XAudio2Version.Default)
            {
                var versionStr = requestedVersion == XAudio2Version.Default ? "2.7, 2.8 or 2.9" : requestedVersion.ToString();
                throw new DllNotFoundException($"Unable to find XAudio2 dlls for requested versions [{versionStr}], not installed on this machine");
            }

            IXAudio2Voice.Version = Version;

            // Register engine callback
            _engineCallbackImpl = new EngineCallbackImpl(this);
            RegisterForCallbacks(_engineCallbackImpl);
        }

        protected override void Dispose(bool disposing)
        {
            if (_engineCallbackImpl != null)
            {
                UnregisterForCallbacks(_engineCallbackImpl);
            }

            if (disposing)
            {
                if (_engineCallbackImpl != null)
                {
                    _engineCallbackImpl.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        public IXAudio2MasteringVoice CreateMasteringVoice(
            int inputChannels = DefaultChannels,
            int inputSampleRate = DefaultSampleRate,
            AudioStreamCategory category = AudioStreamCategory.GameEffects)
        {
            if (Version == XAudio2Version.Version27)
            {
                return CreateMasteringVoice27(inputChannels, inputSampleRate, 0, 0, null);
            }
            else
            {
                return CreateMasteringVoice(inputChannels, inputSampleRate, 0, null, null, category);
            }
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

        #region Version 2.7
        private void CheckVersion27()
        {
            if (Version != XAudio2Version.Version27)
            {
                throw new InvalidOperationException("This method is only valid on the XAudio 2.7 requestedVersion [Current is: " + Version + "]");
            }
        }

        private void CheckNotVersion27()
        {
            if (Version == XAudio2Version.Version27)
            {
                throw new InvalidOperationException("This method is only valid on version greather than XAudio 2.7 [Current is: " + Version + "]");
            }
        }

        /// <summary>
        /// Setups the VTBL for XAudio 2.7. The 2.7 verions had 3 methods starting at VTBL[3]:
        /// - GetDeviceCount
        /// - GetDeviceDetails
        /// - Initialize
        /// </summary>
        private void SetupVtblFor27()
        {
            RegisterForCallbacks__vtbl_index += 3;
            UnregisterForCallbacks__vtbl_index += 3;
            CreateSourceVoice__vtbl_index += 3;
            CreateSubmixVoice__vtbl_index += 3;
            CreateMasteringVoice__vtbl_index += 3;
            StartEngine__vtbl_index += 3;
            StopEngine__vtbl_index += 3;
            CommitChanges__vtbl_index += 3;
            GetPerformanceData__vtbl_index += 3;
            SetDebugConfiguration__vtbl_index += 3;
        }

        private unsafe void Initialize(int flags, ProcessorSpecifier xAudio2Processor)
        {
            var result = (Result)LocalInterop.CalliInitialize(_nativePointer, (int)flags, (int)xAudio2Processor, *(*(void***)_nativePointer + 5));
            result.CheckError();
        }

        private unsafe IXAudio2MasteringVoice CreateMasteringVoice27(int inputChannels, int inputSampleRate, int flags, int deviceIndex, EffectChain? effectChainRef)
        {
            var nativePtr = IntPtr.Zero;
            EffectChain effectChain;
            if (effectChainRef.HasValue)
            {
                effectChain = effectChainRef.Value;
            }

            Result result = LocalInterop.CalliCreateMasteringVoice(_nativePointer, (void*)&nativePtr, inputChannels, inputSampleRate, flags, deviceIndex, effectChainRef.HasValue ? ((void*)(&effectChain)) : ((void*)IntPtr.Zero), *(*(void***)_nativePointer + 10));
            result.CheckError();
            return new IXAudio2MasteringVoice(nativePtr);
        }

        //private unsafe void GetDeviceDetails(int index, out DeviceDetails deviceDetailsRef)
        //{
        //    DeviceDetails.__Native _Native = default(DeviceDetails.__Native);
        //    Result result = LocalInterop.CalliGetDeviceDetails(this._nativePointer, index, &_Native, *(*(void***)this._nativePointer + 4));
        //    deviceDetailsRef = default(DeviceDetails);
        //    deviceDetailsRef.__MarshalFrom(ref _Native);
        //    result.CheckError();
        //}
        #endregion

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

            ShadowContainer ICallbackable.Shadow { get; set; }
        }
        #endregion
    }
}
