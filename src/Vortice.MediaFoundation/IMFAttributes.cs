// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;

namespace Vortice.MediaFoundation
{
    public partial class IMFAttributes
    {
        /// <summary>	
        /// Gets an item value
        /// </summary>	
        /// <param name="guidKey">GUID of the key.</param>	
        /// <returns>The value associated to this key.</returns>	
        /// <unmanaged-short>IMFAttributes::GetItem</unmanaged-short>	
        public object Get(Guid guidKey)
        {
            AttributeType itemType = GetItemType(guidKey);
            switch (itemType)
            {
                case AttributeType.Uint32:
                    return Get<int>(guidKey);
                case AttributeType.Uint64:
                    return Get<long>(guidKey);
                case AttributeType.Double:
                    return Get<double>(guidKey);
                case AttributeType.Guid:
                    return Get<Guid>(guidKey);
                case AttributeType.Blob:
                    return Get<byte[]>(guidKey);
                case AttributeType.String:
                    return Get<string>(guidKey);
                case AttributeType.Iunknown:
                    return Get<ComObject>(guidKey);
                default:
                    break;
            }
            throw new ArgumentException("The type of the value is not supported");
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
        public object GetByIndex(int index, out Guid guidKey)
        {
            guidKey = GetItemByIndex(index, IntPtr.Zero);
            return Get(guidKey);
        }

        /// <summary>	
        /// Gets an item value
        /// </summary>	
        /// <param name="guidKey">GUID of the key.</param>	
        /// <returns>The value associated to this key.</returns>	
        public unsafe T Get<T>(Guid guidKey)
        {
            // Perform conversions to supported types
            // int
            // long
            // string
            // byte[]
            // double
            // ComObject
            // Guid

            if (typeof(T) == typeof(int) || typeof(T) == typeof(bool) || typeof(T) == typeof(byte) || typeof(T) == typeof(uint) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort) || typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte))
            {
                return (T)Convert.ChangeType(GetInt(guidKey), typeof(T));
            }

            if (typeof(T).IsEnum)
            {
                return (T)Enum.ToObject(typeof(T), GetInt(guidKey));
            }

            if (typeof(T) == typeof(IntPtr))
            {
                return (T)(object)new IntPtr((long)GetLong(guidKey));
            }

            if (typeof(T) == typeof(UIntPtr))
            {
                return (T)(object)new UIntPtr(GetLong(guidKey));
            }

            if (typeof(T) == typeof(long) || typeof(T) == typeof(ulong))
            {
                return (T)Convert.ChangeType(GetLong(guidKey), typeof(T));
            }

            if (typeof(T) == typeof(Guid))
            {
                return (T)(object)GetGUID(guidKey);
            }

            if (typeof(T) == typeof(string))
            {
                int length = GetStringLength(guidKey);
                char* wstr = stackalloc char[length + 1];
                GetString(guidKey, new IntPtr(wstr), length + 1, IntPtr.Zero);
                return (T)(object)Marshal.PtrToStringUni(new IntPtr(wstr));
            }

            if (typeof(T) == typeof(double) || typeof(T) == typeof(float))
            {
                return (T)Convert.ChangeType(GetDouble(guidKey), typeof(T));
            }

            if (typeof(T) == typeof(byte[]))
            {
                int length = GetBlobSize(guidKey);
                byte[] buffer = new byte[length];
                fixed (void* pBuffer = buffer)
                {
                    GetBlob(guidKey, (IntPtr)pBuffer, buffer.Length, IntPtr.Zero);
                }

                return (T)(object)buffer;
            }

            if (typeof(T).IsValueType)
            {
                int length = GetBlobSize(guidKey);
                if (length != Unsafe.SizeOf<T>())
                {
                    throw new ArgumentException("Size of the structure doesn't match the size of stored value");
                }
                var value = default(T);
                GetBlob(guidKey, (IntPtr)Unsafe.AsPointer(ref value), Unsafe.SizeOf<T>(), IntPtr.Zero);
                return value;
            }

            if (typeof(T) == typeof(ComObject))
            {
                IntPtr ptr = GetUnknown(guidKey, typeof(IUnknown).GUID);
                return (T)(object)new ComObject(ptr);
            }

