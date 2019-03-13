// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public static class DXGI
    {
        public static readonly Guid All = new Guid("e48ae283-da80-490b-87e6-43e9a9cfda08");
        public static readonly Guid Dx = new Guid("35cdd7fc-13b2-421d-a5d7-7e4451287d64");
        public static readonly Guid Dxgi = new Guid("25cddaa4-b1c6-47e1-ac3e-98875b5a2e2a");
        public static readonly Guid App = new Guid("06cd6e01-4219-4ebd-8709-27ed23360c62");

        /// <summary>
        /// Try to create new instance of <see cref="IDXGIFactory"/>.
        /// </summary>
        /// <param name="factory">The <see cref="IDXGIFactory"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result CreateDXGIFactory<T>(out T factory) where T : IDXGIFactory
        {
            var result = DXGIInternal.CreateDXGIFactory(typeof(T).GUID, out var nativePtr);
            if (result.Success)
            {
                factory = CppObject.FromPointer<T>(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }

        /// <summary>
        /// Try to create new instance of <see cref="IDXGIFactory1"/>.
        /// </summary>
        /// <param name="factory">The <see cref="IDXGIFactory1"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result CreateDXGIFactory1<T>(out T factory) where T : IDXGIFactory1
        {
            var result = DXGIInternal.CreateDXGIFactory1(typeof(T).GUID, out var nativePtr);
            if (result.Success)
            {
                factory = CppObject.FromPointer<T>(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }

        /// <summary>
        /// Try to create new instance of <see cref="IDXGIFactory2"/>.
        /// </summary>
        /// <param name="debug">Whether to enable debug callback.</param>
        /// <param name="factory">The <see cref="IDXGIFactory2"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result CreateDXGIFactory2<T>(bool debug, out T factory) where T : IDXGIFactory2
        {
            int flags = debug ? DXGIInternal.CreateFactoryDebug : 0x00;
            var result = DXGIInternal.CreateDXGIFactory2(flags, typeof(T).GUID, out var nativePtr);
            if (result.Success)
            {
                factory = CppObject.FromPointer<T>(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }

        /// <summary>
        /// Gets debug interface for given type.
        /// </summary>
        /// <typeparam name="T">The <see cref="ComObject"/> to get.</typeparam>
        /// <param name="debugInterface">Instance of T.</param>
        /// <returns>The result code.</returns>
        public static Result DXGIGetDebugInterface<T>(out T debugInterface) where T : ComObject
        {
            try
            {
                var result = DXGIInternal.DXGIGetDebugInterface(typeof(T).GUID, out var nativePtr);
                if (result.Failure)
                {
                    debugInterface = null;
                    return result;
                }

                debugInterface = CppObject.FromPointer<T>(nativePtr);
                return result;
            }
            catch
            {
                debugInterface = default;
                return ResultCode.NotFound;
            }
        }

        /// <summary>
        /// Gets debug interface for given type.
        /// </summary>
        /// <typeparam name="T">The <see cref="ComObject"/> to get.</typeparam>
        /// <param name="debugInterface">Instance of T.</param>
        /// <returns>The result code.</returns>
        public static Result DXGIGetDebugInterface1<T>(out T debugInterface) where T : ComObject
        {
            try
            {
                var result = DXGIInternal.DXGIGetDebugInterface1(0, typeof(T).GUID, out var nativePtr);
                if (result.Failure)
                {
                    debugInterface = null;
                    return result;
                }

                debugInterface = CppObject.FromPointer<T>(nativePtr);
                return result;
            }
            catch
            {
                debugInterface = default;
                return ResultCode.NotFound;
            }
        }

        /// <summary>
        /// Allows a process to indicate that it's resilient to any of its graphics devices being removed.
        /// </summary>
        /// <remarks>Maps to DXGIDeclareAdapterRemovalSupport native call.</remarks>
        /// <returns></returns>
        public static Result DXGIDeclareAdapterRemovalSupport()
        {
            return DXGIInternal.DXGIDeclareAdapterRemovalSupport();
        }
    }
}
