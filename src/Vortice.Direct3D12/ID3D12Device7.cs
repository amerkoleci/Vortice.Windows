// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device7
    {
        public T AddToStateObject<T>(StateObjectDescription addition, ID3D12StateObject stateObjectToGrowFrom) where T : ID3D12StateObject
        {
            Result result = AddToStateObject(addition, stateObjectToGrowFrom, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public T CreateProtectedResourceSession1<T>(ProtectedResourceSessionDescription1 description) where T : ID3D12ProtectedResourceSession1
        {
            Result result = CreateProtectedResourceSession1(ref description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }
    }
}