            if (typeof(T).IsSubclassOf(typeof(ComObject)))
            {
                IntPtr ptr = GetUnknown(guidKey, typeof(T).GUID);
                return (T)Activator.CreateInstance(typeof(T), ptr);
            }

            throw new ArgumentException("The type of the value is not supported");
        }

        /// <summary>	
        /// Gets an item value
        /// </summary>	
        /// <param name="guidKey">GUID of the key.</param>	
        /// <returns>The value associated to this key.</returns>	
        /// <msdn-id>ms704598</msdn-id>	
        /// <unmanaged>HRESULT IMFAttributes::GetItem([In] const GUID&amp; guidKey,[In] void* pValue)</unmanaged>	
        /// <unmanaged-short>IMFAttributes::GetItem</unmanaged-short>	
        public unsafe T Get<T>(MediaAttributeKey<T> guidKey)
        {
            return Get<T>(guidKey.Guid);
        }

        /// <summary>	
        /// <p><strong>Applies to: </strong>desktop apps | Metro style apps</p><p> Adds an attribute value with a specified key. </p>	
        /// </summary>	
        /// <param name="guidKey"><dd> <p> A <see cref="System.Guid"/> that identifies the value to set. If this key already exists, the method overwrites the old value. </p> </dd></param>	
        /// <param name="value"><dd> <p> A <strong><see cref="Variant"/></strong> that contains the attribute value. The method copies the value. The <strong><see cref="Variant"/></strong> type must be one of the types listed in the <strong><see cref="AttributeType"/></strong> enumeration. </p> </dd></param>	
        /// <returns><p> The method returns an <strong><see cref="Result"/></strong>. Possible values include, but are not limited to, those in the following table. </p><table> <tr><th>Return code</th><th>Description</th></tr> <tr><td> <dl> <dt><strong><see cref="Result.Ok"/></strong></dt> </dl> </td><td> <p> The method succeeded. </p> </td></tr> <tr><td> <dl> <dt><strong>E_OUTOFMEMORY</strong></dt> </dl> </td><td> <p> Insufficient memory. </p> </td></tr> <tr><td> <dl> <dt><strong>MF_E_INVALIDTYPE</strong></dt> </dl> </td><td> <p> Invalid attribute type. </p> </td></tr> </table><p>?</p></returns>	
        /// <remarks>	
        /// <p> This method checks whether the <strong><see cref="Variant"/></strong> type is one of the attribute types defined in <strong><see cref="AttributeType"/></strong>, and fails if an unsupported type is used. However, this method does not check whether the <strong><see cref="Variant"/></strong> is the correct type for the specified attribute <see cref="System.Guid"/>. (There is no programmatic way to associate attribute GUIDs with property types.) For a list of Media Foundation attributes and their data types, see Media Foundation Attributes. </p><p>This interface is available on the following platforms if the Windows Media Format 11 SDK redistributable components are installed:</p><ul> <li>Windows?XP with Service Pack?2 (SP2) and later.</li> <li>Windows?XP Media Center Edition?2005 with KB900325 (Windows?XP Media Center Edition?2005) and KB925766 (October 2006 Update Rollup for Windows?XP Media Center Edition) installed.</li> </ul>	
        /// </remarks>	
        /// <msdn-id>bb970346</msdn-id>	
        /// <unmanaged>HRESULT IMFAttributes::SetItem([In] const GUID&amp; guidKey,[In] const PROPVARIANT&amp; Value)</unmanaged>	
        /// <unmanaged-short>IMFAttributes::SetItem</unmanaged-short>	
        public unsafe void Set<T>(Guid guidKey, T value)
        {
            // Perform conversions to supported types
            // int
            // long
            // string
            // byte[]
            // double
            // ComObject
            // Guid

            if (typeof(T) == typeof(int) || typeof(T) == typeof(bool) || typeof(T) == typeof(byte) || typeof(T) == typeof(uint) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort) || typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte)
                || typeof(T).IsEnum)
            {
                Set_(guidKey, Convert.ToInt32(value));
                return;
            }

            if (typeof(T) == typeof(long) || typeof(T) == typeof(ulong))
            {
                Set_(guidKey, Convert.ToInt64(value));
                return;
            }

            if (typeof(T) == typeof(IntPtr))
            {
                Set_(guidKey, ((IntPtr)(object)value).ToInt64());
                return;
            }

            if (typeof(T) == typeof(Guid))
            {
                Set_(guidKey, (Guid)(object)value);
                return;
            }

            if (typeof(T) == typeof(string))
            {
                Set_(guidKey, value.ToString());
                return;
            }

            if (typeof(T) == typeof(double) || typeof(T) == typeof(float))
            {
                Set_(guidKey, Convert.ToDouble(value));
                return;
            }

            if (typeof(T) == typeof(byte[]))
            {
                var arrayValue = ((byte[])(object)value);
                fixed (void* pBuffer = arrayValue)
                    SetBlob(guidKey, (IntPtr)pBuffer, arrayValue.Length);
                return;
            }


            if (typeof(T).IsValueType)
            {
                SetBlob(guidKey, (IntPtr)Unsafe.AsPointer(ref value), Unsafe.SizeOf<T>());
                return;
            }

            if (typeof(T) == typeof(ComObject) || typeof(IUnknown).IsAssignableFrom(typeof(T)))
            {
                Set_(guidKey, ((IUnknown)(object)value));
                return;
            }

            throw new ArgumentException("The type of the value is not supported");
        }

        /// <summary>	
        /// <p><strong>Applies to: </strong>desktop apps | Metro style apps</p><p> Adds an attribute value with a specified key. </p>	
        /// </summary>	
        /// <param name="guidKey"><dd> <p> A <see cref="System.Guid"/> that identifies the value to set. If this key already exists, the method overwrites the old value. </p> </dd></param>	
        /// <param name="value"><dd> <p> A <strong><see cref="Variant"/></strong> that contains the attribute value. The method copies the value. The <strong><see cref="Variant"/></strong> type must be one of the types listed in the <strong><see cref="AttributeType"/></strong> enumeration. </p> </dd></param>	
        /// <returns><p> The method returns an <strong><see cref="Result"/></strong>. Possible values include, but are not limited to, those in the following table. </p><table> <tr><th>Return code</th><th>Description</th></tr> <tr><td> <dl> <dt><strong><see cref="Result.Ok"/></strong></dt> </dl> </td><td> <p> The method succeeded. </p> </td></tr> <tr><td> <dl> <dt><strong>E_OUTOFMEMORY</strong></dt> </dl> </td><td> <p> Insufficient memory. </p> </td></tr> <tr><td> <dl> <dt><strong>MF_E_INVALIDTYPE</strong></dt> </dl> </td><td> <p> Invalid attribute type. </p> </td></tr> </table><p>?</p></returns>	
        /// <remarks>	
        /// <p> This method checks whether the <strong><see cref="Variant"/></strong> type is one of the attribute types defined in <strong><see cref="SharpDX.MediaFoundation.AttributeType"/></strong>, and fails if an unsupported type is used. However, this method does not check whether the <strong><see cref="SharpDX.Win32.Variant"/></strong> is the correct type for the specified attribute <see cref="System.Guid"/>. (There is no programmatic way to associate attribute GUIDs with property types.) For a list of Media Foundation attributes and their data types, see Media Foundation Attributes. </p><p>This interface is available on the following platforms if the Windows Media Format 11 SDK redistributable components are installed:</p><ul> <li>Windows?XP with Service Pack?2 (SP2) and later.</li> <li>Windows?XP Media Center Edition?2005 with KB900325 (Windows?XP Media Center Edition?2005) and KB925766 (October 2006 Update Rollup for Windows?XP Media Center Edition) installed.</li> </ul>	
        /// </remarks>	
        /// <msdn-id>bb970346</msdn-id>	
        /// <unmanaged>HRESULT IMFAttributes::SetItem([In] const GUID&amp; guidKey,[In] const PROPVARIANT&amp; Value)</unmanaged>	
        /// <unmanaged-short>IMFAttributes::SetItem</unmanaged-short>	
        public unsafe void Set<T>(MediaAttributeKey<T> guidKey, T value)
        {
            Set(guidKey.Guid, value);
        }
    }
}
