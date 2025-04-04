using System.Diagnostics;
using Vortice.Direct2D1;
using Vortice.Mathematics;
using Vortice.WinForms;

namespace HelloWinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var form = new RenderForm();
        var stopwatch = new Stopwatch();
        var d2dFactory1 = D2D1.D2D1CreateFactory<ID2D1Factory1>();
        var renderTarget = d2dFactory1.CreateHwndRenderTarget(default,
            new()
            {
                Hwnd = form.Handle,
                PixelSize = new SizeI(form.ClientSize.Width, form.ClientSize.Height)
            }
        );

        stopwatch.Start();
        RenderLoop.Run(form, () =>
        {
            double elapsed = stopwatch.Elapsed.TotalSeconds;
            var clearColor = new Color4(
                red: (float)(Math.Sin(elapsed) * 0.5 + 0.5f),
                green: (float)(Math.Sin(elapsed + Math.PI / 2) * 0.5 + 0.5f),
                blue: (float)(Math.Sin(elapsed + Math.PI) * 0.5 + 0.5f),
                alpha: 1
            );
            renderTarget.BeginDraw();
            renderTarget.Clear(clearColor);
            renderTarget.EndDraw();
        });
    }
}
