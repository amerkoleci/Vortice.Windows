// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XInput;

public static unsafe class XInput
{
    private static readonly nint s_xinputLibrary;
#if NET6_0_OR_GREATER
    private static readonly delegate* unmanaged<int, out State, int> s_XInputGetState;
    private static readonly delegate* unmanaged<int, Vibration*, int> s_XInputSetState;
    private static readonly delegate* unmanaged<int, DeviceQueryType, out Capabilities, int> s_XInputGetCapabilities;

    private static readonly delegate* unmanaged<int, void> s_XInputEnable;
    private static readonly delegate* unmanaged<int, BatteryDeviceType, out BatteryInformation, int> s_XInputGetBatteryInformation;
    private static readonly delegate* unmanaged<int, uint, out Keystroke, int> s_XInputGetKeystroke;
    private static readonly delegate* unmanaged<int, IntPtr, IntPtr, IntPtr, IntPtr, uint> s_XInputGetAudioDeviceIds;
#else
    private static readonly delegate* unmanaged[Stdcall]<int, out State, int> s_XInputGetState;
    private static readonly delegate* unmanaged[Stdcall]<int, Vibration*, int> s_XInputSetState;
    private static readonly delegate* unmanaged[Stdcall]<int, DeviceQueryType, out Capabilities, int> s_XInputGetCapabilities;

    private static readonly delegate* unmanaged[Stdcall]<int, void> s_XInputEnable;
    private static readonly delegate* unmanaged[Stdcall]<int, BatteryDeviceType, out BatteryInformation, int> s_XInputGetBatteryInformation;
    private static readonly delegate* unmanaged[Stdcall]<int, uint, out Keystroke, int> s_XInputGetKeystroke;
    private static readonly delegate* unmanaged[Stdcall]<int, IntPtr, IntPtr, IntPtr, IntPtr, uint> s_XInputGetAudioDeviceIds;
#endif

    public static readonly XInputVersion Version = XInputVersion.Invalid;

    /// <summary>
    /// When true, allows the use of the unofficial XInputGetState entry point
    /// which enables reporting the Guide button status. As this is an unofficial
    /// API, the default is false. 
    /// </summary>
    public static bool AllowUnofficialAPI { get; set; } = false;

    static XInput()
    {
        s_xinputLibrary = LoadXInputLibrary(out Version);
        if (Version == XInputVersion.Invalid)
        {
            throw new PlatformNotSupportedException("XInput is not supported");
        }

        if (AllowUnofficialAPI)
        {
#if NET6_0_OR_GREATER
            s_XInputGetState = (delegate* unmanaged<int, out State, int>)GetExport("#100");
#else
            s_XInputGetState = (delegate* unmanaged[Stdcall]<int, out State, int>)GetExport("#100");
#endif
        }
        else
        {
#if NET6_0_OR_GREATER
            s_XInputGetState = (delegate* unmanaged<int, out State, int>)GetExport("XInputGetState");
#else
            s_XInputGetState = (delegate* unmanaged[Stdcall]<int, out State, int>)GetExport("XInputGetState");
#endif
        }

#if NET6_0_OR_GREATER
        s_XInputSetState = (delegate* unmanaged<int, Vibration*, int>)GetExport("XInputSetState");
        s_XInputGetCapabilities = (delegate* unmanaged<int, DeviceQueryType, out Capabilities, int>)GetExport("XInputGetCapabilities");
#else
        s_XInputSetState = (delegate* unmanaged[Stdcall]<int, Vibration*, int>)GetExport("XInputSetState");
        s_XInputGetCapabilities = (delegate* unmanaged[Stdcall]<int, DeviceQueryType, out Capabilities, int>)GetExport("XInputGetCapabilities");
#endif

        if (Version != XInputVersion.Version910)
        {
#if NET6_0_OR_GREATER
            s_XInputEnable = (delegate* unmanaged<int, void>)GetExport("XInputEnable");
            s_XInputGetBatteryInformation = (delegate* unmanaged<int, BatteryDeviceType, out BatteryInformation, int>)GetExport("XInputGetBatteryInformation");
            s_XInputGetKeystroke = (delegate* unmanaged<int, uint, out Keystroke, int>)GetExport("XInputGetKeystroke");
#else
            s_XInputEnable = (delegate* unmanaged[Stdcall]<int, void>)GetExport("XInputEnable");
            s_XInputGetBatteryInformation = (delegate* unmanaged[Stdcall]<int, BatteryDeviceType, out BatteryInformation, int>)GetExport("XInputGetBatteryInformation");
            s_XInputGetKeystroke = (delegate* unmanaged[Stdcall]<int, uint, out Keystroke, int>)GetExport("XInputGetKeystroke");
#endif
        }

        if (Version == XInputVersion.Version14)
        {
#if NET6_0_OR_GREATER
            s_XInputGetAudioDeviceIds = (delegate* unmanaged<int, IntPtr, IntPtr, IntPtr, IntPtr, uint>)GetExport("XInputGetAudioDeviceIds");
#else
            s_XInputGetAudioDeviceIds = (delegate* unmanaged[Stdcall]<int, IntPtr, IntPtr, IntPtr, IntPtr, uint>)GetExport("XInputGetAudioDeviceIds");
#endif
        }
    }

