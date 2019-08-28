// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D12.Debug
{
    public partial class InfoQueueFilterDescription
    {
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public MessageCategory[] Categories { get; set; }

        /// <summary>
        /// Gets or sets the severities.
        /// </summary>
        public MessageSeverity[] Severities { get; set; }

        /// <summary>
        /// Gets or sets the ids.
        /// </summary>
        public MessageId[] Ids { get; set; }

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
                Interop.Read(@ref.PCategoryList, Categories);
            }

            Severities = new MessageSeverity[@ref.NumSeverities];
            if (@ref.NumSeverities > 0)
            {
                Interop.Read(@ref.PSeverityList, Severities);
            }

            Ids = new MessageId[@ref.NumIDs];
            if (@ref.NumIDs > 0)
            {
                Interop.Read(@ref.PIDList, Ids);
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.NumCategories = Categories?.Length ?? 0;
            @ref.PCategoryList = Interop.AllocToPointer(Categories);
            @ref.NumSeverities = Severities?.Length ?? 0;
            @ref.PSeverityList = Interop.AllocToPointer(Severities);
            @ref.NumIDs = Ids?.Length ?? 0;
            @ref.PIDList = Interop.AllocToPointer(Ids);
        }
        #endregion
    }
}
