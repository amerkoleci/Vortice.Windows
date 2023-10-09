// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class DXGI
{
    /// <summary>
    /// DXGI_DEBUG_ALL.
    /// </summary>
    public static readonly Guid DebugAll = new("e48ae283-da80-490b-87e6-43e9a9cfda08");

    /// <summary>
    /// DXGI_DEBUG_DX
    /// </summary>
    public static readonly Guid DebugDx = new("35cdd7fc-13b2-421d-a5d7-7e4451287d64");

    /// <summary>
    /// DXGI_DEBUG_DXGI
    /// </summary>
    public static readonly Guid DebugDxgi = new("25cddaa4-b1c6-47e1-ac3e-98875b5a2e2a");

    /// <summary>
    /// DXGI_DEBUG_APP
    /// </summary>
    public static readonly Guid DebugApp = new("06cd6e01-4219-4ebd-8709-27ed23360c62");

    /// <summary>
    /// Try to create new instance of <see cref="IDXGIFactory1"/>.
    /// </summary>
    /// <param name="factory">The <see cref="IDXGIFactory1"/> being created.</param>
    /// <returns>Return the <see cref="Result"/>.</returns>
    public static Result CreateDXGIFactory1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(out T? factory) where T : IDXGIFactory1
    {
        Result result = CreateDXGIFactory1(typeof(T).GUID, out IntPtr nativePtr);
        if (result.Success)
        {
            factory = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        factory = null;
        return result;
    }

    /// <summary>
    /// Try to create new instance of <see cref="IDXGIFactory1"/>.
    /// </summary>
    /// <returns>Return an instance of <see cref="IDXGIFactory1"/> or null if failed.</returns>
    public static T CreateDXGIFactory1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>() where T : IDXGIFactory1
    {
        CreateDXGIFactory1(typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    /// <summary>
    /// Try to create new instance of <see cref="IDXGIFactory2"/>.
    /// </summary>
    /// <param name="debug">Whether to enable debug callback.</param>
    /// <param name="factory">The <see cref="IDXGIFactory2"/> being created.</param>
    /// <returns>Return the <see cref="Result"/>.</returns>
    public static Result CreateDXGIFactory2<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(bool debug, out T? factory) where T : IDXGIFactory2
    {
        int flags = debug ? CreateFactoryDebug : 0x00;
        Result result = CreateDXGIFactory2(flags, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Success)
        {
            factory = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        factory = null;
        return result;
    }

    /// <summary>
    /// Try to create new instance of <see cref="IDXGIFactory2"/>.
    /// </summary>
    /// <param name="debug">Whether to enable debug callback.</param>
    /// <returns>Return an instance of <see cref="IDXGIFactory2"/> or null if failed.</returns>
    public static T CreateDXGIFactory2<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(bool debug) where T : IDXGIFactory2
    {
        int flags = debug ? CreateFactoryDebug : 0x00;
        CreateDXGIFactory2(flags, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    /// <summary>
    /// Gets debug interface for given type.
    /// </summary>
    /// <typeparam name="T">The <see cref="ComObject"/> to get.</typeparam>
    /// <param name="debugInterface">Instance of T.</param>
    /// <returns>The result code.</returns>
    public static Result DXGIGetDebugInterface1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(out T? debugInterface) where T : ComObject
    {
        try
        {
            Result result = DXGIGetDebugInterface1(0, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                debugInterface = null;
                return result;
            }

            debugInterface = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }
        catch
        {
            debugInterface = default;
            return ResultCode.NotFound;
        }
    }

    /// <summary>
    /// Gets debug interface for given type.
    /// </summary>
    /// <typeparam name="T">The <see cref="ComObject"/> to get.</typeparam>
    /// <returns>The <see cref="ComObject"/> to get.</returns>
    public static T DXGIGetDebugInterface1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>() where T : ComObject
    {
        DXGIGetDebugInterface1(0, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }
}
