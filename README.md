# Vortice.Windows

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/amerkoleci/Vortice.Windows/blob/master/LICENSE)
[![Build status](https://github.com/amerkoleci/Vortice.Windows/workflows/CI/badge.svg)](https://github.com/amerkoleci/Vortice.Windows/actions)
[![NuGet](https://img.shields.io/nuget/v/Vortice.Runtime.COM.svg)](https://www.nuget.org/packages?q=Tags%3A%22Vortice.Windows%22)

**Vortice.Windows** is a collection of Win32 and UWP libraries with bindings support for [DXGI](https://docs.microsoft.com/en-us/windows/desktop/direct3ddxgi/d3d10-graphics-programming-guide-dxgi), [WIC](https://docs.microsoft.com/en-us/windows/desktop/wic/-wic-lh), [DirectWrite](https://docs.microsoft.com/en-us/windows/desktop/directwrite/direct-write-portal), [Direct2D](https://docs.microsoft.com/en-us/windows/desktop/direct2d/direct2d-portal), [Direct3D9](https://docs.microsoft.com/en-us/windows/win32/direct3d9/dx9-graphics), [Direct3D11](https://docs.microsoft.com/en-us/windows/desktop/direct3d11/atoc-dx-graphics-direct3d-11), [Direct3D12](https://docs.microsoft.com/en-us/windows/desktop/direct3d12/directx-12-programming-guide), [XInput](https://docs.microsoft.com/en-us/windows/win32/xinput/getting-started-with-xinput), [XAudio2](https://docs.microsoft.com/en-us/windows/win32/xaudio2/xaudio2-introduction) and [X3DAudio](https://docs.microsoft.com/it-it/windows/win32/xaudio2/x3daudio).

This project was born as [SharpDX](https://github.com/sharpdx/SharpDX) was recently inactive and the aim is to have .net standard 2.0 API and usage
of modern stuff like Unsafe and SharpGen.Runtime.

The API may change between release, make sure to take look at [CHANGELOG](https://github.com/amerkoleci/Vortice.Windows/blob/master/CHANGELOG.md)

## Credits

Library development, contributions and bugfixes by:

- Amer Koleci

SharpDX bindings where used for some platforms to understand how mapping work using SharpGenTools.

## Build

In order to compile, make sure **no spaces** are present in the solution path otherwise SharpGen will fail to generate bindings and you need to install **Visual Studio 2019** with the components:

- [x] Visual C++ Toolset Component
- [x] Windows 10 - 2004 SDK (10.0.19041.0) Component
- [x] .NET Core SDK

## Download

All packages are available as NuGet packages: [![NuGet](https://img.shields.io/nuget/v/Vortice.Runtime.COM.svg)](https://www.nuget.org/packages?q=Tags%3A%22Vortice.Windows%22)

Nightly packages can be download from Nuget as well.

## Samples
- Direct3D12 DXR samples by [CAMongrel](https://github.com/CAMongrel) [D3D12SampleRaytracerSharp](https://github.com/CAMongrel/D3D12SampleRaytracerSharp).
- Direct3D12 DXR raytracing samples ported from NVIDIA samples by [Jorgemagic](https://github.com/Jorgemagic/CSharpDirectXRaytracing)
- ImGUI example using Vortice.Windows [VorticeImGui](https://github.com/YaakovDavis/VorticeImGui)

## Sponsors
To further help development of thoose bindings consider [sponsoring](https://github.com/sponsors/amerkoleci) my profile in order to allow faster issue track and new features to be implemented.

