// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.IO;
using Vortice;
using Vortice.Mathematics;
using Vortice.WIC;

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
            using (var app = new TestApplication())
            {
                app.Run();
            }
        }

        public static Size GetTextureDimensions(IWICImagingFactory factory, Stream stream)
        {
            IWICStream wicStream = factory.CreateStream();
            wicStream.Initialize(stream);

            using (IWICBitmapDecoder decoder = factory.CreateDecoderFromStream(wicStream, DecodeOptions.CacheOnDemand))
            {
                var frame = decoder.GetFrame(0);
                using (frame)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    return frame.Size;
                }
            }
        }
    }
}
