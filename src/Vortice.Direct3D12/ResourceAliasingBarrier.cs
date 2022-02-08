// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial struct ResourceAliasingBarrier
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceAliasingBarrier"/> struct.
    /// </summary>
    /// <param name="resourceBefore">The resource before.</param>
    /// <param name="resourceAfter">The resource after.</param>
    /// <exception cref="System.ArgumentNullException">resourceBefore</exception>
    public ResourceAliasingBarrier(ID3D12Resource? resourceBefore, ID3D12Resource? resourceAfter)
    {
        ResourceBeforePointer = resourceBefore != null ? resourceBefore.NativePointer : IntPtr.Zero;
        ResourceAfterPointer = resourceAfter != null ? resourceAfter.NativePointer : IntPtr.Zero;
    }
}
