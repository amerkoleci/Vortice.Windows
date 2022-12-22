// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice;

public sealed partial class Window
{
    public string Title { get; private set; }
    public Size ClientSize { get; private set; }

    public Window(string title, int width = 1280, int height = 720)
    {
        Title = title;
        ClientSize = new(width, height);
        PlatformConstruct();
    }
}
