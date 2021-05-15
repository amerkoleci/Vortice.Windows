// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectWrite
{
    partial class IDWriteTextRenderer1Shadow : Vortice.DirectWrite.IDWriteTextRendererShadow
    {
        protected internal unsafe partial class IDWriteTextRenderer1Vtbl : Vortice.DirectWrite.IDWriteTextRendererShadow.IDWriteTextRendererVtbl
        {
            public IDWriteTextRenderer1Vtbl(int numberOfCallbackMethods) : base(numberOfCallbackMethods + 4)
            {
                AddMethod(new DrawGlyphRunDelegate_(DrawGlyphRun_), 10);
                AddMethod(new DrawUnderlineDelegate_(DrawUnderline_), 11);
                AddMethod(new DrawStrikethroughDelegate_(DrawStrikethrough_), 12);
                AddMethod(new DrawInlineObjectDelegate_(DrawInlineObject_), 13);
            }

            [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.StdCall)]
            private delegate int DrawGlyphRunDelegate_(System.IntPtr thisObject, void* _clientDrawingContext, float _baselineOriginX, float _baselineOriginY, int _orientationAngle, int _measuringMode, void* _glyphRun, void* _glyphRunDescription, void* _clientDrawingEffect);
            private static unsafe int DrawGlyphRun_(System.IntPtr thisObject, void* _clientDrawingContext, float _baselineOriginX, float _baselineOriginY, int _orientationAngle, int _measuringMode, void* _glyphRun, void* _glyphRunDescription, void* _clientDrawingEffect)
            {
                try
                {
                    System.IntPtr clientDrawingContext = (System.IntPtr)_clientDrawingContext;
                    float baselineOriginX = (float)_baselineOriginX;
                    float baselineOriginY = (float)_baselineOriginY;
                    Vortice.DirectWrite.GlyphOrientationAngle orientationAngle = (Vortice.DirectWrite.GlyphOrientationAngle)_orientationAngle;
                    Vortice.DCommon.MeasuringMode measuringMode = (Vortice.DCommon.MeasuringMode)_measuringMode;
                    ref Vortice.DirectWrite.GlyphRun.__Native glyphRun_ = ref System.Runtime.CompilerServices.Unsafe.AsRef<Vortice.DirectWrite.GlyphRun.__Native>(_glyphRun);
                    Vortice.DirectWrite.GlyphRun glyphRun;
                    ref Vortice.DirectWrite.GlyphRunDescription.__Native glyphRunDescription_ = ref System.Runtime.CompilerServices.Unsafe.AsRef<Vortice.DirectWrite.GlyphRunDescription.__Native>(_glyphRunDescription);
                    Vortice.DirectWrite.GlyphRunDescription glyphRunDescription = default;
                    SharpGen.Runtime.IUnknown clientDrawingEffect = default;
                    System.IntPtr clientDrawingEffect_ = (System.IntPtr)_clientDrawingEffect;
                    IDWriteTextRenderer1 @this = (IDWriteTextRenderer1)ToShadow<Vortice.DirectWrite.IDWriteTextRenderer1Shadow>(thisObject).Callback;
                    {
                        glyphRun = new Vortice.DirectWrite.GlyphRun();
                        glyphRun.__MarshalFrom(ref glyphRun_);
                    }

                    glyphRunDescription.__MarshalFrom(ref glyphRunDescription_);
                    clientDrawingEffect = clientDrawingEffect_ != System.IntPtr.Zero ? new SharpGen.Runtime.ComObject(clientDrawingEffect_) : null;
                    @this.DrawGlyphRun(clientDrawingContext, baselineOriginX, baselineOriginY, orientationAngle, measuringMode, glyphRun, ref glyphRunDescription, clientDrawingEffect);
                    return SharpGen.Runtime.Result.Ok.Code;
                }
                catch (System.Exception __exception__)
                {
                    IDWriteTextRenderer1 @this = (IDWriteTextRenderer1)ToShadow<Vortice.DirectWrite.IDWriteTextRenderer1Shadow>(thisObject).Callback;
                    (@this as SharpGen.Runtime.IExceptionCallback)?.RaiseException(__exception__);
                    return SharpGen.Runtime.Result.GetResultFromException(__exception__).Code;
                }
            }

            [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.StdCall)]
            private delegate int DrawUnderlineDelegate_(System.IntPtr thisObject, void* _clientDrawingContext, float _baselineOriginX, float _baselineOriginY, int _orientationAngle, void* _underline, void* _clientDrawingEffect);
            private static unsafe int DrawUnderline_(System.IntPtr thisObject, void* _clientDrawingContext, float _baselineOriginX, float _baselineOriginY, int _orientationAngle, void* _underline, void* _clientDrawingEffect)
            {
                try
                {
                    System.IntPtr clientDrawingContext = (System.IntPtr)_clientDrawingContext;
                    float baselineOriginX = (float)_baselineOriginX;
                    float baselineOriginY = (float)_baselineOriginY;
                    Vortice.DirectWrite.GlyphOrientationAngle orientationAngle = (Vortice.DirectWrite.GlyphOrientationAngle)_orientationAngle;
                    ref Vortice.DirectWrite.Underline.__Native underline_ = ref System.Runtime.CompilerServices.Unsafe.AsRef<Vortice.DirectWrite.Underline.__Native>(_underline);
                    Vortice.DirectWrite.Underline underline = default;
                    SharpGen.Runtime.IUnknown clientDrawingEffect = default;
                    System.IntPtr clientDrawingEffect_ = (System.IntPtr)_clientDrawingEffect;
                    IDWriteTextRenderer1 @this = (IDWriteTextRenderer1)ToShadow<Vortice.DirectWrite.IDWriteTextRenderer1Shadow>(thisObject).Callback;
                    underline.__MarshalFrom(ref underline_);
                    clientDrawingEffect = clientDrawingEffect_ != System.IntPtr.Zero ? new SharpGen.Runtime.ComObject(clientDrawingEffect_) : null;
                    @this.DrawUnderline(clientDrawingContext, baselineOriginX, baselineOriginY, orientationAngle, ref underline, clientDrawingEffect);
                    return SharpGen.Runtime.Result.Ok.Code;
                }
                catch (System.Exception __exception__)
                {
                    IDWriteTextRenderer1 @this = (IDWriteTextRenderer1)ToShadow<Vortice.DirectWrite.IDWriteTextRenderer1Shadow>(thisObject).Callback;
                    (@this as SharpGen.Runtime.IExceptionCallback)?.RaiseException(__exception__);
                    return SharpGen.Runtime.Result.GetResultFromException(__exception__).Code;
                }
            }

            [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.StdCall)]
            private delegate int DrawStrikethroughDelegate_(System.IntPtr thisObject, void* _clientDrawingContext, float _baselineOriginX, float _baselineOriginY, int _orientationAngle, void* _strikethrough, void* _clientDrawingEffect);
            private static unsafe int DrawStrikethrough_(System.IntPtr thisObject, void* _clientDrawingContext, float _baselineOriginX, float _baselineOriginY, int _orientationAngle, void* _strikethrough, void* _clientDrawingEffect)
            {
                try
                {
                    System.IntPtr clientDrawingContext = (System.IntPtr)_clientDrawingContext;
                    float baselineOriginX = (float)_baselineOriginX;
                    float baselineOriginY = (float)_baselineOriginY;
                    Vortice.DirectWrite.GlyphOrientationAngle orientationAngle = (Vortice.DirectWrite.GlyphOrientationAngle)_orientationAngle;
                    ref Vortice.DirectWrite.Strikethrough.__Native strikethrough_ = ref System.Runtime.CompilerServices.Unsafe.AsRef<Vortice.DirectWrite.Strikethrough.__Native>(_strikethrough);
                    Vortice.DirectWrite.Strikethrough strikethrough = default;
                    SharpGen.Runtime.IUnknown clientDrawingEffect = default;
                    System.IntPtr clientDrawingEffect_ = (System.IntPtr)_clientDrawingEffect;
                    IDWriteTextRenderer1 @this = (IDWriteTextRenderer1)ToShadow<Vortice.DirectWrite.IDWriteTextRenderer1Shadow>(thisObject).Callback;
                    strikethrough.__MarshalFrom(ref strikethrough_);
                    clientDrawingEffect = clientDrawingEffect_ != System.IntPtr.Zero ? new SharpGen.Runtime.ComObject(clientDrawingEffect_) : null;
                    @this.DrawStrikethrough(clientDrawingContext, baselineOriginX, baselineOriginY, orientationAngle, ref strikethrough, clientDrawingEffect);
                    return SharpGen.Runtime.Result.Ok.Code;
                }
                catch (System.Exception __exception__)
                {
                    IDWriteTextRenderer1 @this = (IDWriteTextRenderer1)ToShadow<Vortice.DirectWrite.IDWriteTextRenderer1Shadow>(thisObject).Callback;
                    (@this as SharpGen.Runtime.IExceptionCallback)?.RaiseException(__exception__);
                    return SharpGen.Runtime.Result.GetResultFromException(__exception__).Code;
                }
            }

            [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.StdCall)]
            private delegate int DrawInlineObjectDelegate_(System.IntPtr thisObject, void* _clientDrawingContext, float _originX, float _originY, int _orientationAngle, void* _inlineObject, SharpGen.Runtime.RawBool _isSideways, SharpGen.Runtime.RawBool _isRightToLeft, void* _clientDrawingEffect);
            private static unsafe int DrawInlineObject_(System.IntPtr thisObject, void* _clientDrawingContext, float _originX, float _originY, int _orientationAngle, void* _inlineObject, SharpGen.Runtime.RawBool _isSideways, SharpGen.Runtime.RawBool _isRightToLeft, void* _clientDrawingEffect)
            {
                try
                {
                    System.IntPtr clientDrawingContext = (System.IntPtr)_clientDrawingContext;
                    float originX = (float)_originX;
                    float originY = (float)_originY;
                    Vortice.DirectWrite.GlyphOrientationAngle orientationAngle = (Vortice.DirectWrite.GlyphOrientationAngle)_orientationAngle;
                    Vortice.DirectWrite.IDWriteInlineObject inlineObject = default;
                    System.IntPtr inlineObject_ = (System.IntPtr)_inlineObject;
                    SharpGen.Runtime.RawBool isSideways = (SharpGen.Runtime.RawBool)_isSideways;
                    SharpGen.Runtime.RawBool isRightToLeft = (SharpGen.Runtime.RawBool)_isRightToLeft;
                    SharpGen.Runtime.IUnknown clientDrawingEffect = default;
                    System.IntPtr clientDrawingEffect_ = (System.IntPtr)_clientDrawingEffect;
                    IDWriteTextRenderer1 @this = (IDWriteTextRenderer1)ToShadow<Vortice.DirectWrite.IDWriteTextRenderer1Shadow>(thisObject).Callback;
                    inlineObject = inlineObject_ != System.IntPtr.Zero ? new Vortice.DirectWrite.IDWriteInlineObjectNative(inlineObject_) : null;
                    clientDrawingEffect = clientDrawingEffect_ != System.IntPtr.Zero ? new SharpGen.Runtime.ComObject(clientDrawingEffect_) : null;
                    @this.DrawInlineObject(clientDrawingContext, originX, originY, orientationAngle, inlineObject, isSideways, isRightToLeft, clientDrawingEffect);
                    return SharpGen.Runtime.Result.Ok.Code;
                }
                catch (System.Exception __exception__)
                {
                    IDWriteTextRenderer1 @this = (IDWriteTextRenderer1)ToShadow<Vortice.DirectWrite.IDWriteTextRenderer1Shadow>(thisObject).Callback;
                    (@this as SharpGen.Runtime.IExceptionCallback)?.RaiseException(__exception__);
                    return SharpGen.Runtime.Result.GetResultFromException(__exception__).Code;
                }
            }
        }

        private static readonly Vortice.DirectWrite.IDWriteTextRenderer1Shadow.IDWriteTextRenderer1Vtbl VtblInstance = new(0);
        protected override SharpGen.Runtime.CppObjectVtbl Vtbl => VtblInstance;
    }
}
