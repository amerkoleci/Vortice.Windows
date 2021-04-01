// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using Vortice.Mathematics;

namespace Vortice.Win32
{
    #region Enums
    [Flags]
    public enum WindowStyles
    {
        WS_BORDER = 0x00800000,
        WS_CAPTION = 0x00C00000,
        WS_CHILD = 0x40000000,
        WS_CHILDWINDOW = 0x40000000,
        WS_CLIPCHILDREN = 0x02000000,
        WS_CLIPSIBLINGS = 0x04000000,
        WS_DISABLED = 0x08000000,
        WS_DLGFRAME = 0x00400000,
        WS_GROUP = 0x00020000,
        WS_HSCROLL = 0x00100000,
        WS_ICONIC = 0x20000000,
        WS_MAXIMIZE = 0x01000000,
        WS_MAXIMIZEBOX = 0x00010000,
        WS_MINIMIZE = 0x20000000,
        WS_MINIMIZEBOX = 0x00020000,
        WS_OVERLAPPED = 0x00000000,
        WS_OVERLAPPEDWINDOW =
            WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
        WS_POPUP = unchecked((int)0x80000000),
        WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
        WS_SIZEBOX = 0x00040000,
        WS_SYSMENU = 0x00080000,
        WS_TABSTOP = 0x00010000,
        WS_THICKFRAME = 0x00040000,
        WS_TILED = 0x00000000,
        WS_TILEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
        WS_VISIBLE = 0x10000000,
        WS_VSCROLL = 0x00200000
    }

    [Flags]
    public enum WindowExStyles : uint
    {
        WS_EX_LEFT = 0x00000000,
        WS_EX_LTRREADING = 0x00000000,
        WS_EX_RIGHTSCROLLBAR = 0x00000000,
        WS_EX_DLGMODALFRAME = 0x00000001,
        WS_EX_NOPARENTNOTIFY = 0x00000004,
        WS_EX_TOPMOST = 0x00000008,
        WS_EX_ACCEPTFILES = 0x00000010,
        WS_EX_TRANSPARENT = 0x00000020,
        WS_EX_MDICHILD = 0x00000040,
        /// <summary>
        ///     The window is intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a
        ///     normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar
        ///     or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not
        ///     displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE.
        /// </summary>
        WS_EX_TOOLWINDOW = 0x00000080,
        WS_EX_WINDOWEDGE = 0x00000100,
        WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
        WS_EX_CLIENTEDGE = 0x00000200,
        WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
        WS_EX_CONTEXTHELP = 0x00000400,
        WS_EX_RIGHT = 0x00001000,
        WS_EX_RTLREADING = 0x00002000,
        WS_EX_LEFTSCROLLBAR = 0x00004000,
        WS_EX_CONTROLPARENT = 0x00010000,
        WS_EX_STATICEDGE = 0x00020000,
        WS_EX_APPWINDOW = 0x00040000,
        WS_EX_LAYERED = 0x00080000,
        WS_EX_NOINHERITLAYOUT = 0x00100000,
        WS_EX_NOREDIRECTIONBITMAP = 0x00200000,
        WS_EX_LAYOUTRTL = 0x00400000,
        WS_EX_COMPOSITED = 0x02000000,
        WS_EX_NOACTIVATE = 0x08000000
    }

    [Flags]
    public enum WindowClassStyles
    {
        CS_BYTEALIGNCLIENT = 0x1000,
        CS_BYTEALIGNWINDOW = 0x2000,
        CS_CLASSDC = 0x0040,
        CS_DBLCLKS = 0x0008,
        CS_DROPSHADOW = 0x00020000,
        CS_GLOBALCLASS = 0x4000,
        CS_HREDRAW = 0x0002,
        CS_NOCLOSE = 0x0200,
        CS_OWNDC = 0x0020,
        CS_PARENTDC = 0x0080,
        CS_SAVEBITS = 0x0800,
        CS_VREDRAW = 0x0001
    }

    public enum ShowWindowCommand
    {
        /// <summary>
        /// Hides the window and activates another window.
        /// </summary>
        Hide = 0,

        /// <summary>
        /// Activates and displays a window. If the window is minimized or
        /// maximized, the system restores it to its original size and position.
        /// An application should specify this flag when displaying the window
        /// for the first time.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Activates the window and displays it as a minimized window.
        /// </summary>
        ShowMinimized = 2,

