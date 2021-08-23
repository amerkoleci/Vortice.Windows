// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D11.Shader
{
    public partial struct ShaderDescription
    {
        public ShaderVersionType Type => (ShaderVersionType)(((Version) >> 16) & 0xffff);
        public int Major => ((Version) >> 4) & 0xf;
        public int Minor => ((Version) >> 0) & 0xf;
    }
}
