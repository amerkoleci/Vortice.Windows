// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct2D1;
using Vortice.DXGI;

namespace Vortice.DirectComposition;

public static partial class DComp
{
    /// <summary>
    /// Creates a new device object that can be used to create other Microsoft DirectComposition objects.
    /// </summary>
    /// <typeparam name="T">A generic type of <see cref="IDCompositionDevice"/>.</typeparam>
    /// <param name="dxgiDevice">The DXGI device <see cref="IDXGIDevice"/> to use to create DirectComposition surface objects.</param>
    /// <returns>An instance of <see cref="IDCompositionDevice"/> interface .</returns>
    public static T DCompositionCreateDevice<T>(IDXGIDevice dxgiDevice) where T : IDCompositionDevice
    {
        DCompositionCreateDevice(dxgiDevice, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    /// <summary>
    /// Creates a new device object that can be used to create other Microsoft DirectComposition objects.
    /// </summary>
    /// <typeparam name="T">A generic type of <see cref="IDCompositionDevice"/>.</typeparam>
    /// <param name="dxgiDevice">The DXGI device <see cref="IDXGIDevice"/> to use to create DirectComposition surface objects.</param>
    /// <param name="compositionDevice">An instance of <see cref="IDCompositionDevice"/> interface or null if failure</param>
    /// <returns>The <see cref="Result"/> code.</returns>
    public static Result DCompositionCreateDevice<T>(IDXGIDevice dxgiDevice, out T? compositionDevice) where T : IDCompositionDevice
    {
        Result result = DCompositionCreateDevice(dxgiDevice, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            compositionDevice = default;
            return result;
        }

        compositionDevice = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    /// <summary>
    /// Creates a new device object that can be used to create other Microsoft DirectComposition objects.
    /// </summary>
    /// <typeparam name="T">A generic type of <see cref="IDCompositionDevice"/>.</typeparam>
    /// <param name="renderingDevice">An optional instance to a DirectX device to be used to create DirectComposition surface objects. Must be a instance of an object implementing the <see cref="IDXGIDevice"/> or <see cref="ID2D1Device"/> interfaces.</param>
    /// <returns>An instance of <see cref="IDCompositionDevice"/> interface.</returns>
    public static T DCompositionCreateDevice2<T>(IUnknown renderingDevice) where T : IDCompositionDevice
    {
        DCompositionCreateDevice2(renderingDevice, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    /// <summary>
    /// Creates a new device object that can be used to create other Microsoft DirectComposition objects.
    /// </summary>
    /// <typeparam name="T">A generic type of <see cref="IDCompositionDevice"/>.</typeparam>
    /// <param name="renderingDevice">An optional instance to a DirectX device to be used to create DirectComposition surface objects. Must be a instance of an object implementing the <see cref="IDXGIDevice"/> or <see cref="ID2D1Device"/> interfaces.</param>
    /// <param name="compositionDevice">An instance of <see cref="IDCompositionDevice"/> interface or null if failure</param>
    /// <returns>The <see cref="Result"/> code.</returns>
    public static Result DCompositionCreateDevice2<T>(IUnknown renderingDevice, out T? compositionDevice) where T : IDCompositionDevice
    {
        Result result = DCompositionCreateDevice2(renderingDevice, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            compositionDevice = default;
            return result;
        }

        compositionDevice = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    /// <summary>
    /// Creates a new DirectComposition device object, which can be used to create other DirectComposition objects.
    /// </summary>
    /// <typeparam name="T">A generic type of <see cref="IDCompositionDevice"/>.</typeparam>
    /// <param name="renderingDevice">An optional instance to a DirectX device to be used to create DirectComposition surface objects. Must be a instance of an object implementing the <see cref="IDXGIDevice"/> or <see cref="ID2D1Device"/> interfaces.</param>
    /// <returns>An instance of <see cref="IDCompositionDevice"/> interface.</returns>
    public static T DCompositionCreateDevice3<T>(IUnknown renderingDevice) where T : IDCompositionDevice
    {
        DCompositionCreateDevice3(renderingDevice, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    /// <summary>
    /// Creates a new DirectComposition device object, which can be used to create other DirectComposition objects.
    /// </summary>
    /// <typeparam name="T">A generic type of <see cref="IDCompositionDevice"/>.</typeparam>
    /// <param name="renderingDevice">An optional instance to a DirectX device to be used to create DirectComposition surface objects. Must be a instance of an object implementing the <see cref="IDXGIDevice"/> or <see cref="ID2D1Device"/> interfaces.</param>
    /// <param name="compositionDevice">An instance of <see cref="IDCompositionDevice"/> interface or null if failure</param>
    /// <returns>The <see cref="Result"/> code.</returns>
    public static Result DCompositionCreateDevice3<T>(IUnknown renderingDevice, out T? compositionDevice) where T : IDCompositionDevice
    {
        Result result = DCompositionCreateDevice3(renderingDevice, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            compositionDevice = default;
            return result;
        }

        compositionDevice = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
