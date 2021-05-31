// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Vortice.DirectWrite
{
    /// <summary>
    /// Specifies a range of text positions where format is applied in the text represented by an <see cref="IDWriteTextLayout"/> object.
    /// </summary>
    public partial struct TextRange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextRange"/> struct.
        /// </summary>
        /// <param name="startPosition">The start position of the text range.</param>
        /// <param name="length">The number positions in the text range.</param>
        public TextRange(int startPosition, int length)
        {
            StartPosition = startPosition;
            Length = length;
        }
    }
}
