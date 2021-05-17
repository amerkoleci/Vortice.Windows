// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Offers base functionality that allows for a consistent way to monitor the validity of a session across the different types of sessions.
    /// The different types of sessions is of type  <see cref="ID3D12ProtectedResourceSession"/>
    /// </summary>
    public partial class ID3D12ProtectedSession
    {
        public T? GetStatusFence<T>() where T : ID3D12Fence
        {
            if (GetStatusFence(typeof(T).GUID, out IntPtr nativePtr).Failure)
            {
                return default;
            }

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }
    }
}
