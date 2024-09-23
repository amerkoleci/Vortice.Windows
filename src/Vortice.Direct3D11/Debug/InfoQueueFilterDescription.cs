// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;

namespace Vortice.Direct3D11.Debug;

public partial class InfoQueueFilterDescription
{
    /// <summary>
    /// Gets or sets the categories.
    /// </summary>
    public MessageCategory[]? Categories { get; set; }

    /// <summary>
    /// Gets or sets the severities.
    /// </summary>
    public MessageSeverity[]? Severities { get; set; }

    /// <summary>
    /// Gets or sets the ids.
    /// </summary>
    public MessageId[]? Ids { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public uint NumCategories;
        public MessageCategory* PCategoryList;
        public uint NumSeverities;
        public MessageSeverity* PSeverityList;
        public uint NumIDs;
        public MessageId* PIDList;

        internal void __MarshalFree()
        {
            if (PCategoryList != null)
                NativeMemory.Free(PCategoryList);
            if (PSeverityList != null)
                NativeMemory.Free(PSeverityList);
            if (PIDList != null)
                NativeMemory.Free(PIDList);
        }
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        Categories = new MessageCategory[@ref.NumCategories];
        if (@ref.NumCategories > 0)
        {
            UnsafeUtilities.Read(@ref.PCategoryList, Categories);
        }

        Severities = new MessageSeverity[@ref.NumSeverities];
        if (@ref.NumSeverities > 0)
        {
            UnsafeUtilities.Read(@ref.PSeverityList, Severities);
        }

        Ids = new MessageId[@ref.NumIDs];
        if (@ref.NumIDs > 0)
        {
            UnsafeUtilities.Read(@ref.PIDList, Ids);
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        if (Categories != null && Categories.Length > 0)
        {
            @ref.NumCategories = (uint)Categories.Length;
            @ref.PCategoryList = UnsafeUtilities.AllocToPointer(Categories);
        }
        else
        {
            @ref.NumCategories = 0;
            @ref.PCategoryList = null;
        }

        if (Severities != null && Severities.Length > 0)
        {
            @ref.NumSeverities = (uint)Severities.Length;
            @ref.PSeverityList = UnsafeUtilities.AllocToPointer(Severities);
        }
        else
        {
            @ref.NumSeverities = 0;
            @ref.PSeverityList = null;
        }

        if (Ids != null && Ids.Length > 0)
        {
            @ref.NumIDs = (uint)Ids.Length;
            @ref.PIDList = UnsafeUtilities.AllocToPointer(Ids);
        }
        else
        {
            @ref.NumIDs = 0;
            @ref.PIDList = null;
        }
    }
    #endregion
}
