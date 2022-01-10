// Copyright © Amer Koleci and Contributors.
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
    internal struct __Native
    {
        public int NumCategories;
        public IntPtr PCategoryList;
        public int NumSeverities;
        public IntPtr PSeverityList;
        public int NumIDs;
        public IntPtr PIDList;

        internal void __MarshalFree()
        {
            if (PCategoryList != IntPtr.Zero)
                Marshal.FreeHGlobal(PCategoryList);
            if (PSeverityList != IntPtr.Zero)
                Marshal.FreeHGlobal(PSeverityList);
            if (PIDList != IntPtr.Zero)
                Marshal.FreeHGlobal(PIDList);
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
            @ref.NumCategories = Categories.Length;
            @ref.PCategoryList = UnsafeUtilities.AllocToPointer(Categories);
        }
        else
        {
            @ref.NumCategories = 0;
            @ref.PCategoryList = IntPtr.Zero;
        }

        if (Severities != null && Severities.Length > 0)
        {
            @ref.NumSeverities = Severities.Length;
            @ref.PSeverityList = UnsafeUtilities.AllocToPointer(Severities);
        }
        else
        {
            @ref.NumSeverities = 0;
            @ref.PSeverityList = IntPtr.Zero;
        }

        if (Ids != null && Ids.Length > 0)
        {
            @ref.NumIDs = Ids.Length;
            @ref.PIDList = UnsafeUtilities.AllocToPointer(Ids);
        }
        else
        {
            @ref.NumIDs = 0;
            @ref.PIDList = IntPtr.Zero;
        }
    }
    #endregion
}
