@echo off
@setlocal

dotnet restore src\Vortice.Windows.sln

dotnet pack -c Release Vortice.DirectX/Vortice.DirectX.csproj
dotnet pack -c Release Vortice.DirectX.Direct3D11/Vortice.DirectX.Direct3D11.csproj
dotnet pack -c Release Vortice.DirectX.Direct3D12/Vortice.DirectX.Direct3D12.csproj
dotnet pack -c Release Vortice.DirectX.Direct2D/Vortice.DirectX.Direct2D.csproj
dotnet pack -c Release Vortice.DirectX.ShaderCompiler/Vortice.DirectX.ShaderCompiler.csproj
