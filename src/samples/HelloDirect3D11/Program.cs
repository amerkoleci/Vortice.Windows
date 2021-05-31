// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.IO;
using SharpGen.Runtime;
using SharpGen.Runtime.Diagnostics;
using Vortice;
using Vortice.Direct3D11;
using Vortice.Mathematics;
using Vortice.WIC;
using WICPixelFormat = Vortice.WIC.PixelFormat;

namespace HelloDirect3D11
{
    public static class Program
    {
        private class TestApplication : Application
        {
            private bool _screenshot;

            public TestApplication(bool headless = false)
                : base(headless)
            {
            }

            protected override void InitializeBeforeRun()
            {
                if (Headless)
                {
                    _graphicsDevice = new D3D11GraphicsDevice(new System.Drawing.Size(800, 600));
                    _screenshot = true;
                }
                else
                {
                    _graphicsDevice = new D3D11GraphicsDevice(MainWindow!);
                }
            }

            protected override void OnKeyboardEvent(KeyboardKey key, bool pressed)
            {
                if (key == KeyboardKey.P && pressed)
                {
                    _screenshot = true;
                }
            }

            protected override void OnDraw(int width, int height)
            {
                ((D3D11GraphicsDevice)_graphicsDevice!).DeviceContext.Flush();

                if (_screenshot)
                {
                    SaveScreenshot("Screenshot.jpg");
                    _screenshot = false;
                }
            }

