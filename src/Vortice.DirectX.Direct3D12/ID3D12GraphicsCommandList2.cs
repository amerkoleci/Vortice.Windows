// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using SharpGen.Runtime;
using Vortice.DirectX.Direct3D;
using Vortice.DirectX.DXGI;

namespace Vortice.DirectX.Direct3D12
{
    public partial class ID3D12GraphicsCommandList2
    {
        public void WriteBufferImmediate(WriteBufferImmediateParameter[] @params, WriteBufferImmediateMode[] modes)
        {
            Guard.NotNullOrEmpty(@params, nameof(@params));
            Guard.NotNullOrEmpty(modes, nameof(modes));

            if (@params.Length != modes.Length)
            {
                throw new InvalidOperationException($"params and modes need to have same length");
            }

            WriteBufferImmediate_(@params.Length, @params, modes);
        }
    }
}
