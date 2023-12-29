// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

public unsafe partial class IDirect3DSurface9
{
    /// <summary>
    /// Gets the parent cube texture or texture (mipmap) object, if this surface is a child level of a cube texture or a mipmap.
    /// This method can also provide access to the parent swap chain if the surface is a back-buffer child.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>The parent container texture.</returns>
    public T GetContainer<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>() where T : ComObject
    {
        GetContainer(typeof(T).GUID, out IntPtr containerPtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(containerPtr)!;
    }

    public Result GetContainer<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(out T? container) where T : ComObject
    {
        Result result = GetContainer(typeof(T).GUID, out IntPtr containerPtr);
        if (result.Failure)
        {
            container = default;
            return result;
        }

        container = MarshallingHelpers.FromPointer<T>(containerPtr);
        return result;
    }

    /// <summary>
    /// Locks a rectangle on a surface.
    /// </summary>
    /// <param name="flags">The type of lock to perform.</param>
    /// <returns>A pointer to the locked region</returns>
    public LockedRectangle LockRect(LockFlags flags)
    {
        return LockRect(null, flags);
    }

    /// <summary>
    /// Locks a rectangle on a surface.
    /// </summary>
    /// <param name="rect">The rectangle to lock.</param>
    /// <param name="flags">The type of lock to perform.</param>
    /// <returns>A pointer to the locked region</returns>
    public LockedRectangle LockRect(Rect rect, LockFlags flags)
    {
        return LockRect(&rect, flags);
    }
}
