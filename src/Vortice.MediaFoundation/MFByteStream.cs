// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using SharpGen.Runtime.Win32;

namespace Vortice.MediaFoundation
{
    public partial class MFByteStream
    {
        private Stream? _sourceStream;
        private ComStreamProxy? _streamProxy;

        /// <summary>
        /// Instantiates a new instance <see cref="MFByteStream"/> from a <see cref="Stream"/>.
        /// </summary>
        public MFByteStream(Stream sourceStream)
        {
            _sourceStream = sourceStream;

            //if (PlatformDetection.IsAppContainerProcess)
            //{
            //    //var randomAccessStream = sourceStream.AsRandomAccessStream();
            //    //MediaFactory.MFCreateMFByteStreamOnStreamEx(new ComObject(Marshal.GetIUnknownForObject(randomAccessStream)), this);
            //}
            //else
            {
                _streamProxy = new ComStreamProxy(sourceStream);
                MediaFactory.MFCreateMFByteStreamOnStream(_streamProxy, this);
            }
        }

        /// <summary>
        /// Instantiates a new instance <see cref="MFByteStream"/> from a <see cref="Stream"/>.
        /// </summary>
        public MFByteStream(byte[] sourceStream)
            : this(new MemoryStream(sourceStream))
        {
        }

        protected override unsafe void DisposeCore(IntPtr nativePointer, bool disposing)
        {
            base.DisposeCore(nativePointer, disposing);

            if (_streamProxy != null)
            {
                _streamProxy.Dispose();
                _streamProxy = null;
            }
        }

        public uint Read(byte[] bRef, int offset, uint count)
        {
            unsafe
            {
                fixed (void* ptr = &bRef[offset])
                {
                    return Read((IntPtr)ptr, count);
                }
            }
        }

        public unsafe void BeginRead(byte[] bRef, int offset, uint count, IMFAsyncCallback callback, object? context = default)
        {
            fixed (void* ptr = &bRef[offset])
            {
                BeginRead((IntPtr)ptr, count, callback, context != null ? Marshal.GetIUnknownForObject(context) : IntPtr.Zero);
            }
        }

        public uint Write(byte[] bRef, int offset, uint count)
        {
            unsafe
            {
                fixed (void* ptr = &bRef[offset])
                {
                    return Write((IntPtr)ptr, count);
                }
            }
        }

        public unsafe void BeginWrite(byte[] bRef, int offset, uint count, IMFAsyncCallback callback, object? context = default)
        {
            fixed (void* ptr = &bRef[offset])
            {
                BeginWrite((IntPtr)ptr, count, callback, context != null ? Marshal.GetIUnknownForObject(context) : IntPtr.Zero);
            }
        }
    }
}
