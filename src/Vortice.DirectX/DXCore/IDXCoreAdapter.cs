// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXCore
{
    public partial class IDXCoreAdapter
    {
        public long InstanceLuid => GetProperty<long>(AdapterProperty.InstanceLuid);
        public ulong DriverVersion => GetProperty<ulong>(AdapterProperty.DriverVersion);
        public string DriverDescription => GetStringProperty(AdapterProperty.DriverDescription);
        public HardwareID HardwareID => GetProperty<HardwareID>(AdapterProperty.HardwareID);
        public ulong DedicatedAdapterMemory => GetProperty<ulong>(AdapterProperty.DedicatedAdapterMemory);
        public ulong DedicatedSystemMemory => GetProperty<ulong>(AdapterProperty.DedicatedSystemMemory);
        public ulong SharedSystemMemory => GetProperty<ulong>(AdapterProperty.SharedSystemMemory);
        public bool AcgCompatible => GetBoolProperty(AdapterProperty.AcgCompatible);
        public bool IsHardware => GetBoolProperty(AdapterProperty.IsHardware);
        public bool IsIntegrated => GetBoolProperty(AdapterProperty.IsIntegrated);
        public bool IsDetachable => GetBoolProperty(AdapterProperty.IsDetachable);
        public HardwareIDParts? HardwareIDParts
        {
            get
            {
                if (GetProperty(AdapterProperty.HardwareIDParts, out HardwareIDParts hardwareIDParts).Success)
                {
                    return hardwareIDParts;
                }

                return null;
            }
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

        public PointerSize GetPropertySize(AdapterProperty property)
        {
            GetPropertySize(property, out PointerSize propertySize).CheckError();
            return propertySize;
        }

        public unsafe bool GetBoolProperty(AdapterProperty property)
        {
            bool result = default;
            GetProperty(property, 1, new IntPtr(&result)).CheckError();
            return result;
        }

        public unsafe string GetStringProperty(AdapterProperty property)
        {
            GetPropertySize(property, out PointerSize propertySize).CheckError();
            byte* strBytes = stackalloc byte[propertySize];
            GetProperty(property, propertySize, new IntPtr(strBytes)).CheckError();
            return System.Text.Encoding.UTF8.GetString(strBytes, propertySize - 1);
        }

        public unsafe T GetProperty<T>(AdapterProperty property) where T : unmanaged
        {
            T result = default;
            GetProperty(property, sizeof(T), new IntPtr(&result)).CheckError();
            return result;
        }

        public unsafe Result GetProperty<T>(AdapterProperty property, out T propertyData) where T : unmanaged
        {
            fixed (void* pPropertyData = &propertyData)
            {
                Result result = GetProperty(property, sizeof(T), new IntPtr(pPropertyData));
                return result;
            }
        }

        public unsafe Result SetState<T1, T2>(AdapterState state, T1 inputStateDetails, T2 inputData)
            where T1 : unmanaged
            where T2 : unmanaged
        {
            return SetState(state,
                sizeof(T1), new IntPtr(&inputStateDetails),
                sizeof(T2), new IntPtr(&inputData));
        }

        public unsafe Result QueryState<T1, T2>(AdapterState state, T1 inputStateDetails, out T2 outputData)
            where T1 : unmanaged
            where T2 : unmanaged
        {
            fixed (void* outputBuffer = &outputData)
            {
                return QueryState(state,
                    sizeof(T1), new IntPtr(&inputStateDetails),
                    sizeof(T2), new IntPtr(outputBuffer)
                    );
            }
        }
    }
}