        /// <summary>
        /// Maximizes the specified window.
        /// </summary>
        Maximize = 3,

        /// <summary>
        /// Activates the window and displays it as a maximized window.
        /// </summary>
        ShowMaximized = 3,

        /// <summary>
        /// Displays a window in its most recent size and position. This value
        /// is similar to <see cref="ShowWindowCommand.Normal"/>, except
        /// the window is not activated.
        /// </summary>
        ShowNoActivate = 4,

        /// <summary>
        /// Activates the window and displays it in its current size and position.
        /// </summary>
        Show = 5,

        /// <summary>
        /// Minimizes the specified window and activates the next top-level
        /// window in the Z order.
        /// </summary>
        Minimize = 6,

        /// <summary>
        /// Displays the window as a minimized window. This value is similar to
        /// <see cref="ShowMinimized"/>, except the
        /// window is not activated.
        /// </summary>
        ShowMinNoActive = 7,

        /// <summary>
        /// Displays the window in its current size and position. This value is
        /// similar to <see cref="Show"/>, except the
        /// window is not activated.
        /// </summary>
        ShowNA = 8,

        /// <summary>
        /// Activates and displays the window. If the window is minimized or
        /// maximized, the system restores it to its original size and position.
        /// An application should specify this flag when restoring a minimized window.
        /// </summary>
        Restore = 9,

        /// <summary>
        /// Sets the show state based on the SW_* value specified in the
        /// STARTUPINFO structure passed to the CreateProcess function by the
        /// program that started the application.
        /// </summary>
        ShowDefault = 10,

        /// <summary>
        ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread
        /// that owns the window is not responding. This flag should only be
        /// used when minimizing windows from a different thread.
        /// </summary>
        ForceMinimize = 11
    }

