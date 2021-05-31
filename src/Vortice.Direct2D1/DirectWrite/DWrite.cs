// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DirectWrite
{
    public static partial class DWrite
    {
        /// <summary>
        ///  Try to create new instance of <see cref="IDWriteFactory"/>.
        /// </summary>
        /// <typeparam name="T">Type based on <see cref="IDWriteFactory"/>.</typeparam>
        /// <param name="factoryType">The <see cref="IDWriteFactory"/> type.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static T DWriteCreateFactory<T>(FactoryType factoryType = FactoryType.Shared) where T : IDWriteFactory
        {
            DWriteCreateFactory(factoryType, typeof(IDWriteFactory).GUID, out IntPtr nativePtr).CheckError();
            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        /// <summary>
        ///  Try to create new instance of <see cref="IDWriteFactory"/>.
        /// </summary>
        /// <typeparam name="T">Type based on <see cref="IDWriteFactory"/>.</typeparam>
        /// <param name="factory">The <see cref="IDWriteFactory"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result DWriteCreateFactory<T>(out T? factory) where T : IDWriteFactory
        {
            return DWriteCreateFactory(FactoryType.Shared, out factory);
        }

        /// <summary>
        ///  Try to create new instance of <see cref="IDWriteFactory"/>.
        /// </summary>
        /// <typeparam name="T">Type based on <see cref="IDWriteFactory"/>.</typeparam>
        /// <param name="factoryType">The type of factory.</param>
        /// <param name="factory">The <see cref="IDWriteFactory"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result DWriteCreateFactory<T>(FactoryType factoryType, out T? factory) where T : IDWriteFactory
        {
            var result = DWriteCreateFactory(factoryType, typeof(IDWriteFactory).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                factory = null;
                return result;
            }

            factory = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }
    }
}
