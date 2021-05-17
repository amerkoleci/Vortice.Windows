// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXCore
{
    public static partial class DXCore
    {
        /// <summary>
        /// DXCORE_ADAPTER_ATTRIBUTE_D3D11_GRAPHICS
        /// </summary>
        public static readonly Guid D3D11_Graphics = new Guid("8c47866b-7583-450d-f0f0-6bada895af4b");

        /// <summary>
        /// DXCORE_ADAPTER_ATTRIBUTE_D3D12_GRAPHICS
        /// </summary>
        public static readonly Guid D3D12_Graphics = new Guid("0c9ece4d-2f6e-4f01-8c96-e89e331b47b1");

        /// <summary>
        /// DXCORE_ADAPTER_ATTRIBUTE_D3D12_CORE_COMPUTE
        /// </summary>
        public static readonly Guid D3D12_CoreCompute = new Guid("248e2800-a793-4724-abaa-23a6de1be090");

        /// <summary>
        /// Try to create new instance of <see cref="IDXCoreAdapterFactory"/>.
        /// </summary>
        /// <param name="factory">The <see cref="IDXCoreAdapterFactory"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result DXCoreCreateAdapterFactory<T>(out T? factory) where T : IDXCoreAdapterFactory
        {
            Result result = DXCoreCreateAdapterFactory(typeof(T).GUID, out IntPtr nativePtr);
            if (result.Success)
            {
                factory = MarshallingHelpers.FromPointer<T>(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }

        /// <summary>
        /// Try to create new instance of <see cref="IDXCoreAdapterFactory"/>.
        /// </summary>
        /// <returns>Return an instance of <see cref="IDXCoreAdapterFactory"/> or null if failed.</returns>
        public static T? DXCoreCreateAdapterFactory<T>() where T : IDXCoreAdapterFactory
        {
            Result result = DXCoreCreateAdapterFactory(typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                return default;
            }

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }
    }
}