    /// <summary>
    /// Retrieves the current state of the specified controller.
    /// </summary>
    /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
    /// <param name="state">Instance of <see cref="State"/> struct.</param>
    /// <returns>True if success, false if not connected or error.</returns>
    public static bool GetState(int userIndex, out State state)
    {
        return s_XInputGetState(userIndex, out state) == 0;
    }

    /// <summary>
    /// Sets the gamepad vibration.
    /// </summary>
    /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
    /// <param name="leftMotor">The level of the left vibration motor. Valid values are between 0.0 and 1.0, where 0.0 signifies no motor use and 1.0 signifies max vibration.</param>
    /// <param name="rightMotor">The level of the right vibration motor. Valid values are between 0.0 and 1.0, where 0.0 signifies no motor use and 1.0 signifies max vibration.</param>
    /// <returns>True if succeed, false otherwise.</returns>
    public static bool SetVibration(int userIndex, float leftMotor, float rightMotor)
    {
        Vibration vibration = new((ushort)(leftMotor * ushort.MaxValue), (ushort)(rightMotor * ushort.MaxValue));
        return s_XInputSetState(userIndex, &vibration) == 0;
    }

    /// <summary>
    /// Sets the gamepad vibration.
    /// </summary>
    /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
    /// <param name="leftMotorSpeed">The level of the left vibration motor speed.</param>
    /// <param name="rightMotorSpeed">The level of the right vibration motor speed.</param>
    /// <returns>True if succeed, false otherwise.</returns>
    public static bool SetVibration(int userIndex, ushort leftMotorSpeed, ushort rightMotorSpeed)
    {
        Vibration vibration = new(leftMotorSpeed, rightMotorSpeed);
        return s_XInputSetState(userIndex, &vibration) == 0;
    }

    /// <summary>
    /// Sets the gamepad vibration.
    /// </summary>
    /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
    /// <param name="vibration">The <see cref="Vibration"/> to set.</param>
    /// <returns>True if succeed, false otherwise.</returns>
    public static bool SetVibration(int userIndex, Vibration vibration)
    {
        return s_XInputSetState(userIndex, &vibration) == 0;
    }

    /// <summary>
    /// Sets the reporting.
    /// </summary>
    /// <param name="enableReporting">if set to <c>true</c> [enable reporting].</param>
    public static void SetReporting(bool enableReporting)
    {
        if (Version == XInputVersion.Version910)
        {
            ThrowNotSupportedXInput91("XInputEnable");
        }

        s_XInputEnable(enableReporting ? 1 : 0);
    }

    /// <summary>
    /// Retrieves the battery type and charge status of a wireless controller.
    /// </summary>
    /// <param name="userIndex">Index of the user's controller. Can be a value in the range 0–3. </param>
    /// <param name="batteryDeviceType">Type of the battery device.</param>
    /// <returns>Instance of <see cref="BatteryInformation"/>.</returns>
    public static BatteryInformation GetBatteryInformation(int userIndex, BatteryDeviceType batteryDeviceType)
    {
        if (Version == XInputVersion.Version910)
        {
            ThrowNotSupportedXInput91("XInputGetBatteryInformation");
        }

        s_XInputGetBatteryInformation(userIndex, batteryDeviceType, out BatteryInformation result);
        return result;
    }

