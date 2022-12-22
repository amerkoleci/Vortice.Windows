// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;
using SharpGen.Runtime;
using SharpGen.Runtime.Diagnostics;
using Vortice;
using Vortice.Direct3D11;
using Vortice.DXGI;
using Vortice.Mathematics;
using Vortice.WIC;
using WICPixelFormat = Vortice.WIC.PixelFormat;

namespace HelloDirect3D11;

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
                _graphicsDevice = new D3D11GraphicsDevice(new Size(800, 600));
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
            ID3D11Texture2D source = Headless ? d3d11GraphicsDevice!.OffscreenTexture! : d3d11GraphicsDevice!.BackBufferTexture!;

            path = Path.Combine(AppContext.BaseDirectory, path);

            using (ID3D11Texture2D staging = d3d11GraphicsDevice!.CaptureTexture(source))
            {
                staging.DebugName = "STAGING";

                var textureDesc = staging!.Description;

                // Determine source format's WIC equivalent
                Guid pfGuid = default;
                bool sRGB = false;
                switch (textureDesc.Format)
                {
                    case Format.R32G32B32A32_Float:
                        pfGuid = WICPixelFormat.Format128bppRGBAFloat;
                        break;

                    case Format.R16G16B16A16_Float:
                        pfGuid = WICPixelFormat.Format64bppRGBAHalf;
                        break;
                    case Format.R16G16B16A16_UNorm:
                        pfGuid = WICPixelFormat.Format64bppRGBA;
                        break;

                    // DXGI 1.1
                    case Format.R10G10B10_Xr_Bias_A2_UNorm:
                        pfGuid = WICPixelFormat.Format32bppRGBA1010102XR;
                        break;
                    case Format.R10G10B10A2_UNorm:
                        pfGuid = WICPixelFormat.Format32bppRGBA1010102;
                        break;
                    case Format.B5G5R5A1_UNorm:
                        pfGuid = WICPixelFormat.Format16bppBGRA5551;
                        break;
                    case Format.B5G6R5_UNorm:
                        pfGuid = WICPixelFormat.Format16bppBGR565;
                        break;
                    case Format.R32_Float:
                        pfGuid = WICPixelFormat.Format32bppGrayFloat;
                        break;
                    case Format.R16_Float:
                        pfGuid = WICPixelFormat.Format16bppGrayHalf;
                        break;
                    case Format.R16_UNorm:
                        pfGuid = WICPixelFormat.Format16bppGray;
                        break;
                    case Format.R8_UNorm:
                        pfGuid = WICPixelFormat.Format8bppGray;
                        break;
                    case Format.A8_UNorm:
                        pfGuid = WICPixelFormat.Format8bppAlpha;
                        break;

                    case Format.R8G8B8A8_UNorm:
                        pfGuid = WICPixelFormat.Format32bppRGBA;
                        break;

                    case Format.R8G8B8A8_UNorm_SRgb:
                        pfGuid = WICPixelFormat.Format32bppRGBA;
                        sRGB = true;
                        break;

                    case Format.B8G8R8A8_UNorm: // DXGI 1.1
                        pfGuid = WICPixelFormat.Format32bppBGRA;
                        break;

                    case Format.B8G8R8A8_UNorm_SRgb: // DXGI 1.1
                        pfGuid = WICPixelFormat.Format32bppBGRA;
                        sRGB = true;
                        break;

                    case Format.B8G8R8X8_UNorm: // DXGI 1.1
                        pfGuid = WICPixelFormat.Format32bppBGR;
                        break;

                    case Format.B8G8R8X8_UNorm_SRgb: // DXGI 1.1
                        pfGuid = WICPixelFormat.Format32bppBGR;
                        sRGB = true;
                        break;

                    default:
                        Console.WriteLine($"ERROR: ScreenGrab does not support all DXGI formats ({textureDesc.Format})");
                        return;
                }

                using var wicFactory = new IWICImagingFactory2();
                //using IWICBitmapDecoder decoder = wicFactory.CreateDecoderFromFileName(path);

                //using Stream stream = File.OpenWrite(path);
                //using IWICStream wicStream = wicFactory.CreateStream(stream);
                using IWICStream wicStream = wicFactory.CreateStream(path, FileAccess.Write);
                using IWICBitmapEncoder encoder = wicFactory.CreateEncoder(format, wicStream);
                // Create a Frame encoder
                using IWICBitmapFrameEncode frame = encoder.CreateNewFrame(out SharpGen.Runtime.Win32.IPropertyBag2? props);
                frame.Initialize(props);
                frame.SetSize(textureDesc.Width, textureDesc.Height);
                frame.SetResolution(72, 72);

                // Screenshots don't typically include the alpha channel of the render target
                Guid targetGuid;
                switch (textureDesc.Format)
                {
                    case Format.R32G32B32A32_Float:
                    case Format.R16G16B16A16_Float:
                        //if (IsWIC2())
                        {
                            targetGuid = WICPixelFormat.Format96bppRGBFloat;
                        }
                        //else
                        //{
                        //    targetGuid = WICPixelFormat.Format24bppBGR;
                        //}
                        break;

                    case Format.R16G16B16A16_UNorm:
                        targetGuid = WICPixelFormat.Format48bppBGR;
                        break;
                    case Format.B5G5R5A1_UNorm:
                        targetGuid = WICPixelFormat.Format16bppBGR555;
                        break;
                    case Format.B5G6R5_UNorm:
                        targetGuid = WICPixelFormat.Format16bppBGR565;
                        break;

                    case Format.R32_Float:
                    case Format.R16_Float:
                    case Format.R16_UNorm:
                    case Format.R8_UNorm:
                    case Format.A8_UNorm:
                        targetGuid = WICPixelFormat.Format8bppGray;
                        break;

                    default:
                        targetGuid = WICPixelFormat.Format24bppBGR;
                        break;
                }
                frame.SetPixelFormat(targetGuid);

                ID3D11DeviceContext1 context = d3d11GraphicsDevice!.DeviceContext;

                const bool native = false;
                if (native)
                {
                    MappedSubresource mappedSubresource = context.Map(staging, 0, MapMode.Read);
                    int imageSize = mappedSubresource.RowPitch * textureDesc.Height;
                    if (targetGuid != pfGuid)
                    {
                        // Conversion required to write
                        using (IWICBitmap bitmapSource = wicFactory.CreateBitmapFromMemory(
                            textureDesc.Width,
                            textureDesc.Height,
                            pfGuid,
                            mappedSubresource.RowPitch,
                            imageSize,
                            mappedSubresource.DataPointer))
                        {
                            using (IWICFormatConverter formatConverter = wicFactory.CreateFormatConverter())
                            {
                                if (!formatConverter.CanConvert(pfGuid, targetGuid))
                                {
                                    context.Unmap(staging, 0);
                                    return;
                                }

                                formatConverter.Initialize(bitmapSource, targetGuid, BitmapDitherType.None, null, 0, BitmapPaletteType.MedianCut);
                                frame.WriteSource(formatConverter, new Rectangle(0, 0, textureDesc.Width, textureDesc.Height));
                            }
                        }
                    }
                    else
                    {
                        // No conversion required
                        frame.WritePixels(textureDesc.Height, mappedSubresource.RowPitch, imageSize, mappedSubresource.DataPointer);
                    }
                }
                else
                {
                    int stride = WICPixelFormat.GetStride(pfGuid, textureDesc.Width);
                    ReadOnlySpan<Vortice.Mathematics.Color> colors = context.MapReadOnly<Vortice.Mathematics.Color>(staging);

                    if (targetGuid != pfGuid)
                    {
                        // Conversion required to write
                        using (IWICBitmap bitmapSource = wicFactory.CreateBitmapFromMemory(
                            textureDesc.Width,
                            textureDesc.Height,
                            pfGuid,
                            colors,
                            stride))
                        {
                            using (IWICFormatConverter formatConverter = wicFactory.CreateFormatConverter())
                            {
                                if (!formatConverter.CanConvert(pfGuid, targetGuid))
                                {
                                    context.Unmap(staging, 0);
                                    return;
                                }

                                formatConverter.Initialize(bitmapSource, targetGuid, BitmapDitherType.None, null, 0, BitmapPaletteType.MedianCut);
                                frame.WriteSource(formatConverter, new Rectangle(0, 0, textureDesc.Width, textureDesc.Height));
                            }
                        }
                    }
                    else
                    {
                        // No conversion required
                        frame.WritePixels(textureDesc.Height, stride, colors);
                    }
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

        using TestApplication app = new(headless: false);
        app.Run();
    }
}
