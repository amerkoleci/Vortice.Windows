// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;
using Vortice.DXGI;

namespace Vortice.Direct3D11;

public static unsafe partial class D3D11
{
    public static ID3D11Device D3D11CreateDevice(DriverType driverType, DeviceCreationFlags flags, params FeatureLevel[] featureLevels)
    {
        RawD3D11CreateDeviceNoContext(
            IntPtr.Zero,
            driverType,
            flags,
            featureLevels,
            out ID3D11Device? device,
            out _).CheckError();
        return device!;
    }

    public static Result D3D11CreateDevice(
        IDXGIAdapter? adapter,
        DriverType driverType,
        DeviceCreationFlags flags,
        FeatureLevel[]? featureLevels,
        out ID3D11Device? device)
    {
        return RawD3D11CreateDeviceNoContext(
            adapter != null ? adapter.NativePointer : IntPtr.Zero,
            driverType,
            flags,
            featureLevels,
            out device,
            out _);
    }

    public static Result D3D11CreateDevice(
        IDXGIAdapter? adapter,
        DriverType driverType,
        DeviceCreationFlags flags,
        FeatureLevel[]? featureLevels,
        out ID3D11Device? device, out FeatureLevel featureLevel)
    {
        return RawD3D11CreateDeviceNoContext(
            adapter != null ? adapter.NativePointer : IntPtr.Zero,
            driverType,
            flags,
            featureLevels,
            out device,
            out featureLevel);
    }

    public static Result D3D11CreateDevice(IDXGIAdapter? adapter, DriverType driverType, DeviceCreationFlags flags, FeatureLevel[] featureLevels,
        out ID3D11Device device, out ID3D11DeviceContext immediateContext)
    {
        return D3D11CreateDevice(adapter, driverType, flags, featureLevels, out device, out _, out immediateContext);
    }

    public static Result D3D11CreateDevice(IDXGIAdapter? adapter, DriverType driverType, DeviceCreationFlags flags, FeatureLevel[] featureLevels,
        out ID3D11Device device, out FeatureLevel featureLevel, out ID3D11DeviceContext immediateContext)
    {
        Result result = D3D11CreateDevice(
            adapter != null ? adapter.NativePointer : IntPtr.Zero,
            driverType,
            IntPtr.Zero,
            (uint)flags,
            featureLevels,
            (featureLevels != null) ? (uint)featureLevels.Length : 0u,
            SdkVersion,
            out device,
            out featureLevel,
            out immediateContext);

        if (result.Failure)
        {
            return result;
        }

        return result;
    }

    public static Result D3D11CreateDevice(
        nint adapterPtr,
        DriverType driverType,
        DeviceCreationFlags flags,
        FeatureLevel[]? featureLevels,
        out ID3D11Device? device)
    {
        return RawD3D11CreateDeviceNoContext(
            adapterPtr,
            driverType,
            flags,
            featureLevels,
            out device,
            out _);
    }

    public static Result D3D11CreateDevice(nint adapterPtr,
        DriverType driverType,
        DeviceCreationFlags flags,
        FeatureLevel featureLevel,
        out ID3D11Device? device, out ID3D11DeviceContext? immediateContext)
    {
        IntPtr devicePtr = IntPtr.Zero;
        IntPtr immediateContextPtr = IntPtr.Zero;
        Result result = D3D11CreateDevice_(
            (void*)adapterPtr,
            (int)driverType,
            null,
            (uint)flags,
            &featureLevel,
            1,
            SdkVersion,
            &devicePtr,
            null,
            &immediateContextPtr);

        if (result.Success)
        {
            device = new ID3D11Device(devicePtr);
            immediateContext = new ID3D11DeviceContext(immediateContextPtr);
            return result;
        }

        device = default;
        immediateContext = default;
        return result;
    }

    public static Result D3D11CreateDevice(nint adapterPtr, DriverType driverType, DeviceCreationFlags flags, FeatureLevel[] featureLevels,
        out ID3D11Device device, out ID3D11DeviceContext immediateContext)
    {
        return D3D11CreateDevice(adapterPtr, driverType, flags, featureLevels, out device, out _, out immediateContext);
    }

    public static Result D3D11CreateDevice(IntPtr adapterPtr, DriverType driverType, DeviceCreationFlags flags, FeatureLevel[] featureLevels,
        out ID3D11Device device, out FeatureLevel featureLevel, out ID3D11DeviceContext immediateContext)
    {
        Result result = D3D11CreateDevice(adapterPtr, driverType, IntPtr.Zero,
            (uint)flags,
            featureLevels,
            (featureLevels != null) ? (uint)featureLevels.Length : 0u,
            (uint)SdkVersion,
            out device,
            out featureLevel,
            out immediateContext);

        if (result.Failure)
        {
            return result;
        }

        return result;
    }

