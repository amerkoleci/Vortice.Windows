// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;
using Vortice.Direct2D1;

namespace Vortice.DirectWrite
{
    internal class IDWriteTextRenderer1Shadow : IDWriteTextRendererShadow
    {
        protected unsafe class IDWriteTextRenderer1Vtbl : IDWriteTextRendererVtbl
        {
            public IDWriteTextRenderer1Vtbl(int numberOfCallbackMethods) : base(numberOfCallbackMethods + 4)
            {
                AddMethod(new DrawGlyphRunDelegate(DrawGlyphRun));
                AddMethod(new DrawUnderlineDelegate(DrawUnderline));
                AddMethod(new DrawStrikethroughDelegate(DrawStrikethrough));
                AddMethod(new DrawInlineObjectDelegate(DrawInlineObject));
            }

            [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
            private delegate int DrawGlyphRunDelegate(IntPtr thisObject, IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, GlyphOrientationAngle orientationAngle, MeasuringMode measuringMode, GlyphRun.__Native* glyphRunNative, GlyphRunDescription.__Native* glyphRunDescriptionNative, IntPtr clientDrawingEffectPtr);
            private static unsafe int DrawGlyphRun(IntPtr thisObject, IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, GlyphOrientationAngle orientationAngle, MeasuringMode measuringMode, GlyphRun.__Native* glyphRunNative, GlyphRunDescription.__Native* glyphRunDescriptionNative, IntPtr clientDrawingEffectPtr)
            {
                try
                {
                    var shadow = ToShadow<IDWriteTextRenderer1Shadow>(thisObject);
                    var callback = (IDWriteTextRenderer1)shadow.Callback;

                    using (var glyphRun = new GlyphRun())
                    {
                        glyphRun.__MarshalFrom(ref *glyphRunNative);

                        var glyphRunDescription = new Vortice.DirectWrite.GlyphRunDescription();
                        glyphRunDescription.__MarshalFrom(ref *glyphRunDescriptionNative);

                        var clientDrawingEffect = clientDrawingEffectPtr == IntPtr.Zero ? null : new ComObject(clientDrawingEffectPtr);

                        callback.DrawGlyphRun(clientDrawingContext, baselineOriginX, baselineOriginY, orientationAngle, measuringMode, glyphRun, ref glyphRunDescription, clientDrawingEffect);
                    }
                    return Result.Ok.Code;
                }
                catch (Exception ex)
                {
                    return Result.GetResultFromException(ex).Code;
                }
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int DrawUnderlineDelegate(IntPtr thisObject, IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, GlyphOrientationAngle orientationAngle, Underline.__Native* underlineNative, IntPtr clientDrawingEffectPtr);
            private static unsafe int DrawUnderline(IntPtr thisObject, IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, GlyphOrientationAngle orientationAngle, Underline.__Native* underlineNative, IntPtr clientDrawingEffectPtr)
            {
                try
                {
                    var shadow = ToShadow<IDWriteTextRenderer1Shadow>(thisObject);
                    var callback = (IDWriteTextRenderer1)shadow.Callback;

                    Underline underline = default;
                    underline.__MarshalFrom(ref *underlineNative);

                    var clientDrawingEffect = clientDrawingEffectPtr == IntPtr.Zero ? null : new ComObject(clientDrawingEffectPtr);

                    callback.DrawUnderline(clientDrawingContext, baselineOriginX, baselineOriginY, orientationAngle, ref underline, clientDrawingEffect);
                    return Result.Ok.Code;
                }
                catch (Exception ex)
                {
                    return Result.GetResultFromException(ex).Code;
                }
            }

            [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
            private delegate int DrawStrikethroughDelegate(IntPtr thisObject, IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, GlyphOrientationAngle orientationAngle, Strikethrough.__Native* strikethroughNative, IntPtr clientDrawingEffectPtr);
            private static unsafe int DrawStrikethrough(IntPtr thisObject, IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, GlyphOrientationAngle orientationAngle, Strikethrough.__Native* strikethroughNative, IntPtr clientDrawingEffectPtr)
            {
                try
                {
                    var shadow = ToShadow<IDWriteTextRenderer1Shadow>(thisObject);
                    var callback = (IDWriteTextRenderer1)shadow.Callback;

                    Strikethrough strikethrough = default;
                    strikethrough.__MarshalFrom(ref *strikethroughNative);

                    var clientDrawingEffect = clientDrawingEffectPtr == IntPtr.Zero ? null : new ComObject(clientDrawingEffectPtr);

                    callback.DrawStrikethrough(clientDrawingContext, baselineOriginX, baselineOriginY, orientationAngle, ref strikethrough, clientDrawingEffect);
                    return Result.Ok.Code;
                }
                catch (Exception ex)
                {
                    return Result.GetResultFromException(ex).Code;
                }
            }

            [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
            private delegate int DrawInlineObjectDelegate(IntPtr thisObject, IntPtr clientDrawingContext, float originX, float originY, GlyphOrientationAngle orientationAngle, IntPtr inlineObjectPtr, RawBool isSideways, RawBool isRightToLeft, IntPtr clientDrawingEffectPtr);
            private static unsafe int DrawInlineObject(IntPtr thisObject, IntPtr clientDrawingContext, float originX, float originY, GlyphOrientationAngle orientationAngle, IntPtr inlineObjectPtr, RawBool isSideways, RawBool isRightToLeft, IntPtr clientDrawingEffectPtr)
            {
                try
                {
                    var shadow = ToShadow<IDWriteTextRenderer1Shadow>(thisObject);
                    var callback = (IDWriteTextRenderer1)shadow.Callback;

                    var inlineObject = inlineObjectPtr == IntPtr.Zero ? null : new IDWriteInlineObjectNative(inlineObjectPtr);
                    var clientDrawingEffect = clientDrawingEffectPtr == IntPtr.Zero ? null : new ComObject(clientDrawingEffectPtr);

                    callback.DrawInlineObject(clientDrawingContext, originX, originY, orientationAngle, inlineObject, isSideways, isRightToLeft, clientDrawingEffect);
                    return Result.Ok.Code;
                }
                catch (Exception ex)
                {
                    return Result.GetResultFromException(ex).Code;
                }
            }
        }

        protected override CppObjectVtbl Vtbl
        {
            get;
        }

        = new IDWriteTextRenderer1Vtbl(0);
    }
}
