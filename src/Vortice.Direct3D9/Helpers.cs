// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime.Win32;
using Vortice.Mathematics;

namespace Vortice.Direct3D9
{
    internal static class Helpers
    {
        /// <summary>
        /// Converts the color into a packed integer.
        /// </summary>
        /// <returns>A packed integer containing all four color components.</returns>
        public static int ToBgra(in Color color)
        {
            int value = color.B;
            value |= color.G << 8;
            value |= color.R << 16;
            value |= color.A << 24;

            return (int)value;
        }

        /// <summary>
        /// Converts the color into a packed integer.
        /// </summary>
        /// <returns>A packed integer containing all four color components.</returns>
        public static int ToBgra(in System.Drawing.Color color)
        {
            int value = color.B;
            value |= color.G << 8;
            value |= color.R << 16;
            value |= color.A << 24;

            return (int)value;
        }

        /// <summary>
        /// Converts bool array to integer pointers array.
        /// </summary>
        /// <param name="array">The bool array.</param>
        /// <param name="dest">The destination array of int pointers.</param>
        public unsafe static void ConvertToIntArray(bool[] array, int* dest)
        {
            for (int i = 0; i < array.Length; i++)
            {
                dest[i] = array[i] ? 1 : 0;
            }
        }

        /// <summary>
        /// Converts bool array to <see cref="RawBool"/> array.
        /// </summary>
        /// <param name="array">The bool array.</param>
        /// <returns>Converted array of <see cref="RawBool"/>.</returns>
        public static RawBool[] ConvertToIntArray(bool[] array)
        {
            var result = new RawBool[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[i];
            }

            return result;
        }
    }
}
