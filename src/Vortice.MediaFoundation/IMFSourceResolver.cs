// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime.Win32;

namespace Vortice.MediaFoundation;

public partial class IMFSourceResolver
{
    public IMFMediaSource CreateObjectFromURL(string url, SourceResolverFlags flags = SourceResolverFlags.None, IPropertyStore? propertyStore = null)
    {
        CreateObjectFromURL(url, (int)(flags | SourceResolverFlags.MediaSource), propertyStore, out ObjectType objectType, out IntPtr nativePtr).CheckError();
        if (objectType != ObjectType.MediaSource)
        {
            throw new InvalidOperationException("Object type is not MediaSource");
        }

        return new IMFMediaSource(nativePtr);
    }

    public MFByteStream CreateObjectFromURLAsByteStream(string url, SourceResolverFlags flags = SourceResolverFlags.None, IPropertyStore? propertyStore = null)
    {
        CreateObjectFromURL(url, (int)(flags | SourceResolverFlags.ByteStream), propertyStore, out ObjectType objectType, out IntPtr nativePtr).CheckError();
        if (objectType != ObjectType.ByteStream)
        {
            throw new InvalidOperationException("Object type is not ByteStream");
        }

        return new MFByteStream(nativePtr);
    }

    public IMFMediaSource CreateObjectFromByteStream(IMFByteStream stream, string url, SourceResolverFlags flags = SourceResolverFlags.None, IPropertyStore? propertyStore = null)
    {
        CreateObjectFromByteStream(stream, url, (int)(flags | SourceResolverFlags.MediaSource), propertyStore, out ObjectType objectType, out IntPtr nativePtr).CheckError();
        if (objectType != ObjectType.MediaSource)
        {
            throw new InvalidOperationException("Object type is not MediaSource");
        }

        return new IMFMediaSource(nativePtr);
    }

    public MFByteStream CreateObjectFromByteStreamAsByteStream(IMFByteStream stream, string url, SourceResolverFlags flags = SourceResolverFlags.None, IPropertyStore? propertyStore = null)
    {
        CreateObjectFromByteStream(stream, url, (int)(flags | SourceResolverFlags.MediaSource), propertyStore, out ObjectType objectType, out IntPtr nativePtr).CheckError();
        if (objectType != ObjectType.ByteStream)
        {
            throw new InvalidOperationException("Object type is not ByteStream");
        }

        return new MFByteStream(nativePtr);
    }
}
