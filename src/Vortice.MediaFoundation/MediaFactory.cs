// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;
using Vortice.Multimedia;

namespace Vortice.MediaFoundation;

public unsafe partial class MediaFactory
{
    public static Result MFStartup(bool useLightVersion = false) => MFStartup(Version, useLightVersion ? 1 : 0);

    public static IMFMediaSession MFCreateMediaSession(IMFAttributes configuration)
    {
        MFCreateMediaSession(configuration, out IMFMediaSession session).CheckError();
        return session;
    }

    public static IMFSourceResolver MFCreateSourceResolver()
    {
        MFCreateSourceResolver(out IMFSourceResolver sourceResolver).CheckError();
        return sourceResolver;
    }

    public static IMFMediaSource MFCreateDeviceSource(IMFAttributes attributes)
    {
        MFCreateDeviceSource(attributes, out IMFMediaSource mediaSource).CheckError();
        return mediaSource;
    }

    public static IMFDXGIDeviceManager MFCreateDXGIDeviceManager()
    {
        MFCreateDXGIDeviceManager(out uint resetToken, out IMFDXGIDeviceManager deviceManager).CheckError();
        deviceManager.ResetToken = resetToken;
        return deviceManager;
    }

    public static IMFPresentationClock MFCreatePresentationClock()
    {
        MFCreatePresentationClock(out IMFPresentationClock presentationClock).CheckError();
        return presentationClock;
    }

    public static IMFTopology MFCreateTopology()
    {
        MFCreateTopology(out IMFTopology topology).CheckError();
        return topology;
    }

    public static IMFTopologyNode MFCreateTopologyNode(TopologyType nodeType)
    {
        MFCreateTopologyNode(nodeType, out IMFTopologyNode node).CheckError();
        return node;
    }

    public static IMFPresentationTimeSource MFCreateSystemTimeSource()
    {
        MFCreateSystemTimeSource(out IMFPresentationTimeSource systemTimeSource).CheckError();
        return systemTimeSource;
    }

    public static IMFMediaTypeHandler MFCreateSimpleTypeHandler()
    {
        MFCreateSimpleTypeHandler(out IMFMediaTypeHandler handler).CheckError();
        return handler;
    }

    public static IMFMediaSink MFCreateAudioRenderer(IMFAttributes audioAttributes)
    {
        MFCreateAudioRenderer(audioAttributes, out IMFMediaSink sink).CheckError();
        return sink;
    }

    public static IMFActivate MFCreateAudioRendererActivate()
    {
        MFCreateAudioRendererActivate(out IMFActivate activate).CheckError();
        return activate;
    }

    public static IMFActivate MFCreateVideoRendererActivate(IntPtr hwndVideo)
    {
        MFCreateVideoRendererActivate(hwndVideo, out IMFActivate activate).CheckError();
        return activate;
    }

    public static unsafe IMFActivateCollection MFEnumDeviceSources(IMFAttributes attributes)
    {
        MFEnumDeviceSources(attributes, out IntPtr pSourceActivate, out uint count).CheckError();
        return new(pSourceActivate, count);
    }

    public static unsafe IMFActivateCollection MFEnumAudioDeviceSources()
    {
        using IMFAttributes attributes = MFCreateAttributes(1);
        attributes.SourceType = CaptureDeviceAttributeKeys.SourceTypeAudcap;
        MFEnumDeviceSources(attributes, out IntPtr pSourceActivate, out uint count).CheckError();
        return new(pSourceActivate, count);
    }

    public static unsafe IMFActivateCollection MFEnumAudioDeviceSources(AudioEndpointRole audioEndpointRole)
    {
        using IMFAttributes attributes = MFCreateAttributes(2);
        attributes.SourceType = CaptureDeviceAttributeKeys.SourceTypeAudcap;
        attributes.AudioEndpointRole = audioEndpointRole;
        MFEnumDeviceSources(attributes, out IntPtr pSourceActivate, out uint count).CheckError();
        return new(pSourceActivate, count);
    }

    public static unsafe IMFActivateCollection MFEnumVideoDeviceSources()
    {
        using IMFAttributes attributes = MFCreateAttributes(1);
        attributes.SourceType = CaptureDeviceAttributeKeys.SourceTypeVidcap;
        MFEnumDeviceSources(attributes, out IntPtr pSourceActivate, out uint count).CheckError();
        return new(pSourceActivate, count);
    }

    public static unsafe IMFActivateCollection MFEnumVideoDeviceSources(Guid videoDeviceCategory)
    {
        using IMFAttributes attributes = MFCreateAttributes(2);
        attributes.SourceType = CaptureDeviceAttributeKeys.SourceTypeVidcap;
        attributes.VideoDeviceCategory = videoDeviceCategory;
        MFEnumDeviceSources(attributes, out IntPtr pSourceActivate, out uint count).CheckError();
        return new(pSourceActivate, count);
    }

    public static unsafe IStream MFCreateStreamOnMFByteStream(IMFByteStream byteStream)
    {
        MFCreateStreamOnMFByteStream(byteStream, out IStream stream).CheckError();
        return stream;
    }

    public static uint HI32(ulong unPacked)
    {
        return (uint)(unPacked >> 32);
    }

    public static uint LO32(ulong unPacked)
    {
        return (uint)(unPacked);
    }

    public static ulong Pack2UInt32AsUInt64(uint high, uint low)
    {
        return ((ulong)(high) << 32) | low;
    }

    public static void Unpack2UInt32AsUInt64(ulong unpacked, out uint highValue, out uint lowValue)
    {
        highValue = HI32(unpacked);
        lowValue = LO32(unpacked);
    }

