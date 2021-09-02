// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Vortice.DirectInput
{
    internal sealed class PropertyGuids
    {
        /// <summary>
        /// Constant Buffersize
        /// </summary>
        /// <unmanaged>DIPROP_BUFFERSIZE</unmanaged>
        /// <unmanaged-short>DIPROP_BUFFERSIZE</unmanaged-short>
        public static readonly System.IntPtr Buffersize = new System.IntPtr(unchecked(1) );
        /// <summary>
        /// Constant Axismode
        /// </summary>
        /// <unmanaged>DIPROP_AXISMODE</unmanaged>
        /// <unmanaged-short>DIPROP_AXISMODE</unmanaged-short>
        public static readonly System.IntPtr Axismode = new System.IntPtr(unchecked(2)  );
        /// <summary>
        /// Constant Granularity
        /// </summary>
        /// <unmanaged>DIPROP_GRANULARITY</unmanaged>
        /// <unmanaged-short>DIPROP_GRANULARITY</unmanaged-short>
        public static readonly System.IntPtr Granularity = new System.IntPtr(unchecked(3) );
        /// <summary>
        /// Constant Range
        /// </summary>
        /// <unmanaged>DIPROP_RANGE</unmanaged>
        /// <unmanaged-short>DIPROP_RANGE</unmanaged-short>
        public static readonly System.IntPtr Range = new System.IntPtr(unchecked((4) ) );
        /// <summary>
        /// Constant Deadzone
        /// </summary>
        /// <unmanaged>DIPROP_DEADZONE</unmanaged>
        /// <unmanaged-short>DIPROP_DEADZONE</unmanaged-short>
        public static readonly System.IntPtr Deadzone = new System.IntPtr(unchecked((5) ) );
        /// <summary>
        /// Constant Saturation
        /// </summary>
        /// <unmanaged>DIPROP_SATURATION</unmanaged>
        /// <unmanaged-short>DIPROP_SATURATION</unmanaged-short>
        public static readonly System.IntPtr Saturation = new System.IntPtr(unchecked((6) ) );
        /// <summary>
        /// Constant Ffgain
        /// </summary>
        /// <unmanaged>DIPROP_FFGAIN</unmanaged>
        /// <unmanaged-short>DIPROP_FFGAIN</unmanaged-short>
        public static readonly System.IntPtr Ffgain = new System.IntPtr(unchecked((7) ) );
        /// <summary>
        /// Constant Ffload
        /// </summary>
        /// <unmanaged>DIPROP_FFLOAD</unmanaged>
        /// <unmanaged-short>DIPROP_FFLOAD</unmanaged-short>
        public static readonly System.IntPtr Ffload = new System.IntPtr(unchecked((8) ) );
        /// <summary>
        /// Constant Autocenter
        /// </summary>
        /// <unmanaged>DIPROP_AUTOCENTER</unmanaged>
        /// <unmanaged-short>DIPROP_AUTOCENTER</unmanaged-short>
        public static readonly System.IntPtr Autocenter = new System.IntPtr(unchecked((9) ) );
        /// <summary>
        /// Constant Calibrationmode
        /// </summary>
        /// <unmanaged>DIPROP_CALIBRATIONMODE</unmanaged>
        /// <unmanaged-short>DIPROP_CALIBRATIONMODE</unmanaged-short>
        public static readonly System.IntPtr Calibrationmode = new System.IntPtr(unchecked((10) ) );
        /// <summary>
        /// Constant Calibration
        /// </summary>
        /// <unmanaged>DIPROP_CALIBRATION</unmanaged>
        /// <unmanaged-short>DIPROP_CALIBRATION</unmanaged-short>
        public static readonly System.IntPtr Calibration = new System.IntPtr(unchecked((11) ) );
        /// <summary>
        /// Constant Guidandpath
        /// </summary>
        /// <unmanaged>DIPROP_GUIDANDPATH</unmanaged>
        /// <unmanaged-short>DIPROP_GUIDANDPATH</unmanaged-short>
        public static readonly System.IntPtr Guidandpath = new System.IntPtr(unchecked((12) ) );
        /// <summary>
        /// Constant Instancename
        /// </summary>
        /// <unmanaged>DIPROP_INSTANCENAME</unmanaged>
        /// <unmanaged-short>DIPROP_INSTANCENAME</unmanaged-short>
        public static readonly System.IntPtr Instancename = new System.IntPtr(unchecked((13) ) );
        /// <summary>
        /// Constant Productname
        /// </summary>
        /// <unmanaged>DIPROP_PRODUCTNAME</unmanaged>
        /// <unmanaged-short>DIPROP_PRODUCTNAME</unmanaged-short>
        public static readonly System.IntPtr Productname = new System.IntPtr(unchecked((14) ) );
        /// <summary>
        /// Constant Joystickid
        /// </summary>
        /// <unmanaged>DIPROP_JOYSTICKID</unmanaged>
        /// <unmanaged-short>DIPROP_JOYSTICKID</unmanaged-short>
        public static readonly System.IntPtr Joystickid = new System.IntPtr(unchecked((15) ) );
        /// <summary>
        /// Constant Getportdisplayname
        /// </summary>
        /// <unmanaged>DIPROP_GETPORTDISPLAYNAME</unmanaged>
        /// <unmanaged-short>DIPROP_GETPORTDISPLAYNAME</unmanaged-short>
        public static readonly System.IntPtr Getportdisplayname = new System.IntPtr(unchecked((16) ) );
        /// <summary>
        /// Constant Physicalrange
        /// </summary>
        /// <unmanaged>DIPROP_PHYSICALRANGE</unmanaged>
        /// <unmanaged-short>DIPROP_PHYSICALRANGE</unmanaged-short>
        public static readonly System.IntPtr Physicalrange = new System.IntPtr(unchecked((18) ) );
        /// <summary>
        /// Constant Logicalrange
        /// </summary>
        /// <unmanaged>DIPROP_LOGICALRANGE</unmanaged>
        /// <unmanaged-short>DIPROP_LOGICALRANGE</unmanaged-short>
        public static readonly System.IntPtr Logicalrange = new System.IntPtr(unchecked((19) ) );
        /// <summary>
        /// Constant Keyname
        /// </summary>
        /// <unmanaged>DIPROP_KEYNAME</unmanaged>
        /// <unmanaged-short>DIPROP_KEYNAME</unmanaged-short>
        public static readonly System.IntPtr Keyname = new System.IntPtr(unchecked((20) ) );
        /// <summary>
        /// Constant Cpoints
        /// </summary>
        /// <unmanaged>DIPROP_CPOINTS</unmanaged>
        /// <unmanaged-short>DIPROP_CPOINTS</unmanaged-short>
        public static readonly System.IntPtr Cpoints = new System.IntPtr(unchecked((21) ) );
        /// <summary>
        /// Constant Appdata
        /// </summary>
        /// <unmanaged>DIPROP_APPDATA</unmanaged>
        /// <unmanaged-short>DIPROP_APPDATA</unmanaged-short>
        public static readonly System.IntPtr Appdata = new System.IntPtr(unchecked((22) ) );
        /// <summary>
        /// Constant Scancode
        /// </summary>
        /// <unmanaged>DIPROP_SCANCODE</unmanaged>
        /// <unmanaged-short>DIPROP_SCANCODE</unmanaged-short>
        public static readonly System.IntPtr Scancode = new System.IntPtr(unchecked((23) ) );
        /// <summary>
        /// Constant Vidpid
        /// </summary>
        /// <unmanaged>DIPROP_VIDPID</unmanaged>
        /// <unmanaged-short>DIPROP_VIDPID</unmanaged-short>
        public static readonly System.IntPtr Vidpid = new System.IntPtr(unchecked((24) ) );
        /// <summary>
        /// Constant Username
        /// </summary>
        /// <unmanaged>DIPROP_USERNAME</unmanaged>
        /// <unmanaged-short>DIPROP_USERNAME</unmanaged-short>
        public static readonly System.IntPtr Username = new System.IntPtr(unchecked((25) ) );
        /// <summary>
        /// Constant Typename
        /// </summary>
        /// <unmanaged>DIPROP_TYPENAME</unmanaged>
        /// <unmanaged-short>DIPROP_TYPENAME</unmanaged-short>
        public static readonly System.IntPtr Typename = new System.IntPtr(unchecked((26) ) );
    }
}
