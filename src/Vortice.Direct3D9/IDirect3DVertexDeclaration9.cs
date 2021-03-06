// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D9
{
    public partial class IDirect3DVertexDeclaration9
    {
        public int ElementsCount
        {
            get
            {
                int count = 0;
                GetDeclaration(null, ref count);
                return count;
            }
        }

        /// <summary>
        /// Gets the <see cref="VertexElement"/> array.
        /// </summary>
        public VertexElement[]? Elements
        {
            get
            {
                int count = 0;
                GetDeclaration(null, ref count);
                if (count == 0)
                {
                    return null;
                }

                var buffer = new VertexElement[count];
                GetDeclaration(buffer, ref count);

                return buffer;
            }
        }

        public int GetElements(VertexElement[] elements)
        {
            int count = elements.Length;
            GetDeclaration(elements, ref count);
            return count;
        }

        protected override void NativePointerUpdated(IntPtr oldNativePointer)
        {
            ReleaseDevice();
            base.NativePointerUpdated(oldNativePointer);
        }

        protected override void DisposeCore(IntPtr nativePointer, bool disposing)
        {
            if (disposing)
                ReleaseDevice();

            base.DisposeCore(nativePointer, disposing);
        }

        private void ReleaseDevice()
        {
            if (Device__ != null)
            {
                // Don't use Dispose() in order to avoid circular references with DeviceContext
                ((IUnknown)Device__).Release();
                Device__ = null;
            }
        }
    }
}