    public enum SystemMetrics
    {
        SM_CXSCREEN = 0,  // 0x00
        SM_CYSCREEN = 1,  // 0x01
        SM_CXVSCROLL = 2,  // 0x02
        SM_CYHSCROLL = 3,  // 0x03
        SM_CYCAPTION = 4,  // 0x04
        SM_CXBORDER = 5,  // 0x05
        SM_CYBORDER = 6,  // 0x06
        SM_CXDLGFRAME = 7,  // 0x07
        SM_CXFIXEDFRAME = 7,  // 0x07
        SM_CYDLGFRAME = 8,  // 0x08
        SM_CYFIXEDFRAME = 8,  // 0x08
        SM_CYVTHUMB = 9,  // 0x09
        SM_CXHTHUMB = 10, // 0x0A
        SM_CXICON = 11, // 0x0B
        SM_CYICON = 12, // 0x0C
        SM_CXCURSOR = 13, // 0x0D
        SM_CYCURSOR = 14, // 0x0E
        SM_CYMENU = 15, // 0x0F
        SM_CXFULLSCREEN = 16, // 0x10
        SM_CYFULLSCREEN = 17, // 0x11
        SM_CYKANJIWINDOW = 18, // 0x12
        SM_MOUSEPRESENT = 19, // 0x13
        SM_CYVSCROLL = 20, // 0x14
        SM_CXHSCROLL = 21, // 0x15
        SM_DEBUG = 22, // 0x16
        SM_SWAPBUTTON = 23, // 0x17
        SM_CXMIN = 28, // 0x1C
        SM_CYMIN = 29, // 0x1D
        SM_CXSIZE = 30, // 0x1E
        SM_CYSIZE = 31, // 0x1F
        SM_CXSIZEFRAME = 32, // 0x20
        SM_CXFRAME = 32, // 0x20
        SM_CYSIZEFRAME = 33, // 0x21
        SM_CYFRAME = 33, // 0x21
        SM_CXMINTRACK = 34, // 0x22
        SM_CYMINTRACK = 35, // 0x23
        SM_CXDOUBLECLK = 36, // 0x24
        SM_CYDOUBLECLK = 37, // 0x25
        SM_CXICONSPACING = 38, // 0x26
        SM_CYICONSPACING = 39, // 0x27
        SM_MENUDROPALIGNMENT = 40, // 0x28
        SM_PENWINDOWS = 41, // 0x29
        SM_DBCSENABLED = 42, // 0x2A
        SM_CMOUSEBUTTONS = 43, // 0x2B
        SM_SECURE = 44, // 0x2C
        SM_CXEDGE = 45, // 0x2D
        SM_CYEDGE = 46, // 0x2E
        SM_CXMINSPACING = 47, // 0x2F
        SM_CYMINSPACING = 48, // 0x30
        SM_CXSMICON = 49, // 0x31
        SM_CYSMICON = 50, // 0x32
        SM_CYSMCAPTION = 51, // 0x33
        SM_CXSMSIZE = 52, // 0x34
        SM_CYSMSIZE = 53, // 0x35
        SM_CXMENUSIZE = 54, // 0x36
        SM_CYMENUSIZE = 55, // 0x37
        SM_ARRANGE = 56, // 0x38
        SM_CXMINIMIZED = 57, // 0x39
        SM_CYMINIMIZED = 58, // 0x3A
        SM_CXMAXTRACK = 59, // 0x3B
        SM_CYMAXTRACK = 60, // 0x3C
        SM_CXMAXIMIZED = 61, // 0x3D
        SM_CYMAXIMIZED = 62, // 0x3E
        SM_NETWORK = 63, // 0x3F
        SM_CLEANBOOT = 67, // 0x43
        SM_CXDRAG = 68, // 0x44
        SM_CYDRAG = 69, // 0x45
        SM_SHOWSOUNDS = 70, // 0x46
        SM_CXMENUCHECK = 71, // 0x47
        SM_CYMENUCHECK = 72, // 0x48
        SM_SLOWMACHINE = 73, // 0x49
        SM_MIDEASTENABLED = 74, // 0x4A
        SM_MOUSEWHEELPRESENT = 75, // 0x4B
        SM_XVIRTUALSCREEN = 76,
        SM_YVIRTUALSCREEN = 77,
        SM_CXVIRTUALSCREEN = 78, // 0x4E
        SM_CYVIRTUALSCREEN = 79, // 0x4F
        SM_CMONITORS = 80, // 0x50
        SM_SAMEDISPLAYFORMAT = 81, // 0x51
        SM_IMMENABLED = 82, // 0x52
        SM_CXFOCUSBORDER = 83, // 0x53
        SM_CYFOCUSBORDER = 84, // 0x54
        SM_TABLETPC = 86,
        SM_MEDIACENTER = 87,
        SM_STARTER = 88,
        SM_SERVERR2 = 89,
        SM_MOUSEHORIZONTALWHEELPRESENT = 91,
        SM_CXPADDEDBORDER = 92,
        SM_DIGITIZER = 94,
        SM_MAXIMUMTOUCHES = 95,

        SM_REMOTESESSION = 0x1000,
        SM_SHUTTINGDOWN = 0x2000,
        SM_REMOTECONTROL = 0x2001,

        SM_CONVERTABLESLATEMODE = 0x2003,
        SM_SYSTEMDOCKED = 0x2004,
    }
    #endregion

    #region Structures
    [StructLayout(LayoutKind.Sequential)]
    public struct Message
    {
        public IntPtr Hwnd;
        public uint Value;
        public IntPtr WParam;
        public IntPtr LParam;
        public uint Time;
        public Point Point;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct WNDCLASSEX
    {
        public int Size;
        public WindowClassStyles Styles;

        public delegate* unmanaged<IntPtr, uint, nuint, nint, nint> WindowProc;
        public int ClassExtraBytes;
        public int WindowExtraBytes;
        public IntPtr InstanceHandle;
        public IntPtr IconHandle;
        public IntPtr CursorHandle;
        public IntPtr BackgroundBrushHandle;
        public ushort* MenuName;
        public ushort* ClassName;
        public IntPtr SmallIconHandle;
    }
    #endregion Structures

    internal static unsafe class User32
    {
        public unsafe static readonly ushort* IDC_ARROW = (ushort*)32512;

        public const int WM_QUIT = 18;
        public const int WM_ACTIVATEAPP = 28;
        public const int WM_KEYDOWN = 256;
        public const int WM_KEYUP = 257;

        public const int WM_CHAR = 258;

        public const int WM_DEADCHAR = 259;

        public const int WM_SYSKEYDOWN = 260;

        public const int WM_SYSKEYUP = 261;

        public const int WM_SYSCHAR = 262;

