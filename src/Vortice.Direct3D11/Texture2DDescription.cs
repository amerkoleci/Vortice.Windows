// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.DXGI;

namespace Vortice.Direct3D11
{
    /// <summary>
    /// Describes a 2D texture.
    /// </summary>
    public partial struct Texture2DDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Texture2DDescription"/> struct.
        /// </summary>
        /// <param name="format">Texture format.</param>
        /// <param name="width">Texture width (in texels).</param>
        /// <param name="height">Texture height (in texels).</param>
        /// <param name="arraySize">Number of textures in the array.</param>
        /// <param name="mipLevels">The maximum number of mipmap levels in the texture.</param>
        /// <param name="bindFlags">The <see cref="Vortice.Direct3D11.BindFlags"/> for binding to pipeline stages.</param>
        /// <param name="usage">Value that identifies how the texture is to be read from and written to.</param>
        /// <param name="cpuAccessFlags">The <see cref="Direct3D11.CpuAccessFlags"/> to specify the types of CPU access allowed.</param>
        /// <param name="sampleCount">Specifies multisampling parameters for the texture.</param>
        /// <param name="sampleQuality">Specifies multisampling parameters for the texture.</param>
        /// <param name="optionFlags">The <see cref="ResourceOptionFlags"/> that identify other, less common resource options. </param>
        public Texture2DDescription(
            Format format, 
            int width,
            int height,
            int arraySize = 1,
            int mipLevels = 0, 
            BindFlags bindFlags = BindFlags.ShaderResource,
            Usage usage = Usage.Default, 
            CpuAccessFlags cpuAccessFlags = CpuAccessFlags.None, 
            int sampleCount = 1,
            int sampleQuality = 0,
            ResourceOptionFlags optionFlags = ResourceOptionFlags.None)
        {
            if (format == Format.Unknown)
                throw new ArgumentException($"format need to be valid", nameof(format));

            if (width < 1 || width > ID3D11Resource.MaximumTexture2DSize)
                throw new ArgumentException($"Width need to be in range 1-{ID3D11Resource.MaximumTexture2DSize}", nameof(width));

            if (height < 1 || height > ID3D11Resource.MaximumTexture2DSize)
                throw new ArgumentException($"Height need to be in range 1-{ID3D11Resource.MaximumTexture2DSize}", nameof(height));

            if (arraySize < 1 || arraySize > ID3D11Resource.MaximumTexture2DArraySize)
                throw new ArgumentException($"Array size need to be in range 1-{ID3D11Resource.MaximumTexture2DArraySize}", nameof(arraySize));

            Width = width;
            Height = height;
            MipLevels = mipLevels;
            ArraySize = arraySize;
            Format = format;
            SampleDescription = new SampleDescription(sampleCount, sampleQuality);
            Usage = usage;
            BindFlags = bindFlags;
            CpuAccessFlags = cpuAccessFlags;
            OptionFlags = optionFlags;
        }
    }
}
