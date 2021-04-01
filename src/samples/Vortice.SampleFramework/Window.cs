// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.Mathematics;

namespace Vortice
{
    public sealed partial class Window
    {
        public string Title { get; private set; }
        public Size ClientSize { get; private set; }
        public IntPtr Handle { get; private set; }

        public Window(string title, int width, int height)
        {
            Title = title;
            ClientSize = new Size(width, height);
            PlatformConstruct();
        }
    }
}
