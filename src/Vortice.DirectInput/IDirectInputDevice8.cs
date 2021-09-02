// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using SharpGen.Runtime;

namespace Vortice.DirectInput
{
    public partial class IDirectInputDevice8
    {
        private DataFormat? _dataFormat;
        private readonly Dictionary<string, ObjectDataFormat> _mapNameToObjectFormat = new Dictionary<string, ObjectDataFormat>();
        private ObjectProperties _properties;

        public Capabilities Capabilities
        {
            get
            {
                unsafe
                {
                    Capabilities capabilities = default;
                    capabilities.Size = sizeof(Capabilities);
                    GetCapabilities(&capabilities).CheckError();
                    return capabilities;
                };
            }
        }

        public DeviceInstance DeviceInfo
        {
            get
            {
                unsafe
                {
                    DeviceInstance.__Native deviceInfoNative = DeviceInstance.__NewNative();
                    GetDeviceInfo(&deviceInfoNative).CheckError();

                    var deviceInfo = new DeviceInstance();
                    deviceInfo.__MarshalFrom(ref deviceInfoNative);
                    return deviceInfo;
                };
            }
        }

        public ObjectProperties Properties
        {
            get
            {
                if (_properties == null)
                    _properties = new ObjectProperties(this, 0, PropertyHowType.Device);
                return _properties;
            }
        }

        /// <summary>
        /// Gets the created effects.
        /// </summary>
        /// <value>The created effects.</value>
        public IList<IDirectInputEffect> CreatedEffects
        {
            get
            {
                var enumCreatedEffectsCallback = new EnumCreatedEffectsCallback();
                EnumCreatedEffectObjects(enumCreatedEffectsCallback.NativePointer, IntPtr.Zero, 0);
                return enumCreatedEffectsCallback.Effects;
            }
        }

        public IDirectInputEffect CreateEffect(Guid effectGuid, EffectParameters parameters)
        {
            CreateEffect(effectGuid, parameters, out IDirectInputEffect effect, null).CheckError();
            return effect;
        }

        public Result CreateEffect(Guid effectGuid, EffectParameters parameters, out IDirectInputEffect? effect)
        {
            return CreateEffect(effectGuid, parameters, out effect, null);
        }

        public T GetCurrentState<T, TRaw, TUpdate>()
            where T : class, IDeviceState<TRaw, TUpdate>, new()
            where TRaw : unmanaged
            where TUpdate : unmanaged, IStateUpdate
        {
            var value = new T();
            GetCurrentState<T, TRaw, TUpdate>(ref value);
            return value;
        }

        public void GetCurrentState<T, TRaw, TUpdate>(ref T data)
            where T : class, IDeviceState<TRaw, TUpdate>, new()
            where TRaw : unmanaged
            where TUpdate : unmanaged, IStateUpdate
        {
            unsafe
            {
                int size = sizeof(TRaw);
                byte* pTemp = stackalloc byte[size * 2];
                TRaw temp = default;
                GetDeviceState(size, (IntPtr)pTemp);
                MemoryHelpers.Read((IntPtr)pTemp, ref temp);
                data.MarshalFrom(ref temp);
            }
        }

        /// <summary>
        /// Retrieves buffered data from the device.
        /// </summary>
        /// <returns>A collection of buffered input events.</returns>
        public TUpdate[] GetBufferedData<TUpdate>() where TUpdate : unmanaged, IStateUpdate
        {
            TUpdate[] updates = Array.Empty<TUpdate>();
            unsafe
            {
                var sizeOfObjectData = Unsafe.SizeOf<ObjectData>();
                int size = -1;
                // 1 for peek
                GetDeviceData(sizeOfObjectData, IntPtr.Zero, ref size, 1);

                if (size == 0)
                    return updates;

                var pData = stackalloc ObjectData[size];
                GetDeviceData(sizeOfObjectData, (IntPtr)pData, ref size, 0);

                if (size == 0)
                    return updates;

                updates = new TUpdate[size];
                for (int i = 0; i < size; i++)
                {
                    var update = new TUpdate
                    {
                        RawOffset = pData[i].Offset,
                        Value = pData[i].Data,
                        Timestamp = pData[i].TimeStamp,
                        Sequence = pData[i].Sequence
                    };
                    updates[i] = update;
                }
            }

            return updates;
        }

