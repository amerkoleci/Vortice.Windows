// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Direct2D1;

namespace Vortice.DirectWrite
{
    internal class IDWriteTextRendererShadow : IDWritePixelSnappingShadow
    {
        public static IntPtr ToIntPtr(IDWriteTextRenderer callback)
        {
            return ToCallbackPtr<IDWriteTextRenderer>(callback);
        }
        protected unsafe class IDWriteTextRendererVtbl : IDWritePixelSnappingVtbl
        {
            public IDWriteTextRendererVtbl(int nbMethods) : base(nbMethods + 4)
            {
                AddMethod(new DrawGlyphRunDelegate(DrawGlyphRunImpl));
                AddMethod(new DrawUnderlineDelegate(DrawUnderlineImpl));
                AddMethod(new DrawStrikethroughDelegate(DrawStrikethroughImpl));
                AddMethod(new DrawInlineObjectDelegate(DrawInlineObjectImpl));
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private unsafe delegate int DrawGlyphRunDelegate(IntPtr thisObject, IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun.__Native* glyphRunNative, GlyphRunDescription.__Native* glyphRunDescriptionNative, IntPtr clientDrawingEffect);
            private unsafe static int DrawGlyphRunImpl(IntPtr thisObject, IntPtr clientDrawingContextPtr, float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun.__Native* glyphRunNative, GlyphRunDescription.__Native* glyphRunDescriptionNative, IntPtr clientDrawingEffectPtr)
            {
                try
                {
                    var shadow = ToShadow<IDWriteTextRendererShadow>(thisObject);
                    var callback = (IDWriteTextRenderer)shadow.Callback;

                    using (var glyphRun = new GlyphRun())
                    {
                        glyphRun.__MarshalFrom(ref *glyphRunNative);

                        var glyphRunDescription = new GlyphRunDescription();
                        glyphRunDescription.__MarshalFrom(ref *glyphRunDescriptionNative);

                        callback.DrawGlyphRun(clientDrawingContextPtr,
                                              baselineOriginX,
                                              baselineOriginY,
                                              measuringMode,
                                              glyphRun,
                                              ref glyphRunDescription,
                                              clientDrawingEffectPtr == IntPtr.Zero ? null : (IUnknown)Marshal.GetObjectForIUnknown(clientDrawingEffectPtr));
                    }
                    return Result.Ok.Code;
                }
                catch (Exception ex)
                {
                    return Result.GetResultFromException(ex).Code;
                }
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private unsafe delegate int DrawUnderlineDelegate(IntPtr thisObject, IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, Underline.__Native* underlineNative, IntPtr clientDrawingEffect);
            private unsafe static int DrawUnderlineImpl(IntPtr thisObject, IntPtr clientDrawingContextPtr, float baselineOriginX, float baselineOriginY, Underline.__Native* underlineNative, IntPtr clientDrawingEffectPtr)
            {
                try
                {
                    var shadow = ToShadow<IDWriteTextRendererShadow>(thisObject);
                    var callback = (IDWriteTextRenderer)shadow.Callback;

                    var underline = new Underline();
                    underline.__MarshalFrom(ref *underlineNative);
                    callback.DrawUnderline(clientDrawingContextPtr,
                                           baselineOriginX,
                                           baselineOriginY,
                                           ref underline,
                                           clientDrawingEffectPtr == IntPtr.Zero ? null : (IUnknown)Marshal.GetObjectForIUnknown(clientDrawingEffectPtr));
                    return Result.Ok.Code;
                }
                catch (Exception ex)
                {
                    return Result.GetResultFromException(ex).Code;
                }
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private unsafe delegate int DrawStrikethroughDelegate(IntPtr thisObject, IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, Strikethrough.__Native* strikethroughNative, IntPtr clientDrawingEffect);
            private unsafe static int DrawStrikethroughImpl(IntPtr thisObject, IntPtr clientDrawingContextPtr, float baselineOriginX, float baselineOriginY, Strikethrough.__Native* strikethroughNative, IntPtr clientDrawingEffectPtr)
            {
                try
                {
                    var shadow = ToShadow<IDWriteTextRendererShadow>(thisObject);
                    var callback = (IDWriteTextRenderer)shadow.Callback;

                    var strikethrough = new Strikethrough();
                    strikethrough.__MarshalFrom(ref *strikethroughNative);
                    callback.DrawStrikethrough(clientDrawingContextPtr,
                                               baselineOriginX,
                                               baselineOriginY,
                                               ref strikethrough,
                                               clientDrawingEffectPtr == IntPtr.Zero ? null : (IUnknown)Marshal.GetObjectForIUnknown(clientDrawingEffectPtr));
                    return Result.Ok.Code;
                }
                catch (Exception ex)
                {
                    return Result.GetResultFromException(ex).Code;
                }
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int DrawInlineObjectDelegate(IntPtr thisObject, IntPtr clientDrawingContext, float originX, float originY, IntPtr inlineObject, int isSideways, int isRightToLeft, IntPtr clientDrawingEffect);
            private static int DrawInlineObjectImpl(IntPtr thisObject, IntPtr clientDrawingContextPtr, float originX, float originY, IntPtr inlineObject, int isSideways, int isRightToLeft, IntPtr clientDrawingEffectPtr)
            {
                try
                {
                    var shadow = ToShadow<IDWriteTextRendererShadow>(thisObject);
                    var callback = (IDWriteTextRenderer)shadow.Callback;

                    callback.DrawInlineObject(clientDrawingContextPtr,
                                              originX,
                                              originY,
                                              inlineObject == IntPtr.Zero ? null : new IDWriteInlineObject(inlineObject),
                                              isSideways != 0,
                                              isRightToLeft != 0,
                                              clientDrawingEffectPtr == IntPtr.Zero ? null : (IUnknown)Marshal.GetObjectForIUnknown(clientDrawingEffectPtr));
                    return Result.Ok.Code;
                }
                catch (Exception ex)
                {
                    return Result.GetResultFromException(ex).Code;
                }
            }
        }
        protected override CppObjectVtbl Vtbl { get; } = new IDWriteTextRendererVtbl(0);
    }
}
