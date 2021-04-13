// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.DirectWrite
{
    public partial class IDWriteFontFile
    {
        /// <summary>	
        /// Obtains the reference to the reference key of a font file. The returned reference is valid until the font file object is released.  	
        /// </summary>	
        /// <returns>the reference to the reference key of a font file. </returns>
        public unsafe Span<byte> GetReferenceKey()
        {
            void* keyPtr;
            GetReferenceKey(&keyPtr, out int keySize);
            return new Span<byte>(keyPtr, keySize);
        }
    }
}
