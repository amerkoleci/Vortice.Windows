using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGen.Runtime.Win32
{
    public static class RawBoolHelpers
    {
        /// <summary>
        /// Converts bool array to <see cref="RawBool"/> array.
        /// </summary>
        /// <param name="array">The bool array.</param>
        /// <returns>Converted array of <see cref="RawBool"/>.</returns>
        public static RawBool[] ConvertToRawBoolArray(ReadOnlySpan<bool> array)
        {
            var temp = new RawBool[array.Length];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = array[i];
            return temp;
        }

        /// <summary>
        /// Converts <see cref="RawBool"/> array to bool array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>Converted array of bool.</returns>
        public static bool[] ConvertToBoolArray(ReadOnlySpan<RawBool> array)
        {
            var temp = new bool[array.Length];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = array[i];
            return temp;
        }

    }
}
