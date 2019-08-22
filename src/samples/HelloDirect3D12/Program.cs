// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.IO;
using Vortice;
using Vortice.DirectX.Direct2D;
using Vortice.DirectX.DXGI;
using Vortice.DirectX.WIC;
using Vortice.Interop;

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

            var rectangleGeometry = d2dFactory.CreateRoundedRectangleGeometry(new RoundedRectangle(new RectangleF(128, 128, width - 128 * 2, height - 128 * 2), 32, 32));

            var wicBitmap = wicFactory.CreateBitmap(width, height, Vortice.DirectX.WIC.PixelFormat.Format32bppBGR, BitmapCreateCacheOption.CacheOnLoad);

            var renderTargetProperties = new RenderTargetProperties(new Vortice.DirectX.Direct2D.PixelFormat(Format.Unknown, Vortice.DirectX.Direct2D.AlphaMode.Unknown));

            var d2dRenderTarget = d2dFactory.CreateWicBitmapRenderTarget(wicBitmap, renderTargetProperties);

            var solidColorBrush = d2dRenderTarget.CreateSolidColorBrush(new RawColor4(1.0f, 1.0f, 1.0f, 1.0f));
            d2dRenderTarget.BeginDraw();
            d2dRenderTarget.Clear(new RawColor4(0.0f, 0.0f, 0.0f, 1.0f));
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
                    var pixelFormatGuid = Vortice.DirectX.WIC.PixelFormat.FormatDontCare;
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
