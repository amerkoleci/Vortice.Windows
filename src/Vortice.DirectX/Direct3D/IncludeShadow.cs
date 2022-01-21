// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;

namespace Vortice.Direct3D;

/// <summary>
/// Shadow callback for <see cref="Include"/>.
/// </summary>
internal partial class IncludeShadow
{
    private readonly Dictionary<IntPtr, Frame> _frames = new();

    private struct Frame : IDisposable
    {
        public Frame(Stream stream, GCHandle handle)
        {
            Stream = stream;
            _handle = handle;
        }

        public readonly Stream Stream;
        private GCHandle _handle;

        public void Dispose()
        {
            if (_handle.IsAllocated)
                _handle.Free();
        }
    }

    /// <summary>
    /// Internal Include Callback
    /// </summary>
    [DebuggerTypeProxy(typeof(CppObjectVtblDebugView))]
    private class IncludeVtbl : CppObjectVtbl
    {
        public IncludeVtbl(int numberOfCallbackMethods) : base(numberOfCallbackMethods + 2)
        {
            AddMethod(new OpenDelegate(OpenImpl), 0u);
            AddMethod(new CloseDelegate(CloseImpl), 1u);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate Result OpenDelegate(IntPtr thisPtr, IncludeType includeType, IntPtr fileNameRef, IntPtr pParentData, ref IntPtr dataRef, ref int bytesRef);

        private static Result OpenImpl(IntPtr thisPtr, IncludeType includeType, IntPtr fileNameRef, IntPtr pParentData, ref IntPtr dataRef, ref int bytesRef)
        {
            try
            {
                IncludeShadow shadow = ToShadow<IncludeShadow>(thisPtr);
                Include callback = (Include)shadow.Callback;

                Stream? stream = null;
                Stream? parentStream = null;

                if (shadow._frames.ContainsKey(pParentData))
                {
                    parentStream = shadow._frames[pParentData].Stream;
                }

                stream = callback.Open(includeType, Marshal.PtrToStringAnsi(fileNameRef), parentStream);
                if (stream == null)
                    return Result.Fail;

                GCHandle handle;

                //if (stream is DataStream)
                //{
                //    // Magic shortcut if we happen to get a DataStream
                //    var data = (DataStream)stream;
                //    dataRef = data.PositionPointer;
                //    bytesRef = (int)(data.Length - data.Position);
                //    handle = new GCHandle();
                //}
                //else
                {
                    // Read the stream into a byte array and pin it
                    byte[] data = ReadStream(stream);
                    handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                    dataRef = handle.AddrOfPinnedObject();
                    bytesRef = data.Length;
                }

                shadow._frames.Add(dataRef, new Frame(stream, handle));

                return Result.Ok;
            }
            catch (SharpGenException exception)
            {
                return exception.ResultCode.Code;
            }
            catch (Exception)
            {
                return Result.Fail;
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate Result CloseDelegate(IntPtr thisPtr, IntPtr pData);

        private static Result CloseImpl(IntPtr thisPtr, IntPtr pData)
        {
            try
            {
                IncludeShadow shadow = ToShadow<IncludeShadow>(thisPtr);
                Include callback = (Include)shadow.Callback;

                if (shadow._frames.TryGetValue(pData, out Frame frame))
                {
                    shadow._frames.Remove(pData);
                    callback.Close(frame.Stream);
                    frame.Dispose();
                }

                return Result.Ok;
            }
            catch (SharpGenException exception)
            {
                return exception.ResultCode.Code;
            }
            catch (Exception)
            {
                return Result.Fail;
            }
        }

        private static byte[] ReadStream(Stream stream)
        {
            int readLength = 0;
            return ReadStream(stream, ref readLength);
        }

        private static byte[] ReadStream(Stream stream, ref int readLength)
        {
            Debug.Assert(stream != null);
            Debug.Assert(stream!.CanRead);

            int count = readLength;
            Debug.Assert(count <= stream.Length - stream.Position);
            if (count == 0)
            {
                readLength = (int)(stream.Length - stream.Position);
            }

            count = readLength;

            Debug.Assert(count >= 0);
            if (count == 0)
                return Array.Empty<byte>();

            byte[] buffer = new byte[count];
            int bytesRead = 0;

            do
            {
                bytesRead += stream.Read(buffer, bytesRead, readLength - bytesRead);
            } while (bytesRead < readLength);

            return buffer;
        }
    }
}
