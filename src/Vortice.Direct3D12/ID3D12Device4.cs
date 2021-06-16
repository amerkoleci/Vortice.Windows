// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device4
    {
        public T CreateCommandList1<T>(CommandListType type, CommandListFlags commandListFlags = CommandListFlags.None) where T : ID3D12GraphicsCommandList1
        {
            CreateCommandList1(0, type, commandListFlags, typeof(T).GUID, out IntPtr nativePtr).CheckError();
            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        public T CreateCommandList1<T>(int nodeMask, CommandListType type, CommandListFlags commandListFlags = CommandListFlags.None) where T : ID3D12GraphicsCommandList1
        {
            CreateCommandList1(nodeMask, type, commandListFlags, typeof(T).GUID, out IntPtr nativePtr).CheckError();
            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        public Result CreateCommandList1<T>(CommandListType type, CommandListFlags commandListFlags, out T? commandList) where T : ID3D12GraphicsCommandList1
        {
            return CreateCommandList1<T>(0, type, commandListFlags, out commandList);
        }

        public Result CreateCommandList1<T>(int nodeMask, CommandListType type, CommandListFlags commandListFlags, out T? commandList) where T : ID3D12GraphicsCommandList1
        {
            Result result = CreateCommandList1(nodeMask, type, commandListFlags, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                commandList = default;
                return result;
            }

            commandList = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        public T? CreateCommittedResource1<T>(
            HeapProperties heapProperties,
            HeapFlags heapFlags,
            ResourceDescription description,
            ResourceStates initialResourceState,
            ID3D12ProtectedResourceSession protectedSession,
            ClearValue? optimizedClearValue = null) where T : ID3D12Resource1
        {
            Result result = CreateCommittedResource1(ref heapProperties, heapFlags,
                ref description,
                initialResourceState,
                optimizedClearValue,
                protectedSession,
                typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        public T? CreateHeap1<T>(HeapDescription description, ID3D12ProtectedResourceSession protectedSession) where T : ID3D12Heap1
        {
            Result result = CreateHeap1(ref description, protectedSession, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        public T? CreateProtectedResourceSession<T>(ProtectedResourceSessionDescription description) where T : ID3D12ProtectedResourceSession
        {
            Result result = CreateProtectedResourceSession(description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        public T? CreateReservedResource1<T>(ResourceDescription description, ResourceStates initialState, ClearValue clearValue, ID3D12ProtectedResourceSession protectedResourceSession) where T : ID3D12Resource1
        {
            Result result = CreateReservedResource1(ref description, initialState, clearValue, protectedResourceSession, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        public T? CreateReservedResource1<T>(ResourceDescription description, ResourceStates initialState, ID3D12ProtectedResourceSession protectedResourceSession) where T : ID3D12Resource1
        {
            Result result = CreateReservedResource1(ref description, initialState, null, protectedResourceSession, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }
    }
}
