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
        private const uint GENERIC_READ = (0x80000000);
        private const uint GENERIC_WRITE = (0x40000000);

        private ComStreamProxy _streamProxy;

        /// <summary>
        /// Initialize stream from file name.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="access">The <see cref="FileAccess"/> mode.</param>
        public void Initialize(string fileName, FileAccess access)
        {
            uint desiredAccess = 0;
            switch (access)
            {
                case FileAccess.Read:
                    desiredAccess = GENERIC_READ;
                    break;

                case FileAccess.Write:
                    desiredAccess = GENERIC_WRITE;
                    break;

                case FileAccess.ReadWrite:
                    desiredAccess = GENERIC_READ | GENERIC_WRITE;
                    break;
            }

            DisposeStreamProxy();
            InitializeFromFilename(fileName, (int)desiredAccess);
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
            InitializeFromMemory((IntPtr)Unsafe.AsPointer(ref data[0]), data.Length);
        }

        public unsafe void Initialize<T>(T[] data) where T : struct
        {
            Guard.NotNullOrEmpty(data, nameof(data));

            DisposeStreamProxy();
            InitializeFromMemory((IntPtr)Unsafe.AsPointer(ref data[0]), data.Length * Unsafe.SizeOf<T>());
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