    /// <summary>
    /// Retrieves the battery type and charge status of a wireless controller.
    /// </summary>
    /// <param name="userIndex">Index of the user's controller. Can be a value in the range 0–3. </param>
    /// <param name="batteryDeviceType">Type of the battery device.</param>
    /// <param name="batteryInformation">The battery information.</param>
    /// <returns>True if succeed, false otherwise.</returns>
    public static bool GetBatteryInformation(int userIndex, BatteryDeviceType batteryDeviceType, out BatteryInformation batteryInformation)
    {
        return s_XInputGetBatteryInformation(userIndex, batteryDeviceType, out batteryInformation) == 0;
    }

    /// <summary>
    /// Retrieves the capabilities and features of a connected controller.
    /// </summary>
    /// <param name="userIndex">Index of the user's controller. Can be a value in the range 0–3. </param>
    /// <param name="deviceQueryType">Type of the device query.</param>
    /// <param name="capabilities">The capabilities of this controller.</param>
    /// <returns>True if the controller is connected and succeed, false otherwise.</returns>
    public static bool GetCapabilities(int userIndex, DeviceQueryType deviceQueryType, out Capabilities capabilities)
    {
        return s_XInputGetCapabilities(userIndex, deviceQueryType, out capabilities) == 0;
    }

    /// <summary>
    /// Retrieves a gamepad input event.
    /// </summary>
    /// <param name="userIndex">Index of the user's controller. Can be a value in the range 0–3. </param>
    /// <param name="keystroke">The keystroke.</param>
    /// <returns>False if the controller is not connected and no new keys have been pressed, true otherwise.</returns>
    public static bool GetKeystroke(int userIndex, out Keystroke keystroke)
    {
        if (Version == XInputVersion.Version910)
        {
            ThrowNotSupportedXInput91("XInputGetKeystroke");
        }

        return s_XInputGetKeystroke(userIndex, 0u, out keystroke) == 0;
    }

    public static uint GetAudioDeviceIds(int userIndex, IntPtr renderDeviceId, IntPtr renderCount, IntPtr captureDeviceId, IntPtr captureCount)
    {
        if (Version != XInputVersion.Version14)
        {
            throw new NotSupportedException($"XInputGetAudioDeviceIds is only supported on XInput 1.4");
        }

        return s_XInputGetAudioDeviceIds(userIndex, renderDeviceId, renderCount, captureDeviceId, captureCount);
    }

    private static void ThrowNotSupportedXInput91(string name)
    {
        throw new NotSupportedException($"{name} is not supported on XInput9.1.0");
    }

#if NET6_0_OR_GREATER
    private static nint LoadXInputLibrary(out XInputVersion version)
    {
        if (NativeLibrary.TryLoad("xinput1_4.dll", out IntPtr libraryHandle))
        {
            version = XInputVersion.Version14;
            return libraryHandle;
        }
        else if (NativeLibrary.TryLoad("xinput1_3.dll", out libraryHandle))
        {
            version = XInputVersion.Version13;
            return libraryHandle;
        }
        else if (NativeLibrary.TryLoad("xinput9_1_0.dll", out libraryHandle))
        {
            version = XInputVersion.Version910;
            return libraryHandle;
        }

        version = XInputVersion.Invalid;
        return 0;
    }

    private static nint GetExport(string name) => NativeLibrary.GetExport(s_xinputLibrary, name);

#else
    private static nint LoadXInputLibrary(out XInputVersion version)
    {
        nint libraryHandle = LoadLibrary("xinput1_4.dll");
        if (libraryHandle != 0)
        {
            version = XInputVersion.Version14;
            return libraryHandle;
        }

        libraryHandle = LoadLibrary("xinput1_3.dll");
        if (libraryHandle != 0)
        {
            version = XInputVersion.Version13;
            return libraryHandle;
        }

        libraryHandle = LoadLibrary("xinput9_1_0.dll");
        if (libraryHandle != 0)
        {
            version = XInputVersion.Version910;
            return libraryHandle;
        }

        version = XInputVersion.Invalid;
        return libraryHandle;
    }

    private static nint GetExport(string name) => GetProcAddress(s_xinputLibrary, name);

    [DllImport("kernel32")]
    private static extern nint LoadLibrary(string fileName);

    [DllImport("kernel32")]
    private static extern nint GetProcAddress(nint module, string procName);

    [DllImport("kernel32")]
    private static extern int FreeLibrary(nint module);
#endif
}
