// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D9;

public partial class IDirect3DQuery9
{
    /// <summary>
    /// Gets the type.
    /// </summary>
    public QueryType Type => GetQueryType();

    /// <summary>	
    /// <p>Polls a queried resource to get the query state or a query result. For more information about queries, see Queries (Direct3D 9).</p>	
    /// </summary>	
    /// <typeparam name="T">Type of the object to query. See remarks.</typeparam>
    /// <param name="data">The value of the query </param>
    /// <param name="flush">if set to <c>true</c> [flush].</param>
    /// <returns>
    /// The return type identifies the query state (see Queries (Direct3D 9)).
    /// The method returns <strong>true</strong> if the query data is available and <strong>false</strong>if it is not.
    /// These are considered successful return values. If the method fails when <strong>D3DGETDATA_FLUSH</strong> is used, the return value can be <see cref="Vortice.Direct3D9.ResultCode.DeviceLost"/>.
    /// </returns>	
    /// <remarks>
    /// Each <see cref="QueryType"/> is expecting a particular type.
    /// <ul>
    /// <li>QueryType.VCache  => <see cref="VCache"/></li>
    /// <li>QueryType.ResourceManager  => <see cref="ResourceManager"/></li>
    /// <li>QueryType.VertexStats  => <see cref="VertexStats"/></li>
    /// <li>QueryType.Event  => <see cref="bool"/></li>
    /// <li>QueryType.Occlusion  => <see cref="int"/> or <see cref="uint"/></li>
    /// <li>QueryType.Timestamp  => <see cref="long"/> or <see cref="ulong"/></li>
    /// <li>QueryType.TimestampDisjoint  => <see cref="bool"/></li>
    /// <li>QueryType.PipelineTimings  => <see cref="PipelineTimings"/></li>
    /// <li>QueryType.InterfaceTimings  => <see cref="InterfaceTimings"/></li>
    /// <li>QueryType.VertexTimings  => <see cref="StageTimings"/></li>
    /// <li>QueryType.BandwidthTimings  => <see cref="BandwidthTimings"/></li>
    /// <li>QueryType.CacheUtilization  => <see cref="CacheUtilization"/></li>
    /// </ul>
    /// </remarks>
    public unsafe bool GetData<T>(out T data, bool flush) where T : unmanaged
    {
        QueryType type = Type;
        bool isInvalid = true;
        switch (type)
        {
            case QueryType.VCache:
                isInvalid = typeof(T) != typeof(VCache);
                break;

            case QueryType.ResourceManager:
                isInvalid = typeof(T) != typeof(ResourceManager);
                break;

            case QueryType.VertexStats:
                isInvalid = typeof(T) != typeof(VertexStats);
                break;

            case QueryType.Event:
                isInvalid = typeof(T) != typeof(bool);
                break;

            case QueryType.Occlusion:
                isInvalid = (typeof(T) != typeof(int)) && (typeof(T) != typeof(uint));
                break;

            case QueryType.Timestamp:
                isInvalid = (typeof(T) != typeof(long)) && (typeof(T) != typeof(ulong));
                break;

            case QueryType.TimestampDisjoint:
                isInvalid = typeof(T) != typeof(bool);
                break;

            case QueryType.TimestampFreq:
                isInvalid = (typeof(T) != typeof(long)) && (typeof(T) != typeof(ulong));
                break;

            case QueryType.PipelineTimings:
                isInvalid = typeof(T) != typeof(PipelineTimings);
                break;

            case QueryType.InterfaceTimings:
                isInvalid = typeof(T) != typeof(InterfaceTimings);
                break;

            case QueryType.VertexTimings:
                isInvalid = typeof(T) != typeof(StageTimings);
                break;

            case QueryType.BandwidthTimings:
                isInvalid = typeof(T) != typeof(BandwidthTimings);
                break;

            case QueryType.CacheUtilization:
                isInvalid = typeof(T) != typeof(CacheUtilization);
                break;
        }

        if (isInvalid)
            throw new ArgumentException(string.Format("Unsupported data size [{0}] for type [{1}]. See documentation for expecting type.", typeof(T), type));

        Result result;
        if (typeof(T) == typeof(bool))
        {
            int value = 0;
            result = GetData(&value, 4, flush ? 1 : 0);
            data = (T)Convert.ChangeType(value, typeof(T));
        }
        else
        {
            data = default;
            result = GetData(Unsafe.AsPointer(ref data), sizeof(T), flush ? 1 : 0);
        }

        if (result == Result.Ok)
            return true;
        if (result == Result.False)
            return false;

        // Should throw an exception
        result.CheckError();
        return false;
    }
}
