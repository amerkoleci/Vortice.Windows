// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Globalization;
using System.Runtime.CompilerServices;

namespace Vortice.Multimedia;

/// <summary>
/// WaveFormatExtensible
/// http://www.microsoft.com/whdc/device/audio/multichaud.mspx
/// </summary>
public class WaveFormatExtensible : WaveFormat
{
    private short _wValidBitsPerSample; // bits of precision, or is wSamplesPerBlock if wBitsPerSample==0        

    /// <summary>
    /// Guid of the subformat.
    /// </summary>
    public Guid GuidSubFormat;

    /// <summary>
    /// Speaker configuration
    /// </summary>
    public int ChannelMask; 

    /// <summary>
    /// Parameterless constructor for marshalling
    /// </summary>
    internal WaveFormatExtensible()
    {
    }

    /// <summary>
    /// Creates a new WaveFormatExtensible for PCM or IEEE
    /// </summary>
    public WaveFormatExtensible(int rate, int bits, int channels)
        : base(rate, bits, channels)
    {
        waveFormatTag = WaveFormatEncoding.Extensible;
        extraSize = 22;
        _wValidBitsPerSample = (short)bits;
        int dwChannelMask = 0;
        for (int n = 0; n < channels; n++)
            dwChannelMask |= (1 << n);
        ChannelMask = dwChannelMask;

        // KSDATAFORMAT_SUBTYPE_IEEE_FLOAT // AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT
        // KSDATAFORMAT_SUBTYPE_PCM // AudioMediaSubtypes.MEDIASUBTYPE_PCM;
        GuidSubFormat = bits == 32 ? new Guid("00000003-0000-0010-8000-00aa00389b71") : new Guid("00000001-0000-0010-8000-00aa00389b71");
    }

    protected unsafe override IntPtr MarshalToPtr()
    {
        var result = Marshal.AllocHGlobal(Unsafe.SizeOf<__Native>());
        __MarshalTo(ref *(__Native*)result);
        return result;
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal new struct __Native
    {
        public WaveFormat.__Native waveFormat;
        public short wValidBitsPerSample; // bits of precision, or is wSamplesPerBlock if wBitsPerSample==0
        public int dwChannelMask; // which channels are present in stream
        public Guid subFormat;

        // Method to free native struct
        internal unsafe void __MarshalFree()
        {
            waveFormat.__MarshalFree();
        }
    }
    // Method to marshal from native to managed struct
    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        __MarshalFrom(ref @ref.waveFormat);
        _wValidBitsPerSample = @ref.wValidBitsPerSample;
        ChannelMask = @ref.dwChannelMask;
        GuidSubFormat = @ref.subFormat;
    }

    // Method to marshal from managed struct tot native
    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        __MarshalTo(ref @ref.waveFormat);
        @ref.wValidBitsPerSample = _wValidBitsPerSample;
        @ref.dwChannelMask = ChannelMask;
        @ref.subFormat = GuidSubFormat;
    }

    internal static __Native __NewNative()
    {
        unsafe
        {
            __Native temp = default(__Native);
            temp.waveFormat.extraSize = 22;
            return temp;
        }
    } 
    #endregion

    /// <summary>
    /// String representation
    /// </summary>
    public override string ToString()
    {
        return string.Format(
            CultureInfo.InvariantCulture, 
            "{0} wBitsPerSample:{1} ChannelMask:{2} SubFormat:{3} extraSize:{4}",
            base.ToString(),
            _wValidBitsPerSample,
            ChannelMask,
            GuidSubFormat,
            extraSize);
    }
}
