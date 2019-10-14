# Vortice.Windows

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/amerkoleci/Vortice.Windows/blob/master/LICENSE)
[![Build status](https://ci.appveyor.com/api/projects/status/p7d0w62bm1cew9xn?svg=true)](https://ci.appveyor.com/project/amerkoleci/vortice-windows)
[![NuGet](https://img.shields.io/nuget/v/Vortice.Runtime.COM.svg)](https://www.nuget.org/packages?q=Tags%3A%22Vortice.Windows%22)

**Vortice.Windows** is a collection of Win32 and UWP libraries with bindings support for [DXGI](https://docs.microsoft.com/en-us/windows/desktop/direct3ddxgi/d3d10-graphics-programming-guide-dxgi), [WIC](https://docs.microsoft.com/en-us/windows/desktop/wic/-wic-lh), [DirectWrite](https://docs.microsoft.com/en-us/windows/desktop/directwrite/direct-write-portal), [Direct2D](https://docs.microsoft.com/en-us/windows/desktop/direct2d/direct2d-portal), [Direct3D11](https://docs.microsoft.com/en-us/windows/desktop/direct3d11/atoc-dx-graphics-direct3d-11), [Direct3D12](https://docs.microsoft.com/en-us/windows/desktop/direct3d12/directx-12-programming-guide), [XInput](https://docs.microsoft.com/en-us/windows/win32/xinput/getting-started-with-xinput) and [XAudio2](https://docs.microsoft.com/en-us/windows/win32/xaudio2/xaudio2-introduction).

This project was born as [SharpDX](https://github.com/sharpdx/SharpDX) was recently inactive and the aim is to have .net standard 2.0 API and usage
of modern stuff like Unsafe and SharpGen.Runtime.

The API as still experimental and may change between release, make sure to take look at [CHANGELOG](https://github.com/amerkoleci/Vortice.Windows/blob/master/CHANGELOG.md)

## Credits

Library development, contributions and bugfixes by:

- Amer Koleci

SharpDX bindings where used for some platforms to understand how mapping work using SharpGenTools.

## Build

In order to compile, you need to install **Visual Studio 2017 or newer** with the components:

- [x] Visual C++ Toolset Component
- [x] Windows 10 - 1903 SDK (10.0.18362.0) Component
- [x] .NET Core SDK

## Download

All packages are available as NuGet packages: [![NuGet](https://img.shields.io/nuget/v/Vortice.Runtime.COM.svg)](https://www.nuget.org/packages?q=Tags%3A%22Vortice.Windows%22)

Nightly packages can be download by adding the NuGet feed "https://ci.appveyor.com/nuget/vortice-windows" to your `NuGet.config` file:

```xml
 <configuration>
   <packageSources>
     <!-- ... -->
     <add key="myget vortice_windows" value="https://ci.appveyor.com/nuget/vortice-windows" />
     <!-- ... -->
   </packageSources>
</configuration>     
```

## Samples
Direct3D12 DXR samples by @CAMongrel [D3D12SampleRaytracerSharp](https://github.com/CAMongrel/D3D12SampleRaytracerSharp)

