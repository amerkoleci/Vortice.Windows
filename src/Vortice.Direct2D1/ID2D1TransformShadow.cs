// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.Direct2D1
{
    internal class ID2D1TransformShadow : ID2D1TransformNodeShadow
    {
        protected unsafe class ID2D1TransformVtbl : ID2D1TransformNodeVtbl
        {
            public ID2D1TransformVtbl(int numberOfCallbackMethods) : base(numberOfCallbackMethods + 3)
            {
                AddMethod(new MapOutputRectToInputRectsDelegate(MapOutputRectToInputRects));
                AddMethod(new MapInputRectsToOutputRectDelegate(MapInputRectsToOutputRect));
                AddMethod(new MapInvalidRectDelegate(MapInvalidRect));
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int MapOutputRectToInputRectsDelegate(IntPtr thisObject, RawRect* outputRect, RawRect* pInputRects, int inputRectsCount);
            private static unsafe int MapOutputRectToInputRects(IntPtr thisObject, RawRect* outputRect, RawRect* pInputRects, int inputRectsCount)
            {
                try
                {
                    var inputRects = new RawRect[inputRectsCount];
                    fixed (void* rectsPtr = &inputRects[0])
                    {
                        Unsafe.CopyBlock(
                            rectsPtr,
                            pInputRects,
                            (uint)(sizeof(RawRect) * inputRectsCount));
                    

                        ID2D1Transform @this = (ID2D1Transform)ToShadow<ID2D1TransformShadow>(thisObject).Callback;
                        @this.MapOutputRectToInputRects(*outputRect, inputRects);

                        Unsafe.CopyBlock(
                            pInputRects,
                            rectsPtr,
                            (uint)(sizeof(RawRect) * inputRectsCount));

                        return Result.Ok.Code;
                    }
                }
                catch (Exception __exception__)
                {
                    return Result.GetResultFromException(__exception__).Code;
                }
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int MapInputRectsToOutputRectDelegate(IntPtr thisObject, void* pInputRects, void* pInputOpaqueSubRects, int inputRectCount, RawRect* outputRect, RawRect* outputOpaqueSubRect);
            private static unsafe int MapInputRectsToOutputRect(IntPtr thisObject, void* pInputRects, void* pInputOpaqueSubRects, int inputRectCount, RawRect* outputRect, RawRect* outputOpaqueSubRect)
            {
                try
                {
                    var inputRects = new RawRect[inputRectCount];
                    fixed (void* rectsPtr = &inputRects[0])
                    {
                        Unsafe.CopyBlock(
                            rectsPtr,
                            pInputRects,
                            (uint)(sizeof(RawRect) * inputRectCount));

                        var inputOpaqueSubRects = new RawRect[inputRectCount];
                        fixed (void* opaqueSubRectsPtr = &inputOpaqueSubRects[0])
                        {
                            Unsafe.CopyBlock(
                                opaqueSubRectsPtr,
                                pInputOpaqueSubRects,
                                (uint)(sizeof(RawRect) * inputRectCount));

                            ID2D1Transform @this = (ID2D1Transform)ToShadow<ID2D1TransformShadow>(thisObject).Callback;
                            @this.MapInputRectsToOutputRect(inputRects, inputOpaqueSubRects, out *outputRect, out *outputOpaqueSubRect);

                            return Result.Ok.Code;
                        }
                    }
                }
                catch (Exception __exception__)
                {
                    return Result.GetResultFromException(__exception__).Code;
                }
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int MapInvalidRectDelegate(IntPtr thisObject, int inputIndex, RawRect* invalidInputRect, RawRect* invalidOutputRect);
            private static unsafe int MapInvalidRect(IntPtr thisObject, int inputIndex, RawRect* invalidInputRect, RawRect* invalidOutputRect)
            {
                try
                {
                    ID2D1Transform @this = (ID2D1Transform)ToShadow<ID2D1TransformShadow>(thisObject).Callback;
                    @this.MapInvalidRect(inputIndex, *invalidInputRect, out *invalidOutputRect);
                    return Result.Ok.Code;
                }
                catch (Exception __exception__)
                {
                    return Result.GetResultFromException(__exception__).Code;
                }
            }
        }

        protected override CppObjectVtbl Vtbl { get; } = new ID2D1TransformVtbl(0);
    }
}
