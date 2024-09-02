// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;
using Vortice.DXGI;
using Vortice.Multimedia;

namespace Vortice.MediaFoundation;

public unsafe partial class IMFAttributes
{
    public string FriendlyName
    {
        get => GetString(CaptureDeviceAttributeKeys.FriendlyName);
        set => Set(CaptureDeviceAttributeKeys.FriendlyName, value);
    }

    public Guid SourceType
    {
        get => GetGUID(CaptureDeviceAttributeKeys.SourceType);
        set => Set(CaptureDeviceAttributeKeys.SourceType, value);
    }

    public bool IsAudioDevice => SourceType == CaptureDeviceAttributeKeys.SourceTypeAudcap;

    public RegisterTypeInfo MediaType
    {
        get
        {
            if (SourceType != CaptureDeviceAttributeKeys.SourceTypeVidcap)
            {
                return default;
            }

            RegisterTypeInfo result = default;
            GetBlob(CaptureDeviceAttributeKeys.MediaType, &result, (uint)sizeof(RegisterTypeInfo), IntPtr.Zero);
            return result;
        }
        set
        {
            if (SourceType != CaptureDeviceAttributeKeys.SourceTypeVidcap)
            {
                return;
            }

            SetBlob(CaptureDeviceAttributeKeys.MediaType, ref value);
        }
    }

    public string AudioEndPointID
    {
        get
        {
            if (SourceType != CaptureDeviceAttributeKeys.SourceTypeAudcap)
            {
                return string.Empty;
            }

            return GetString(CaptureDeviceAttributeKeys.SourceTypeAudcapEndpointId);
        }
        set
        {
            if (SourceType != CaptureDeviceAttributeKeys.SourceTypeAudcap)
            {
                return;
            }

            Set(CaptureDeviceAttributeKeys.SourceTypeAudcapEndpointId, value);
        }
    }

    public Guid VideoDeviceCategory
    {
        get
        {
            if (SourceType != CaptureDeviceAttributeKeys.SourceTypeVidcap)
            {
                return Guid.Empty;
            }

            return GetGUID(CaptureDeviceAttributeKeys.SourceTypeVidcapCategory);
        }
        set
        {
            if (SourceType != CaptureDeviceAttributeKeys.SourceTypeVidcap)
            {
                return;
            }

            Set(CaptureDeviceAttributeKeys.SourceTypeVidcapCategory, value);
        }
    }

    public bool VideoDeviceIsHardware
    {
        get
        {
            if (SourceType != CaptureDeviceAttributeKeys.SourceTypeVidcap)
            {
                return false;
            }

            return GetUInt32(CaptureDeviceAttributeKeys.SourceTypeVidcapHwSource) == 1;
        }
        set
        {
            if (SourceType != CaptureDeviceAttributeKeys.SourceTypeVidcap)
            {
                return;
            }

            Set(CaptureDeviceAttributeKeys.SourceTypeVidcapHwSource, value);
        }
    }

    public string SymbolicLink
    {
        get
        {
            if (SourceType != CaptureDeviceAttributeKeys.SourceTypeVidcap)
            {
                return string.Empty;
            }

            return GetString(CaptureDeviceAttributeKeys.SourceTypeVidcapSymbolicLink);
        }
        set
        {
            if (SourceType != CaptureDeviceAttributeKeys.SourceTypeVidcap)
            {
                return;
            }

            Set(CaptureDeviceAttributeKeys.SourceTypeVidcapSymbolicLink, value);
        }
    }

    public AudioStreamCategory AudioCategory
    {
        get => GetEnumValue(MediaEngineAttributeKeys.AudioCategory);
        set => SetEnumValue(MediaEngineAttributeKeys.AudioCategory, value);
    }

    public AudioEndpointRole AudioEndpointRole
    {
        get => GetEnumValue(MediaEngineAttributeKeys.AudioEndpointRole);
        set => SetEnumValue(MediaEngineAttributeKeys.AudioEndpointRole, value);
    }

    public MediaEngineProtectionFlags ContentProtectionFlags
    {
        get => GetEnumValue(MediaEngineAttributeKeys.ContentProtectionFlags);
        set => SetEnumValue(MediaEngineAttributeKeys.ContentProtectionFlags, value);
    }

