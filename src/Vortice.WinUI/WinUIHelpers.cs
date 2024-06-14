// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

#if WINDOWS
using WinRT;

namespace Vortice.WinUI;

internal static class WinUIHelpers
{
    public static nint GetNativeObject(Guid guid, object obj)
    {
        Result result = ((IWinRTObject)obj).NativeObject.TryAs(guid, out nint handle);
        result.CheckError();
        return handle;
    }
}

#endif
