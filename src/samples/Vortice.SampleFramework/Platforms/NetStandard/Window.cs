// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using Vortice.Win32;
using static Vortice.Win32.User32;

namespace Vortice
{
    public sealed partial class Window
    {
        private const int CW_USEDEFAULT = unchecked((int)0x80000000);

        private unsafe void PlatformConstruct()
        {
            int x = 0;
            int y = 0;
            WindowStyles style = 0;
            WindowExStyles styleEx = 0;
            const bool resizable = true;

            // Setup the screen settings depending on whether it is running in full screen or in windowed mode.
            //if (fullscreen)
            //{
            //style = User32.WindowStyles.WS_POPUP | User32.WindowStyles.WS_VISIBLE;
            //styleEx = User32.WindowStyles.WS_EX_APPWINDOW;

            //width = screenWidth;
            //height = screenHeight;
            //}
            //else
            {
                if (Width > 0 && Height > 0)
                {
                    int screenWidth = GetSystemMetrics(SystemMetrics.SM_CXSCREEN);
                    int screenHeight = GetSystemMetrics(SystemMetrics.SM_CYSCREEN);

                    // Place the window in the middle of the screen.WS_EX_APPWINDOW
                    x = (screenWidth - Width) / 2;
                    y = (screenHeight - Height) / 2;
                }

                if (resizable)
                {
                    style = WindowStyles.WS_OVERLAPPEDWINDOW;
                }
                else
                {
                    style = WindowStyles.WS_POPUP | WindowStyles.WS_BORDER | WindowStyles.WS_CAPTION | WindowStyles.WS_SYSMENU;
                }

                styleEx = WindowExStyles.WS_EX_APPWINDOW | WindowExStyles.WS_EX_WINDOWEDGE;
            }
            style |= WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_CLIPSIBLINGS;

            int windowWidth;
            int windowHeight;

            if (Width > 0 && Height > 0)
            {
                var rect = new RawRect(0, 0, Width, Height);

                // Adjust according to window styles
                AdjustWindowRectEx(&rect, (uint)style, 0, (uint)styleEx);

                windowWidth = rect.Right - rect.Left;
                windowHeight = rect.Bottom - rect.Top;
            }
            else
            {
                x = y = windowWidth = windowHeight = CW_USEDEFAULT;
            }

            fixed (char* lpszClassName = Application.WindowClassName)
            {
                fixed (char* lpWindowName = Title)
                {
                    Handle = CreateWindowExW(
                    (uint)styleEx,
                    (ushort*)lpszClassName,
                    (ushort*)lpWindowName,
                    (uint)style,
                    x,
                    y,
                    windowWidth,
                    windowHeight,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    null
                    );

                    if (Handle == IntPtr.Zero)
                    {
                        return;
                    }
                }
            }

            ShowWindow(Handle, (int)ShowWindowCommand.Normal);
            Width = windowWidth;
            Height = windowHeight;
        }

        public void Destroy()
        {
            IntPtr hwnd = Handle;
            if (hwnd != IntPtr.Zero)
            {
                IntPtr destroyHandle = hwnd;
                Handle = IntPtr.Zero;

                Debug.WriteLine($"[WIN32] - Destroying window: {destroyHandle}");
                DestroyWindow(destroyHandle);
            }
        }
    }
}
