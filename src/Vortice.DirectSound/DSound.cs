// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;

namespace Vortice.DirectSound;

public static unsafe partial class DSound
{
    #region DirectSoundCreate/DirectSoundCreate8
    public static IDirectSound DirectSoundCreate()
    {
        IntPtr dsPtr = IntPtr.Zero;
        Result result = DirectSoundCreate_(null, &dsPtr, null);
        result.CheckError();
        return new IDirectSound(dsPtr);
    }

    public static Result DirectSoundCreate(out IDirectSound? sound)
    {
        IntPtr dsPtr = IntPtr.Zero;
        Result result = DirectSoundCreate_(null, &dsPtr, null);
        sound = (dsPtr != IntPtr.Zero) ? new IDirectSound(dsPtr) : null;
        return result;
    }

    public static IDirectSound DirectSoundCreate(Guid driverGuid)
    {
        IntPtr dsPtr = IntPtr.Zero;
        Result result = DirectSoundCreate_(&driverGuid, &dsPtr, null);
        result.CheckError();
        return new IDirectSound(dsPtr);
    }

    public static Result DirectSoundCreate(Guid driverGuid, out IDirectSound? sound)
    {
        IntPtr dsPtr = IntPtr.Zero;
        Result result = DirectSoundCreate8_(&driverGuid, &dsPtr, null);
        sound = (dsPtr != IntPtr.Zero) ? new IDirectSound(dsPtr) : null;
        return result;
    }

    public static IDirectSound8 DirectSoundCreate8()
    {
        IntPtr ds8Ptr = IntPtr.Zero;
        Result result = DirectSoundCreate8_(null, &ds8Ptr, null);
        result.CheckError();
        return new IDirectSound8(ds8Ptr);
    }

    public static Result DirectSoundCreate8(out IDirectSound8? sound)
    {
        IntPtr ds8Ptr = IntPtr.Zero;
        Result result = DirectSoundCreate8_(null, &ds8Ptr, null);
        sound = (ds8Ptr != IntPtr.Zero) ? new IDirectSound8(ds8Ptr) : null;
        return result;
    }

    public static IDirectSound8 DirectSoundCreate8(Guid driverGuid)
    {
        IntPtr ds8Ptr = IntPtr.Zero;
        Result result = DirectSoundCreate8_(&driverGuid, &ds8Ptr, null);
        result.CheckError();
        return new IDirectSound8(ds8Ptr);
    }

    public static Result DirectSoundCreate8(Guid driverGuid, out IDirectSound8? sound)
    {
        IntPtr ds8Ptr = IntPtr.Zero;
        Result result = DirectSoundCreate8_(&driverGuid, &ds8Ptr, null);
        sound = (ds8Ptr != IntPtr.Zero) ? new IDirectSound8(ds8Ptr) : null;
        return result;
    }
    #endregion

    #region DirectSoundCaptureCreate/DirectSoundCaptureCreate8
    public static IDirectSoundCapture DirectSoundCaptureCreate()
    {
        IntPtr dsPtr = IntPtr.Zero;
        Result result = DirectSoundCaptureCreate_(null, &dsPtr, null);
        result.CheckError();
        return new IDirectSoundCapture(dsPtr);
    }

    public static Result DirectSoundCaptureCreate(out IDirectSoundCapture? sound)
    {
        IntPtr dsPtr = IntPtr.Zero;
        Result result = DirectSoundCaptureCreate_(null, &dsPtr, null);
        sound = (dsPtr != IntPtr.Zero) ? new IDirectSoundCapture(dsPtr) : null;
        return result;
    }

    public static IDirectSoundCapture DirectSoundCaptureCreate(Guid driverGuid)
    {
        IntPtr dsPtr = IntPtr.Zero;
        Result result = DirectSoundCaptureCreate_(&driverGuid, &dsPtr, null);
        result.CheckError();
        return new IDirectSoundCapture(dsPtr);
    }

    public static Result DirectSoundCaptureCreate(Guid driverGuid, out IDirectSoundCapture? sound)
    {
        IntPtr dsPtr = IntPtr.Zero;
        Result result = DirectSoundCaptureCreate_(&driverGuid, &dsPtr, null);
        sound = (dsPtr != IntPtr.Zero) ? new IDirectSoundCapture(dsPtr) : null;
        return result;
    }

    public static IDirectSoundCapture DirectSoundCaptureCreate8()
    {
        IntPtr ds8Ptr = IntPtr.Zero;
        Result result = DirectSoundCaptureCreate8_(null, &ds8Ptr, null);
        result.CheckError();
        return new IDirectSoundCapture(ds8Ptr);
    }

    public static Result DirectSoundCaptureCreate8(out IDirectSoundCapture? sound)
    {
        IntPtr ds8Ptr = IntPtr.Zero;
        Result result = DirectSoundCaptureCreate8_(null, &ds8Ptr, null);
        sound = (ds8Ptr != IntPtr.Zero) ? new IDirectSoundCapture(ds8Ptr) : null;
        return result;
    }

    public static IDirectSoundCapture DirectSoundCaptureCreate8(Guid driverGuid)
    {
        IntPtr ds8Ptr = IntPtr.Zero;
        Result result = DirectSoundCaptureCreate8_(&driverGuid, &ds8Ptr, null);
        result.CheckError();
        return new IDirectSoundCapture(ds8Ptr);
    }

    public static Result DirectSoundCaptureCreate8(Guid driverGuid, out IDirectSoundCapture? sound)
    {
        IntPtr ds8Ptr = IntPtr.Zero;
        Result result = DirectSoundCaptureCreate8_(&driverGuid, &ds8Ptr, null);
        sound = (ds8Ptr != IntPtr.Zero) ? new IDirectSoundCapture(ds8Ptr) : null;
        return result;
    }
    #endregion

    public static Guid GetDeviceID(Guid? guidSrc)
    {
        GetDeviceID(guidSrc, out Guid guidDest).CheckError();
        return guidDest;
    }
}
