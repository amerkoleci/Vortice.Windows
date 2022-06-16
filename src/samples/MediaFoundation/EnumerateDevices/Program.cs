// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.MediaFoundation;
using static Vortice.MediaFoundation.MediaFactory;

namespace EnumerateDevices;

public static unsafe class Program
{
    static void Main()
    {
        // Initialize MediaFoundation
        if (MFStartup(true).Failure)
        {
            return;
        }

        EnumerateAudioCaptureDevices();
        EnumerateVideoCaptureDevices();

        MFShutdown().CheckError();
    }

    private static void EnumerateAudioCaptureDevices()
    {
        Console.WriteLine("Enumerating Audio capture devices:");

        using IMFAttributes attrs = MFCreateAttributes(1);
        attrs.Set(CaptureDeviceAttributeKeys.SourceType, CaptureDeviceAttributeKeys.SourceTypeAudcap);

        using (IMFActivateCollection devices = MFEnumDeviceSources(attrs))
        {
            foreach (IMFActivate capDevice in devices)
            {
                Console.WriteLine(" -    " + capDevice.FriendlyName);
                //IMFMediaSource mediaSource = capDevice.ActivateObject<IMFMediaSource>();
            }
        }
    }

    private static void EnumerateVideoCaptureDevices()
    {
        Console.WriteLine("Enumerating Video capture devices");

        using IMFAttributes attrs = MFCreateAttributes(1);
        attrs.Set(CaptureDeviceAttributeKeys.SourceType, CaptureDeviceAttributeKeys.SourceTypeVidcap);

        using (IMFActivateCollection devices = MFEnumDeviceSources(attrs))
        {
            foreach (IMFActivate capDevice in devices)
            {
                Console.WriteLine(" -    " + capDevice.FriendlyName);
                //IMFMediaSource mediaSource = capDevice.ActivateObject<IMFMediaSource>();
            }
        }
    }
}