        public const int WM_SYSDEADCHAR = 263;

        public const int WM_UNICHAR = 265;

        public const int WM_KEYLAST = 265;

        public const int WM_DESTROY = 2;

        public const int VK_LBUTTON = 1;

        public const int VK_RBUTTON = 2;

        public const int VK_CANCEL = 3;

        public const int VK_MBUTTON = 4;

        public const int VK_XBUTTON1 = 5;

        public const int VK_XBUTTON2 = 6;

        public const int VK_BACK = 8;

        public const int VK_TAB = 9;

        public const int VK_CLEAR = 12;

        public const int VK_RETURN = 13;

        public const int VK_SHIFT = 16;

        public const int VK_CONTROL = 17;

        public const int VK_MENU = 18;

        public const int VK_PAUSE = 19;

        public const int VK_CAPITAL = 20;

        public const int VK_KANA = 21;

        public const int VK_HANGEUL = 21;

        public const int VK_HANGUL = 21;

        public const int VK_IME_ON = 22;

        public const int VK_JUNJA = 23;

        public const int VK_FINAL = 24;

        public const int VK_HANJA = 25;

        public const int VK_KANJI = 25;

        public const int VK_IME_OFF = 26;

        public const int VK_ESCAPE = 27;

        public const int VK_CONVERT = 28;

        public const int VK_NONCONVERT = 29;

        public const int VK_ACCEPT = 30;

        public const int VK_MODECHANGE = 31;

        public const int VK_SPACE = 32;

        public const int VK_PRIOR = 33;

        public const int VK_NEXT = 34;

        public const int VK_END = 35;

        public const int VK_HOME = 36;

        public const int VK_LEFT = 37;

        public const int VK_UP = 38;

        public const int VK_RIGHT = 39;

        public const int VK_DOWN = 40;

        public const int VK_SELECT = 41;

        public const int VK_PRINT = 42;

        public const int VK_EXECUTE = 43;

        public const int VK_SNAPSHOT = 44;

        public const int VK_INSERT = 45;

        public const int VK_DELETE = 46;

        public const int VK_HELP = 47;

        public const int VK_LWIN = 91;

        public const int VK_RWIN = 92;

        public const int VK_APPS = 93;

        public const int VK_SLEEP = 95;

        public const int VK_NUMPAD0 = 96;

        public const int VK_NUMPAD1 = 97;

        public const int VK_NUMPAD2 = 98;

        public const int VK_NUMPAD3 = 99;

        public const int VK_NUMPAD4 = 100;

        public const int VK_NUMPAD5 = 101;

        public const int VK_NUMPAD6 = 102;

        public const int VK_NUMPAD7 = 103;

        public const int VK_NUMPAD8 = 104;

        public const int VK_NUMPAD9 = 105;

        public const int VK_MULTIPLY = 106;

        public const int VK_ADD = 107;

        public const int VK_SEPARATOR = 108;

        public const int VK_SUBTRACT = 109;

        public const int VK_DECIMAL = 110;

        public const int VK_DIVIDE = 111;

        public const int VK_F1 = 112;

        public const int VK_F2 = 113;

        public const int VK_F3 = 114;

        public const int VK_F4 = 115;

        public const int VK_F5 = 116;

        public const int VK_F6 = 117;

        public const int VK_F7 = 118;

        public const int VK_F8 = 119;

        public const int VK_F9 = 120;

        public const int VK_F10 = 121;

        public const int VK_F11 = 122;

        public const int VK_F12 = 123;

        public const int VK_F13 = 124;

        public const int VK_F14 = 125;

        public const int VK_F15 = 126;

        public const int VK_F16 = 127;

        public const int VK_F17 = 128;

        public const int VK_F18 = 129;

        public const int VK_F19 = 130;

        public const int VK_F20 = 131;

        public const int VK_F21 = 132;

        public const int VK_F22 = 133;

        public const int VK_F23 = 134;

        public const int VK_F24 = 135;

        public const int VK_NAVIGATION_VIEW = 136;

        public const int VK_NAVIGATION_MENU = 137;

        public const int VK_NAVIGATION_UP = 138;

        public const int VK_NAVIGATION_DOWN = 139;

        public const int VK_NAVIGATION_LEFT = 140;

        public const int VK_NAVIGATION_RIGHT = 141;

