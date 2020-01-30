// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.XAudio2
{
    [Shadow(typeof(IXAudio2EngineCallbackShadow))]
    internal partial interface IXAudio2EngineCallback
    {
        void OnProcessingPassStart();
        void OnProcessingPassEnd();
        void OnCriticalError(Result error);
    }

    internal class IXAudio2EngineCallbackShadow : CppObjectShadow
    {
        protected override CppObjectVtbl Vtbl => new VTable();

        private class VTable : CppObjectVtbl
        {
            public VTable() : base(3)
            {
                AddMethod(new OnProcessingPassStartDelegate(OnProcessingPassStartImpl));
                AddMethod(new OnProcessingPassEndDelegate(OnProcessingPassEndImpl));
                AddMethod(new OnCriticalErrorDelegate(OnCriticalErrorImpl));
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void OnProcessingPassStartDelegate(IntPtr thisObject);
            private static void OnProcessingPassStartImpl(IntPtr thisObject)
            {
                var shadow = ToShadow<IXAudio2EngineCallbackShadow>(thisObject);
                var callback = (IXAudio2EngineCallback)shadow.Callback;
                callback.OnProcessingPassStart();
            }

            /// <summary>	
            /// Called by XAudio2 just after an audio processing pass ends.	
            /// </summary>	
            /// <unmanaged>void IXAudio2EngineCallback::OnProcessingPassEnd()</unmanaged>
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void OnProcessingPassEndDelegate(IntPtr thisObject);
            private static void OnProcessingPassEndImpl(IntPtr thisObject)
            {
                var shadow = ToShadow<IXAudio2EngineCallbackShadow>(thisObject);
                var callback = (IXAudio2EngineCallback)shadow.Callback;
                callback.OnProcessingPassEnd();
            }

            /// <summary>	
            /// Called if a critical system error occurs that requires XAudio2 to be closed down and restarted.	
            /// </summary>
            /// <param name="thisObject">This pointer</param>
            /// <param name="error"> Error code returned by XAudio2. </param>
            /// <unmanaged>void IXAudio2EngineCallback::OnCriticalError([None] HRESULT Error)</unmanaged>
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void OnCriticalErrorDelegate(IntPtr thisObject, int error);
            private static void OnCriticalErrorImpl(IntPtr thisObject, int error)
            {
                var shadow = ToShadow<IXAudio2EngineCallbackShadow>(thisObject);
                var callback = (IXAudio2EngineCallback)shadow.Callback;
                callback.OnCriticalError(new Result(error));
            }
        }
    }
}
