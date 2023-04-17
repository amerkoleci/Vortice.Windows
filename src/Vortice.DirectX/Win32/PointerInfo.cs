// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice.Win32;

/// <summary>
/// Contains basic pointer information common to all pointer types.
/// </summary>
/// <un
public partial struct PointerInfo
{
    public PointerInputType pointerType;

    public uint PointerId;

    public uint FrameId;

    public PointerFlags PointerFlags;

    public nint SourceDevice;

    public nint HwndTarget;

    public Point PixelLocation;

    public Point HimetricLocation;

    public Point PixelLocationRaw;

    public Point HimetricLocationRaw;

    public uint Time;

    public uint HistoryCount;

    public int InputData;

    public uint KeyStates;

    public ulong PerformanceCount;

    public PointerButtonChangeType ButtonChangeType;
}
