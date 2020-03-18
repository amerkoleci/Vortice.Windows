// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using SharpGen.Runtime.Diagnostics;
using Vortice;
using Vortice.Direct3D;

namespace HelloDirect3D11
{
    public static class Program
    {
        private static readonly FeatureLevel[] s_featureLevels = new[]
        {
            FeatureLevel.Level_11_1,
            FeatureLevel.Level_11_0,
            FeatureLevel.Level_10_1,
            FeatureLevel.Level_10_0
        };

        private class TestApplication : Application
        {
            public TestApplication()
                : base(false)
            {
            }
        }

        public static void Main()
        {
#if DEBUG
            Configuration.EnableObjectTracking = true;
#endif

            using (var app = new TestApplication())
            {
                app.Run();
            }
#if DEBUG
            Console.WriteLine(ObjectTracker.ReportActiveObjects());
#endif
        }
    }
}
