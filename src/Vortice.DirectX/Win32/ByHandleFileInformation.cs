// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Win32;

/// <summary>
/// BY_HANDLE_FILE_INFORMATION
/// </summary>
public partial struct ByHandleFileInformation
{
    public uint FileAttributes;
    public ulong CreationTime;
    public ulong LastAccessTime;
    public ulong LastWriteTime;
    public uint VolumeSerialNumber;
    public uint FileSizeHigh;
    public uint FileSizeLow;
    public uint NumberOfLinks;
    public uint FileIndexHigh;
    public uint FileIndexLow;
}
