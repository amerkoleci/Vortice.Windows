// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DirectWrite
{
    public partial class GlyphRunDescription
    {
        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal partial struct __Native
        {
            public IntPtr LocaleName;
            public IntPtr Text;
            public int TextLength;
            public IntPtr ClusterMap;
            public int TextPosition;

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
            Text = (@ref.Text == IntPtr.Zero) ? null : Marshal.PtrToStringUni(@ref.Text, @ref.TextLength);
            TextLength = @ref.TextLength;
            ClusterMap = @ref.ClusterMap;
            TextPosition = @ref.TextPosition;
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.LocaleName = string.IsNullOrEmpty(LocaleName) ? IntPtr.Zero : Marshal.StringToHGlobalUni(LocaleName);
            @ref.Text = string.IsNullOrEmpty(Text) ? IntPtr.Zero : Marshal.StringToHGlobalUni(Text);
            @ref.TextLength = string.IsNullOrEmpty(Text) ? 0 : Text.Length;
            @ref.ClusterMap = ClusterMap;
            @ref.TextPosition = TextPosition;
        }
        #endregion Marshal
    }
}
