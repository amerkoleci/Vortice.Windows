// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.MediaFoundation;

public static partial class VideoFormatGuids
{
    /// <summary>
    /// Returns a standard Media foundation GUID format from a FourCC input
    /// </summary>
    /// <param name="fourCC">FourCC input</param>
    /// <returns>Media foundation unique ID</returns>
    public static Guid FromFourCC(FourCC fourCC)
    {
        return new Guid(string.Concat(fourCC.ToString("I", null), "-0000-0010-8000-00aa00389b71"));
    }

    public static readonly Guid Wmv1 = new("31564d57-0000-0010-8000-00aa00389b71");
    public static readonly Guid Wmv2 = new("32564d57-0000-0010-8000-00aa00389b71");
    public static readonly Guid Wmv3 = new("33564d57-0000-0010-8000-00aa00389b71");

    public static readonly Guid Dvc = new("20637664-0000-0010-8000-00aa00389b71");
    public static readonly Guid Dv50 = new("30357664-0000-0010-8000-00aa00389b71");
    public static readonly Guid Dv25 = new("35327664-0000-0010-8000-00aa00389b71");

    public static readonly Guid H263 = new("33363248-0000-0010-8000-00aa00389b71");
    public static readonly Guid H264 = new("34363248-0000-0010-8000-00aa00389b71");
    public static readonly Guid H265 = new("35363248-0000-0010-8000-00aa00389b71");

    public static readonly Guid Hevc = new("43564548-0000-0010-8000-00aa00389b71");
    public static readonly Guid HevcEs = new("53564548-0000-0010-8000-00aa00389b71");

    public static readonly Guid Vp80 = new("30385056-0000-0010-8000-00aa00389b71");
    public static readonly Guid Vp90 = new("30395056-0000-0010-8000-00aa00389b71");

    public static readonly Guid MultisampledS2 = new("3253534d-0000-0010-8000-00aa00389b71");
    public static readonly Guid M4S2 = new("3253344d-0000-0010-8000-00aa00389b71");
    public static readonly Guid Wvc1 = new("31435657-0000-0010-8000-00aa00389b71");

    public static readonly Guid P010 = new("30313050-0000-0010-8000-00aa00389b71");
    public static readonly Guid AI44 = new("34344941-0000-0010-8000-00aa00389b71");

    public static readonly Guid Dvh1 = new("31687664-0000-0010-8000-00aa00389b71");
    public static readonly Guid Dvhd = new("64687664-0000-0010-8000-00aa00389b71");

    public static readonly Guid MultisampledS1 = new("3153534d-0000-0010-8000-00aa00389b71");

    public static readonly Guid Mp43 = new("3334504d-0000-0010-8000-00aa00389b71");
    public static readonly Guid Mp4s = new("5334504d-0000-0010-8000-00aa00389b71");
    public static readonly Guid Mp4v = new("5634504d-0000-0010-8000-00aa00389b71");
    public static readonly Guid Mpg1 = new("3147504d-0000-0010-8000-00aa00389b71");
    public static readonly Guid Mjpg = new("47504a4d-0000-0010-8000-00aa00389b71");

    public static readonly Guid Dvsl = new("6c737664-0000-0010-8000-00aa00389b71");
    public static readonly Guid YUY2 = new("32595559-0000-0010-8000-00aa00389b71");

    public static readonly Guid Yv12 = new("32315659-0000-0010-8000-00aa00389b71");
    public static readonly Guid P016 = new("36313050-0000-0010-8000-00aa00389b71");

    public static readonly Guid P210 = new("30313250-0000-0010-8000-00aa00389b71");
    public static readonly Guid P216 = new("36313250-0000-0010-8000-00aa00389b71");
    public static readonly Guid I420 = new("30323449-0000-0010-8000-00aa00389b71");
    public static readonly Guid Dvsd = new("64737664-0000-0010-8000-00aa00389b71");

    public static readonly Guid Y42T = new("54323459-0000-0010-8000-00aa00389b71");
    public static readonly Guid NV12 = new("3231564e-0000-0010-8000-00aa00389b71");
    public static readonly Guid NV11 = new("3131564e-0000-0010-8000-00aa00389b71");
    public static readonly Guid Y210 = new("30313259-0000-0010-8000-00aa00389b71");
    public static readonly Guid Y216 = new("36313259-0000-0010-8000-00aa00389b71");
    public static readonly Guid Y410 = new("30313459-0000-0010-8000-00aa00389b71");
    public static readonly Guid Y416 = new("36313459-0000-0010-8000-00aa00389b71");
    public static readonly Guid Y41P = new("50313459-0000-0010-8000-00aa00389b71");
    public static readonly Guid Y41T = new("54313459-0000-0010-8000-00aa00389b71");
    public static readonly Guid Yvu9 = new("39555659-0000-0010-8000-00aa00389b71");
    public static readonly Guid Yvyu = new("55595659-0000-0010-8000-00aa00389b71");
    public static readonly Guid Iyuv = new("56555949-0000-0010-8000-00aa00389b71");
    public static readonly Guid Uyvy = new("59565955-0000-0010-8000-00aa00389b71");

    public static readonly Guid AYUV = new("56555941-0000-0010-8000-00aa00389b71");
    public static readonly Guid Y420O = new("4f303234-0000-0010-8000-00aa00389b71");
}
