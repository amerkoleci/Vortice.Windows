// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectStorage;

public static partial class DirectStorage
{
    public static event DllImportResolver? ResolveLibrary;

    static DirectStorage()
    {
        ResolveLibrary += static (libraryName, assembly, searchPath) =>
        {
            if (libraryName is not "dstorage.dll")
            {
                return IntPtr.Zero;
            }

            string rid = RuntimeInformation.RuntimeIdentifier;

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

    public static Result DStorageSetConfiguration(Configuration configuration) => DStorageSetConfiguration(ref configuration);
    public static Result DStorageSetConfiguration1(Configuration1 configuration) => DStorageSetConfiguration1(ref configuration);

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

        factory = default;
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
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public static Result DStorageCreateCompressionCodec(CompressionFormat format, uint numThreads, out IDStorageCompressionCodec? codec)
    {
        Result result = DStorageCreateCompressionCodec(format, numThreads, typeof(IDStorageCompressionCodec).GUID, out IntPtr nativePtr);
        if (result.Success)
        {
            codec = new(nativePtr);
            return result;
        }

        codec = default;
        return result;
    }

    public static IDStorageCompressionCodec DStorageCreateCompressionCodec(CompressionFormat format, uint numThreads)
    {
        DStorageCreateCompressionCodec(format, numThreads, typeof(IDStorageCompressionCodec).GUID, out IntPtr nativePtr).CheckError();
        return new(nativePtr)!;
    }

    public static Result DStorageCreateCompressionCodec<T>(CompressionFormat format, uint numThreads, out T? codec) where T : ComObject
    {
        Result result = DStorageCreateCompressionCodec(format, numThreads, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Success)
        {
            codec = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        codec = default;
        return result;
    }

    public static T DStorageCreateCompressionCodec<T>(CompressionFormat format, uint numThreads) where T : ComObject
    {
        DStorageCreateCompressionCodec(format, numThreads, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }
}
