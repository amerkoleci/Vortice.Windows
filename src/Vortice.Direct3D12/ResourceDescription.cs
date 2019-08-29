// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D12
{
    public partial struct ResourceDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceDescription"/> struct.
        /// </summary>
        /// <param name="dimension"></param>
        /// <param name="alignment"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depthOrArraySize"></param>
        /// <param name="mipLevels"></param>
        /// <param name="format"></param>
        /// <param name="sampleCount"></param>
        /// <param name="sampleQuality"></param>
        /// <param name="layout"></param>
        /// <param name="optionFlags"></param>
        public ResourceDescription(
            ResourceDimension dimension,
            long alignment,
            long width,
            int height,
            short depthOrArraySize,
            short mipLevels,
            Format format,
            int sampleCount,
            int sampleQuality,
            TextureLayout layout,
            ResourceFlags optionFlags)
        {
            Dimension = dimension;
            Alignment = alignment;
            Width = width;
            Height = height;
            DepthOrArraySize = depthOrArraySize;
            MipLevels = mipLevels;
            Format = format;
            SampleDescription = new SampleDescription(sampleCount, sampleQuality);
            Layout = layout;
            Flags = optionFlags;
        }

        public static ResourceDescription Buffer(ResourceAllocationInfo resourceAllocInfo, ResourceFlags flags = ResourceFlags.None)
        {
            return new ResourceDescription(
                ResourceDimension.Buffer, 
                resourceAllocInfo.Alignment, 
                resourceAllocInfo.SizeInBytes,
                1, 1, 1, Format.Unknown, 1, 0, TextureLayout.RowMajor, flags);
        }

        public static ResourceDescription Buffer(long width, ResourceFlags flags = ResourceFlags.None, long alignment = 0)
        {
            return new ResourceDescription(ResourceDimension.Buffer, alignment, width, 1, 1, 1, Format.Unknown, 1, 0, TextureLayout.RowMajor, flags);
        }

        public static ResourceDescription Texture1D(Format format,
            long width,
            short arraySize = 1,
            short mipLevels = 0,
            ResourceFlags flags = ResourceFlags.None,
            TextureLayout layout = TextureLayout.Unknown,
            long alignment = 0)
        {
            return new ResourceDescription(ResourceDimension.Texture1D, alignment, width, 1, arraySize, mipLevels, format, 1, 0, layout, flags);
        }

        public static ResourceDescription Texture2D(Format format,
            long width,
            int height,
            short arraySize = 1,
            short mipLevels = 0,
            int sampleCount = 1,
            int sampleQuality = 0,
            ResourceFlags flags = ResourceFlags.None,
            TextureLayout layout = TextureLayout.Unknown,
            long alignment = 0)
        {
            return new ResourceDescription(ResourceDimension.Texture2D, alignment, width, height, arraySize, mipLevels, format, sampleCount, sampleQuality, layout, flags);
        }

        public static ResourceDescription Texture3D(Format format,
            long width,
            int height,
            short depth,
            short mipLevels = 0,
            ResourceFlags flags = ResourceFlags.None,
            TextureLayout layout = TextureLayout.Unknown,
            long alignment = 0)
        {
            return new ResourceDescription(ResourceDimension.Texture3D, alignment, width, height, depth, mipLevels, format, 1, 0, layout, flags);
        }

        public int Depth => Dimension == ResourceDimension.Texture3D ? DepthOrArraySize : 1;
        public int ArraySize => Dimension != ResourceDimension.Texture3D ? DepthOrArraySize : 1;
    }
}
