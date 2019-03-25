// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Numerics;
using SharpDirect2D;
using SharpDirect2D.WIC;
using SharpDXGI;
using Vortice;

namespace HelloDirect3D11
{
    public static class Program
    {
        private class TestApplication : Application
        {
            public TestApplication()
                : base(true)
            {
            }
        }

        public static void Main()
        {
            var wicFactory = new IWICImagingFactory();
            D2D1.D2D1CreateFactory(FactoryType.SingleThreaded, out ID2D1Factory d2dFactory);

            const string fileName = "output.jpg";
            const int width = 512;
            const int height = 512;

            var rectangleGeometry = d2dFactory.CreateRoundedRectangleGeometry(new RoundedRectangle() { RadiusX = 32, RadiusY = 32, Rect = new RawRectangleF(128, 128, width - 128 * 2, height - 128 * 2) });

            var wicBitmap = wicFactory.CreateBitmap(width, height, SharpDirect2D.WIC.PixelFormat.Format32bppBGR, BitmapCreateCacheOption.CacheOnLoad);

            var renderTargetProperties = new RenderTargetProperties(new SharpDirect2D.PixelFormat(Format.Unknown, SharpDirect2D.AlphaMode.Unknown));

            var d2dRenderTarget = d2dFactory.CreateWicBitmapRenderTarget(wicBitmap, renderTargetProperties);

            var solidColorBrush = d2dRenderTarget.CreateSolidColorBrush(new Vector4(1.0f));
            d2dRenderTarget.BeginDraw();
            d2dRenderTarget.Clear(new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
            d2dRenderTarget.FillGeometry(rectangleGeometry, solidColorBrush, null);
            d2dRenderTarget.EndDraw();

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (var stream = wicFactory.CreateStream())
            {
                stream.Initialize(fileName, FileAccess.Write);

                // Initialize a Jpeg encoder with this stream
                using (var encoder = wicFactory.CreateEncoder(ContainerFormat.Jpeg))
                {
                    encoder.Initialize(stream);

                    // Create a Frame encoder
                    var props = new SharpGen.Runtime.Win32.PropertyBag(IntPtr.Zero);
                    var bitmapFrameEncode = encoder.CreateNewFrame(props);
                    bitmapFrameEncode.Initialize(null);
                    bitmapFrameEncode.SetSize(width, height);
                    var pixelFormatGuid = SharpDirect2D.WIC.PixelFormat.FormatDontCare;
                    bitmapFrameEncode.SetPixelFormat(ref pixelFormatGuid);
                    bitmapFrameEncode.WriteSource(wicBitmap, null);

                    bitmapFrameEncode.Commit();
                    encoder.Commit();
                }
            }

            using (var app = new TestApplication())
            {
                app.Run();
            }
        }
    }
}
