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
            private delegate int MapOutputRectToInputRectsDelegate(IntPtr thisObject, Rect* outputRect, Rect* pInputRects, int inputRectsCount);
            private static unsafe int MapOutputRectToInputRects(IntPtr thisObject, Rect* outputRect, Rect* pInputRects, int inputRectsCount)
            {
                try
                {
                    var inputRects = new Rect[inputRectsCount];
                    Unsafe.CopyBlock(
                        Unsafe.AsPointer(ref inputRects[0]),
                        pInputRects,
                        (uint)(sizeof(Rect) * inputRectsCount));

                    ID2D1Transform @this = (ID2D1Transform)ToShadow<ID2D1TransformShadow>(thisObject).Callback;
                    @this.MapOutputRectToInputRects(*outputRect, inputRects);

                    Unsafe.CopyBlock(
                        pInputRects,
                        Unsafe.AsPointer(ref inputRects[0]),
                        (uint)(sizeof(Rect) * inputRectsCount));

                    return Result.Ok.Code;
                }
                catch (Exception __exception__)
                {
                    return Result.GetResultFromException(__exception__).Code;
                }
            }

            [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
            private delegate int MapInputRectsToOutputRectDelegate(IntPtr thisObject, void* pInputRects, void* pInputOpaqueSubRects, int inputRectCount, Rect* outputRect, Rect* outputOpaqueSubRect);
            private static unsafe int MapInputRectsToOutputRect(IntPtr thisObject, void* pInputRects, void* pInputOpaqueSubRects, int inputRectCount, Rect* outputRect, Rect* outputOpaqueSubRect)
            {
                try
                {
                    var inputRects = new Rect[inputRectCount];
                    Unsafe.CopyBlock(
                        Unsafe.AsPointer(ref inputRects[0]),
                        pInputRects,
                        (uint)(sizeof(Rect) * inputRectCount));

                    var inputOpaqueSubRects = new Rect[inputRectCount];
                    Unsafe.CopyBlock(
                        Unsafe.AsPointer(ref inputOpaqueSubRects[0]),
                        pInputOpaqueSubRects,
                        (uint)(sizeof(Rect) * inputRectCount));

                    ID2D1Transform @this = (ID2D1Transform)ToShadow<ID2D1TransformShadow>(thisObject).Callback;
                    @this.MapInputRectsToOutputRect(inputRects, inputOpaqueSubRects, out *outputRect, out *outputOpaqueSubRect);

                    return Result.Ok.Code;
                }
                catch (Exception __exception__)
                {
                    return Result.GetResultFromException(__exception__).Code;
                }
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int MapInvalidRectDelegate(IntPtr thisObject, int inputIndex, Rect* invalidInputRect, Rect* invalidOutputRect);
            private static unsafe int MapInvalidRect(IntPtr thisObject, int inputIndex, Rect* invalidInputRect, Rect* invalidOutputRect)
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
