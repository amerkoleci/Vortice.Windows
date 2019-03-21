// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice;

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

        public static void Main()
        {
            using (var app = new TestApplication())
            {
                app.Run();
            }
        }
    }
}
