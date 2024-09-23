// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D12;

namespace Vortice.DirectML;

public static partial class DML
{
    public static event DllImportResolver? ResolveLibrary;

    static DML()
    {
        ResolveLibrary += static (libraryName, assembly, searchPath) =>
        {
            if (libraryName is not "directml.dll")
            {
                return IntPtr.Zero;
            }

            string rid = RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.X86 => "win-x86",
                Architecture.X64 => "win-x64",
                Architecture.Arm => "win-arm",
                Architecture.Arm64 => "win-arm64",
                _ => throw new NotSupportedException("Invalid process architecture")
            };

            // Test whether the native libraries are present in the same folder of the executable
            // (which is the case when the program was built with a runtime identifier), or whether
            // they are in the "runtimes\win-x64\native" folder in the executable directory.
            string nugetNativeLibsPath = Path.Combine(AppContext.BaseDirectory, $@"runtimes\{rid}\native");
            bool isNuGetRuntimeLibrariesDirectoryPresent = Directory.Exists(nugetNativeLibsPath);

            if (isNuGetRuntimeLibrariesDirectoryPresent)
            {
                string directMLPath = Path.Combine(AppContext.BaseDirectory, $@"runtimes\{rid}\native\DirectML.dll");
                string directMLDebugPath = Path.Combine(AppContext.BaseDirectory, $@"runtimes\{rid}\native\DirectML.Debug.dll");

                // Load DXIL first so that DXC doesn't fail to load it, and then DXIL, both from the NuGet path
                if (NativeLibrary.TryLoad(directMLDebugPath, out _) &&
                    NativeLibrary.TryLoad(directMLPath, out IntPtr handle))
                {
                    return handle;
                }
            }
            else
            {
                // Even when the two libraries are correctly copied next to the executable in use, we load them
                // manually to ensure the operation is successful. This is to avoid failures in cases such as when
                // doing "dotnet bin\MyApp.dll", ie. when the host is in another path than the executable in use.
                // This is probably because DXIL is a native dependency for DXC, but the way Windows loads these
                // libraries doesn't take into account the .NET concepts of "app directory": neither the current "bin"
                // directory nor the "process directory", which is "C:\Program Files\dotnet", actually contain the
                // native library we need, hence the runtime crash. Manually loading the library this way solves this.
                if (NativeLibrary.TryLoad("DirectML.Debug", assembly, searchPath, out _) && NativeLibrary.TryLoad("DirectML", assembly, searchPath, out IntPtr handle))
                {
                    return handle;
                }
            }

            return IntPtr.Zero;
        };

        NativeLibrary.SetDllImportResolver(System.Reflection.Assembly.GetExecutingAssembly(), OnDllImport);
    }

    /// <summary>The default <see cref="DllImportResolver"/> for TerraFX.Interop.Windows.</summary>
    /// <inheritdoc cref="DllImportResolver"/>
    private static IntPtr OnDllImport(string libraryName, System.Reflection.Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (TryResolveLibrary(libraryName, assembly, searchPath, out IntPtr nativeLibrary))
        {
            return nativeLibrary;
        }

        return NativeLibrary.Load(libraryName, assembly, searchPath);
    }

    /// <summary>Tries to resolve a native library using the handlers for the <see cref="ResolveLibrary"/> event.</summary>
    /// <param name="libraryName">The native library to resolve.</param>
    /// <param name="assembly">The assembly requesting the resolution.</param>
    /// <param name="searchPath">The <see cref="DllImportSearchPath"/> value on the P/Invoke or assembly, or <see langword="null"/>.</param>
    /// <param name="nativeLibrary">The loaded library, if one was resolved.</param>
    /// <returns>Whether or not the requested library was successfully loaded.</returns>
    private static bool TryResolveLibrary(string libraryName, System.Reflection.Assembly assembly, DllImportSearchPath? searchPath, out IntPtr nativeLibrary)
    {
        var resolveLibrary = ResolveLibrary;

        if (resolveLibrary != null)
        {
            var resolvers = resolveLibrary.GetInvocationList();

            foreach (DllImportResolver resolver in resolvers)
            {
                nativeLibrary = resolver(libraryName, assembly, searchPath);

                if (nativeLibrary != IntPtr.Zero)
                {
                    return true;
                }
            }
        }

        nativeLibrary = IntPtr.Zero;
        return false;
    }

    public static IDMLDevice DMLCreateDevice(ID3D12Device d3d12Device, CreateDeviceFlags createDeviceFlags)
    {
        DMLCreateDevice(d3d12Device, createDeviceFlags, typeof(IDMLDevice).GUID, out IntPtr nativePtr).CheckError();
        return new(nativePtr);
    }

    public static T DMLCreateDevice<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ID3D12Device d3d12Device, CreateDeviceFlags createDeviceFlags)
        where T : IDMLDevice
    {
        DMLCreateDevice(d3d12Device, createDeviceFlags, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr) ?? throw new NullReferenceException();
    }

    public static Result DMLCreateDevice<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ID3D12Device d3d12Device, CreateDeviceFlags createDeviceFlags, out T? device)
        where T : IDMLDevice
    {
        Result result = DMLCreateDevice(
            d3d12Device,
            createDeviceFlags,
            typeof(T).GUID,
            out IntPtr nativePtr);

        if (result.Failure)
        {
            device = default;
            return result;
        }

        device = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public static T DMLCreateDevice<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ID3D12Device d3d12Device, CreateDeviceFlags createDeviceFlags, FeatureLevel minimumFeatureLevel)
        where T : IDMLDevice
    {
        DMLCreateDevice1(
            d3d12Device,
            createDeviceFlags,
            minimumFeatureLevel,
            typeof(T).GUID,
            out IntPtr nativePtr).CheckError();

        return MarshallingHelpers.FromPointer<T>(nativePtr) ?? throw new NullReferenceException();
    }

    public static Result DMLCreateDevice<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ID3D12Device d3d12Device, CreateDeviceFlags createDeviceFlags, FeatureLevel minimumFeatureLevel, out T? device)
        where T : IDMLDevice
    {
        Result result = DMLCreateDevice1(
            d3d12Device,
            createDeviceFlags,
            minimumFeatureLevel,
            typeof(T).GUID,
            out IntPtr nativePtr);

        if (result.Failure)
        {
            device = default;
            return result;
        }

        device = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
