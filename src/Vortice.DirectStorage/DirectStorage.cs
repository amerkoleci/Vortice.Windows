// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectStorage;

public static unsafe partial class DirectStorage
{
#if NET6_0_OR_GREATER 
    public static event DllImportResolver? ResolveLibrary;

    static DirectStorage()
    {
        ResolveLibrary += static (libraryName, assembly, searchPath) =>
        {
            if (libraryName is not "dstorage.dll")
            {
                return IntPtr.Zero;
            }

            string rid = RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.X86 => "win10-x86",
                Architecture.X64 => "win10-x64",
                Architecture.Arm => "win10-arm",
                Architecture.Arm64 => "win10-arm64",
                _ => throw new NotSupportedException("Invalid process architecture")
            };

            // Test whether the native libraries are present in the same folder of the executable
            // (which is the case when the program was built with a runtime identifier), or whether
            // they are in the "runtimes\win-x64\native" folder in the executable directory.
            string nugetNativeLibsPath = Path.Combine(AppContext.BaseDirectory, $@"runtimes\{rid}\native");
            bool isNuGetRuntimeLibrariesDirectoryPresent = Directory.Exists(nugetNativeLibsPath);

            if (isNuGetRuntimeLibrariesDirectoryPresent)
            {
                string dstorageCorePath = Path.Combine(AppContext.BaseDirectory, $@"runtimes\{rid}\native\dstoragecore.dll");
                string dstoragePath = Path.Combine(AppContext.BaseDirectory, $@"runtimes\{rid}\native\dstorage.dll");

                // Load dstoragecore first so that dstorage doesn't fail to load it, and then dstoragecore, both from the NuGet path
                if (NativeLibrary.TryLoad(dstorageCorePath, out _) &&
                    NativeLibrary.TryLoad(dstoragePath, out IntPtr handle))
                {
                    return handle;
                }
            }
            else
            {
                if (NativeLibrary.TryLoad("dstoragecore", assembly, searchPath, out _) &&
                    NativeLibrary.TryLoad("dstorage", assembly, searchPath, out IntPtr handle))
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
#endif

    /// <summary>
    /// Returns the static DStorage factory object used to create DStorage queues,
    /// open files for DStorage access, and other global operations.
    /// </summary>
    /// <param name="factory">Specifies the DStorage factory interface, such as <see cref="IDStorageFactory"/>.</param>
    /// <returns>Return the <see cref="Result"/>.</returns>
    public static Result DStorageGetFactory<T>(out T? factory) where T : ComObject
    {
        Result result = DStorageGetFactory(typeof(T).GUID, out IntPtr nativePtr);
        if (result.Success)
        {
            factory = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        factory = null;
        return result;
    }

    /// <summary>
    /// Returns the static DStorage factory object used to create DStorage queues,
    /// open files for DStorage access, and other global operations.
    /// </summary>
    /// <returns>Return the DStorage factory interface, such as <see cref="IDStorageFactory"/>..</returns>
    public static T DStorageGetFactory<T>() where T : ComObject
    {
        DStorageGetFactory(typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }
}
