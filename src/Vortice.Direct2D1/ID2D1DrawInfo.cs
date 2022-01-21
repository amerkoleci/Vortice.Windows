// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;

public unsafe partial class ID2D1DrawInfo
{
    #region SetVertexShaderConstantBuffer
    public void SetVertexShaderConstantBuffer(byte[] data)
    {
        fixed (byte* dataPtr = data)
        {
            SetVertexShaderConstantBuffer(dataPtr, data.Length);
        }
    }

    public void SetVertexShaderConstantBuffer<T>(T[] constants) where T : unmanaged
    {
        fixed (T* pConstants = constants)
        {
            SetVertexShaderConstantBuffer(pConstants, constants.Length * sizeof(T));
        }
    }

    public void SetVertexShaderConstantBuffer<T>(Span<T> constants) where T : unmanaged
    {
        fixed (T* pConstants = constants)
        {
            SetVertexShaderConstantBuffer(pConstants, constants.Length * sizeof(T));
        }
    }

    public void SetVertexShaderConstantBuffer<T>(in T constants) where T : unmanaged
    {
        fixed (T* pConstants = &constants)
        {
            SetVertexShaderConstantBuffer(pConstants, sizeof(T));
        }
    }
    #endregion SetVertexShaderConstantBuffer

    #region SetPixelShaderConstantBuffer
    public void SetPixelShaderConstantBuffer(byte[] data)
    {
        fixed (byte* dataPtr = data)
        {
            SetPixelShaderConstantBuffer(dataPtr, data.Length);
        }
    }

    public void SetPixelShaderConstantBuffer<T>(T[] constants) where T : unmanaged
    {
        fixed (T* pConstants = constants)
        {
            SetPixelShaderConstantBuffer(pConstants, constants.Length * sizeof(T));
        }
    }

    public void SetPixelShaderConstantBuffer<T>(Span<T> constants) where T : unmanaged
    {
        fixed (T* pConstants = constants)
        {
            SetPixelShaderConstantBuffer(pConstants, constants.Length * sizeof(T));
        }
    }

    public void SetPixelShaderConstantBuffer<T>(in T constants) where T : unmanaged
    {
        fixed (T* pConstants = &constants)
        {
            SetPixelShaderConstantBuffer(pConstants, sizeof(T));
        }
    }
    #endregion SetPixelShaderConstantBuffer
}
