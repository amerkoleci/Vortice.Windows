// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Video;

public partial class VideoDecodeOutputStreamArguments1
{
    /// <summary>
    /// An <see cref="ID3D12Resource"/> representing the output texture.
    /// If decode conversion is enabled, this texture will contain the post-conversion output.
    /// If decode conversion is not enabled, this texture will contain the decode output.
    /// </summary>
    public ID3D12Resource? OutputTexture2D;
    /// <summary>
    /// The index of the output subresource of <see cref="OutputTexture2D"/> to use.
    /// This allows you to specify array indices if the output is an array.
    /// </summary>
    public int OutputSubresource;
    /// <summary>
    /// An optional <see cref="VideoDecodeConversionArguments1"/> structure containing output conversion parameters.
    /// </summary>
    /// <unmanaged>ConversionArguments</unmanaged>
    /// <unmanaged-short>ConversionArguments</unmanaged-short>
    public VideoDecodeConversionArguments1 ConversionArguments;

    /// <summary>
    /// An array of <see cref="VideoDecodeOutputHistogram"/> structures that are populated with histogram data.
    /// The maximum size of the array is 4.
    /// </summary>
    public VideoDecodeOutputHistogram[] Histograms
    {
        get => _histograms ??= new VideoDecodeOutputHistogram[4];
        private set => _histograms = value;
    }

    private VideoDecodeOutputHistogram[]? _histograms;

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    internal partial struct __Native
    {
        public IntPtr POutputTexture2D;
        public int OutputSubresource;
        public VideoDecodeConversionArguments1.__Native ConversionArguments;
        public VideoDecodeOutputHistogram.__Native Histograms0;
        public VideoDecodeOutputHistogram.__Native Histograms1;
        public VideoDecodeOutputHistogram.__Native Histograms2;
        public VideoDecodeOutputHistogram.__Native Histograms3;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        GC.KeepAlive(OutputTexture2D);
        ConversionArguments.__MarshalFree(ref @ref.ConversionArguments);
        Histograms[0].__MarshalFree(ref @ref.Histograms0);
        Histograms[1].__MarshalFree(ref @ref.Histograms1);
        Histograms[2].__MarshalFree(ref @ref.Histograms2);
        Histograms[3].__MarshalFree(ref @ref.Histograms3);
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.POutputTexture2D = MarshallingHelpers.ToCallbackPtr<ID3D12Resource>(OutputTexture2D);
        @ref.OutputSubresource = OutputSubresource;
        ConversionArguments.__MarshalTo(ref @ref.ConversionArguments);
        Histograms[0].__MarshalTo(ref @ref.Histograms0);
        Histograms[1].__MarshalTo(ref @ref.Histograms1);
        Histograms[2].__MarshalTo(ref @ref.Histograms2);
        Histograms[3].__MarshalTo(ref @ref.Histograms3);
    }
    #endregion
}
