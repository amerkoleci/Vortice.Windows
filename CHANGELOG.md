# Changelog

Current Stable Release:    2.1.0 (December 2021)

-----------------------------------------------
Release:     2.1.1 (February 2022)
-----------------------------------------------
- [ENH] Direct3D11: Peformance improvements in mappings.
- [ENH] Direct3D11: Improvements in Video mapping.
- [ENH] XAudio2: Use Windows SDK headers instead of XAudio2 redist and move to function pointers.

-----------------------------------------------
Release:     2.1.0 (December 2021)
-----------------------------------------------
- [ENH] ID3D12On12: Improve binding logic.
- [ENH] General: Move D3D12_RESOURCE_STATES to Vortice.DirectX as used from ID3D12On12.
- [ENH] DXC: Update to December 2021 with HLSL 2021 Preview.
- [ENH] Direct3D12: Update to Agility SDK 1.700.10.
- [ENH] DirectX: Add Luid support.
- [ENH] Build: Add .NET 6.0 SDK support
- [ENH] Build: Update to 10.0.22000.0 SDK
- [ENH] General: Add .NET 5.0 TFM
- [ENH] MediaFoundation: Map IMFAsyncResult, IMFAsyncCallback and IMFAttributes + add missing VideoFormatGuids
- [ENH] MediaFoundation: Improvements in IMMDevice and handle property store in automatic way
- [ADD] General: Add PropertyStore support in Vortice.DirectX under Win32
- [CHG] DXGI: Remove legacy DXGIGetDebugInterface function, prefer DXGIGetDebugInterface1
- [FIX] Direct3D12: D3D12CreateDevice with return call
- [FIX] Direct3D12: GetCopyableFootprints signature and accepting correct null values
- [ENH] Direct3D11: Improvements and helper methods for creating buffers and textures

-----------------------------------------------
Release:     1.9.143 (September 2021)
-----------------------------------------------
- [ENH] Direct3D12: Add ID3D12Device.CreateRootSignature with blob creation.
- [ENH] Direct3D11: Rename Usage to ResourceUsage and improve CreateBuffer, improve Create shaders with Blob directly.
- [ENH] DXGI: dxgidebug.h under Vortice.DXGI.Debug namespace.
- [ENH] Direct3D12: Improve Debug MessageID enum values names.
- [ENH] WinUI: New Vortice.WinUI based on Microsoft.ProjectReunion.WinUI (https://www.nuget.org/packages/Microsoft.ProjectReunion.WinUI/)
- [ENH] DirectInput: New Vortice.DirectInput support.
- [ENH] MediaFoundation: Preview MediaFoundation support.

-----------------------------------------------
Release:     1.9.80 (May 2021)
-----------------------------------------------
- [ENH] Direct3D12: Add Create overload which returns result.
- [ENH] Direct2D1: FIX namespace issues and improvements in bindings.
- [ENH] WIC: Some improvements.
- [ENH] DXC: Allow IDxcIncludeHandler subclass, with example in HelloWorldDirect3D12.
- [ENH] Direct3D9: Improve methods taking sharedHandle and various improvements.
- [ENH] Direct3D12: Update headers to DirectX Agility SDK.
- [ENH] Dxc: Update to April 2021.
- [ENH] General: Move to standard types for Point, PointF, Size, SizeF, Rectangle and RectangleF.
- [ENH] Direct2D1: Improve mappings.
- [ENH] DirectWrite: GlyphRun improvements and example for AdvancedText rendering.

-----------------------------------------------
Release:     1.9.45 (April 2021)
-----------------------------------------------
KEY CHANGES:
- [ENH] General: Update to newer SharpGen SDK and remove Vortice.Runtime.COM.
- [FIX] BUG: Critical bug with Interop.Patch for Read and Write.
- [CHG] XAudio2: Remove XAudio2 redist and improve bindings.
- [ENH] Direct3D12: Add Hello raytracing example.
- [ENH] Move common types from DXGI and DCommon to Vortice.DirectX for better separation and interop.
- [ENH] dxgitype.h: Moved in Vortice.DirectX.

-----------------------------------------------
Release:     1.8.59 (January 2021)
-----------------------------------------------
KEY CHANGES:
- [ENH] General: Integrate MIT licensed headers from (https://github.com/microsoft/DirectX-Headers)
- [ENH] DXCore: Add support for DXCore.
- [ENH] Direct3D12: Add Video binding generation.
- [ENH] General: Add DataStream from SharpDX.
- [ENH] Dxc: Bindings generated from headers for DirectX Shader Compiler and bring in parity with latest release.
- [ENH] Direct3D12: Pipeline State stream object support and amplification and mesh shader support.

-----------------------------------------------
Release:     1.8.35 (November 2020)
-----------------------------------------------
KEY CHANGES:
- [ENH] DirectComposition: Initial DirectComposition support.
- [ENH] DXGI: Initial WinUI headers generation under WinUI folder + namespace.
- [FIX] D3D12: BeginEvent, SetMarker for ID3D12CommandQueue and ID3D12GraphicsCommandList.
- [ENH] DXGI: Map IDXGraphicsAnalysis from DXProgrammableCapture.h.
- [ENH] XAudio2: Update to Microsoft.XAudio2.Redist 1.2.4
- [ENH] XAudio2: Implement clean CreateSubmixVoice mapping.
- [ENH] XAudioFX: Reverb and Volume metter under namespace Vortice.XAudio2.Fx.
- [ENH] XAudioFX: Correctly marshal VolumeMeterLevels.
- [EHN] XAudio: Allow AudioBuffer with externally owned memory.
- [ENH] D3D12: Add count in some methods (for example: ID3D12GraphicsCommandList)

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
