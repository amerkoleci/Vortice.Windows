// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using SharpGen.Runtime.Diagnostics;
using Vortice;
using Vortice.DXCore;
using static Vortice.DXCore.DXCore;

namespace HelloDirect3D12;

public static class Program
{
    private class TestApplication : Application
    {
        protected override void InitializeBeforeRun()
        {
            var validation = false;
#if DEBUG
            validation = true;
#endif

            _graphicsDevice = new D3D12GraphicsDevice(validation, MainWindow!);
        }
    }

    private unsafe static void TestDXCore()
    {
        using (IDXCoreAdapterFactory adapterFactory = DXCoreCreateAdapterFactory<IDXCoreAdapterFactory>())
        {
            using (IDXCoreAdapterList adapterList = adapterFactory.CreateAdapterList<IDXCoreAdapterList>(new[] { D3D12_CoreCompute }))
            {
                adapterList.Sort(new[] { AdapterPreference.Hardware, AdapterPreference.HighPerformance }).CheckError();
                for (int i = 0; i < adapterList.AdapterCount; i++)
                {
                    using (IDXCoreAdapter candidateAdapter = adapterList.GetAdapter<IDXCoreAdapter>(i))
                    {
                        AdapterMemoryBudgetNodeSegmentGroup nodeSegmentGroup = new AdapterMemoryBudgetNodeSegmentGroup
                        {
                            NodeIndex = 0,
                            SegmentGroup = SegmentGroup.Local
                        };

                        candidateAdapter.QueryState(AdapterState.AdapterMemoryBudget,
                            nodeSegmentGroup,
                            out AdapterMemoryBudget memoryBudget
                            ).CheckError();

                        string driverDescription = candidateAdapter.DriverDescription;

                        bool isHardware = candidateAdapter.IsHardware;
                        HardwareID hardwareID = candidateAdapter.HardwareID;

                        if (candidateAdapter.GetProperty(AdapterProperty.HardwareIDParts, out HardwareIDParts hardwareIDParts).Success)
                        {

                        }

                    }
                }
            }
        }
    }

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

        using (var app = new TestApplication())
            app.Run();

#if DEBUG
        Console.WriteLine(ObjectTracker.ReportActiveObjects());
#endif
    }
}
