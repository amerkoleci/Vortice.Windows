// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.Direct3D;
using SharpGen.Runtime;

namespace Vortice.Direct3D11
{
    public partial class ID3D11Device1
    {
        public ID3D11DeviceContext1 CreateDeferredContext1()
        {
            return CreateDeferredContext1(0);
        }

        public ID3DDeviceContextState CreateDeviceContextState<T>(CreateDeviceContextStateFlags flags, FeatureLevel[] featureLevels, out FeatureLevel chosenFeatureLevel) where T : ComObject
        {
            return CreateDeviceContextState(
                flags, featureLevels, featureLevels.Length,
                D3D11.SdkVersion,
                typeof(T).GUID, out chosenFeatureLevel);
        }

        /// <summary>
        /// Gives a device access to a shared resource that is referenced by a handle and that was created on a different device.
        /// </summary>
        /// <typeparam name="T">A handle to the resource to open.</typeparam>
        /// <param name="handle"></param>
        /// <returns></returns>
        public T OpenSharedResource1<T>(IntPtr handle) where T : ID3D11Resource
        {
            var result = OpenSharedResource1(handle, typeof(T).GUID, out var nativePtr);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(nativePtr);
        }

        public T OpenSharedResourceByName<T>(string name, SharedResourceFlags access) where T : ID3D11Resource
        {
            var result = OpenSharedResourceByName(name, (int)access, typeof(T).GUID, out var nativePtr);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(nativePtr);
        }
    }
}
