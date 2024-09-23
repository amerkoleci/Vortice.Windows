// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

#pragma warning disable CS0649
namespace Vortice.Direct3D12;

public partial class DredAllocationNode
{
    public string? ObjectName { get; set; }
    public DredAllocationType AllocationType { get; set; }
    public DredAllocationNode? Next { get; set; }

    #region Marshal
    internal unsafe struct __Native
    {
        public IntPtr ObjectNameA;
        public IntPtr ObjectNameW;
        public DredAllocationType AllocationType;
        public __Native* pNext;
    }

    internal unsafe void __MarshalFree(__Native* @ref)
    {
        Marshal.FreeHGlobal(@ref->ObjectNameA);
        Marshal.FreeHGlobal(@ref->ObjectNameW);
        Next?.__MarshalFree(@ref->pNext);
    }

    internal unsafe void __MarshalFrom(__Native* @ref)
    {
        ObjectName = Marshal.PtrToStringUni(@ref->ObjectNameW);
        AllocationType = @ref->AllocationType;
        if (@ref->pNext != null)
        {
            Next = new DredAllocationNode();
            Next.__MarshalFrom(@ref->pNext);
        }
    }

    internal unsafe void __MarshalTo(__Native* @ref)
    {
        if (string.IsNullOrEmpty(ObjectName) == false)
        {
            @ref->ObjectNameA = Marshal.StringToHGlobalAnsi(ObjectName);
            @ref->ObjectNameW = Marshal.StringToHGlobalUni(ObjectName);
        }
        else
        {
            @ref->ObjectNameA = IntPtr.Zero;
            @ref->ObjectNameW = IntPtr.Zero;
        }

        @ref->AllocationType = AllocationType;

        if (Next != null)
        {
            Next.__MarshalTo(@ref->pNext);
        }
    }
    #endregion
}
