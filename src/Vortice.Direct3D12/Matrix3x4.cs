// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.Direct3D12
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix3x4
    {
        /// <summary>
        /// Value at row 1 column 1.
        /// </summary>
        public float M11;

        /// <summary>
        /// Value at row 1 column 2.
        /// </summary>
        public float M12;

        /// <summary>
        /// Value at row 1 column 3.
        /// </summary>
        public float M13;

        /// <summary>
        /// Value at row 1 column 4.
        /// </summary>
        public float M14;

        /// <summary>
        /// Value at row 2 column 1.
        /// </summary>
        public float M21;

        /// <summary>
        /// Value at row 2 column 2.
        /// </summary>
        public float M22;

        /// <summary>
        /// Value at row 2 column 3.
        /// </summary>
        public float M23;

        /// <summary>
        /// Value at row 2 column 4.
        /// </summary>
        public float M24;

        /// <summary>
        /// Value at row 3 column 1.
        /// </summary>
        public float M31;

        /// <summary>
        /// Value at row 3 column 2.
        /// </summary>
        public float M32;

        /// <summary>
        /// Value at row 3 column 3.
        /// </summary>
        public float M33;

        /// <summary>
        /// Value at row 3 column 4.
        /// </summary>
        public float M34;
    }
}
