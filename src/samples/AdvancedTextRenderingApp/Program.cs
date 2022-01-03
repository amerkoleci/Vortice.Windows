// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using Vortice;
using Vortice.DCommon;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using static Vortice.Direct2D1.D2D1;
using static Vortice.DirectWrite.DWrite;

namespace AdvancedTextRenderingApp;

public class CustomColorRenderer : TextRendererBase
{
    private readonly ID2D1RenderTarget _renderTarget;
    private readonly ID2D1SolidColorBrush _defaultBrush;

    public CustomColorRenderer(ID2D1RenderTarget renderTarget, ID2D1SolidColorBrush defaultBrush)
    {
        _renderTarget = renderTarget;
        _defaultBrush = defaultBrush;
    }

    public override void DrawGlyphRun(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun glyphRun, GlyphRunDescription glyphRunDescription, IUnknown clientDrawingEffect)
    {
        ID2D1SolidColorBrush brush = _defaultBrush;
        if (clientDrawingEffect is ComObject comObject)
        {
            brush = comObject.QueryInterfaceOrNull<ID2D1SolidColorBrush>();
        }

        try
        {
            _renderTarget.DrawGlyphRun(
                new System.Drawing.PointF(baselineOriginX, baselineOriginY),
                glyphRun,
                brush,
                measuringMode);
        }
        catch
        {
        }
    }
}

public static class Program
{
    private class TestApplication : Application
    {
        private static string introText = @"Hello from Vortice, this is a long text to show some more advanced features like paragraph alignment, custom drawing...";

        private readonly ID2D1Factory1 _d2dFactory;
        private readonly IDWriteFactory1 _dwriteFactory;
        private CustomColorRenderer _textRenderer;

        private ID2D1HwndRenderTarget _renderTarget;

        private IDWriteTextFormat textFormat;
        private IDWriteTextLayout textLayout;

        // Various brushes for our example
        private ID2D1SolidColorBrush backgroundBrush;
        private ID2D1SolidColorBrush defaultBrush;
        private ID2D1SolidColorBrush greenBrush;
        private ID2D1SolidColorBrush redBrush;

        private Vortice.Mathematics.Color4 bgcolor = new(0.1f, 0.1f, 0.1f, 1.0f);

        //This is the offset where we start our text layout
        private System.Drawing.PointF offset = new(202.0f, 250.0f);
        private System.Drawing.RectangleF fullTextBackground;
        private System.Drawing.RectangleF textRegionRect;

        public TestApplication()
            : base(false)
        {
            _d2dFactory = D2D1CreateFactory<ID2D1Factory1>();
            _dwriteFactory = DWriteCreateFactory<IDWriteFactory1>();


            CreateResources();
            _textRenderer = new CustomColorRenderer(_renderTarget, defaultBrush);

            textFormat = _dwriteFactory.CreateTextFormat("Arial", FontWeight.Regular, FontStyle.Normal, 16.0f);
            textLayout = _dwriteFactory.CreateTextLayout(introText, textFormat, 300.0f, 200.0f);

            // Apply various modifications to text
            textLayout.SetUnderline(true, new TextRange(0, 5));
            textLayout.SetDrawingEffect(greenBrush, new TextRange(10, 20));
            textLayout.SetFontSize(24.0f, new TextRange(6, 4));
            textLayout.SetFontFamilyName("Comic Sans MS", new TextRange(11, 7));

            //Measure full layout
            var textSize = textLayout.Metrics;
            fullTextBackground = new(textSize.Left + offset.X, textSize.Top + offset.Y, textSize.Width, textSize.Height);

            var metrics = textLayout.HitTestTextRange(53, 4, 0.0f, 0.0f)[0];
            textRegionRect = new(metrics.Left + offset.X, metrics.Top + offset.Y, metrics.Width, metrics.Height);
        }

        public override void Dispose()
        {
            if (defaultBrush != null) { defaultBrush.Dispose(); }
            if (greenBrush != null) { greenBrush.Dispose(); }
            if (redBrush != null) { redBrush.Dispose(); }
            if (backgroundBrush != null) { backgroundBrush.Dispose(); }

            _textRenderer.Dispose();

            _renderTarget.Dispose();
            _d2dFactory.Dispose();
            _dwriteFactory.Dispose();
        }

        private void CreateResources()
        {
            if (_renderTarget != null) { _renderTarget.Dispose(); }
            if (defaultBrush != null) { defaultBrush.Dispose(); }
            if (greenBrush != null) { greenBrush.Dispose(); }
            if (redBrush != null) { redBrush.Dispose(); }
            if (backgroundBrush != null) { backgroundBrush.Dispose(); }


            HwndRenderTargetProperties wtp = new();
            wtp.Hwnd = MainWindow!.Handle;
            wtp.PixelSize = MainWindow!.ClientSize;
            wtp.PresentOptions = PresentOptions.Immediately;
            _renderTarget = _d2dFactory.CreateHwndRenderTarget(
                new RenderTargetProperties(),
                wtp);

            defaultBrush = _renderTarget.CreateSolidColorBrush(System.Drawing.Color.White);
            greenBrush = _renderTarget.CreateSolidColorBrush(System.Drawing.Color.Green);
            redBrush = _renderTarget.CreateSolidColorBrush(System.Drawing.Color.Red);
            backgroundBrush = _renderTarget.CreateSolidColorBrush(new Vortice.Mathematics.Color4(0.3f, 0.3f, 0.3f, 0.5f));

            //_textRenderer.AssignResources(renderTarget, defaultBrush);
        }

        protected override void InitializeBeforeRun()
        {
        }

        protected override void OnKeyboardEvent(KeyboardKey key, bool pressed)
        {
        }

        protected override void OnDraw(int width, int height)
        {
            _renderTarget.BeginDraw();
            _renderTarget.Clear(bgcolor);

            _renderTarget.FillRectangle(fullTextBackground, backgroundBrush);
            _renderTarget.FillRectangle(textRegionRect, redBrush);

            textLayout.Draw(_textRenderer, offset.X, offset.Y);

            try
            {
                _renderTarget.EndDraw();
            }
            catch
            {
                CreateResources();
            }
        }
    }

    public static void Main()
    {
        using var app = new TestApplication();
        app.Run();
    }
}
