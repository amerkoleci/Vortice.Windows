@echo off
@setlocal

dotnet restore src/Vortice.Windows.sln

dotnet pack -c Release src/Vortice.Runtime.COM/Vortice.Runtime.COM.csproj
dotnet pack -c Release src/Vortice.DirectX/Vortice.DirectX.csproj
dotnet pack -c Release src/Vortice.DXGI/Vortice.DXGI.csproj
dotnet pack -c Release src/Vortice.Direct3D11/Vortice.Direct3D11.csproj
dotnet pack -c Release src/Vortice.Direct3D12/Vortice.Direct3D12.csproj
dotnet pack -c Release src/Vortice.Direct2D1/Vortice.Direct2D1.csproj
dotnet pack -c Release src/Vortice.D3DCompiler/Vortice.D3DCompiler.csproj
dotnet pack -c Release src/Vortice.XInput/Vortice.XInput.csproj
dotnet pack -c Release src/Vortice.Dxc/Vortice.Dxc.csproj