    public ComObject? ContentProtectionManager
    {
        get => GetUnknown(MediaEngineAttributeKeys.ContentProtectionManager.Guid);
        set => Set(MediaEngineAttributeKeys.ContentProtectionManager.Guid, value);
    }

    public IMFDXGIDeviceManager? DxgiManager
    {
        get => GetUnknown<IMFDXGIDeviceManager>(MediaEngineAttributeKeys.DxgiManager.Guid);
        set => Set(MediaEngineAttributeKeys.DxgiManager.Guid, value);
    }

    public IMFMediaEngineExtension? Extension
    {
        get => GetUnknown<IMFMediaEngineExtension>(MediaEngineAttributeKeys.Extension.Guid);
        set => Set(MediaEngineAttributeKeys.Extension.Guid, value);
    }

    public IMFSourceReaderCallback? AsyncCallback
    {
        get => GetICallbackable<IMFSourceReaderCallback>(SourceReaderAttributeKeys.AsyncCallback);
        set => Set(SourceReaderAttributeKeys.AsyncCallback, value);
    }

    public Format VideoOutputFormat
    {
        get => GetEnumValue(MediaEngineAttributeKeys.VideoOutputFormat);
        set => SetEnumValue(MediaEngineAttributeKeys.VideoOutputFormat, value);
    }

    /// <unmanaged>HRESULT IMFAttributes::GetAllocatedBlob([In] const GUID&amp; guidKey, [Out, Buffer, Optional] unsigned char** ppBuf, [Out] UINT32* pcbSize)</unmanaged>
    /// <unmanaged-short>IMFAttributes::GetAllocatedBlob</unmanaged-short>
    public Span<byte> GetAllocatedBlob(Guid guidKey)
    {
        GetAllocatedBlob(guidKey, out nint buff, out uint pcbSize).CheckError();
        return new Span<byte>(buff.ToPointer(), (int)pcbSize);
    }

    /// <unmanaged>HRESULT IMFAttributes::GetAllocatedString([In] const GUID&amp; guidKey, [Out, Buffer, Optional] wchar_t** ppwszValue, [Out] UINT32* pcchLength)</unmanaged>
    /// <unmanaged-short>IMFAttributes::GetAllocatedString</unmanaged-short>
    public string? GetAllocatedString(Guid guidKey)
    {
        char* pwszValue = default;
        GetAllocatedString(guidKey, &pwszValue, out uint pcchLength).CheckError();
        if (pcchLength > 0 && pwszValue != null)
            return new string(pwszValue, 0, (int)pcchLength);

        return default;
    }

    /// <unmanaged>HRESULT IMFAttributes::GetBlobSize([In] const GUID&amp; guidKey, [Out] UINT32* pcbBlobSize)</unmanaged>
    /// <unmanaged-short>IMFAttributes::GetBlobSize</unmanaged-short>
    public Result GetBlob(Guid guidKey, byte[] buffer)
    {
        fixed (void* pBuffer = buffer)
            return GetBlob(guidKey, pBuffer, (uint)buffer.Length, IntPtr.Zero);
    }

    /// <unmanaged>HRESULT IMFAttributes::GetBlobSize([In] const GUID&amp; guidKey, [Out] UINT32* pcbBlobSize)</unmanaged>
    /// <unmanaged-short>IMFAttributes::GetBlobSize</unmanaged-short>
    public byte[] GetBlob(Guid guidKey)
    {
        int length = (int)GetBlobSize(guidKey);
        byte[] buffer = new byte[length];
        fixed (void* pBuffer = buffer)
        {
            GetBlob(guidKey, pBuffer, (uint)buffer.Length, IntPtr.Zero).CheckError();
        }

        return buffer;
    }

    /// <unmanaged>HRESULT IMFAttributes::GetBlobSize([In] const GUID&amp; guidKey, [Out] UINT32* pcbBlobSize)</unmanaged>
    /// <unmanaged-short>IMFAttributes::GetBlobSize</unmanaged-short>
    public Result GetBlob(Guid guidKey, Span<byte> buffer)
    {
        fixed (void* pBuffer = buffer)
            return GetBlob(guidKey, pBuffer, (uint)buffer.Length, IntPtr.Zero);
    }

