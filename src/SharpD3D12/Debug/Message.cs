// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace SharpD3D12.Debug
{
    public partial struct Message
    {
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[{Id}] [{Severity}] [{Category}] : {Description}";
        }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal struct __Native
        {
            public MessageCategory Category;
            public MessageSeverity Severity;
            public MessageId Id;
            public IntPtr PDescription;
            public PointerSize DescriptionByteLength;
        }

        internal unsafe void __MarshalFrom(ref __Native @ref)
        {
            Category = @ref.Category;
            Severity = @ref.Severity;
            Id = @ref.Id;
            Description = (@ref.PDescription == IntPtr.Zero) ? null : Marshal.PtrToStringAnsi(@ref.PDescription, @ref.DescriptionByteLength);
            DescriptionByteLength = @ref.DescriptionByteLength;
        }
        #endregion Marshal
    }
}