        public const int VK_NAVIGATION_ACCEPT = 142;

        public const int VK_NAVIGATION_CANCEL = 143;

        public const int VK_NUMLOCK = 144;

        public const int VK_SCROLL = 145;

        public const int VK_OEM_NEC_EQUAL = 146;

        public const int VK_OEM_FJ_JISHO = 146;

        public const int VK_OEM_FJ_MASSHOU = 147;

        public const int VK_OEM_FJ_TOUROKU = 148;

        public const int VK_OEM_FJ_LOYA = 149;

        public const int VK_OEM_FJ_ROYA = 150;

        public const int VK_LSHIFT = 160;

        public const int VK_RSHIFT = 161;

        public const int VK_LCONTROL = 162;

        public const int VK_RCONTROL = 163;

        public const int VK_LMENU = 164;

        public const int VK_RMENU = 165;

        public const int VK_BROWSER_BACK = 166;

        public const int VK_BROWSER_FORWARD = 167;

        public const int VK_BROWSER_REFRESH = 168;

        public const int VK_BROWSER_STOP = 169;

        public const int VK_BROWSER_SEARCH = 170;

        public const int VK_BROWSER_FAVORITES = 171;

        public const int VK_BROWSER_HOME = 172;

        public const int VK_VOLUME_MUTE = 173;

        public const int VK_VOLUME_DOWN = 174;

        public const int VK_VOLUME_UP = 175;

        public const int VK_MEDIA_NEXT_TRACK = 176;

        public const int VK_MEDIA_PREV_TRACK = 177;

        public const int VK_MEDIA_STOP = 178;

        public const int VK_MEDIA_PLAY_PAUSE = 179;

        public const int VK_LAUNCH_MAIL = 180;

        public const int VK_LAUNCH_MEDIA_SELECT = 181;

        public const int VK_LAUNCH_APP1 = 182;

        public const int VK_LAUNCH_APP2 = 183;

        public const int VK_OEM_1 = 186;

        public const int VK_OEM_PLUS = 187;

        public const int VK_OEM_COMMA = 188;

        public const int VK_OEM_MINUS = 189;

        public const int VK_OEM_PERIOD = 190;

        public const int VK_OEM_2 = 191;

        public const int VK_OEM_3 = 192;

        public const int VK_ATTN = 246;

        public const int VK_CRSEL = 247;

        public const int VK_EXSEL = 248;

        public const int VK_EREOF = 249;

        public const int VK_PLAY = 250;

        public const int VK_ZOOM = 251;

        public const int VK_NONAME = 252;

        public const int VK_PA1 = 253;

        public const int VK_OEM_CLEAR = 254;
        public const int VK_OEM_102 = 226;


        [DllImport("user32", ExactSpelling = true)]
        public unsafe static extern ushort RegisterClassExW(WNDCLASSEX* lpwcx);

        [DllImport("user32", ExactSpelling = true)]
        public static extern nint DefWindowProcW(IntPtr hWnd, uint msg, nuint wParam, nint lParam);

        [DllImport("user32", ExactSpelling = true)]
        public static extern IntPtr LoadCursorW(IntPtr hInstance, ushort* lpCursorName);

        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        public unsafe static extern int GetMessageW(Message* lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32", ExactSpelling = true)]
        public unsafe static extern int PeekMessageW(Message* lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32", ExactSpelling = true)]
        public static extern int TranslateMessage(Message* lpMsg);

        [DllImport("user32", ExactSpelling = true)]
        public unsafe static extern nint DispatchMessageW(Message* lpMsg);

        [DllImport("user32", ExactSpelling = true)]
        public static extern int GetSystemMetrics(SystemMetrics smIndex);

        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        public static extern int AdjustWindowRectEx(RawRect* lpRect, uint dwStyle, int bMenu, uint dwExStyle);

        [DllImport("user32", ExactSpelling = true)]
        public static extern IntPtr CreateWindowExW(uint dwExStyle, ushort* lpClassName, ushort* lpWindowName, uint dwStyle, int X, int Y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, void* lpParam);

        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        public static extern int DestroyWindow(IntPtr hWnd);

        [DllImport("user32", ExactSpelling = true)]
        public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32", ExactSpelling = true)]
        public static extern void PostQuitMessage(int nExitCode);
    }
}
