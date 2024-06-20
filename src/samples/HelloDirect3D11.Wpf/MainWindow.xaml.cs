using System.Numerics;
using System.Windows;
using Vortice;
using Vortice.Direct3D11;
using Vortice.Mathematics;
using Vortice.Wpf;
using Vortice.DXGI;
using System.IO;
using Vortice.D3DCompiler;
using Vortice.Direct3D;

namespace HelloDirect3D11.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : System.Windows.Window
{
    private ID3D11Buffer? _vertexBuffer;
    private ID3D11VertexShader? _vertexShader;
    private ID3D11PixelShader? _pixelShader;
    private ID3D11InputLayout? _inputLayout;
    private Color4 _clearColor = Colors.CornflowerBlue;
    private readonly Random _random = new ();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void DrawingSurface_LoadContent(object? sender, DrawingSurfaceEventArgs e)
    {
        ReadOnlySpan<VertexPositionColor> triangleVertices =
        [
            new VertexPositionColor(new Vector3(0f, 0.5f, 0.0f), new Color4(1.0f, 0.0f, 0.0f, 1.0f)),
            new VertexPositionColor(new Vector3(0.5f, -0.5f, 0.0f), new Color4(0.0f, 1.0f, 0.0f, 1.0f)),
            new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.0f), new Color4(0.0f, 0.0f, 1.0f, 1.0f))
        ];

        _vertexBuffer = e.Device.CreateBuffer(triangleVertices, BindFlags.VertexBuffer);

        InputElementDescription[] inputElementDescs =
        [
            new InputElementDescription("POSITION", 0, Format.R32G32B32_Float, 0, 0),
            new InputElementDescription("COLOR", 0, Format.R32G32B32A32_Float, 12, 0)
        ];

        ReadOnlyMemory<byte> vertexShaderByteCode = CompileBytecode("Triangle.hlsl", "VSMain", "vs_4_0");
        ReadOnlyMemory<byte> pixelShaderByteCode = CompileBytecode("Triangle.hlsl", "PSMain", "ps_4_0");

        _vertexShader = e.Device.CreateVertexShader(vertexShaderByteCode.Span);
        _pixelShader = e.Device.CreatePixelShader(pixelShaderByteCode.Span);
        _inputLayout = e.Device.CreateInputLayout(inputElementDescs, vertexShaderByteCode.Span);
    }

    private void DrawingSurface_UnloadContent(object sender, DrawingSurfaceEventArgs e)
    {
        _vertexBuffer?.Dispose();
        _vertexShader?.Dispose();
        _pixelShader?.Dispose();
        _inputLayout?.Dispose();

    }

    private void DrawingSurface_Draw(object? sender, DrawEventArgs e)
    {
        e.Context.OMSetBlendState(null);
        e.Context.ClearRenderTargetView(e.Surface.ColorTextureView, _clearColor);

        if (e.Surface.DepthStencilView != null)
        {
            e.Context.ClearDepthStencilView(e.Surface.DepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
        }

        e.Context.IASetPrimitiveTopology(PrimitiveTopology.TriangleList);
        e.Context.VSSetShader(_vertexShader);
        e.Context.PSSetShader(_pixelShader);
        e.Context.IASetInputLayout(_inputLayout);
        e.Context.IASetVertexBuffer(0, _vertexBuffer, VertexPositionColor.SizeInBytes);
        e.Context.Draw(3, 0);
    }

    private static ReadOnlyMemory<byte> CompileBytecode(string shaderName, string entryPoint, string profile)
    {
        string assetsPath = Path.Combine(AppContext.BaseDirectory, "Assets");
        string shaderFile = Path.Combine(assetsPath, shaderName);
        //string shaderSource = File.ReadAllText(Path.Combine(assetsPath, shaderName));

        return Compiler.CompileFromFile(shaderFile, entryPoint, profile);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _clearColor = new Color4(_random.NextSingle(), _random.NextSingle(), _random.NextSingle(), 1.0f);
    }
}
