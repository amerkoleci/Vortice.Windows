// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.XAudio2
{
    [Shadow(typeof(IXAudio2VoiceCallbackShadow))]
    public partial interface IXAudio2VoiceCallback
    {
        /// <summary>	
        /// Called during each processing pass for each voice, just before XAudio2 reads data from the voice's buffer queue.	
        /// </summary>	
        /// <param name="bytesRequired"> The number of bytes that must be submitted immediately to avoid starvation. This allows the implementation of just-in-time streaming scenarios; the client can keep the absolute minimum data queued on the voice at all times, and pass it fresh data just before the data is required. This model provides the lowest possible latency attainable with XAudio2. For xWMA and XMA data BytesRequired will always be zero, since the concept of a frame of xWMA or XMA data is meaningless. Note In a situation where there is always plenty of data available on the source voice, BytesRequired should always report zero, because it doesn't need any samples immediately to avoid glitching. </param>
        void OnVoiceProcessingPassStart(int bytesRequired);

        /// <summary>	
        /// Called just after the processing pass for the voice ends.	
        /// </summary>	
        void OnVoiceProcessingPassEnd();


        /// <summary>	
        /// Called when the voice has just finished playing a contiguous audio stream.	
        /// </summary>	
        void OnStreamEnd();


        /// <summary>	
        /// Called when the voice is about to start processing a new audio buffer.	
        /// </summary>	
        /// <param name="context"> Context pointer that was assigned to the pContext member of the <see cref="AudioBuffer"/> structure when the buffer was submitted. </param>
        void OnBufferStart(IntPtr context);


        /// <summary>	
        /// Called when the voice finishes processing a buffer.	
        /// </summary>	
        /// <param name="context"> Context pointer assigned to the pContext member of the <see cref="AudioBuffer"/> structure when the buffer was submitted. </param>
        void OnBufferEnd(IntPtr context);


        /// <summary>	
        /// Called when the voice reaches the end position of a loop.	
        /// </summary>	
        /// <param name="context"> Context pointer that was assigned to the pContext member of the <see cref="AudioBuffer"/> structure when the buffer was submitted. </param>
        void OnLoopEnd(IntPtr context);


        /// <summary>	
        /// Called when a critical error occurs during voice processing.	
        /// </summary>	
        /// <param name="context"> Context pointer that was assigned to the pContext member of the <see cref="AudioBuffer"/> structure when the buffer was submitted. </param>
        /// <param name="error"> The HRESULT code of the error encountered. </param>
        void OnVoiceError(IntPtr context, Result error);
    }

    internal class IXAudio2VoiceCallbackShadow : CppObjectShadow
    {
        private static readonly VTable _vTable = new VTable();

        protected override CppObjectVtbl Vtbl => _vTable;

        /// <summary>
        /// Return a pointer to the unmanaged version of this callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>A pointer to a shadow c++ callback</returns>
        public static IntPtr ToIntPtr(IXAudio2VoiceCallback callback)
        {
            return ToCallbackPtr<IXAudio2VoiceCallback>(callback);
        }

        private class VTable : CppObjectVtbl
        {
            public VTable() : base(7)
            {
                AddMethod(new IntDelegate(OnVoiceProcessingPassStartImpl));
                AddMethod(new VoidDelegate(OnVoiceProcessingPassEndImpl));
                AddMethod(new VoidDelegate(OnStreamEndImpl));
                AddMethod(new IntPtrDelegate(OnBufferStartImpl));
                AddMethod(new IntPtrDelegate(OnBufferEndImpl));
                AddMethod(new IntPtrDelegate(OnLoopEndImpl));
                AddMethod(new IntPtrIntDelegate(OnVoiceErrorImpl));
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void VoidDelegate(IntPtr thisObject);
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void IntDelegate(IntPtr thisObject, int bytes);
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void IntPtrDelegate(IntPtr thisObject, IntPtr address);
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void IntPtrIntDelegate(IntPtr thisObject, IntPtr address, int error);

            static private void OnVoiceProcessingPassStartImpl(IntPtr thisObject, int bytes)
            {
                var shadow = ToShadow<IXAudio2VoiceCallbackShadow>(thisObject);
                var callback = (IXAudio2VoiceCallback)shadow.Callback;
                callback.OnVoiceProcessingPassStart(bytes);
            }

            static private void OnVoiceProcessingPassEndImpl(IntPtr thisObject)
            {
                var shadow = ToShadow<IXAudio2VoiceCallbackShadow>(thisObject);
                var callback = (IXAudio2VoiceCallback)shadow.Callback;
                callback.OnVoiceProcessingPassEnd();
            }

            static private void OnStreamEndImpl(IntPtr thisObject)
            {
                var shadow = ToShadow<IXAudio2VoiceCallbackShadow>(thisObject);
                var callback = (IXAudio2VoiceCallback)shadow.Callback;
                callback.OnStreamEnd();
            }

            static private void OnBufferStartImpl(IntPtr thisObject, IntPtr address)
            {
                var shadow = ToShadow<IXAudio2VoiceCallbackShadow>(thisObject);
                var callback = (IXAudio2VoiceCallback)shadow.Callback;
                callback.OnBufferStart(address);
            }

            static private void OnBufferEndImpl(IntPtr thisObject, IntPtr address)
            {
                var shadow = ToShadow<IXAudio2VoiceCallbackShadow>(thisObject);
                var callback = (IXAudio2VoiceCallback)shadow.Callback;
                callback.OnBufferEnd(address);
            }


            static private void OnLoopEndImpl(IntPtr thisObject, IntPtr address)
            {
                var shadow = ToShadow<IXAudio2VoiceCallbackShadow>(thisObject);
                var callback = (IXAudio2VoiceCallback)shadow.Callback;
                callback.OnLoopEnd(address);
            }

            static private void OnVoiceErrorImpl(IntPtr thisObject, IntPtr bufferContextRef, int error)
            {
                var shadow = ToShadow<IXAudio2VoiceCallbackShadow>(thisObject);
                var callback = (IXAudio2VoiceCallback)shadow.Callback;
                callback.OnVoiceError(bufferContextRef, new Result(error));
            }
        }
    }
}
