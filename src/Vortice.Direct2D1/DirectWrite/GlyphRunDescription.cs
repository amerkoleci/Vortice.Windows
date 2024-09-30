// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectWrite;

public partial class GlyphRunDescription
{
    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public IntPtr LocaleName;
        public IntPtr Text;
        public uint TextLength;
        public ushort* ClusterMap;
        public uint TextPosition;

        internal unsafe void __MarshalFree()
        {
            if (LocaleName != IntPtr.Zero)
                Marshal.FreeHGlobal(LocaleName);
            if (Text != IntPtr.Zero)
                Marshal.FreeHGlobal(Text);
        }
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        LocaleName = (@ref.LocaleName == IntPtr.Zero) ? null : Marshal.PtrToStringUni(@ref.LocaleName);
        Text = (@ref.Text == IntPtr.Zero) ? null : Marshal.PtrToStringUni(@ref.Text, (int)@ref.TextLength);
        TextLength = @ref.TextLength;
        ClusterMap = (nint)@ref.ClusterMap;
        TextPosition = @ref.TextPosition;
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.LocaleName = string.IsNullOrEmpty(LocaleName) ? IntPtr.Zero : Marshal.StringToHGlobalUni(LocaleName);
        @ref.Text = string.IsNullOrEmpty(Text) ? IntPtr.Zero : Marshal.StringToHGlobalUni(Text);
        @ref.TextLength = string.IsNullOrEmpty(Text) ? 0u : (uint)Text.Length;
        @ref.ClusterMap = (ushort*)ClusterMap.ToPointer();
        @ref.TextPosition = TextPosition;
    }
    #endregion Marshal
}
