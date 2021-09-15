// Copyright (c) Amer Koleci and Contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;

namespace Vortice.Win32
{
    public class PropertyStore : ComObject
    {
        private IPropertyStore? _storeInterface;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyStore"/> class.
        /// </summary>
        public PropertyStore() : base(IntPtr.Zero)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyStore"/> class.
        /// </summary>
        /// <param name="propertyBagPointer">The property bag pointer.</param>
        public PropertyStore(IntPtr propertyBagPointer) : base(propertyBagPointer)
        {
        }

        protected override void NativePointerUpdated(IntPtr oldNativePointer)
        {
            base.NativePointerUpdated(oldNativePointer);
            _storeInterface = NativePointer != IntPtr.Zero
                                    ? (IPropertyStore)Marshal.GetObjectForIUnknown(NativePointer)
                                    : null;
        }

        private void CheckIfInitialized()
        {
            if (_storeInterface == null)
                throw new InvalidOperationException("This instance is not bound to an unmanaged IPropertyStore");
        }

        /// <summary>
        /// Property Count
        /// </summary>
        public int Count
        {
            get
            {
                CheckIfInitialized();
                _storeInterface!.GetCount(out int result).CheckError();
                return result;
            }
        }

        /// <summary>
        /// Gets property key at sepecified index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Property key</returns>
        public PropertyKey Get(int index)
        {
            CheckIfInitialized();
            _storeInterface!.GetAt(index, out PropertyKey key).CheckError();
            return key;
        }

        /// <summary>
        /// Gets property value at specified index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Property value</returns>
        public Variant GetValue(int index)
        {
            CheckIfInitialized();
            PropertyKey key = Get(index);
            _storeInterface!.GetValue(ref key, out Variant result).CheckError();
            return result;
        }

        /// <summary>
        /// Gets property value at specified key.
        /// </summary>
        /// <param name="key">Key of property to get.</param>
        /// <returns>Property value</returns>
        public Variant GetValue(PropertyKey key)
        {
            CheckIfInitialized();
            _storeInterface!.GetValue(ref key, out Variant result).CheckError();
            return result;
        }

        /// <summary>
        /// Sets property value at specified key.
        /// </summary>
        /// <param name="key">Key of property to set.</param>
        /// <param name="value">Value to write.</param>
        public void SetValue(PropertyKey key, Variant value)
        {
            CheckIfInitialized();
            _storeInterface!.SetValue(ref key, ref value).CheckError();
        }

        /// <summary>
        /// Saves a property change.
        /// </summary>
        public void Commit()
        {
            CheckIfInitialized();
            _storeInterface!.Commit().CheckError();
        }

        /// <summary>
        /// Contains property guid
        /// </summary>
        /// <param name="key">Looks for a specific key</param>
        /// <returns>True if found</returns>
        public bool Contains(PropertyKey key)
        {
            CheckIfInitialized();
            for (int i = 0; i < Count; i++)
            {
                PropertyKey ikey = Get(i);
                if ((ikey.FormatId == key.FormatId) && (ikey.PropertyId == key.PropertyId))
                {
                    return true;
                }
            }

            return false;
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99")]
        private interface IPropertyStore
        {
            Result GetCount(out int propCount);
            Result GetAt(int property, out PropertyKey key);
            Result GetValue(ref PropertyKey key, out Variant value);
            Result SetValue(ref PropertyKey key, ref Variant value);
            Result Commit();
        }
    }
}
