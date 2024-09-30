// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectWrite;

public partial class IDWriteFontFile
{
    /// <summary>	
    /// Obtains the reference to the reference key of a font file. The returned reference is valid until the font file object is released.  	
    /// </summary>	
    /// <returns>the reference to the reference key of a font file. </returns>
    public unsafe Span<byte> GetReferenceKey()
    {
        void* keyPtr;
        GetReferenceKey(&keyPtr, out uint keySize);
        return new Span<byte>(keyPtr, (int)keySize);
    }
}
