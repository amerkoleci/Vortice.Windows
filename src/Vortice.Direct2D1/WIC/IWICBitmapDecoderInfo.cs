// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

public partial class IWICBitmapDecoderInfo
{
    public uint PatternsCount
    {
        get
        {
            GetPatterns(0, null, out uint count, out _);
            return count;
        }
    }

    public uint PatternsActualCount
    {
        get
        {
            GetPatterns(0, null, out uint _, out uint actualCount);
            return actualCount;
        }
    }

    /// <summary>
    /// Gets the file pattern signatures supported by the decoder.
    /// </summary>
    /// <unmanaged>HRESULT IWICBitmapDecoderInfo::GetPatterns([In] unsigned int cbSizePatterns,[Out, Buffer, Optional] WICBitmapPattern* pPatterns,[Out] unsigned int* pcPatterns,[Out] unsigned int* pcbPatternsActual)</unmanaged>	
    public WICBitmapPattern[] Patterns
    {
        get
        {
            GetPatterns(0, null, out uint count, out uint actualCount);
            if (actualCount == 0)
                return Array.Empty<WICBitmapPattern>();

            count = actualCount;
            WICBitmapPattern[] result = new WICBitmapPattern[actualCount];
            GetPatterns(count, result, out count, out actualCount);

            return result;
        }
    }

    public void GetPatterns(WICBitmapPattern[] patterns)
    {
        GetPatterns((uint)patterns.Length, patterns, out _, out _);
    }
}
