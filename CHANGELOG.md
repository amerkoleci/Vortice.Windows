# Changelog

Current Release:    1.2.0 (September 2019)

-----------------------------------------------
Release:     1.3.0 (September 2019)
-----------------------------------------------
KEY CHANGES:

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