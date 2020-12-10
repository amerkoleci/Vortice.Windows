// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXCore
{
    public partial class IDXCoreAdapterFactory
    {
        public Result CreateAdapterList<T>(Guid[] filterAttributes, out T adapterList) where T : IDXCoreAdapterList
        {
            return CreateAdapterList((uint)filterAttributes.Length, filterAttributes, out adapterList);
        }

        public Result CreateAdapterList<T>(uint numAttributes, Guid[] filterAttributes, out T adapterList) where T : IDXCoreAdapterList
        {
            Result result = CreateAdapterList(numAttributes, filterAttributes, typeof(T).GUID, out IntPtr adapterListPtr);
            if (result.Failure)
            {
                adapterList = default;
                return result;
            }

            adapterList = FromPointer<T>(adapterListPtr);
            return result;
        }

        public T CreateAdapterList<T>(Guid[] filterAttributes) where T : IDXCoreAdapterList
        {
            return CreateAdapterList<T>((uint)filterAttributes.Length, filterAttributes);
        }

        public T CreateAdapterList<T>(uint numAttributes, Guid[] filterAttributes) where T : IDXCoreAdapterList
        {
            Result result = CreateAdapterList(numAttributes, filterAttributes, typeof(T).GUID, out IntPtr adapterListPtr);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(adapterListPtr);
        }

        public Result GetAdapterByLuid<T>(long adapterLUID, out T adapter) where T : IDXCoreAdapter
        {
            Result result = GetAdapterByLuid(adapterLUID, typeof(T).GUID, out IntPtr adapterPtr);
            if (result.Failure)
            {
                adapter = default;
                return result;
            }

            adapter = FromPointer<T>(adapterPtr);
            return result;
        }

        public T GetAdapterByLuid<T>(long adapterLUID) where T : IDXCoreAdapter
        {
            Result result = GetAdapterByLuid(adapterLUID, typeof(T).GUID, out IntPtr adapterPtr);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(adapterPtr);
        }
    }
}
