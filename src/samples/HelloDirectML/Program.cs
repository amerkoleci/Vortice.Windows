// Copyright © Aaron Sun and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using SharpGen.Runtime.Diagnostics;
using Vortice;
using Vortice.Direct3D12;
using Vortice.Direct3D12.Debug;
using Vortice.DXCore;
using Vortice.DXGI;

using static Vortice.DXCore.DXCore;

namespace HelloDirectML;

public static class Program
{
    public static void Main()
    {
        //try
        //{
        //    // Just safelly test DXCore stuff
        //    TestDXCore();
        //}
        //catch
        //{

        //}
#if DEBUG
        Configuration.EnableObjectTracking = true;
#endif

        using (var app = new DmlDevice())
            app.Run();

#if DEBUG
        Console.WriteLine(ObjectTracker.ReportActiveObjects());
#endif
    }
}