    /// <summary>
    /// Check if a feature level is supported by a primary adapter.
    /// </summary>
    /// <param name="featureLevel">The feature level.</param>
    /// <param name="flags">The <see cref="DeviceCreationFlags"/> flags.</param>
    /// <returns><c>true</c> if the primary adapter is supporting this feature level; otherwise, <c>false</c>.</returns>
    public static bool IsSupportedFeatureLevel(FeatureLevel featureLevel, DeviceCreationFlags flags = DeviceCreationFlags.None)
    {
        Result result = RawD3D11CreateDeviceNoDeviceAndContext(
            IntPtr.Zero,
            DriverType.Hardware,
            flags,
            [featureLevel],
            out FeatureLevel outputLevel);
        return result.Success && outputLevel == featureLevel;
    }

    /// <summary>
    /// Check if a feature level is supported by a particular adapter.
    /// </summary>
    /// <param name="adapter">The adapter.</param>
    /// <param name="featureLevel">The feature level.</param>
    /// <param name="flags">The <see cref="DeviceCreationFlags"/> flags.</param>
    /// <returns><c>true</c> if the specified adapter is supporting this feature level; otherwise, <c>false</c>.</returns>
    public static unsafe bool IsSupportedFeatureLevel(
        IDXGIAdapter adapter,
        FeatureLevel featureLevel,
        DeviceCreationFlags flags = DeviceCreationFlags.None)
    {
        if (adapter == null)
            throw new ArgumentNullException(nameof(adapter), "Invalid adapter");

        Result result = RawD3D11CreateDeviceNoDeviceAndContext(
            adapter.NativePointer,
            DriverType.Unknown,
            flags,
            [featureLevel],
            out FeatureLevel outputLevel);
        return result.Success && outputLevel == featureLevel;
    }

    /// <summary>
    /// Check if a feature level is supported by a particular adapter.
    /// </summary>
    /// <param name="adapterPtr">The native handle of <see cref="IDXGIAdapter"/>.</param>
    /// <param name="featureLevel">The feature level.</param>
    /// <param name="flags">The <see cref="DeviceCreationFlags"/> flags.</param>
    /// <returns><c>true</c> if the specified adapter is supporting this feature level; otherwise, <c>false</c>.</returns>
    public static unsafe bool IsSupportedFeatureLevel(
        IntPtr adapterPtr,
        FeatureLevel featureLevel,
        DeviceCreationFlags flags = DeviceCreationFlags.None)
    {
        if (adapterPtr == IntPtr.Zero)
            throw new ArgumentNullException(nameof(adapterPtr), "Invalid adapter handle");

        Result result = RawD3D11CreateDeviceNoDeviceAndContext(
            adapterPtr,
            DriverType.Unknown,
            flags,
            [featureLevel],
            out FeatureLevel outputLevel);
        return result.Success && outputLevel == featureLevel;
    }

    /// <summary>
    /// Gets the highest supported hardware feature level of the primary adapter.
    /// </summary>
    /// <returns>The highest supported hardware feature level.</returns>
    public static FeatureLevel GetSupportedFeatureLevel()
    {
        RawD3D11CreateDeviceNoDeviceAndContext(
            IntPtr.Zero,
            DriverType.Hardware,
            DeviceCreationFlags.None,
            null,
            out FeatureLevel featureLevel);
        return featureLevel;
    }

    /// <summary>
    /// Gets the highest supported hardware feature level of the primary adapter.
    /// </summary>
    /// <param name="adapter">The <see cref="IDXGIAdapter"/>.</param>
    /// <returns>The highest supported hardware feature level.</returns>
    public static FeatureLevel GetSupportedFeatureLevel(IDXGIAdapter adapter)
    {
        if (adapter == null)
            throw new ArgumentNullException(nameof(adapter), "Invalid adapter");

        RawD3D11CreateDeviceNoDeviceAndContext(
            adapter.NativePointer,
            DriverType.Unknown,
            DeviceCreationFlags.None,
            null,
            out FeatureLevel featureLevel);
        return featureLevel;
    }

    /// <summary>
    /// Gets the highest supported hardware feature level of the primary adapter.
    /// </summary>
    /// <param name="adapterPtr">The native handle of <see cref="IDXGIAdapter"/>.</param>
    /// <returns>The highest supported hardware feature level.</returns>
    public static FeatureLevel GetSupportedFeatureLevel(IntPtr adapterPtr)
    {
        if (adapterPtr == IntPtr.Zero)
            throw new ArgumentNullException(nameof(adapterPtr), "Invalid adapter handle");

        RawD3D11CreateDeviceNoDeviceAndContext(
            adapterPtr,
            DriverType.Unknown,
            DeviceCreationFlags.None,
            null,
            out FeatureLevel featureLevel);
        return featureLevel;
    }

    /// <summary>
    /// Check for SDK Layer support.
    /// </summary>
    /// <returns>True if available, false otherwise.</returns>
    public static bool SdkLayersAvailable()
    {
        Result result = D3D11CreateDevice_(
            null, (int)DriverType.Null,
            null, (int)DeviceCreationFlags.Debug,
            null, 0,
            SdkVersion,
            null, null, null);
        return result.Success;
    }

