// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Win32
{
    internal static unsafe class User32
    {
        public unsafe static readonly char* IDC_ARROW = (char*)32512;

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
    }
}
