// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    public partial class ID3D12CommandQueue
    {
        public unsafe void ExecuteCommandList(ID3D12CommandList commandList)
        {
            Guard.NotNull(commandList, nameof(commandList));
            var ptr = commandList.NativePointer;
            ExecuteCommandLists(1, new IntPtr(&ptr));
        }

        public void BeginEvent(string name)
        {
            Guard.NotNullOrEmpty(name, nameof(name));

            var handle = IntPtr.Zero;
            try
            {
                handle = Marshal.StringToHGlobalUni(name);
                BeginEvent(1, handle, name.Length);
            }
            finally
            {
                if (handle != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(handle);
                    handle = IntPtr.Zero;
                }
            }
        }

        public void SetMarker(string name)
        {
            Guard.NotNullOrEmpty(name, nameof(name));

            var handle = IntPtr.Zero;
            try
            {
                handle = Marshal.StringToHGlobalUni(name);
                SetMarker(1, handle, name.Length);
            }
            finally
            {
                if (handle != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(handle);
                    handle = IntPtr.Zero;
                }
            }
        }
    }
}
