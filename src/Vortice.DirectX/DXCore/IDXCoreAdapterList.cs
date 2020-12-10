// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXCore
{
    public partial class IDXCoreAdapterList
    {
        public T GetAdapter<T>(int index) where T : IDXCoreAdapter
        {
            Result result = GetAdapter((uint)index, typeof(T).GUID, out IntPtr adapterPtr);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(adapterPtr);
        }

        public Result GetAdapter<T>(int index, out T adapter) where T : IDXCoreAdapter
        {
            Result result = GetAdapter((uint)index, typeof(T).GUID, out IntPtr adapterPtr);
            if (result.Failure)
            {
                adapter = default;
                return result;
            }

            adapter = FromPointer<T>(adapterPtr);
            return result;
        }

        public T GetFactory<T>() where T : IDXCoreAdapterFactory
        {
            Result result = GetFactory(typeof(T).GUID, out IntPtr factoryPtr);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(factoryPtr);
        }

        public Result GetFactory<T>(out T factory) where T : IDXCoreAdapterFactory
        {
            Result result = GetFactory(typeof(T).GUID, out IntPtr factoryPtr);
            if (result.Failure)
            {
                factory = default;
                return result;
            }

            factory = FromPointer<T>(factoryPtr);
            return result;
        }

        public Result Sort(AdapterPreference[] preferences)
        {
            return Sort((uint)preferences.Length, preferences);
        }

        public Result Sort(int preferencesCount, AdapterPreference[] preferences)
        {
            return Sort((uint)preferencesCount, preferences);
        }
    }
}
