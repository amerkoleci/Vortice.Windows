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
        IDWriteFontFileEnumerator CreateEnumeratorFromKey(IDWriteFactory factory, Span<byte> collectionKey);
    }

    internal class IDWriteFontCollectionLoaderShadow : ComObjectShadow
    {
        protected override CppObjectVtbl Vtbl => new FontCollectionLoaderVtbl();

        private IDWriteFactory _factory;

        public static IntPtr ToIntPtr(IDWriteFontCollectionLoader loader) => ToCallbackPtr<IDWriteFontCollectionLoader>(loader);

        public static void SetFactory(IDWriteFontCollectionLoader loader, IDWriteFactory factory)
        {
            var shadowPtr = ToIntPtr(loader);
            var shadow = ToShadow<IDWriteFontCollectionLoaderShadow>(shadowPtr);
            shadow._factory = factory;
        }

        private class FontCollectionLoaderVtbl : ComObjectVtbl
        {
            public FontCollectionLoaderVtbl() : base(1)
            {
                AddMethod(new CreateEnumeratorFromKeyDelegate(CreateEnumeratorFromKeyImpl));
            }


            /// <unmanaged>HRESULT IDWriteFontCollectionLoader::CreateEnumeratorFromKey([None] IDWriteFactory* factory,[In, Buffer] const void* collectionKey,[None] int collectionKeySize,[Out] IDWriteFontFileEnumerator** fontFileEnumerator)</unmanaged>
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int CreateEnumeratorFromKeyDelegate(IntPtr thisPtr, IntPtr factory, IntPtr collectionKey, int collectionKeySize, out IntPtr fontFileEnumerator);

            private static unsafe int CreateEnumeratorFromKeyImpl(IntPtr thisPtr, IntPtr factory, IntPtr collectionKey, int collectionKeySize, out IntPtr fontFileEnumerator)
            {
                fontFileEnumerator = IntPtr.Zero;
                try
                {
                    var shadow = ToShadow<IDWriteFontCollectionLoaderShadow>(thisPtr);
                    var callback = (IDWriteFontCollectionLoader)shadow.Callback;
                    Debug.Assert(factory == shadow._factory.NativePointer);
                    var enumerator = callback.CreateEnumeratorFromKey(shadow._factory, new Span<byte>(collectionKey.ToPointer(), collectionKeySize));
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
