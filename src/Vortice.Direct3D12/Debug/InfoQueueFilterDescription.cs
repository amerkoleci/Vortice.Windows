// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Debug;

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
        public MessageCategory* pCategoryList;
        public uint NumSeverities;
        public MessageSeverity* pSeverityList;
        public uint NumIDs;
        public MessageId* pIDList;

        internal void __MarshalFree()
        {
            if (pCategoryList != null)
                NativeMemory.Free(pCategoryList);
            if (pSeverityList != null)
                NativeMemory.Free(pSeverityList);
            if (pIDList != null)
                NativeMemory.Free(pIDList);
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
            UnsafeUtilities.Read(@ref.pCategoryList, Categories);
        }

        Severities = new MessageSeverity[@ref.NumSeverities];
        if (@ref.NumSeverities > 0)
        {
            UnsafeUtilities.Read(@ref.pSeverityList, Severities);
        }

        Ids = new MessageId[@ref.NumIDs];
        if (@ref.NumIDs > 0)
        {
            UnsafeUtilities.Read(@ref.pIDList, Ids);
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.NumCategories = (uint)(Categories?.Length ?? 0);
        @ref.pCategoryList = (MessageCategory*)UnsafeUtilities.AllocToPointer(Categories);
        @ref.NumSeverities = (uint)(Severities?.Length ?? 0);
        @ref.pSeverityList = (MessageSeverity*)UnsafeUtilities.AllocToPointer(Severities);
        @ref.NumIDs = (uint)(Ids?.Length ?? 0);
        @ref.pIDList = (MessageId*)UnsafeUtilities.AllocToPointer(Ids);
    }
    #endregion
}
