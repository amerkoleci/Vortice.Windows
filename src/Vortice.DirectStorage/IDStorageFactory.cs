// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectStorage;

public partial class IDStorageFactory
{
    /// <summary>
    /// Creates DStorage queue object.
    /// </summary>
    /// <typeparam name="T">Type to create, such as <see cref="IDStorageQueue"/></typeparam>
    /// <param name="description">Descriptor to specify the properties of the queue.</param>
    /// <param name="queue">Receives the new queue created.</param>
    /// <returns>The result of operation.</returns>
    public Result CreateQueue<T>(QueueDesc description, out T? queue) where T : ComObject
    {
        Result result = CreateQueue(ref description, typeof(T).GUID, out IntPtr nativePtr);

        if (result.Success)
        {
            queue = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        queue = null;
        return result;
    }

    /// <summary>
    /// Creates DStorage queue object.
    /// </summary>
    /// <typeparam name="T">Type to create, such as <see cref="IDStorageQueue"/>.</typeparam>
    /// <param name="description">Descriptor to specify the properties of the queue.</param>
    /// <returns>The new queue created.</returns>
    public T CreateQueue<T>(QueueDesc description) where T : ComObject
    {
        CreateQueue(ref description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    public Result CreateQueue(QueueDesc description, out IDStorageQueue? queue)
    {
        Result result = CreateQueue(ref description, typeof(IDStorageQueue).GUID, out IntPtr nativePtr);

        if (result.Success)
        {
            queue = new IDStorageQueue(nativePtr);
            return result;
        }

        queue = null;
        return result;
    }

    public IDStorageQueue CreateQueue(QueueDesc description)
    {
        CreateQueue(ref description, typeof(IDStorageQueue).GUID, out IntPtr nativePtr).CheckError();
        return new IDStorageQueue(nativePtr);
    }

    /// <summary>
    /// Opens a file for DStorage access. The file must be stored on a DStorage supported NVMe device.
    /// </summary>
    /// <typeparam name="T">Type of storage file, such as <see cref="IDStorageFile"/>.</typeparam>
    /// <param name="path">Path of the file to be opened.</param>
    /// <param name="file">Receives the new file opened.</param>
    /// <returns>The result of operation.</returns>
    public Result OpenFile<T>(string path, out T? file) where T : ComObject
    {
        Result result = OpenFile(path, typeof(T).GUID, out IntPtr nativePtr);

        if (result.Success)
        {
            file = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        file = null;
        return result;
    }

    /// <summary>
    /// Opens a file for DStorage access. The file must be stored on a DStorage supported NVMe device.
    /// </summary>
    /// <typeparam name="T">Type of storage file, such as <see cref="IDStorageFile"/>.</typeparam>
    /// <param name="path">Path of the file to be opened.</param>
    /// <returns>The new file opened.</returns>
    public T OpenFile<T>(string path) where T : ComObject
    {
        OpenFile(path, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    /// <summary>
    /// Opens a file for DStorage access. The file must be stored on a DStorage supported NVMe device.
    /// </summary>
    /// <param name="path">Path of the file to be opened.</param>
    /// <param name="file">Receives the new file opened.</param>
    /// <returns>The result of operation.</returns>
    public Result OpenFile(string path, out IDStorageFile? file)
    {
        Result result = OpenFile(path, typeof(IDStorageFile).GUID, out IntPtr nativePtr);

        if (result.Success)
        {
            file = new IDStorageFile(nativePtr);
            return result;
        }

        file = null;
        return result;
    }

    /// <summary>
    /// Opens a file for DStorage access. The file must be stored on a DStorage supported NVMe device.
    /// </summary>
    /// <param name="path">Path of the file to be opened.</param>
    /// <returns>The new file opened.</returns>
    public IDStorageFile OpenFile(string path)
    {
        OpenFile(path, typeof(IDStorageFile).GUID, out IntPtr nativePtr).CheckError();
        return new IDStorageFile(nativePtr);
    }

    public Result CreateStatusArray<T>(uint capacity, string name, out T? statusArray) where T : ComObject
    {
        Result result = CreateStatusArray(capacity, name, typeof(T).GUID, out IntPtr nativePtr);

        if (result.Success)
        {
            statusArray = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        statusArray = null;
        return result;
    }

    public T CreateStatusArray<T>(uint capacity, string name) where T : ComObject
    {
        CreateStatusArray(capacity, name, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    public Result CreateStatusArray(uint capacity, string name, out IDStorageStatusArray? statusArray) 
    {
        Result result = CreateStatusArray(capacity, name, typeof(IDStorageStatusArray).GUID, out IntPtr nativePtr);

        if (result.Success)
        {
            statusArray = new IDStorageStatusArray(nativePtr);
            return result;
        }

        statusArray = null;
        return result;
    }

    public IDStorageStatusArray CreateStatusArray(uint capacity, string name)
    {
        CreateStatusArray(capacity, name, typeof(IDStorageStatusArray).GUID, out IntPtr nativePtr).CheckError();
        return new IDStorageStatusArray(nativePtr);
    }
}
