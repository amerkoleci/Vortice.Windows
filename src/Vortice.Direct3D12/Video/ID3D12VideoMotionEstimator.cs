// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D12.Video
{
    public partial class ID3D12VideoMotionEstimator
    {
        public unsafe Result GetProtectedResourceSession<T>(out T protectedSession) where T : ID3D12ProtectedResourceSession
        {
            Result result = GetProtectedResourceSession(typeof(T).GUID, out IntPtr protectedSessionPtr);
            if (result.Failure)
            {
                protectedSession = default;
                return result;
            }

            protectedSession = FromPointer<T>(protectedSessionPtr);
            return result;
        }
    }
}
