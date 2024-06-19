// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public abstract class IVirtualSurfaceUpdatesCallbackNativeBase : CallbackBase, IVirtualSurfaceUpdatesCallbackNative
{
    public abstract Result UpdatesNeeded();
}
