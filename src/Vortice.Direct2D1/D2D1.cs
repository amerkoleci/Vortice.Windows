// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;

public static partial class D2D1
{
    /// <summary>
    /// Try to create new instance of <see cref="ID2D1Factory"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T D2D1CreateFactory<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        FactoryType factoryType = FactoryType.SingleThreaded,
        DebugLevel debugLevel = DebugLevel.None) where T : ID2D1Factory
    {
        FactoryOptions options = new()
        {
            DebugLevel = debugLevel,
        };

        D2D1CreateFactory(factoryType, typeof(T).GUID, options, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    /// <summary>
    /// Try to create new instance of <see cref="ID2D1Factory"/>.
    /// </summary>
    /// <param name="factoryType">The type of factory.</param>
    /// <param name="options">The <see cref="FactoryOptions"/>.</param>
    /// <returns>Return the <see cref="Result"/>.</returns>
    public static T D2D1CreateFactory<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(FactoryType factoryType, FactoryOptions options) where T : ID2D1Factory
    {
        D2D1CreateFactory(factoryType, typeof(T).GUID, options, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    /// <summary>
    /// Try to create new instance of <see cref="ID2D1Factory"/>.
    /// </summary>
    /// <param name="factory">The <see cref="ID2D1Factory"/> being created.</param>
    /// <returns>Return the <see cref="Result"/>.</returns>
    public static Result D2D1CreateFactory<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(out T? factory) where T : ID2D1Factory
    {
        return D2D1CreateFactory(FactoryType.SingleThreaded, out factory);
    }

    /// <summary>
    /// Try to create new instance of <see cref="ID2D1Factory"/>.
    /// </summary>
    /// <param name="factoryType">The type of factory.</param>
    /// <param name="factory">The <see cref="ID2D1Factory"/> being created.</param>
    /// <returns>Return the <see cref="Result"/>.</returns>
    public static Result D2D1CreateFactory<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(FactoryType factoryType, out T? factory) where T : ID2D1Factory
    {
        var options = new FactoryOptions
        {
            DebugLevel = DebugLevel.None,
        };

        Result result = D2D1CreateFactory(factoryType, typeof(T).GUID, options, out IntPtr nativePtr);
        if (result.Success)
        {
            factory = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        factory = null;
        return result;
    }

    /// <summary>
    /// Try to create new instance of <see cref="ID2D1Factory"/>.
    /// </summary>
    /// <param name="factoryType">The type of factory.</param>
    /// <param name="options">The <see cref="FactoryOptions"/>.</param>
    /// <param name="factory">The <see cref="ID2D1Factory"/> being created.</param>
    /// <returns>Return the <see cref="Result"/>.</returns>
    public static Result D2D1CreateFactory<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(FactoryType factoryType, FactoryOptions options, out T? factory) where T : ID2D1Factory
    {
        Result result = D2D1CreateFactory(factoryType, typeof(T).GUID, options, out IntPtr nativePtr);
        if (result.Success)
        {
            factory = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        factory = null;
        return result;
    }
}
