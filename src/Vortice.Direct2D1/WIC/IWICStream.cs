// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using SharpGen.Runtime.Win32;

namespace Vortice.DirectX.WIC
{
    public partial class IWICStream
    {
        private ComStreamProxy _streamProxy;

        /// <summary>
        /// Initialize stream from file name.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="access">The <see cref="FileAccess"/> mode.</param>
        public void Initialize(string fileName, FileAccess access)
        {
            DisposeStreamProxy();
            NativeFileAccess desiredAccess = access.ToNative();
            InitializeFromFilename(fileName, (uint)desiredAccess);
        }

        public void Initialize(IStream comStream)
        {
            Guard.NotNull(comStream, nameof(comStream));

            DisposeStreamProxy();
            InitializeFromIStream(comStream);
        }

        public void Initialize(Stream stream)
        {
            Guard.NotNull(stream, nameof(stream));

            DisposeStreamProxy();
            _streamProxy = new ComStreamProxy(stream);
            InitializeFromIStream(_streamProxy);
        }

        public unsafe void Initialize(byte[] data)
        {
            Guard.NotNullOrEmpty(data, nameof(data));

            DisposeStreamProxy();
            fixed (void* dataPtr = &data[0])
            {
                InitializeFromMemory(new IntPtr(dataPtr), (uint)data.Length);
            }
        }

        public void Initialize<T>(ReadOnlySpan<T> data) where T : unmanaged
        {
            DisposeStreamProxy();
            unsafe
            {
                fixed (void* dataPtr = data)
                {
                    InitializeFromMemory(new IntPtr(dataPtr), (uint)(data.Length * sizeof(T)));
                }
            }
        }

        public void Initialize<T>(T[] data) where T : unmanaged
        {
            Guard.NotNullOrEmpty(data, nameof(data));

            DisposeStreamProxy();
            unsafe
            {
                fixed (void* dataPtr = data)
                {
                    InitializeFromMemory(new IntPtr(dataPtr), (uint)(data.Length * sizeof(T)));
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            DisposeStreamProxy();
        }

        private void DisposeStreamProxy()
        {
            if (_streamProxy != null)
            {
                _streamProxy.Dispose();
                _streamProxy = null;
            }
        }
    }
}
