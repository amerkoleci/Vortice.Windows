// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Offers base functionality that allows for a consistent way to monitor the validity of a session across the different types of sessions.
/// The different types of sessions is of type  <see cref="ID3D12ProtectedResourceSession"/>
/// </summary>
public partial class ID3D12ProtectedSession
{
    /// <summary>
    /// Retrieves the fence for the protected session.
    /// From the fence, you can retrieve the current uniqueness validity value (using <see cref="ID3D12Fence.GetCompletedValue()"/>), and add monitors for changes to its value.
    /// This is a read-only fence.
    /// </summary>
    /// <typeparam name="T">Instance type of <see cref="ID3D12Fence"/>.</typeparam>
    /// <param name="fence">An instance to the fence for the given protected session.</param>
    /// <returns>The operation result.</returns>
    public Result GetStatusFence<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(out T? fence) where T : ID3D12Fence
    {
        Result result = GetStatusFence(typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            fence = default;
            return result;
        }

        fence = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public T GetStatusFence<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>() where T : ID3D12Fence
    {
        GetStatusFence(typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }
}