    public static Result D3D11CreateDeviceAndSwapChain(
        IDXGIAdapter? adapter,
        DriverType driverType,
        DeviceCreationFlags flags,
        FeatureLevel[] featureLevels,
        SwapChainDescription? swapChainDesc,
        out IDXGISwapChain? swapChain,
        out ID3D11Device? device,
        out FeatureLevel? featureLevel,
        out ID3D11DeviceContext? immediateContext)
    {
        IntPtr adapterPtr = adapter != null ? adapter.NativePointer : IntPtr.Zero;

        fixed (void* featureLevelPtr = &featureLevel)
        fixed (FeatureLevel* featureLevelsPtr = featureLevels)
        {
            SwapChainDescription swapChainDescIn;

            if (swapChainDesc != null)
                swapChainDescIn = swapChainDesc.Value;

            IntPtr swapChainPtr = IntPtr.Zero;
            IntPtr devicePtr = IntPtr.Zero;
            IntPtr immediateContextPtr = IntPtr.Zero;

            Result result = D3D11CreateDeviceAndSwapChain_(
                (void*)adapterPtr,
                (int)driverType,
                null,
                (uint)flags,
                featureLevelsPtr,
                featureLevels != null ? (uint)featureLevels.Length : 0u,
                (uint)SdkVersion,
                swapChainDesc == null ? (void*)0 : &swapChainDescIn,
                &swapChainPtr,
                &devicePtr,
                featureLevelPtr,
                &immediateContextPtr);

            swapChain = swapChainPtr != IntPtr.Zero ? new IDXGISwapChain(swapChainPtr) : null;
            device = devicePtr != IntPtr.Zero ? new ID3D11Device(devicePtr) : null;
            immediateContext = immediateContextPtr != IntPtr.Zero ? new ID3D11DeviceContext(immediateContextPtr) : null;

            return result;
        }
    }


    /// <summary>
    /// Calculates the sub resource index from a miplevel.
    /// </summary>
    /// <param name="mipSlice">A zero-based index for the mipmap level to address; 0 indicates the first, most detailed mipmap level.</param>
    /// <param name="arraySlice">The zero-based index for the array level to address; always use 0 for volume (3D) textures.</param>
    /// <param name="mipLevels">Number of mipmap levels in the resource.</param>
    /// <returns>
    /// The index which equals mipSlice + (arraySlice * mipLevels).
    /// </returns>
    public static uint CalculateSubResourceIndex(uint mipSlice, uint arraySlice, uint mipLevels)
    {
        return (mipLevels * arraySlice) + mipSlice;
    }

    /// <summary>
    /// Calculates the resulting size at a single level for an original size.
    /// </summary>
    /// <param name="mipLevel">The mip level to get the size.</param>
    /// <param name="baseSize">Size of the base.</param>
    /// <returns>
    /// Size of the mipLevel
    /// </returns>
    public static uint CalculateMipSize(uint mipLevel, uint baseSize)
    {
        baseSize >>= (int)mipLevel;
        return baseSize > 0 ? baseSize : 1;
    }

    #region RawD3D11CreateDevice Methods
    private static Result RawD3D11CreateDeviceNoContext(
        IntPtr adapterPtr,
        DriverType driverType,
        DeviceCreationFlags flags,
        FeatureLevel[]? featureLevels,
        out ID3D11Device? device,
        out FeatureLevel featureLevel)
    {
        device = default;
        fixed (FeatureLevel* featureLevelsPtr = featureLevels)
        fixed (void* featureLevelPtr = &featureLevel)
        {
            IntPtr devicePtr = IntPtr.Zero;
            Result result = D3D11CreateDevice_(
                (void*)adapterPtr,
                (int)driverType,
                null,
                (uint)flags,
                featureLevelsPtr,
                (featureLevels != null) ? (uint)featureLevels.Length : 0u,
                SdkVersion,
                &devicePtr,
                featureLevelPtr,
                null);

            if (result.Success && devicePtr != IntPtr.Zero)
            {
                device = new ID3D11Device(devicePtr);
            }
            return result;
        }
    }

    private static unsafe Result RawD3D11CreateDeviceNoDeviceAndContext(
        IntPtr adapterPtr,
        DriverType driverType,
        DeviceCreationFlags flags,
        FeatureLevel[]? featureLevels,
        out FeatureLevel featureLevel)
    {
        if (featureLevels != null && featureLevels.Length > 0)
        {
            fixed (FeatureLevel* featureLevelsPtr = &featureLevels[0])
            fixed (void* featureLevelPtr = &featureLevel)
            {
                Result result = D3D11CreateDevice_(
                    (void*)adapterPtr,
                    (int)driverType,
                    null,
                    (uint)flags,
                    featureLevelsPtr,
                    (uint)featureLevels.Length,
                    (uint)SdkVersion,
                    null,
                    featureLevelPtr,
                    null);
                return result;
            }
        }
        else
        {
            fixed (void* featureLevelPtr = &featureLevel)
            {
                Result result = D3D11CreateDevice_(
                    (void*)adapterPtr,
                    (int)driverType,
                    null,
                    (uint)flags,
                    null,
                    0,
                    SdkVersion,
                    null,
                    featureLevelPtr,
                    null);
                return result;
            }
        }
    }
    #endregion
}
