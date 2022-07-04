// Copyright � Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Vortice.Direct3D;

/// <summary>
/// Vtbl implementation for <see cref="Include"/>.
/// </summary>

internal static unsafe partial class IncludeVtbl
{
    private static unsafe partial int OpenImpl_(IntPtr thisObject, int _includeType, void* _fileName, void* _parentData, void* _data, void* _bytes)
    {
        IncludeShadow shadow = CppObjectShadow.ToAnyShadow<IncludeShadow>(thisObject);
        Include callback = shadow.ToCallback<Include>();

        try
        {
            IncludeType includeType = (IncludeType)_includeType;
            IntPtr parentData = (IntPtr)_parentData;
            ref IntPtr data = ref Unsafe.AsRef<IntPtr>(_data);
            ref int bytes = ref Unsafe.AsRef<int>(_bytes);
            string fileName = Marshal.PtrToStringAnsi((IntPtr)_fileName);

            Stream? stream = null;
            Stream? parentStream = null;

            if (shadow._frames.ContainsKey(parentData))
            {
                parentStream = shadow._frames[parentData].Stream;
            }

            stream = callback.Open(includeType, fileName, parentStream);
            if (stream == null)
                return Result.Fail.Code;

            GCHandle handle;

            if (stream is DataStream dataStream)
            {
                // Magic shortcut if we happen to get a DataStream
                data = dataStream.PositionPointer;
                bytes = (int)(dataStream.Length - dataStream.Position);
                handle = new GCHandle();
            }
            else
            {
                // Read the stream into a byte array and pin it
                byte[] streamBytes = ReadStream(stream);
                handle = GCHandle.Alloc(streamBytes, GCHandleType.Pinned);
                data = handle.AddrOfPinnedObject();
                bytes = streamBytes.Length;
            }

            shadow._frames.Add(data, new IncludeShadow.Frame(stream, handle));

            return Result.Ok.Code;
        }
        catch (System.Exception __exception__)
        {
            (callback as IExceptionCallback)?.RaiseException(__exception__);
            return Result.GetResultFromException(__exception__).Code;
        }
    }

    private static unsafe partial int CloseImpl_(IntPtr thisObject, void* _data)
    {
        var shadow = CppObjectShadow.ToAnyShadow<IncludeShadow>(thisObject);
        var callback = shadow.ToCallback<Include>();
        try
        {
            IntPtr data = (IntPtr)_data;

            if (shadow._frames.TryGetValue(data, out IncludeShadow.Frame frame))
            {
                shadow._frames.Remove(data);
                callback.Close(frame.Stream);
                frame.Dispose();
            }

            return Result.Ok.Code;
        }
        catch (System.Exception __exception__)
        {
            (callback as IExceptionCallback)?.RaiseException(__exception__);
            return Result.GetResultFromException(__exception__).Code;
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