            private unsafe void SaveScreenshot(string path, ContainerFormat format = ContainerFormat.Jpeg)
            {
                var d3d11GraphicsDevice = ((D3D11GraphicsDevice)_graphicsDevice!);
                ID3D11Texture2D source = Headless ? d3d11GraphicsDevice!.OffscreenTexture : d3d11GraphicsDevice!.BackBufferTexture;

                using (ID3D11Texture2D staging = d3d11GraphicsDevice!.CaptureTexture(source))
                {
                    staging.DebugName = "STAGING";

                    var textureDesc = staging!.Description;

                    // Determine source format's WIC equivalent
                    Guid pfGuid = default;
                    bool sRGB = false;
                    switch (textureDesc.Format)
                    {
                        case Vortice.DXGI.Format.R32G32B32A32_Float:
                            pfGuid = WICPixelFormat.Format128bppRGBAFloat;
                            break;

                        //case DXGI_FORMAT_R16G16B16A16_FLOAT: pfGuid = GUID_WICPixelFormat64bppRGBAHalf; break;
                        //case DXGI_FORMAT_R16G16B16A16_UNORM: pfGuid = GUID_WICPixelFormat64bppRGBA; break;
                        //case DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM: pfGuid = GUID_WICPixelFormat32bppRGBA1010102XR; break; // DXGI 1.1
                        //case DXGI_FORMAT_R10G10B10A2_UNORM: pfGuid = GUID_WICPixelFormat32bppRGBA1010102; break;
                        //case DXGI_FORMAT_B5G5R5A1_UNORM: pfGuid = GUID_WICPixelFormat16bppBGRA5551; break;
                        //case DXGI_FORMAT_B5G6R5_UNORM: pfGuid = GUID_WICPixelFormat16bppBGR565; break;
                        //case DXGI_FORMAT_R32_FLOAT: pfGuid = GUID_WICPixelFormat32bppGrayFloat; break;
                        //case DXGI_FORMAT_R16_FLOAT: pfGuid = GUID_WICPixelFormat16bppGrayHalf; break;
                        //case DXGI_FORMAT_R16_UNORM: pfGuid = GUID_WICPixelFormat16bppGray; break;
                        //case DXGI_FORMAT_R8_UNORM: pfGuid = GUID_WICPixelFormat8bppGray; break;
                        //case DXGI_FORMAT_A8_UNORM: pfGuid = GUID_WICPixelFormat8bppAlpha; break;

                        case Vortice.DXGI.Format.R8G8B8A8_UNorm:
                            pfGuid = WICPixelFormat.Format32bppRGBA;
                            break;

                        case Vortice.DXGI.Format.R8G8B8A8_UNorm_SRgb:
                            pfGuid = WICPixelFormat.Format32bppRGBA;
                            sRGB = true;
                            break;

                        case Vortice.DXGI.Format.B8G8R8A8_UNorm: // DXGI 1.1
                            pfGuid = WICPixelFormat.Format32bppBGRA;
                            break;

                        case Vortice.DXGI.Format.B8G8R8A8_UNorm_SRgb: // DXGI 1.1
                            pfGuid = WICPixelFormat.Format32bppBGRA;
                            sRGB = true;
                            break;

                        case Vortice.DXGI.Format.B8G8R8X8_UNorm: // DXGI 1.1
                            pfGuid = WICPixelFormat.Format32bppBGR;
                            break;

                        case Vortice.DXGI.Format.B8G8R8X8_UNorm_SRgb: // DXGI 1.1
                            pfGuid = WICPixelFormat.Format32bppBGR;
                            sRGB = true;
                            break;

                        default:
                            //Console.WriteLine("ERROR: ScreenGrab does not support all DXGI formats (%u). Consider using DirectXTex.\n", static_cast<uint32_t>(desc.Format));
                            return;
                    }

                    // Screenshots don't typically include the alpha channel of the render target
                    Guid targetGuid = default;
                    switch (textureDesc.Format)
                    {
                        case Vortice.DXGI.Format.R32G32B32A32_Float:
                        case Vortice.DXGI.Format.R16G16B16A16_Float:
                            //if (_IsWIC2())
                            {
                                targetGuid = WICPixelFormat.Format96bppRGBFloat;
                            }
                            //else
                            //{
                            //    targetGuid = WICPixelFormat.Format24bppBGR;
                            //}
                            break;

                        case Vortice.DXGI.Format.R16G16B16A16_UNorm:
                            targetGuid = WICPixelFormat.Format48bppBGR;
                            break;

                        case Vortice.DXGI.Format.B5G5R5A1_UNorm:
                            targetGuid = WICPixelFormat.Format16bppBGR555;
                            break;

                        case Vortice.DXGI.Format.B5G6R5_UNorm:
                            targetGuid = WICPixelFormat.Format16bppBGR565;
                            break;

                        case Vortice.DXGI.Format.R32_Float:
                        case Vortice.DXGI.Format.R16_Float:
                        case Vortice.DXGI.Format.R16_UNorm:
                        case Vortice.DXGI.Format.R8_UNorm:
                        case Vortice.DXGI.Format.A8_UNorm:
                            targetGuid = WICPixelFormat.Format8bppGray;
                            break;

                        default:
                            targetGuid = WICPixelFormat.Format24bppBGR;
                            break;
                    }

                    using var wicFactory = new IWICImagingFactory();
                    using IWICBitmapDecoder decoder = wicFactory.CreateDecoderFromFileName(path);


                    using Stream stream = File.OpenWrite(path);
                    using IWICStream wicStream = wicFactory.CreateStream(stream);
                    using IWICBitmapEncoder encoder = wicFactory.CreateEncoder(format, wicStream);
                    // Create a Frame encoder
                    var props = new SharpGen.Runtime.Win32.PropertyBag();
                    var frame = encoder.CreateNewFrame(props);
                    frame.Initialize(props);
                    frame.SetSize(textureDesc.Width, textureDesc.Height);
                    frame.SetResolution(72, 72);
                    frame.SetPixelFormat(targetGuid);

                    var context = d3d11GraphicsDevice!.DeviceContext;
                    //var mapped = context.Map(staging, 0, MapMode.Read, MapFlags.None);
                    Span<Color> colors = context.Map<Color>(staging, 0, 0, MapMode.Read, MapFlags.None);

                    // Check conversion
                    if (targetGuid != pfGuid)
                    {
                        // Conversion required to write
                        using (IWICBitmap bitmapSource = wicFactory.CreateBitmapFromMemory(
                            textureDesc.Width,
                            textureDesc.Height,
                            pfGuid,
                            colors))
                        {
                            using (IWICFormatConverter formatConverter = wicFactory.CreateFormatConverter())
                            {
                                if (!formatConverter.CanConvert(pfGuid, targetGuid))
                                {
                                    context.Unmap(staging, 0);
                                    return;
                                }

                                formatConverter.Initialize(bitmapSource, targetGuid, BitmapDitherType.None, null, 0, BitmapPaletteType.MedianCut);
                                frame.WriteSource(formatConverter, new System.Drawing.Rectangle(0, 0, textureDesc.Width, textureDesc.Height));
                            }
                        }
                    }
                    else
                    {
                        // No conversion required
                        int stride = WICPixelFormat.GetStride(pfGuid, textureDesc.Width);
                        frame.WritePixels(textureDesc.Height, stride, colors);
                    }

                    context.Unmap(staging, 0);
                    frame.Commit();
                    encoder.Commit();
                }
            }
        }

        public static void Main()
        {
#if DEBUG
            Configuration.EnableObjectTracking = true;
#endif

            using var app = new TestApplication(headless: false);
            app.Run();
#if DEBUG
            Console.WriteLine(ObjectTracker.ReportActiveObjects());
#endif
        }
    }
}
