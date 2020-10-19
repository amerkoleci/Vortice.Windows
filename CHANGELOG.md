# Changelog

Current Stable Release:    1.8.17 (September 2020)

-----------------------------------------------
Release:     1.8.XX (October 2020)
-----------------------------------------------
KEY CHANGES:
- [ENH] DirectComposition: Initial DirectComposition support.
- [ENH] DXGI: Initial WinUI headers generation under WinUI folder + namespace.
- [FIX] D3D12: BeginEvent, SetMarker for ID3D12CommandQueue and ID3D12GraphicsCommandList.
- [ENH] DXGI: Map IDXGraphicsAnalysis from DXProgrammableCapture.h.

-----------------------------------------------
Release:     1.8.17 (October 2020)
-----------------------------------------------
KEY CHANGES:
- [FIX] D3D11: ID3D11DeviceContext fix various calls and avoid usage of params that can lead memory allocation.
- [ENH] D3D11: ID3D11Device - Add feature check helper methods.
- [ENH] D3D11: ID3D11DeviceContext Add VS/HS/DS/GS/PS/CS UnsetConstantBuffer faster method and fix allocation in ID3D11CommandList.
- [FIX] DXGI: IDXGIDevice::GetAdapter is not property and not persisted, user need to manually Dispose the IDXGIAdapter (just like native code).
- [ENH] D3D11: D3D11CreateDevice takes also native IntPtr adapter.
- [ENH] Samples: Cleanup code and remove all leakages.
- [ENH] D3D12: Add more helpers from d3d12x and cleanup.
- [ENH] D3D12: D3D12CreateDevice allow direct native IDXGIAdapter handle and improve GetMaxSupportedFeatureLevel, IsSupported to accept native handle.
- [FIX] VorticePlatformDetection correct usage of GetVersionEx.
- [ENH] D3D12: D3D12_GPU_VIRTUAL_ADDRESS map to ulong
- [ENH] D3D12: More API and methods improvements.
- [ENH] Mapping: Map UINT64 to ulong instead of long, better keep close to native API.

-----------------------------------------------
Release:     1.7.37 (August 2020)
-----------------------------------------------
KEY CHANGES:
- [ENH] DXGI: Expose types from windows.ui.xaml.media.dxinterop.h
- [FIX] WIC: Crash with IWICBitmapLock.GetDataPointer, correctly handle different types
- [FIX] D3D11: Fix OMSetRenderTargetsAndUnorderedAccessViews overloads and add KeepRenderTargetsAndDepthStencil and KeepUnorderedAccessViews support.
- [ENH] DXGI: Improvements in API usage for IDXGIObject and IDXGIDeviceSubObject.
- [ENH] D3D Legacy Compiler: Compile accepts byte[] as shader source.
- [FIX] D3D11: ID3DUserDefinedAnnotation::SetMarker don't map as property.
- [ENH] DXGI: Remove dependency from System.Collections.Immutable and make API calls close to native.
- [FIX] DXGI: Fix IDXGIDevice.CreateSurface methods overload.

-----------------------------------------------
Release:     1.6.0 (April 2020)
-----------------------------------------------
KEY CHANGES:
- [BREAKING CHANGE] Switch to use Point, PointF, Size, SizeF, Rectangle and RectangleF from Vortice.Mathematics
- [ENH] Point, PointF, Size, SizeF, Rectangle and RectangleF supports conversion to System.Drawing types.
- [FIX] DXGIGetDebugInterface1 is part of dxgi.dll and not dxgidebug.dll
- [ENH] Direct3D11 helpers for creating Texture1D, Texture2D, Texture3D etc.
- [ENH] Usage of standard types from System.Drawing.*
- [ENH] Include winerror result codes for Direct2D1.
- [FIX] Various Direct3D11 issues.
- [ENH] Use XAudio2 Redist to support Windows7 and remove XAudio 2.7 and 2.8 logic (https://docs.microsoft.com/en-us/windows/win32/xaudio2/xaudio2-redistributable).
- [ENH] Merge X3DAudio in XAudio2 in addition to new XAudio redist.

-----------------------------------------------
Release:     1.5.0 (December 2019)
-----------------------------------------------
KEY CHANGES:
- Update dependencies.
- Direct3D9 support.

-----------------------------------------------
Release:     1.5.0 (December 2019)
-----------------------------------------------
KEY CHANGES:
- Update dependencies.
- Direct3D9 support.

-----------------------------------------------
Release:     1.4.0 (November 2019)
-----------------------------------------------
KEY CHANGES:
- Update dependencies.
- Correctly map D3D12_RAYTRACING_INSTANCE_DESC::Transform (using Matrix3x4).
- Direct3D12 API improvements.
- Persist IDXGIDevice::GetAdapter and handle Dispose.
- Direct3D11 API improvements (ID3D11DeviceContext) etc.
- MappedSubresource added AsSpan methods.
- IXAudio2SourceVoice - Add StateNoSamplesPlayed.
- [X3DAudio](https://docs.microsoft.com/it-it/windows/win32/xaudio2/x3daudio) support added.
- Direct2D1 bindings improvements, thanks to [manju-summoner](https://github.com/manju-summoner)
- Direct2D1 builtin effects support.

-----------------------------------------------
Release:     1.3.0 (September 2019)
-----------------------------------------------
KEY CHANGES:
- Improvements in Direct3D12 raytracing API, StateObject and StateSubObject.
- Fixes #22 - Load dxil.dll first before dxcompiler.dll.
- Improvements in Direct3D12 raytracing API and structures.
- New: Vortice.Multimedia separation for lightweight logic.
- New: XAudio2 support.

-----------------------------------------------
Release:     1.2.0 (September 2019)
-----------------------------------------------
KEY CHANGES:
- Rework Vortice.Dxc to use native library loader.
- Optionally load dxil.dll using Dxil.LoadLibrary() if required.

-----------------------------------------------
Release:     1.1.0 (August 2019)
-----------------------------------------------
KEY CHANGES:
 - Rename project with better naming.
 - Remove some usage of Unsafe.SizeOf and use unmanaged C# feature.
 - Removing usage of Guard class, user need to take care of arguments to native call, for better performance. 
 - d3d11shader.h bindings moved to Vortice.Direct3D11.Shader.
 - d3d12shader.h bindings moved to Vortice.Direct3D12.Shader.
 - Vortice.D3DCompiler - Improvements in Compile and CompileFromFile.
 - ID3D12Object improvements.

-----------------------------------------------
Release:     1.0.0 (August 2019)
-----------------------------------------------
KEY CHANGES:
 - Initial 1.0.0 release.

Detailed changes:
[all] Initial release
