// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace SharpDirect2D
{
    public static partial class D2D1
    {
        /// <summary>
        /// Try to create new instance of <see cref="ID2D1Factory"/>.
        /// </summary>
        /// <param name="factoryType">The type of factory.</param>
        /// <param name="factory">The <see cref="ID2D1Factory"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result D2D1CreateFactory<T>(FactoryType factoryType, out T factory) where T : ID2D1Factory
        {
            var options = new FactoryOptions
            {
                DebugLevel = DebugLevel.None,
            };

            var result = D2D1CreateFactory(factoryType, typeof(T).GUID, options, out var nativePtr);
            if (result.Success)
            {
                factory = CppObject.FromPointer<T>(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }

        /// <summary>
        /// Try to create new instance of <see cref="ID2D1Factory"/>.
        /// </summary>
        /// <param name="factoryType">The type of factory.</param>
        /// <param name="options">The <see cref="FactoryOptions"/>.</param>
        /// <param name="factory">The <see cref="ID2D1Factory"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result D2D1CreateFactory<T>(FactoryType factoryType, FactoryOptions options, out T factory) where T : ID2D1Factory
        {
            var result = D2D1CreateFactory(factoryType, typeof(T).GUID, options, out var nativePtr);
            if (result.Success)
            {
                factory = CppObject.FromPointer<T>(nativePtr);
                return result;
            }

            factory = null;
            return result;
        }
    }
}
