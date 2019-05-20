// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.DirectX.Direct2D.DirectWrite
{
    public static partial class DWrite
    {
        /// <summary>
        /// Try to create new instance of <see cref="IDWriteFactory"/>.
        /// </summary>
        /// <param name="factoryType">The type of factory.</param>
        /// <param name="factory">The <see cref="IDWriteFactory"/> being created.</param>
        /// <returns>Return the <see cref="Result"/>.</returns>
        public static Result DWriteCreateFactory(FactoryType factoryType, out IDWriteFactory factory)
        {
            factory = new IDWriteFactory();
            var result = DWriteCreateFactory(factoryType, typeof(IDWriteFactory).GUID, factory);
            if (result.Success)
            {
                return result;
            }

            factory = null;
            return result;
        }
    }
}
