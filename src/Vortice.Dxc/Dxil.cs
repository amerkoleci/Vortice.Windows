// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Dxc;

public static partial class Dxil
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static IDxcValidator CreateDxilValidator()
    {
        DxilCreateInstance(Dxc.CLSID_DxcValidator, out IDxcValidator? result).CheckError();
        return result!;
    }


    public static Result DxilCreateInstance<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Guid classGuid, out T? instance) where T : ComObject
    {
        Result result = DxcCreateInstance(classGuid, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Success)
        {
            instance = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        instance = null;
        return result;
    }

    public static unsafe SharpGen.Runtime.Result DxcCreateInstance(System.Guid rclsid, System.Guid riid, out System.IntPtr ppv)
    {
        SharpGen.Runtime.Result __result__;
        fixed (void* ppv_ = &ppv)
            __result__ = DxcCreateInstance_(&rclsid, &riid, ppv_);
        return __result__;
    }

    [System.Runtime.InteropServices.DllImportAttribute("dxil.dll", EntryPoint = "DxcCreateInstance", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    private unsafe static extern int DxcCreateInstance_(void* _rclsid, void* _riid, void* _ppv);
}
