// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using SharpGen.Runtime;

namespace Vortice.XAudio2
{
    public class EngineCallback : DisposeBase, IXAudio2EngineCallback
    {
        private int _refCount = 1;
        private ShadowContainer _shadow;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Release();
            }
        }

        public uint AddRef()
        {
            return (uint)Interlocked.Increment(ref _refCount);
        }

        public uint Release()
        {
            var newRefCount = Interlocked.Decrement(ref _refCount);
            if (newRefCount == 1)
            {
                // Dispose native resources
                var callback = ((ICallbackable)this);
                callback.Shadow = null;
            }
            return (uint)newRefCount;
        }

        /// <summary>	
        /// Called by XAudio2 just before an audio processing pass begins.	
        /// </summary>	
        protected virtual void OnProcessingPassStart()
        {
        }

        /// <summary>	
        /// Called by XAudio2 just after an audio processing pass ends.	
        /// </summary>	
        protected virtual void OnProcessingPassEnd()
        {
        }

        /// <summary>	
        /// Called if a critical system error occurs that requires XAudio2 to be closed down and restarted.	
        /// </summary>	
        /// <param name="error"> Error code returned by XAudio2. </param>
        protected virtual void OnCriticalError(Result error)
        {
        }

        void IXAudio2EngineCallback.OnProcessingPassStart()
        {
            OnProcessingPassStart();
        }

        void IXAudio2EngineCallback.OnProcessingPassEnd()
        {
            OnProcessingPassEnd();
        }

        void IXAudio2EngineCallback.OnCriticalError(Result error)
        {
            OnCriticalError(error);
        }

        ShadowContainer ICallbackable.Shadow
        {
            get => Volatile.Read(ref _shadow);
            set
            {
                if (value != null)
                {
                    // Only set the shadow container if it is not already set.
                    if (Interlocked.CompareExchange(ref _shadow, value, null) != null)
                    {
                        value.Dispose();
                    }
                }
                else
                {
                    Interlocked.Exchange(ref _shadow, value)?.Dispose();
                }
            }
        }
    }
}
