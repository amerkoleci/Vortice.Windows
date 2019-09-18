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
    [Shadow(typeof(IStreamShadow))]
    public partial interface IStream
    {
        /// <summary>
        /// Changes the seek pointer to a new location relative to the beginning of the stream, to the end of the stream, or to the current seek pointer.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="origin">The origin.</param>
        /// <returns>The offset of the seek pointer from the beginning of the stream.</returns>
        long Seek(long offset, SeekOrigin origin);

        /// <summary>
        /// Changes the size of the stream object.
        /// </summary>
        /// <param name="newSize">The new size.</param>
        void SetSize(long newSize);

        /// <summary>
        /// Copies a specified number of bytes from the current seek pointer in the stream to the current seek pointer in another stream.
        /// </summary>
        /// <param name="streamDest">The stream destination.</param>
        /// <param name="numberOfBytesToCopy">The number of bytes to copy.</param>
        /// <param name="bytesWritten">The number of bytes written.</param>
        /// <returns>The number of bytes read</returns>
        long CopyTo(IStream streamDest, long numberOfBytesToCopy, out long bytesWritten);

        /// <summary>
        /// Commit method ensures that any changes made to a stream object open in transacted mode are reflected in the parent storage. If the stream object is open in direct mode, Commit has no effect other than flushing all memory buffers to the next-level storage object. The COM compound file implementation of streams does not support opening streams in transacted mode.
        /// </summary>
        /// <param name="commitFlags">The GRF commit flags.</param>
        void Commit(CommitFlags commitFlags);

        /// <summary>
        /// Discards all changes that have been made to a transacted stream since the last <see cref="Commit"/> call. 
        /// </summary>
        void Revert();

        /// <summary>
        /// Restricts access to a specified range of bytes in the stream.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="numberOfBytesToLock">The number of bytes to lock.</param>
        /// <param name="dwLockType">Type of the dw lock.</param>
        void LockRegion(long offset, long numberOfBytesToLock, LockType dwLockType);

        /// <summary>
        /// Unlocks access to a specified range of bytes in the stream.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="numberOfBytesToLock">The number of bytes to lock.</param>
        /// <param name="dwLockType">Type of the dw lock.</param>
        void UnlockRegion(long offset, long numberOfBytesToLock, LockType dwLockType);

        /// <summary>
        /// Gets the statistics.
        /// </summary>
        /// <param name="storageStatisticsFlags">The storage statistics flags.</param>
        /// <returns></returns>
        StorageStatistics GetStatistics(StorageStatisticsFlags storageStatisticsFlags);

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        IStream Clone();
    }

    internal class IStreamShadow : IStreamBaseShadow
    {
        private static readonly ComStreamVtbl _vTable = new ComStreamVtbl();

        protected override CppObjectVtbl Vtbl => _vTable;

        /// <summary>
        /// Callbacks to pointer.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static IntPtr ToIntPtr(IStream stream)
        {
            return ToCallbackPtr<IStream>(stream);
        }

        private class ComStreamVtbl : ComStreamBaseVtbl
        {
            public ComStreamVtbl()
                : base(9)
            {
                AddMethod(new SeekDelegate(SeekImpl));
                AddMethod(new SetSizeDelegate(SetSizeImpl));
                AddMethod(new CopyToDelegate(CopyToImpl));
                AddMethod(new CommitDelegate(CommitImpl));
                AddMethod(new RevertDelegate(RevertImpl));
                AddMethod(new LockRegionDelegate(LockRegionImpl));
                AddMethod(new UnlockRegionDelegate(UnlockRegionImpl));
                AddMethod(new StatDelegate(StatImpl));
                AddMethod(new CloneDelegate(CloneImpl));
            }

            /* public long Seek(long dlibMove, System.IO.SeekOrigin dwOrigin) */
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private unsafe delegate int SeekDelegate(IntPtr thisPtr, long offset, SeekOrigin origin, IntPtr newPosition);
            private unsafe static int SeekImpl(IntPtr thisPtr, long offset, SeekOrigin origin, IntPtr newPosition)
            {
                try
                {
                    var shadow = ToShadow<IStreamShadow>(thisPtr);
                    var callback = ((IStream)shadow.Callback);
                    long position = callback.Seek(offset, origin);

                    // pointer can be null, so we need to test it
                    if (newPosition != IntPtr.Zero)
                    {
                        *(long*)newPosition = position;
                    }
                }
                catch (Exception exception)
                {
                    return (int)Result.GetResultFromException(exception);
                }
                return Result.Ok.Code;
            }

            /* public SharpDX.Result SetSize(long libNewSize) */
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate Result SetSizeDelegate(IntPtr thisPtr, long newSize);
            private static Result SetSizeImpl(IntPtr thisPtr, long newSize)
            {
                var result = Result.Ok;
                try
                {
                    var shadow = ToShadow<IStreamShadow>(thisPtr);
                    var callback = ((IStream)shadow.Callback);
                    callback.SetSize(newSize);
                }
                catch (SharpGenException exception)
                {
                    result = exception.ResultCode;
                }
                catch (Exception)
                {
                    result = Result.Fail.Code;
                }
                return result;
            }

            /* internal long CopyTo_(System.IntPtr stmRef, long cb, out long cbWrittenRef) */
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int CopyToDelegate(IntPtr thisPtr, IntPtr streamPointer, long numberOfBytes, out long numberOfBytesRead, out long numberOfBytesWritten);
            private static int CopyToImpl(IntPtr thisPtr, IntPtr streamPointer, long numberOfBytes, out long numberOfBytesRead, out long numberOfBytesWritten)
            {
                numberOfBytesRead = 0;
                numberOfBytesWritten = 0;
                try
                {
                    var shadow = ToShadow<IStreamShadow>(thisPtr);
                    var callback = ((IStream)shadow.Callback);
                    numberOfBytesRead = callback.CopyTo(new ComStream(streamPointer), numberOfBytes, out numberOfBytesWritten);
                }
                catch (Exception exception)
                {
                    return (int)Result.GetResultFromException(exception);
                }
                return Result.Ok.Code;
            }

            /* public SharpDX.Result Commit(SharpDX.Win32.CommitFlags grfCommitFlags) */
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate Result CommitDelegate(IntPtr thisPtr, CommitFlags flags);
            private static Result CommitImpl(IntPtr thisPtr, CommitFlags flags)
            {
                var result = Result.Ok;
                try
                {
                    var shadow = ToShadow<IStreamShadow>(thisPtr);
                    var callback = ((IStream)shadow.Callback);
                    callback.Commit(flags);
                }
                catch (SharpGenException exception)
                {
                    result = exception.ResultCode;
                }
                catch (Exception)
                {
                    result = Result.Fail.Code;
                }
                return result;
            }

            /* public SharpDX.Result Revert() */
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate Result RevertDelegate(IntPtr thisPtr);
            private static Result RevertImpl(IntPtr thisPtr)
            {
                var result = Result.Ok;
                try
                {
                    var shadow = ToShadow<IStreamShadow>(thisPtr);
                    var callback = ((IStream)shadow.Callback);
                    callback.Revert();
                }
                catch (SharpGenException exception)
                {
                    result = exception.ResultCode;
                }
                catch (Exception)
                {
                    result = Result.Fail.Code;
                }
                return result;
            }

            /* public SharpDX.Result LockRegion(long libOffset, long cb, SharpDX.Win32.LockType dwLockType) */
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate Result LockRegionDelegate(IntPtr thisPtr, long offset, long numberOfBytes, LockType lockType);
            private static Result LockRegionImpl(IntPtr thisPtr, long offset, long numberOfBytes, LockType lockType)
            {
                var result = Result.Ok;
                try
                {
                    var shadow = ToShadow<IStreamShadow>(thisPtr);
                    var callback = ((IStream)shadow.Callback);
                    callback.LockRegion(offset, numberOfBytes, lockType);
                }
                catch (SharpGenException exception)
                {
                    result = exception.ResultCode;
                }
                catch (Exception)
                {
                    result = Result.Fail.Code;
                }
                return result;
            }


            /* public SharpDX.Result UnlockRegion(long libOffset, long cb, SharpDX.Win32.LockType dwLockType) */
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate Result UnlockRegionDelegate(IntPtr thisPtr, long offset, long numberOfBytes, LockType lockType);
            private static Result UnlockRegionImpl(IntPtr thisPtr, long offset, long numberOfBytes, LockType lockType)
            {
                var result = Result.Ok;
                try
                {
                    var shadow = ToShadow<IStreamShadow>(thisPtr);
                    var callback = ((IStream)shadow.Callback);
                    callback.UnlockRegion(offset, numberOfBytes, lockType);
                }
                catch (SharpGenException exception)
                {
                    result = exception.ResultCode;
                }
                catch (Exception)
                {
                    result = Result.Fail.Code;
                }
                return result;
            }

            /* public SharpDX.Win32.StorageStatistics GetStatistics(SharpDX.Win32.StorageStatisticsFlags grfStatFlag) */
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate Result StatDelegate(IntPtr thisPtr, ref StorageStatistics.__Native statisticsPtr, StorageStatisticsFlags flags);
            private static Result StatImpl(IntPtr thisPtr, ref StorageStatistics.__Native statisticsPtr, StorageStatisticsFlags flags)
            {
                try
                {
                    var shadow = ToShadow<IStreamShadow>(thisPtr);
                    var callback = ((IStream)shadow.Callback);
                    var statistics = callback.GetStatistics(flags);
                    statistics.__MarshalTo(ref statisticsPtr);
                }
                catch (SharpGenException exception)
                {
                    return exception.ResultCode;
                }
                catch (Exception)
                {
                    return Result.Fail.Code;
                }
                return Result.Ok;
            }

            /* public SharpDX.Win32.IStream Clone() */
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate Result CloneDelegate(IntPtr thisPtr, out IntPtr streamPointer);
            private static Result CloneImpl(IntPtr thisPtr, out IntPtr streamPointer)
            {
                streamPointer = IntPtr.Zero;
                var result = Result.Ok;
                try
                {
                    var shadow = ToShadow<IStreamShadow>(thisPtr);
                    var callback = ((IStream)shadow.Callback);
                    var clone = callback.Clone();
                    streamPointer = IStreamShadow.ToIntPtr(clone);
                }
                catch (SharpGenException exception)
                {
                    result = exception.ResultCode;
                }
                catch (Exception)
                {
                    result = Result.Fail.Code;
                }
                return result;
            }
        }
    }
}
