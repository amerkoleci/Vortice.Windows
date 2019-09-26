// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct3D12
{
    public partial class ID3D12GraphicsCommandList2
    {
        public void WriteBufferImmediate(WriteBufferImmediateParameter[] @params, WriteBufferImmediateMode[] modes)
        {
            if (@params.Length != modes.Length)
            {
                throw new InvalidOperationException($"params and modes need to have same length");
            }

            WriteBufferImmediate_(@params.Length, @params, modes);
        }
    }
}
