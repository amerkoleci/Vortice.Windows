// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI.Debug;

public partial class InfoQueueFilterDescription
{
    /// <summary>
    /// Gets or sets the categories.
    /// </summary>
    public InfoQueueMessageCategory[]? Categories { get; set; }

    /// <summary>
    /// Gets or sets the severities.
    /// </summary>
    public InfoQueueMessageSeverity[]? Severities { get; set; }

    /// <summary>
    /// Gets or sets the ids.
    /// </summary>
    public int[]? Ids { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public uint NumCategories;
        public InfoQueueMessageCategory* pCategoryList;
        public uint NumSeverities;
        public InfoQueueMessageSeverity* pSeverityList;
        public uint NumIDs;
        public int* pIDList;

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
        Categories = new InfoQueueMessageCategory[@ref.NumCategories];
        if (@ref.NumCategories > 0)
        {
            UnsafeUtilities.Read(@ref.pCategoryList, Categories, @ref.NumCategories);
        }

        Severities = new InfoQueueMessageSeverity[@ref.NumSeverities];
        if (@ref.NumSeverities > 0)
        {
            UnsafeUtilities.Read(@ref.pSeverityList, Severities, @ref.NumSeverities);
        }

        Ids = new int[@ref.NumIDs];
        if (@ref.NumIDs > 0)
        {
            UnsafeUtilities.Read(@ref.pIDList, Ids,  @ref.NumIDs);
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        if (Categories != null && Categories.Length > 0)
        {
            @ref.NumCategories = (uint)Categories.Length;
            @ref.pCategoryList = (InfoQueueMessageCategory*)UnsafeUtilities.AllocToPointer(Categories);
        }
        else
        {
            @ref.NumCategories = 0;
            @ref.pCategoryList = null;
        }

        if (Severities != null && Severities.Length > 0)
        {
            @ref.NumSeverities = (uint)Severities.Length;
            @ref.pSeverityList = (InfoQueueMessageSeverity*)UnsafeUtilities.AllocToPointer(Severities);
        }
        else
        {
            @ref.NumSeverities = 0;
            @ref.pSeverityList = null;
        }

        if (Ids != null && Ids.Length > 0)
        {
            @ref.NumIDs = (uint)Ids.Length;
            @ref.pIDList = (int*)UnsafeUtilities.AllocToPointer(Ids);
        }
        else
        {
            @ref.NumIDs = 0;
            @ref.pIDList = null;
        }
    }
    #endregion
}
