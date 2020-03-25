// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using Vortice.Mathematics;
using Vortice.Win32;
using static Vortice.Win32.User32;

namespace Vortice
{
    public sealed class Window
    {
        private const int CW_USEDEFAULT = unchecked((int)0x80000000);
        private const WindowStyles _windowStyles = WindowStyles.WS_POPUP | WindowStyles.WS_VISIBLE | WindowStyles.WS_BORDER | WindowStyles.WS_OVERLAPPEDWINDOW | WindowStyles.WS_CLIPSIBLINGS | WindowStyles.WS_DLGFRAME;
        private const WindowExStyles _windowExStyles = WindowExStyles.WS_EX_NOREDIRECTIONBITMAP | WindowExStyles.WS_EX_WINDOWEDGE;

        public IntPtr Handle { get; private set; }

        public void GetClientRect(ref RawRect rect)
        {
            User32.GetClientRect(Handle, ref rect);
        }

        public Size ClientSize
        {
            get
            {
                RawRect rect = default;
                GetClientRect(ref rect);
                return new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
            }
        }

        public Window(string title, int width, int height, string className)
        {
            var x = 0;
            var y = 0;

            if (width > 0 && height > 0)
            {
                var screenWidth = GetSystemMetrics(SystemMetrics.SM_CXSCREEN);
                var screenHeight = GetSystemMetrics(SystemMetrics.SM_CYSCREEN);

                // Place the window in the middle of the screen.
                x = (screenWidth - width) / 2;
                y = (screenHeight - height) / 2;
            }

            int windowWidth;
            int windowHeight;

            if (width > 0 && height > 0)
            {
                var rect = new RawRect(0, 0, width, height);

                // Adjust according to window styles
                AdjustWindowRectEx(
                    ref rect,
                    _windowStyles,
                    false,
                    _windowExStyles);

                windowWidth = rect.Right - rect.Left;
                windowHeight = rect.Bottom - rect.Top;
            }
            else
            {
                x = y = windowWidth = windowHeight = CW_USEDEFAULT;
            }

            Handle = CreateWindowEx(
                (int)_windowExStyles,
                className,
                title,
                (int)_windowStyles,
                x,
                y,
                windowWidth,
                windowHeight,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero);

            if (Handle == IntPtr.Zero)
            {
                return;
            }

            ShowWindow(Handle, ShowWindowCommand.Normal);
        }

        public void Destroy()
        {
            if (Handle == IntPtr.Zero) return;

            var destroyHandle = Handle;
            Handle = IntPtr.Zero;

            Debug.WriteLine($"[WIN32] - Destroying window: {destroyHandle}");
            DestroyWindow(destroyHandle);
        }
    }
}