        public DeviceObjectInstance GetObjectInfoByName(string name)
        {
            return GetObjectInfo(GetFromName(name).Offset, PropertyHowType.Byoffset);
        }

        public DeviceObjectInstance GetObjectInfoByOffset(int offset)
        {
            return GetObjectInfo(offset, PropertyHowType.Byoffset);
        }

        //public ObjectProperties GetObjectPropertiesByName(string name)
        //{
        //    return new ObjectProperties(this, GetFromName(name).Offset, PropertyHowType.Byoffset);
        //}

        private ObjectDataFormat GetFromName(string name)
        {
            ObjectDataFormat objectFormat;
            if (!_mapNameToObjectFormat.TryGetValue(name, out objectFormat))
            {
                throw new ArgumentException(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Invalid name [{0}]. Must be in [{1}]", name, string.Join(";", _mapNameToObjectFormat.Keys)));
            }

            return objectFormat;
        }


        public KeyboardState GetCurrentKeyboardState()
        {
            var value = new KeyboardState();
            GetCurrentKeyboardState(ref value);
            return value;
        }

        public void GetCurrentKeyboardState(ref KeyboardState data) => GetCurrentState<KeyboardState, RawKeyboardState, KeyboardUpdate>(ref data);

        public KeyboardUpdate[] GetBufferedKeyboardData() => GetBufferedData<KeyboardUpdate>();

        public MouseState GetCurrentMouseState()
        {
            var value = new MouseState();
            GetCurrentMouseState(ref value);
            return value;
        }

        public void GetCurrentMouseState(ref MouseState data) => GetCurrentState<MouseState, RawMouseState, MouseUpdate>(ref data);

        public MouseUpdate[] GetBufferedMouseData() => GetBufferedData<MouseUpdate>();

        public JoystickState GetCurrentJoystickState()
        {
            var value = new JoystickState();
            GetCurrentJoystickState(ref value);
            return value;
        }

        public void GetCurrentJoystickState(ref JoystickState data) => GetCurrentState<JoystickState, RawJoystickState, JoystickUpdate>(ref data);

        public JoystickUpdate[] GetBufferedJoystickData() => GetBufferedData<JoystickUpdate>();

        /// <summary>
        /// Sends a hardware-specific command to the force-feedback driver.
        /// </summary>
        /// <param name="command">Driver-specific command number. Consult the driver documentation for a list of valid commands. </param>
        /// <param name="inData">Buffer containing the data required to perform the operation. </param>
        /// <param name="outData">Buffer in which the operation's output data is returned. </param>
        /// <returns>Number of bytes written to the output buffer</returns>
        /// <remarks>
        /// Because each driver implements different escapes, it is the application's responsibility to ensure that it is sending the escape to the correct driver by comparing the value of the guidFFDriver member of the <see cref="DeviceInstance"/> structure against the value the application is expecting.
        /// </remarks>
        public unsafe int Escape(int command, byte[] inData, byte[] outData)
        {
            var effectEscape = new EffectEscape();
            fixed (void* pInData = &inData[0])
            fixed (void* pOutData = &outData[0])
            {
                effectEscape.Command = command;
                effectEscape.InBufferPointer = (IntPtr)pInData;
                effectEscape.InBufferSize = inData.Length;
                effectEscape.OutBufferPointer = (IntPtr)pOutData;
                effectEscape.OutBufferSize = outData.Length;

                Escape(ref effectEscape);
                return effectEscape.OutBufferSize;
            }
        }

        /// <summary>
        /// Gets information about a device image for use in a configuration property sheet.
        /// </summary>
        /// <returns>A structure that receives information about the device image.</returns>
        public DeviceImageHeader GetDeviceImages()
        {
            var imageHeader = new DeviceImageHeader();
            GetImageInfo(imageHeader);

            if (imageHeader.BufferUsed > 0)
            {
                unsafe
                {
                    imageHeader.BufferSize = imageHeader.BufferUsed;
                    var pImages = stackalloc DeviceImage.__Native[imageHeader.BufferSize / sizeof(DeviceImage.__Native)];
                    imageHeader.ImageInfoArrayPointer = (IntPtr)pImages;
                }
                GetImageInfo(imageHeader);
            }
            return imageHeader;
        }

        /// <summary>
        /// Gets all effects.
        /// </summary>
        /// <returns>A collection of <see cref="EffectInfo"/></returns>
        public IList<EffectInfo> GetEffects()
        {
            return GetEffects(EffectType.All);
        }

        /// <summary>
        /// Gets the effects for a particular <see cref="EffectType"/>.
        /// </summary>
        /// <param name="effectType">Effect type.</param>
        /// <returns>A collection of <see cref="EffectInfo"/></returns>
        public IList<EffectInfo> GetEffects(EffectType effectType)
        {
            var enumEffectsCallback = new EnumEffectsCallback();
            EnumEffects(enumEffectsCallback.NativePointer, IntPtr.Zero, effectType);
            return enumEffectsCallback.EffectInfos;
        }

        /// <summary>
        /// Gets the effects stored in a RIFF Force Feedback file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>A collection of <see cref="EffectFile"/></returns>
        public IList<EffectFile> GetEffectsInFile(string fileName)
        {
            return GetEffectsInFile(fileName, EffectFileFlags.None);
        }

        /// <summary>
        /// Gets the effects stored in a RIFF Force Feedback file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="effectFileFlags">Flags used to filter effects.</param>
        /// <returns>A collection of <see cref="EffectFile"/></returns>
        public IList<EffectFile> GetEffectsInFile(string fileName, EffectFileFlags effectFileFlags)
        {
            var enumEffectsInFileCallback = new EnumEffectsInFileCallback();
            EnumEffectsInFile(fileName, enumEffectsInFileCallback.NativePointer, IntPtr.Zero, effectFileFlags);
            return enumEffectsInFileCallback.EffectsInFile;
        }

        // <summary>
        /// Gets information about a device object, such as a button or axis.
        /// </summary>
        /// <param name="objectId">The object type/instance identifier. This identifier is returned in the <see cref="DeviceObjectInstance.ObjectId"/> member of the <see cref="DeviceObjectInstance"/> structure returned from a previous call to the <see cref="GetObjects()"/> method.</param>
        /// <returns>A <see cref="DeviceObjectInstance"/> information</returns>
        public DeviceObjectInstance GetObjectInfoById(DeviceObjectId objectId)
        {
            return GetObjectInfo((int)objectId, PropertyHowType.Byid);
        }

        /// <summary>
        /// Gets information about a device object, such as a button or axis.
        /// </summary>
        /// <param name="usageCode">the HID Usage Page and Usage values.</param>
        /// <returns>A <see cref="DeviceObjectInstance"/> information</returns>
        public DeviceObjectInstance GetObjectInfoByUsage(int usageCode)
        {
            return GetObjectInfo(usageCode, PropertyHowType.Byusage);
        }

#if TODO
        /// <summary>
        /// Gets the properties about a device object, such as a button or axis.
        /// </summary>
        /// <param name="objectId">The object type/instance identifier. This identifier is returned in the <see cref="DeviceObjectInstance.Type"/> member of the <see cref="DeviceObjectInstance"/> structure returned from a previous call to the <see cref="GetObjects()"/> method.</param>
        /// <returns>an ObjectProperties</returns>
        public ObjectProperties GetObjectPropertiesById(DeviceObjectId objectId)
        {
            return new ObjectProperties(this, (int)objectId, PropertyHowType.Byid);
        }

        /// <summary>
        /// Gets the properties about a device object, such as a button or axis.
        /// </summary>
        /// <param name="usageCode">the HID Usage Page and Usage values.</param>
        /// <returns>an ObjectProperties</returns>
        public ObjectProperties GetObjectPropertiesByUsage(int usageCode)
        {
            return new ObjectProperties(this, usageCode, PropertyHowType.Byusage);
        } 
#endif

        /// <summary>
        /// Retrieves a collection of objects on the device.
        /// </summary>
        /// <returns>A collection of all device objects on the device.</returns>
        public IList<DeviceObjectInstance> GetObjects() => GetObjects(DeviceObjectTypeFlags.All);

        /// <summary>
        /// Retrieves a collection of objects on the device.
        /// </summary>
        /// <param name="deviceObjectTypeFlag">A filter for the returned device objects collection.</param>
        /// <returns>A collection of device objects matching the specified filter.</returns>
        public IList<DeviceObjectInstance> GetObjects(DeviceObjectTypeFlags deviceObjectTypeFlag)
        {
            var enumEffectsInFileCallback = new EnumObjectsCallback();
            EnumObjects(enumEffectsInFileCallback.NativePointer, IntPtr.Zero, (int)deviceObjectTypeFlag);
            return enumEffectsInFileCallback.Objects;
        }

        /// <summary>
        /// Specifies an event that is to be set when the device state changes. It is also used to turn off event notification.
        /// </summary>
        /// <param name="eventHandle">Handle to the event that is to be set when the device state changes. DirectInput uses the Microsoft Win32 SetEvent function on the handle when the state of the device changes. If the eventHandle parameter is null, notification is disabled.</param>
        /// <returns>A <see cref = "T:SharpDX.Result" /> object describing the result of the operation.</returns>
        public Result SetEventNotification(WaitHandle? eventHandle)
        {
            if (eventHandle == null)
            {
                return SetEventNotification(IntPtr.Zero);
            }
            else
            {
                return SetEventNotification(eventHandle!.SafeWaitHandle.DangerousGetHandle());
            }
        }

        /// <summary>
        /// Runs the DirectInput control panel associated with this device. If the device does not have a control panel associated with it, the default device control panel is launched.
        /// </summary>
        /// <returns>A <see cref="Result" /> object describing the result of the operation.</returns>
        public Result RunControlPanel() => RunControlPanel(IntPtr.Zero, 0);

        /// <summary>
        /// Runs the DirectInput control panel associated with this device. If the device does not have a control panel associated with it, the default device control panel is launched.
        /// </summary>
        /// <param name="parentHwnd">The parent control.</param>
        /// <returns>A <see cref="Result" /> object describing the result of the operation.</returns>
        public Result RunControlPanel(IntPtr parentHwnd) => RunControlPanel(parentHwnd, 0);

        /// <summary>
        /// Writes the effects to a file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="effects">The effects.</param>
        public void WriteEffectsToFile(string fileName, EffectFile[] effects)
        {
            WriteEffectsToFile(fileName, effects, false);
        }

        /// <summary>
        /// Writes the effects to file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="effects">The effects.</param>
        /// <param name="includeNonstandardEffects">if set to <c>true</c> [include nonstandard effects].</param>
        public void WriteEffectsToFile(string fileName, EffectFile[] effects, bool includeNonstandardEffects)
        {
            WriteEffectToFile(fileName, effects.Length, effects, (int)(includeNonstandardEffects ? EffectFileFlags.IncludeNonStandard : 0));
        }

        public Result SetDataFormat<TRaw>() where TRaw : unmanaged
        {
            var dataFormat = GetDataFormat<TRaw>();
            return SetDataFormat(dataFormat);
        }

        private unsafe DataFormat GetDataFormat<TRaw>() where TRaw : unmanaged
        {
            if (_dataFormat == null)
            {
                // Build DataFormat from IDataFormatProvider
                if (typeof(IDataFormatProvider).IsAssignableFrom(typeof(TRaw)))
                {
                    var provider = (IDataFormatProvider)(new TRaw());
                    _dataFormat = new DataFormat(provider.Flags)
                    {
                        DataSize = sizeof(TRaw),
                        ObjectsFormat = provider.ObjectsFormat
                    };
                }
                else
                {
                    // Build DataFormat from DataFormat and DataObjectFormat attributes
                    IEnumerable<DataFormatAttribute> dataFormatAttributes = typeof(TRaw).GetCustomAttributes<DataFormatAttribute>(false);
                    if (dataFormatAttributes.Count() != 1)
                        throw new InvalidOperationException(
                            string.Format(System.Globalization.CultureInfo.InvariantCulture, "The structure [{0}] must be marked with DataFormatAttribute or provide a IDataFormatProvider",
                                            typeof(TRaw).FullName));

                    _dataFormat = new DataFormat(((DataFormatAttribute)dataFormatAttributes.First()).Flags)
                    {
                        DataSize = sizeof(TRaw)
                    };

                    var dataObjects = new List<ObjectDataFormat>();

                    IEnumerable<FieldInfo> fields = typeof(TRaw).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                    // Iterates on fields
                    foreach (var field in fields)
                    {
                        IEnumerable<DataObjectFormatAttribute> dataObjectAttributes = field.GetCustomAttributes<DataObjectFormatAttribute>(false);
                        if (dataObjectAttributes.Count() > 0)
                        {
                            int fieldOffset = Marshal.OffsetOf(typeof(TRaw), field.Name).ToInt32();
                            int totalSizeOfField = Marshal.SizeOf(field.FieldType);
                            int offset = fieldOffset;
                            int numberOfDataObjects = 0;

                            // Count the number of effective sub-field for a field
                            // A field that contains a fixed array should have sub-field
                            for (int i = 0; i < dataObjectAttributes.Count(); i++)
                            {
                                var attr = dataObjectAttributes.ElementAt(i);
                                numberOfDataObjects += attr.ArrayCount == 0 ? 1 : attr.ArrayCount;
                            }

                            // Check that the size of the field is compatible with the number of sub-field
                            // For a simple field without any array element, sub-field = field
                            int sizeOfField = totalSizeOfField / numberOfDataObjects;
                            if ((sizeOfField * numberOfDataObjects) != totalSizeOfField)
                                throw new InvalidOperationException(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Field [{0}] has incompatible size [{1}] and number of DataObjectAttributes [{2}]", field.Name, (double)totalSizeOfField / numberOfDataObjects, numberOfDataObjects));

                            int subFieldIndex = 0;

                            // Iterates on attributes
                            for (int i = 0; i < dataObjectAttributes.Count(); i++)
                            {

                                var attr = dataObjectAttributes.ElementAt(i);
                                numberOfDataObjects = attr.ArrayCount == 0 ? 1 : attr.ArrayCount;

                                // Add DataObjectFormat
                                for (int j = 0; j < numberOfDataObjects; j++)
                                {
                                    var dataObject = new ObjectDataFormat(
                                        string.IsNullOrEmpty(attr.Guid) ? Guid.Empty : new Guid(attr.Guid), offset,
                                        attr.TypeFlags, attr.Flags, attr.InstanceNumber);

                                    // Use attribute name or fallback to field's name
                                    string name = (string.IsNullOrEmpty(attr.Name)) ? field.Name : attr.Name;
                                    name = numberOfDataObjects == 1 ? name : name + subFieldIndex;

                                    dataObject.Name = name;
                                    dataObjects.Add(dataObject);

                                    offset += sizeOfField;
                                    subFieldIndex++;
                                }
                            }
                        }
                    }
                    _dataFormat.ObjectsFormat = dataObjects.ToArray();
                }

                for (int i = 0; i < _dataFormat.ObjectsFormat.Length; i++)
                {
                    var dataObject = _dataFormat.ObjectsFormat[i];

                    // Map field name to object
                    if (_mapNameToObjectFormat.ContainsKey(dataObject.Name))
                        throw new InvalidOperationException(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Incorrect field name [{0}]. Field name must be unique", dataObject.Name));
                    _mapNameToObjectFormat.Add(dataObject.Name, dataObject);
                }

                // DumpDataFormat(_dataFormat);
            }
            return _dataFormat;
        }
    }
}
