// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Description of a state object. Pass a value of this structure type <see cref="ID3D12Device5.CreateStateObject(StateObjectDescription)"/>.
/// </summary>
public partial class StateObjectDescription
{
    public StateObjectDescription(StateObjectType type, params StateSubObject[] subObjects)
    {
        Type = type;
        SubObjects = subObjects;
    }

    /// <summary>
    /// The type of the state object.
    /// </summary>
    public StateObjectType Type { get; }

    /// <summary>	
    /// An array of <see cref="InputElementDescription"/> that describe the data types of the input-assembler stage.
    /// </summary>	
    public StateSubObject[] SubObjects { get; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public StateObjectType Type;
        public int NumSubobjects;
        public StateSubObject.__Native* pSubobjects;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        if (@ref.NumSubobjects > 0)
        {
            for (int i = 0; i < @ref.NumSubobjects; i++)
            {
                SubObjects[i].__MarshalFree(ref @ref.pSubobjects[i]);
            }

            Marshal.FreeHGlobal((IntPtr)@ref.pSubobjects);
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.Type = Type;
        @ref.NumSubobjects = SubObjects?.Length ?? 0;
        if (@ref.NumSubobjects > 0)
        {
            var subObjectLookup = new Dictionary<StateSubObject, IntPtr>();
            var nativeSubObjects = (StateSubObject.__Native*)UnsafeUtilities.Alloc<StateSubObject.__Native>(@ref.NumSubobjects);

            // Create lookup table first
            for (int i = 0; i < @ref.NumSubobjects; i++)
            {
                subObjectLookup.Add(SubObjects![i], new IntPtr(&nativeSubObjects[i]));
            }

            for (int i = 0; i < @ref.NumSubobjects; i++)
            {
                SubObjects![i].__MarshalTo(ref nativeSubObjects[i], subObjectLookup);
            }

            @ref.pSubobjects = nativeSubObjects;
        }
    }
    #endregion
}
