// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice;

public sealed partial class Window
{
    public string Title { get; private set; }
    public SizeI ClientSize { get; private set; }

    public Window(string title, int width = 1280, int height = 720)
    {
        Title = title;
        ClientSize = new(width, height);
        PlatformConstruct();
    }
}
