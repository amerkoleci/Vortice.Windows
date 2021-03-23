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
    }

    internal class IXAudio2VoiceCallbackShadow : CppObjectShadow
    {
        private static readonly VTable s_vtbl = new VTable();

        protected override CppObjectVtbl Vtbl => s_vtbl;

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

            private static void OnVoiceProcessingPassStartImpl(IntPtr thisObject, int bytes)
            {
                IXAudio2VoiceCallbackShadow shadow = ToShadow<IXAudio2VoiceCallbackShadow>(thisObject);
                IXAudio2VoiceCallback callback = (IXAudio2VoiceCallback)shadow.Callback;
                callback.OnVoiceProcessingPassStart(bytes);
            }

            private static void OnVoiceProcessingPassEndImpl(IntPtr thisObject)
            {
                IXAudio2VoiceCallbackShadow shadow = ToShadow<IXAudio2VoiceCallbackShadow>(thisObject);
                IXAudio2VoiceCallback callback = (IXAudio2VoiceCallback)shadow.Callback;
                callback.OnVoiceProcessingPassEnd();
            }

            private static void OnStreamEndImpl(IntPtr thisObject)
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
