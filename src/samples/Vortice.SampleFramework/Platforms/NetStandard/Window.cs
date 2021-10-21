// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Drawing;
using Vortice.Win32;
using static Vortice.Win32.User32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;
using static Windows.Win32.PInvoke;
using static Windows.Win32.UI.WindowsAndMessaging.WINDOW_STYLE;
using static Windows.Win32.UI.WindowsAndMessaging.WINDOW_EX_STYLE;
using static Windows.Win32.UI.WindowsAndMessaging.SYSTEM_METRICS_INDEX;
using static Windows.Win32.UI.WindowsAndMessaging.SHOW_WINDOW_CMD;

namespace Vortice
{
    public sealed partial class Window
    {
        private const int CW_USEDEFAULT = unchecked((int)0x80000000);
        private HWND hWnd;

        public nint Handle => hWnd.Value;

        private unsafe void PlatformConstruct()
        {
            int x = 0;
            int y = 0;
            WINDOW_STYLE style = 0;
            WINDOW_EX_STYLE styleEx = 0;
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
                if (ClientSize.Width > 0 && ClientSize.Height > 0)
                {
                    int screenWidth = GetSystemMetrics(SM_CXSCREEN);
                    int screenHeight = GetSystemMetrics(SM_CYSCREEN);

                    // Place the window in the middle of the screen.WS_EX_APPWINDOW
                    x = (screenWidth - ClientSize.Width) / 2;
                    y = (screenHeight - ClientSize.Height) / 2;
                }

                if (resizable)
                {
                    style = WS_OVERLAPPEDWINDOW;
                }
                else
                {
                    style = WS_POPUP | WS_BORDER | WS_CAPTION | WS_SYSMENU;
                }

                styleEx = WS_EX_APPWINDOW | WS_EX_WINDOWEDGE;
            }
            style |= WS_CLIPCHILDREN | WS_CLIPSIBLINGS;

            int windowWidth;
            int windowHeight;

            if (ClientSize.Width > 0 && ClientSize.Height > 0)
            {
                var rect = new RECT
                {
                    right = ClientSize.Width,
                    bottom = ClientSize.Height
                };

                // Adjust according to window styles
                AdjustWindowRectEx(&rect, style, default, styleEx);

                windowWidth = rect.right - rect.left;
                windowHeight = rect.bottom - rect.top;
            }
            else
            {
                x = y = windowWidth = windowHeight = CW_USEDEFAULT;
            }

            hWnd = CreateWindowEx(
                styleEx,
                Application.WindowClassName,
                Title,
                style,
                x,
                y,
                windowWidth,
                windowHeight,
                default,
                default,
                default,
                null
            );

            if (hWnd.Value == IntPtr.Zero)
            {
                return;
            }

            ShowWindow(hWnd, SW_NORMAL);
            ClientSize = new Size(windowWidth, windowHeight);
        }

        public void Destroy()
        {
            if (hWnd != IntPtr.Zero)
            {
                HWND destroyHandle = hWnd;
                hWnd = default;

                Debug.WriteLine($"[WIN32] - Destroying window: {destroyHandle}");
                DestroyWindow(destroyHandle);
            }
        }
    }
}