    /// <unmanaged>HRESULT IMFAttributes::GetUINT32([In] const GUID&amp; guidKey, [Out] UINT32* punValue)</unmanaged>
    /// <unmanaged-short>IMFAttributes::GetUINT32</unmanaged-short>
    public uint GetUInt32(Guid guidKey)
    {
        GetUInt32(guidKey, out uint value).CheckError();
        return value;
    }

    /// <unmanaged>HRESULT IMFAttributes::GetUINT32([In] const GUID&amp; guidKey, [Out] UINT32* punValue)</unmanaged>
    /// <unmanaged-short>IMFAttributes::GetUINT32</unmanaged-short>
    public T GetEnumValue<T>(Guid guidKey)
        where T : unmanaged, Enum
    {
        GetUInt32(guidKey, out uint value).CheckError();
        return (T)(object)value;
    }

    /// <unmanaged>HRESULT IMFAttributes::GetUINT32([In] const GUID&amp; guidKey, [Out] UINT32* punValue)</unmanaged>
    /// <unmanaged-short>IMFAttributes::GetUINT32</unmanaged-short>
    public T GetEnumValue<T>(MediaAttributeKey<T> key)
        where T : unmanaged, Enum
    {
        GetUInt32(key.Guid, out uint value).CheckError();
        return (T)(object)value;
    }

    /// <unmanaged>HRESULT IMFAttributes::GetUINT64([In] const GUID&amp; guidKey, [Out] unsigned long long* punValue)</unmanaged>
    /// <unmanaged-short>IMFAttributes::GetUINT64</unmanaged-short>
    public ulong GetUInt64(Guid guidKey)
    {
        GetUInt64(guidKey, out ulong value).CheckError();
        return value;
    }

    /// <unmanaged>HRESULT IMFAttributes::GetDouble([In] const GUID&amp; guidKey, [Out] double* pfValue)</unmanaged>
    /// <unmanaged-short>IMFAttributes::GetDouble</unmanaged-short>
    public double GetDouble(Guid guidKey)
    {
        GetDouble(guidKey, out double value).CheckError();
        return value;
    }

    /// <unmanaged>HRESULT IMFAttributes::GetGUID([In] const GUID&amp; guidKey, [Out] GUID* pguidValue)</unmanaged>
    /// <unmanaged-short>IMFAttributes::GetGUID</unmanaged-short>
    public Guid GetGUID(Guid guidKey)
    {
        GetGUID(guidKey, out Guid value).CheckError();
        return value;
    }

    public string GetString(Guid guidKey)
    {
        uint length = GetStringLength(guidKey);
        char* wstr = stackalloc char[(int)length + 1];
        GetString(guidKey, wstr, length + 1, IntPtr.Zero);
        return Marshal.PtrToStringUni(new IntPtr(wstr)) ?? string.Empty;
    }

    public string GetString(MediaAttributeKey guidKey) => GetString(guidKey.Guid);

    /// <unmanaged>HRESULT IMFAttributes::GetBlobSize([In] const GUID&amp; guidKey, [Out] UINT32* pcbBlobSize)</unmanaged>
    /// <unmanaged-short>IMFAttributes::GetBlobSize</unmanaged-short>
    public uint GetBlobSize(Guid guidKey)
    {
        GetBlobSize(guidKey, out uint blobSize).CheckError();
        return blobSize;
    }

    public ComObject? GetUnknown(Guid guidKey)
    {
        Result result = GetUnknown(guidKey, typeof(IUnknown).GUID, out nint nativePtr);
        if (result.Success)
        {
            return new ComObject(nativePtr);
        }

        return default;
    }