    public static ulong PackSize(uint width, uint height)
    {
        return Pack2UInt32AsUInt64(width, height);
    }

    public static void UnpackSize(ulong unpacked, out uint width, out uint height)
    {
        Unpack2UInt32AsUInt64(unpacked, out width, out height);
    }

    public static ulong PackRatio(int numerator, uint denominator)
    {
        return Pack2UInt32AsUInt64((uint)(numerator), denominator);
    }

    public static void UnpackRatio(ulong unpacked, out uint numerator, out uint denominator)
    {
        Unpack2UInt32AsUInt64(unpacked, out numerator, out denominator);
    }

    public static uint MFGetAttributeUInt32(IMFAttributes attributes, Guid guidKey, uint defaultValue = 0u)
    {
        if (attributes.GetUInt32(guidKey, out uint result).Failure)
        {
            result = defaultValue;
        }

        return result;
    }

    public static ulong MFGetAttributeUInt64(IMFAttributes attributes, Guid guidKey, ulong defaultValue = 0u)
    {
        if (attributes.GetUInt64(guidKey, out ulong result).Failure)
        {
            result = defaultValue;
        }

        return result;
    }

    public static double MFGetAttributeDouble(IMFAttributes attributes, Guid guidKey, double defaultValue = 0.0)
    {
        if (attributes.GetDouble(guidKey, out double result).Failure)
        {
            result = defaultValue;
        }

        return result;
    }

    public static Result MFGetAttribute2UInt32asUInt64(IMFAttributes attributes, Guid guidKey, out uint high32, out uint low32)
    {
        Result hr = attributes.GetUInt64(guidKey, out ulong unpacked);
        if (hr.Failure)
        {
            high32 = default;
            low32 = default;
            return hr;
        }

        Unpack2UInt32AsUInt64(unpacked, out high32, out low32);
        return hr;
    }


    public static Result MFSetAttribute2UInt32asUInt64(IMFAttributes attributes, Guid guidKey, uint unHigh32, uint unLow32)
    {
        return attributes.Set(guidKey, Pack2UInt32AsUInt64(unHigh32, unLow32));
    }

    public static Result MFGetAttributeRatio(IMFAttributes attributes, Guid guidKey, out uint numerator, out uint denominator)
    {
        return MFGetAttribute2UInt32asUInt64(attributes, guidKey, out numerator, out denominator);
    }

    public static Result MFGetAttributeSize(IMFAttributes attributes, Guid guidKey, out uint width, out uint height)
    {
        return MFGetAttribute2UInt32asUInt64(attributes, guidKey, out width, out height);
    }

    public static Result MFSetAttributeRatio(IMFAttributes attributes, Guid guidKey, uint numerator, uint denominator)
    {
        return MFSetAttribute2UInt32asUInt64(attributes, guidKey, numerator, denominator);
    }

    public static Result MFSetAttributeSize(IMFAttributes attributes, Guid guidKey, uint width, uint height)
    {
        return MFSetAttribute2UInt32asUInt64(attributes, guidKey, width, height);
    }

    #region IMFSourceReader
    public static unsafe IMFSourceReader MFCreateSourceReaderFromByteStream(byte[] buffer, IMFAttributes? attributes = null)
    {
        var byteStream = new MFByteStream(new MemoryStream(buffer));
        IMFSourceReader reader = MFCreateSourceReaderFromByteStream(byteStream, attributes);
        reader._byteStream = byteStream;
        return reader;
    }

    public static unsafe IMFSourceReader MFCreateSourceReaderFromByteStream(Stream buffer, IMFAttributes? attributes = null)
    {
        var byteStream = new MFByteStream(buffer);
        IMFSourceReader reader = MFCreateSourceReaderFromByteStream(byteStream, attributes);
        reader._byteStream = byteStream;
        return reader;
    }

    public static unsafe IMFSourceReader MFCreateSourceReaderFromByteStream(ComStream comStream, IMFAttributes attributes = null)
    {
        var byteStream = new MFByteStream(comStream);
        IMFSourceReader reader = MFCreateSourceReaderFromByteStream(byteStream, attributes);
        reader._byteStream = byteStream;
        return reader;
    }
    #endregion

    public static IMFVirtualCamera MFCreateVirtualCamera(
        VirtualCameraType type,
        VirtualCameraLifetime lifetime,
        VirtualCameraAccess access,
        string friendlyName,
        string sourceId,
        Guid[]? categories = default)
    {
        nint virtualCameraPtr = default;
        Result result;

        fixed (char* sourceId_ = sourceId)
        fixed (char* friendlyName_ = friendlyName)
        {
            if (categories?.Length > 0)
            {
                fixed (Guid* categories_ = categories)
                    result = (Result)MFCreateVirtualCamera_(
                        (int)type,
                        (int)lifetime,
                        (int)access,
                        friendlyName_,
                        sourceId_,
                        categories_,
                        (uint)categories.Length,
                        &virtualCameraPtr
                    );
            }
            else
            {
                result = (Result)MFCreateVirtualCamera_(
                    (int)type,
                    (int)lifetime,
                    (int)access,
                    friendlyName_,
                    sourceId_,
                    null,
                    0,
                    &virtualCameraPtr);
            }
        }

        result.CheckError();

        return new IMFVirtualCamera(virtualCameraPtr);
    }

    public static bool MFIsVirtualCameraTypeSupported(VirtualCameraType type)
    {
        RawBool supported;
        Result result = MFIsVirtualCameraTypeSupported_(unchecked((int)type), &supported);
        result.CheckError();
        return supported;
    }
}
