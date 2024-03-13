// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using Vortice.Win32;

namespace Vortice.WinForms;

internal unsafe static partial class Win32Native
{
    [LibraryImport("user32")]
    public static partial RawBool PeekMessageW(NativeMessage* lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

    [LibraryImport("user32")]
    public static partial RawBool GetMessageW(NativeMessage* lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

    [LibraryImport("user32")]
    public static partial RawBool TranslateMessage(NativeMessage* lpMsg);

    [LibraryImport("user32")]
    public static partial /*LRESULT*/nint DispatchMessageW(NativeMessage* lpMsg);

    [LibraryImport("user32")]
    public static partial RawBool GetClientRect(nint hWnd, RawRect* lpRect);
}
