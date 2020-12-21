// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DirectWrite
{
    [Shadow(typeof(IDWriteFontCollectionLoaderShadow))]
    public partial interface IDWriteFontCollectionLoader
    {
        IDWriteFontFileEnumerator CreateEnumeratorFromKey(IDWriteFactory factory, IntPtr collectionKey, int size);
    }

    internal class IDWriteFontCollectionLoaderShadow : ComObjectShadow
    {
        private static readonly ComObjectVtbl s_vtbl = new FontCollectionLoaderVtbl();

        protected override CppObjectVtbl Vtbl => s_vtbl;

        private IDWriteFactory _factory;

        public static void SetFactory(IDWriteFontCollectionLoader loader, IDWriteFactory factory)
        {
            IntPtr shadowPtr = ToCallbackPtr<IDWriteFontCollectionLoader>(loader);
            IDWriteFontCollectionLoaderShadow shadow = ToShadow<IDWriteFontCollectionLoaderShadow>(shadowPtr);
            shadow._factory = factory;
        }

        private class FontCollectionLoaderVtbl : ComObjectVtbl
        {
            public FontCollectionLoaderVtbl() : base(1)
            {
                AddMethod(new CreateEnumeratorFromKeyDelegate(CreateEnumeratorFromKeyImpl));
            }


            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int CreateEnumeratorFromKeyDelegate(IntPtr thisPtr, IntPtr factory, IntPtr collectionKey, int collectionKeySize, out IntPtr fontFileEnumerator);

            private static int CreateEnumeratorFromKeyImpl(IntPtr thisPtr, IntPtr factory, IntPtr collectionKey, int collectionKeySize, out IntPtr fontFileEnumerator)
            {
                fontFileEnumerator = IntPtr.Zero;
                try
                {
                    IDWriteFontCollectionLoaderShadow shadow = ToShadow<IDWriteFontCollectionLoaderShadow>(thisPtr);
                    IDWriteFontCollectionLoader callback = (IDWriteFontCollectionLoader)shadow.Callback;
                    Debug.Assert(factory == shadow._factory.NativePointer);
                    IDWriteFontFileEnumerator enumerator = callback.CreateEnumeratorFromKey(shadow._factory, collectionKey, collectionKeySize);
                    fontFileEnumerator = IDWriteFontFileEnumeratorShadow.ToIntPtr(enumerator);
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
