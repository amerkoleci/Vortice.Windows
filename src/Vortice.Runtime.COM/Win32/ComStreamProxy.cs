// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SharpGen.Runtime.Win32
{
    [Guid("0000000c-0000-0000-C000-000000000046")]
    public class ComStreamProxy : CallbackBase, IStream
    {
        private Stream sourceStream;
        byte[] tempBuffer = new byte[0x1000];

        public ComStreamProxy(Stream sourceStream)
        {
            this.sourceStream = sourceStream;
        }

        public unsafe uint Read(IntPtr buffer, uint numberOfBytesToRead)
        {
            uint totalRead = 0;

            while (numberOfBytesToRead > 0)
            {
                uint countRead = (uint)Math.Min(numberOfBytesToRead, tempBuffer.Length);
                uint count = (uint)sourceStream.Read(tempBuffer, 0, (int)countRead);
                if (count == 0)
                    return totalRead;
                MemoryHelpers.Write(new IntPtr(totalRead + (byte*)buffer), new Span<byte>(tempBuffer), (int)count);
                numberOfBytesToRead -= count;
                totalRead += count;
            }
            return totalRead;
        }

        public unsafe uint Write(IntPtr buffer, uint numberOfBytesToWrite)
        {
            uint totalWrite = 0;

            while (numberOfBytesToWrite > 0)
            {
                uint countWrite = (uint)Math.Min(numberOfBytesToWrite, tempBuffer.Length);
                MemoryHelpers.Read(new IntPtr(totalWrite + (byte*)buffer), new ReadOnlySpan<byte>(tempBuffer), (int)countWrite);
                sourceStream.Write(tempBuffer, 0, (int)countWrite);
                numberOfBytesToWrite -= countWrite;
                totalWrite += countWrite;
            }
            return totalWrite;
        }

        public ulong Seek(long offset, SeekOrigin origin)
        {
            return (ulong)sourceStream.Seek(offset, origin);
        }

        public void SetSize(ulong newSize)
        {
        }

        public unsafe ulong CopyTo(IStream streamDest, ulong numberOfBytesToCopy, out ulong bytesWritten)
        {
            bytesWritten = 0;

            fixed (void* pBuffer = tempBuffer)
            {
                while (numberOfBytesToCopy > 0)
                {
                    int countCopy = (int)Math.Min((long)numberOfBytesToCopy, tempBuffer.Length);
                    int count = sourceStream.Read(tempBuffer, 0, countCopy);
                    if (count == 0)
                        break;
                    streamDest.Write((IntPtr)pBuffer, (uint)count);
                    numberOfBytesToCopy -= (ulong)count;
                    bytesWritten += (ulong)count;
                }
            }
            return bytesWritten;
        }

        public void Commit(CommitFlags commitFlags)
        {
            sourceStream.Flush();
        }

        public void Revert()
        {
            throw new NotImplementedException();
        }

        public void LockRegion(ulong offset, ulong numberOfBytesToLock, LockType dwLockType)
        {
            throw new NotImplementedException();
        }

        public void UnlockRegion(ulong offset, ulong numberOfBytesToLock, LockType dwLockType)
        {
            throw new NotImplementedException();
        }

        public StorageStatistics GetStatistics(StorageStatisticsFlags storageStatisticsFlags)
        {
            long length = sourceStream.Length;
            if (length == 0)
                length = 0x7fffffff;

            return new StorageStatistics
                {
                    Type = 2, // IStream
                    CbSize = (ulong)length,
                    GrfLocksSupported = 2, // exclusive
                    GrfMode = 0x00000002, // read-write
                };
        }

        public IStream Clone()
        {
            return new ComStreamProxy(sourceStream);
        }

        protected override void Dispose(bool disposing)
        {
            sourceStream = null;
            base.Dispose(disposing);
        }
    }
}