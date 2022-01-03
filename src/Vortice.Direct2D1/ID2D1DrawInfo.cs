// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;

public partial class ID2D1DrawInfo
{
    #region SetVertexShaderConstantBuffer
    public unsafe void SetVertexShaderConstantBuffer(byte[] data)
    {
        fixed (void* dataPtr = &data[0])
        {
            SetVertexShaderConstantBuffer(new IntPtr(dataPtr), data.Length);
        }
    }

    public unsafe void SetVertexShaderConstantBuffer<T>(T[] constants) where T : unmanaged
    {
        fixed (void* pConstants = constants)
        {
            SetVertexShaderConstantBuffer(new IntPtr(pConstants), constants.Length * sizeof(T));
        }
    }

    public unsafe void SetVertexShaderConstantBuffer<T>(ReadOnlySpan<T> constants) where T : unmanaged
    {
        fixed (void* pConstants = constants)
        {
            SetVertexShaderConstantBuffer(new IntPtr(pConstants), constants.Length * sizeof(T));
        }
    }

    public unsafe void SetVertexShaderConstantBuffer<T>(ref T constants) where T : unmanaged
    {
        fixed (T* pConstants = &constants)
        {
            SetVertexShaderConstantBuffer(new IntPtr(pConstants), sizeof(T));
        }
    }
    #endregion SetVertexShaderConstantBuffer

    #region SetPixelShaderConstantBuffer
    public unsafe void SetPixelShaderConstantBuffer(byte[] data)
    {
        fixed (void* dataPtr = &data[0])
        {
            SetPixelShaderConstantBuffer(new IntPtr(dataPtr), data.Length);
        }
    }

    public unsafe void SetPixelShaderConstantBuffer<T>(T[] constants) where T : unmanaged
    {
        fixed (void* pConstants = constants)
        {
            SetPixelShaderConstantBuffer(new IntPtr(pConstants), constants.Length * sizeof(T));
        }
    }

    public unsafe void SetPixelShaderConstantBuffer<T>(ReadOnlySpan<T> constants) where T : unmanaged
    {
        fixed (void* pConstants = constants)
        {
            SetPixelShaderConstantBuffer(new IntPtr(pConstants), constants.Length * sizeof(T));
        }
    }

    public unsafe void SetPixelShaderConstantBuffer<T>(ref T constants) where T : unmanaged
    {
        fixed (T* pConstants = &constants)
        {
            SetPixelShaderConstantBuffer(new IntPtr(pConstants), sizeof(T));
        }
    }
    #endregion SetPixelShaderConstantBuffer
}