    public T? GetUnknown<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Guid guidKey)
        where T : ComObject
    {
        Result result = GetUnknown(guidKey, typeof(T).GUID, out nint nativePtr);
        if (result.Success)
        {
            return MarshallingHelpers.FromPointer<T>(nativePtr)!;
        }

        return default;
    }

    public Result GetUnknown<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Guid guidKey, out T? unknown)
        where T : ComObject
    {
        Result result = GetUnknown(guidKey, typeof(T).GUID, out nint nativePtr);
        if (result.Success)
        {
            unknown = MarshallingHelpers.FromPointer<T>(nativePtr)!;
            return result;
        }

        unknown = default;
        return result;
    }

    public T? GetICallbackable<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Guid guidKey)
        where T : ICallbackable
    {
        Result result = GetUnknown(guidKey, typeof(T).GUID, out nint nativePtr);
        if (result.Success)
        {
            return CppObjectShadow.ToCallback<T>(nativePtr);
        }

        return default;
    }

    /// <summary>
    /// <p><strong>Applies to: </strong>desktop apps | Metro style apps</p><p> </p><p>Retrieves an attribute at the specified index.</p>
    /// </summary>
    /// <param name="index"><dd> <p>Index of the attribute to retrieve. To get the number of attributes, call <strong><see cref="GetCount"/></strong>.</p> </dd></param>
    /// <param name="guidKey"><dd> <p>Receives the <see cref="Guid"/> that identifies this attribute.</p> </dd></param>
    /// <returns>The value associated to this index</returns>
    /// <remarks>
    /// <p>To enumerate all of an object's attributes in a thread-safe way, do the following:</p><ol> <li> <p>Call <strong><see cref="LockStore"/></strong> to prevent another thread from adding or deleting attributes.</p> </li> <li> <p>Call <strong><see cref="GetCount"/></strong> to find the number of attributes.</p> </li> <li> <p>Call <strong>GetItemByIndex</strong> to get each attribute by index.</p> </li> <li> <p>Call <strong><see cref="UnlockStore"/></strong> to unlock the attribute store.</p> </li> </ol><p>This interface is available on the following platforms if the Windows Media Format 11 SDK redistributable components are installed:</p><ul> <li>Windows?XP with Service Pack?2 (SP2) and later.</li> <li>Windows?XP Media Center Edition?2005 with KB900325 (Windows?XP Media Center Edition?2005) and KB925766 (October 2006 Update Rollup for Windows?XP Media Center Edition) installed.</li> </ul>
    /// </remarks>
    public Variant GetItemByIndex(uint index, out Guid guidKey)
    {
        Variant result = default;
        GetItemByIndex(index, out guidKey, &result).CheckError();
        return result;
    }

    public Result Set(Guid guidKey, bool value) => Set(guidKey, value ? 1u : 0u);
    public Result Set(Guid guidKey, float value) => Set(guidKey, (double)value);

    /// <unmanaged>HRESULT IMFAttributes::SetUINT32([In] const GUID&amp; guidKey, [In] UINT32 unValue)</unmanaged>
    /// <unmanaged-short>IMFAttributes::SetUINT32</unmanaged-short>
    public Result SetEnumValue<T>(Guid guidKey, T value)
        where T : unmanaged, Enum
    {
        return Set(guidKey, Convert.ToUInt32(value));
    }

    /// <unmanaged>HRESULT IMFAttributes::SetUINT32([In] const GUID&amp; guidKey, [In] UINT32 unValue)</unmanaged>
    /// <unmanaged-short>IMFAttributes::SetUINT32</unmanaged-short>
    public Result SetEnumValue<T>(MediaAttributeKey<T> key, T value)
        where T : unmanaged, Enum
    {
        return Set(key.Guid, Convert.ToUInt32(value));
    }

    /// <unmanaged>HRESULT IMFAttributes::SetBlob([In] const GUID&amp; guidKey, [In] const unsigned char* pBuf, [In] UINT32 cbBufSize)</unmanaged>
    /// <unmanaged-short>IMFAttributes::SetBlob</unmanaged-short>
    public Result SetBlob(Guid guidKey, byte[] buffer)
    {
        fixed (void* pBuffer = buffer)
            return SetBlob(guidKey, pBuffer, (uint)buffer.Length);
    }

    /// <unmanaged>HRESULT IMFAttributes::SetBlob([In] const GUID&amp; guidKey, [In] const unsigned char* pBuf, [In] UINT32 cbBufSize)</unmanaged>
    /// <unmanaged-short>IMFAttributes::SetBlob</unmanaged-short>
    public Result SetBlob(Guid guidKey, Span<byte> buffer)
    {
        fixed (void* pBuffer = buffer)
            return SetBlob(guidKey, pBuffer, (uint)buffer.Length);
    }

    public Result SetBlob<T>(Guid guidKey, ref T value)
        where T : unmanaged
    {
        return SetBlob(guidKey, Unsafe.AsPointer(ref value), (uint)sizeof(T));
    }
}
