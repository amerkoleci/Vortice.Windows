// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class AutoBreadcrumbNode
{
    public string? CommandListDebugName { get; internal set; }
    public string? CommandQueueDebugName { get; internal set; }

    public ID3D12GraphicsCommandList? CommandList { get; internal set; }
    public ID3D12CommandQueue? CommandQueue { get; internal set; }
    public int BreadcrumbCount { get; internal set; }
    public int? LastBreadcrumbValue { get; internal set; }

    public AutoBreadcrumbOp[]? CommandHistory { get; internal set; }

    public AutoBreadcrumbNode? Next { get; internal set; }

    #region Marshal
    internal unsafe struct __Native
    {
        public IntPtr pCommandListDebugNameA;

        public IntPtr pCommandListDebugNameW;

        public IntPtr pCommandQueueDebugNameA;

        public IntPtr pCommandQueueDebugNameW;

        public IntPtr pCommandList; /* ID3D12GraphicsCommandList* */

        public IntPtr pCommandQueue; /* ID3D12CommandQueue* */

        public int BreadcrumbCount;

        public int* pLastBreadcrumbValue;

        public AutoBreadcrumbOp* pCommandHistory;

        public __Native* pNext;
    }

    internal unsafe void __MarshalFree(__Native* @ref)
    {
        Marshal.FreeHGlobal(@ref->pCommandListDebugNameA);
        Marshal.FreeHGlobal(@ref->pCommandListDebugNameW);
        Marshal.FreeHGlobal(@ref->pCommandQueueDebugNameA);
        Marshal.FreeHGlobal(@ref->pCommandQueueDebugNameW);
        GC.KeepAlive(CommandList);
        GC.KeepAlive(CommandQueue);
        Next?.__MarshalFree(@ref->pNext);
    }

    internal unsafe void __MarshalFrom(__Native* @ref)
    {
        CommandListDebugName = Marshal.PtrToStringUni(@ref->pCommandListDebugNameW);
        CommandQueueDebugName = Marshal.PtrToStringUni(@ref->pCommandQueueDebugNameW);
        CommandList = @ref->pCommandList != IntPtr.Zero ? new ID3D12GraphicsCommandList(@ref->pCommandList) : null;
        CommandQueue = @ref->pCommandQueue != IntPtr.Zero ? new ID3D12CommandQueue(@ref->pCommandQueue) : null;
        BreadcrumbCount = @ref->BreadcrumbCount;
        LastBreadcrumbValue = @ref->pLastBreadcrumbValue != null ? *@ref->pLastBreadcrumbValue : default;

        if (@ref->BreadcrumbCount > 0)
        {
            CommandHistory = new AutoBreadcrumbOp[@ref->BreadcrumbCount];
            UnsafeUtilities.Read(@ref->pCommandHistory, CommandHistory, @ref->BreadcrumbCount);
        }

        if (@ref->pNext != null)
        {
            Next = new AutoBreadcrumbNode();
            Next.__MarshalFrom(@ref->pNext);
        }
    }

    internal unsafe void __MarshalTo(__Native* @ref)
    {
        if (string.IsNullOrEmpty(CommandListDebugName) == false)
        {
            @ref->pCommandListDebugNameA = Marshal.StringToHGlobalAnsi(CommandListDebugName);
            @ref->pCommandListDebugNameW = Marshal.StringToHGlobalUni(CommandListDebugName);
        }
        else
        {
            @ref->pCommandListDebugNameA = IntPtr.Zero;
            @ref->pCommandListDebugNameW = IntPtr.Zero;
        }

        if (string.IsNullOrEmpty(CommandQueueDebugName) == false)
        {
            @ref->pCommandQueueDebugNameA = Marshal.StringToHGlobalAnsi(CommandQueueDebugName);
            @ref->pCommandQueueDebugNameW = Marshal.StringToHGlobalUni(CommandQueueDebugName);
        }
        else
        {
            @ref->pCommandQueueDebugNameA = IntPtr.Zero;
            @ref->pCommandQueueDebugNameW = IntPtr.Zero;
        }

        @ref->pCommandList = CommandList?.NativePointer ?? IntPtr.Zero;
        @ref->pCommandQueue = CommandQueue?.NativePointer ?? IntPtr.Zero;
        @ref->BreadcrumbCount = BreadcrumbCount;
        //@ref->pLastBreadcrumbValue = LastBreadcrumbValue.GetValueOrDefault();
        //@ref->CommandHistory = CommandHistory;
        //@ref->Next = Next;
    }
    #endregion
}
