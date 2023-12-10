# Vortice.Windows

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/amerkoleci/Vortice.Windows/blob/master/LICENSE)
[![Build status](https://github.com/amerkoleci/Vortice.Windows/workflows/Build/badge.svg)](https://github.com/amerkoleci/Vortice.Windows/actions)
[![NuGet](https://img.shields.io/nuget/v/Vortice.Direct3D12.svg)](https://www.nuget.org/packages?q=Tags%3A%22Vortice.Windows%22,%22Direct3D12%22)

**Vortice.Windows** is a collection of Win32 and UWP libraries with bindings support for [DXGI](https://docs.microsoft.com/en-us/windows/desktop/direct3ddxgi/d3d10-graphics-programming-guide-dxgi), [WIC](https://docs.microsoft.com/en-us/windows/desktop/wic/-wic-lh), [DirectWrite](https://docs.microsoft.com/en-us/windows/desktop/directwrite/direct-write-portal), [Direct2D](https://docs.microsoft.com/en-us/windows/desktop/direct2d/direct2d-portal), [Direct3D9](https://docs.microsoft.com/en-us/windows/win32/direct3d9/dx9-graphics), [Direct3D11](https://docs.microsoft.com/en-us/windows/desktop/direct3d11/atoc-dx-graphics-direct3d-11), [Direct3D12](https://docs.microsoft.com/en-us/windows/desktop/direct3d12/directx-12-programming-guide), [XInput](https://docs.microsoft.com/en-us/windows/win32/xinput/getting-started-with-xinput), [XAudio2](https://docs.microsoft.com/en-us/windows/win32/xaudio2/xaudio2-introduction), [X3DAudio](https://docs.microsoft.com/it-it/windows/win32/xaudio2/x3daudio), [DirectInput](https://docs.microsoft.com/en-us/previous-versions/windows/desktop/ee416842(v=vs.85)), [DirectStorage](https://devblogs.microsoft.com/directx/landing-page/), [DirectML](https://docs.microsoft.com/en-us/windows/ai/directml/dml-intro), [UIAnimation](https://docs.microsoft.com/en-us/windows/win32/api/_uianimation) and [DirectSound](https://learn.microsoft.com/en-us/previous-versions/windows/desktop/bb318665(v=vs.85)).

This library targets **.net7.0** and **.net8.0** and uses modern C# 12, see [CHANGELOG](https://github.com/amerkoleci/Vortice.Windows/blob/main/CHANGELOG.md) for list of changes between commits.

If you are looking for high-performance low level bindings, please visit [Vortice.Win32](https://github.com/amerkoleci/Vortice.Win32)

## Sponsors
Please consider becoming a [SPONSOR](https://github.com/sponsors/amerkoleci) to further help development and to allow faster issue triaging and new features to be implemented.
**_NOTE:_** **any feature request** would require a [sponsor](https://github.com/sponsors/amerkoleci) in order to allow faster implementation and allow this project to continue.

There is a collection of samples available [Vortice.Windows.Samples](https://github.com/amerkoleci/Vortice.Windows.Samples)

## Credits

Library development, contributions and bugfixes by:

- Amer Koleci
- Aaron Sun (DirectML)

[SharpDX](https://github.com/sharpdx/SharpDX) bindings were used for some platforms to understand how mapping works using SharpGenTools.

## Build

In order to compile, make sure **no spaces** are present in the solution path otherwise SharpGen will fail to generate bindings.
Also, you need to install **Visual Studio 2022** with the following components:

- [x] Visual C++ Toolset Component
- [x] Windows 11 SDK (10.0.22621.0)
- [x] .NET 8 SDK

## Projects using Vortice.Windows

- [Evergine](https://evergine.com/)
- [Veldrid](https://github.com/mellinoe/veldrid)
- [MiniEngine](https://github.com/roy-t/MiniEngine3) 
- [SharpAudio](https://github.com/feliwir/SharpAudio)

## Samples
- [Collection of samples](https://github.com/amerkoleci/Vortice.Windows/tree/HEAD/src/samples)
- Direct3D12 DXR samples by [CAMongrel](https://github.com/CAMongrel) [D3D12SampleRaytracerSharp](https://github.com/CAMongrel/D3D12SampleRaytracerSharp).
- Direct3D12 DXR raytracing samples ported from NVIDIA samples by [Jorgemagic](https://github.com/Jorgemagic/CSharpDirectXRaytracing)
- ImGUI example using Vortice.Windows [VorticeImGui](https://github.com/YaakovDavis/VorticeImGui)

