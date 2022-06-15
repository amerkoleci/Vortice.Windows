// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Multimedia;

/// <summary>
/// WaveFormatAdpcm
/// http://msdn.microsoft.com/en-us/library/microsoft.directx_sdk.xaudio2.adpcmwaveformat%28v=vs.85%29.aspx
/// Additional documentation: http://icculus.org/SDL_sound/downloads/external_documentation/wavecomp.htm
/// </summary>
public class WaveFormatAdpcm : WaveFormat
{
    /// <summary>
    /// Parameterless constructor for marshalling
    /// </summary>
    internal WaveFormatAdpcm()
    {
    }

    /// <summary>
    /// Creates a new WaveFormatAdpcm for MicrosoftADPCM
    /// </summary>
    /// <param name="rate">The rate.</param>
    /// <param name="channels">The channels.</param>
    /// <param name="blockAlign">The block align. If 0, then 256 for [0, 11KHz], 512 for ]11KHz, 22Khz], 1024 for ]22Khz, +inf]</param>
    public WaveFormatAdpcm(int rate, int channels, int blockAlign = 0) : base(rate, 4, channels)
    {
        if (blockAlign == 0)
        {
            if (rate <= 11025)
                blockAlign = 256;
            else if (rate <= 22050)
                blockAlign = 512;
            else
                blockAlign = 1024;
        }

        if (rate <= 0)
            throw new ArgumentOutOfRangeException("rate", "Must be > 0");
        if (channels <= 0)
            throw new ArgumentOutOfRangeException("channels", "Must be > 0");
        if (blockAlign <= 0)
            throw new ArgumentOutOfRangeException("blockAlign", "Must be > 0");
        if (blockAlign > short.MaxValue)
            throw new ArgumentOutOfRangeException("blockAlign", "Must be < 32767");

        waveFormatTag = WaveFormatEncoding.Adpcm;
        this.blockAlign = (short)blockAlign;

        SamplesPerBlock = (ushort)(blockAlign * 2 / channels - 12);
        averageBytesPerSecond = (SampleRate * blockAlign) / SamplesPerBlock;

        // Default Microsoft ADPCM coefficients
        Coefficients1 = new short[] { 256, 512, 0, 192, 240, 460, 392 };
        Coefficients2 = new short[] { 0, -256, 0, 64, 0, -208, -232 };
        extraSize = 32;
    }

    /// <summary>
    /// Gets or sets the samples per block.
    /// </summary>
    /// <value>
    /// The samples per block.
    /// </value>
    public ushort SamplesPerBlock { get; private set; }

    /// <summary>
    /// Gets or sets the coefficients.
    /// </summary>
    /// <value>
    /// The coefficients.
    /// </value>
    public short[]? Coefficients1 { get; set; }

    /// <summary>
    /// Gets or sets the coefficients.
    /// </summary>
    /// <value>
    /// The coefficients.
    /// </value>
    public short[]? Coefficients2 { get; set; }

    #region Marshal
    protected unsafe override IntPtr MarshalToPtr()
    {
        var result = Marshal.AllocHGlobal(Unsafe.SizeOf<__Native>() + sizeof(int) + sizeof(int) * Coefficients1.Length);
        __MarshalTo(ref *(__Native*)result);
        return result;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal unsafe new struct __Native
    {
        public WaveFormat.__Native waveFormat;
        public ushort samplesPerBlock;
        public ushort numberOfCoefficients;
        public short coefficients;

        // Method to free native struct
        internal unsafe void __MarshalFree()
        {
            waveFormat.__MarshalFree();
        }
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        __MarshalFrom(ref @ref.waveFormat);
        SamplesPerBlock = @ref.samplesPerBlock;
        Coefficients1 = new short[@ref.numberOfCoefficients];
        Coefficients2 = new short[@ref.numberOfCoefficients];
        if (@ref.numberOfCoefficients > 7)
        {
            throw new InvalidOperationException("Unable to read Adpcm format. Too may coefficients (max 7)");
        }

        fixed (short* pCoefs = &@ref.coefficients)
        {
            for (int i = 0; i < @ref.numberOfCoefficients; i++)
            {
                Coefficients1[i] = pCoefs[i * 2];
                Coefficients2[i] = pCoefs[i * 2 + 1];
            }
        }

        extraSize = (short)(sizeof(int) + sizeof(int) * @ref.numberOfCoefficients);
    }

    private unsafe void __MarshalTo(ref __Native @ref)
    {
        if (Coefficients1.Length > 7)
            throw new InvalidOperationException("Unable to encode Adpcm format. Too may coefficients (max 7)");

        extraSize = (short)(sizeof(int) + sizeof(int) * Coefficients1.Length);
        __MarshalTo(ref @ref.waveFormat);
        @ref.samplesPerBlock = SamplesPerBlock;
        @ref.numberOfCoefficients = (ushort)Coefficients1.Length;
        fixed (short* pCoefs = &@ref.coefficients)
        {
            for (int i = 0; i < @ref.numberOfCoefficients; i++)
            {
                pCoefs[i * 2] = Coefficients1[i];
                pCoefs[i * 2 + 1] = Coefficients2[i];
            }
        }
    } 
    #endregion
}
