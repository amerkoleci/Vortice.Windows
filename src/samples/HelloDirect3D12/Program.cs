// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.IO;
using Vortice;
using Vortice.Mathematics;
using Vortice.WIC;
using Vortice.DXCore;
using static Vortice.DXCore.DXCore;
using System;
using System.Runtime.InteropServices;

namespace HelloDirect3D11
{
    public static class Program
    {
        private class TestApplication : Application
        {
            public TestApplication()
                : base(true)
            {
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
            try
            {
                // Just safelly test DXCore stuff
                TestDXCore();
            }
            catch
            {

            }

            using (var app = new TestApplication())
            {
                app.Run();
            }
        }
    }
}
