// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DirectWrite
{
    [Shadow(typeof(IDWriteFontFileEnumeratorShadow))]
    public partial interface IDWriteFontFileEnumerator
    {
        /// <summary>	
        /// Advances to the next font file in the collection. When it is first created, the enumerator is positioned before the first element of the collection and the first call to MoveNext advances to the first file. 	
        /// </summary>	
        /// <returns>the value TRUE if the enumerator advances to a file; otherwise, FALSE if the enumerator advances past the last file in the collection.</returns>
        bool MoveNext();

        /// <summary>	
        /// Gets a reference to the current font file. 	
        /// </summary>	
        /// <returns>a reference to the newly created <see cref="IDWriteFontFile"/> object.</returns>
        IDWriteFontFile CurrentFontFile { get; }
    }

    internal class IDWriteFontFileEnumeratorShadow : ComObjectShadow
    {
        protected override CppObjectVtbl Vtbl => new FontFileEnumeratorVtbl();

        /// <summary>
        /// Return a pointer to the unmanaged version of this callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns>A pointer to a shadow c++ callback</returns>
        public static IntPtr ToIntPtr(IDWriteFontFileEnumerator callback) => ToCallbackPtr<IDWriteFontFileEnumerator>(callback);

        private class FontFileEnumeratorVtbl : ComObjectVtbl
        {
            public FontFileEnumeratorVtbl() : base(2)
            {
                AddMethod(new MoveNextDelegate(MoveNextImpl));
                AddMethod(new GetCurrentFontFileDelegate(GetCurrentFontFileImpl));
            }

            /// <summary>	
            /// Advances to the next font file in the collection. When it is first created, the enumerator is positioned before the first element of the collection and the first call to MoveNext advances to the first file. 	
            /// </summary>	
            /// <returns>the value TRUE if the enumerator advances to a file; otherwise, FALSE if the enumerator advances past the last file in the collection.</returns>
            /// <unmanaged>HRESULT IDWriteFontFileEnumerator::MoveNext([Out] BOOL* hasCurrentFile)</unmanaged>
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int MoveNextDelegate(IntPtr thisPtr, out int hasCurrentFile);

            private static int MoveNextImpl(IntPtr thisPtr, out int hasCurrentFile)
            {
                hasCurrentFile = 0;
                try
                {
                    var shadow = ToShadow<IDWriteFontFileEnumeratorShadow>(thisPtr);
                    var callback = (IDWriteFontFileEnumerator)shadow.Callback;
                    hasCurrentFile = callback.MoveNext() ? 1 : 0;
                }
                catch (Exception exception)
                {
                    return (int)Result.GetResultFromException(exception);
                }
                return Result.Ok.Code;
            }

            /// <summary>	
            /// Gets a reference to the current font file. 	
            /// </summary>	
            /// <returns>a reference to the newly created <see cref="IDWriteFontFile"/> object.</returns>
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int GetCurrentFontFileDelegate(IntPtr thisPtr, out IntPtr fontFile);

            private static int GetCurrentFontFileImpl(IntPtr thisPtr, out IntPtr fontFile)
            {
                fontFile = IntPtr.Zero;
                try
                {
                    var shadow = ToShadow<IDWriteFontFileEnumeratorShadow>(thisPtr);
                    var callback = (IDWriteFontFileEnumerator)shadow.Callback;
                    fontFile = callback.CurrentFontFile.NativePointer;
                }
                catch (Exception exception)
                {
                    return (int)Result.GetResultFromException(exception);
                }

                return Result.Ok.Code;
            }
        }

    }
}
