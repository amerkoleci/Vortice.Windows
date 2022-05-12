// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class ID3D12GraphicsCommandList2
{
    public void WriteBufferImmediate(WriteBufferImmediateParameter[] @params, WriteBufferImmediateMode[]? modes = null)
    {
        if (modes != null && @params.Length != modes.Length)
        {
            throw new ArgumentException($"If {nameof(modes)} is not null, it must have the same length as {nameof(@params)}", nameof(modes));
        }

        WriteBufferImmediate_(@params.Length, @params, modes);
    }
}
