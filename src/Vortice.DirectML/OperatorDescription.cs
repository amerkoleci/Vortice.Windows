// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial class OperatorDescription
{
    public IOperatorDescription Description { get; set; }

    public OperatorDescription(IOperatorDescription description)
    {
        Description = description;
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public OperatorType Type;
        public IntPtr Description;
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        ((IOperatorDescriptionMarshal)Description).__MarshalFree(ref @ref.Description);
        @ref.Description = IntPtr.Zero;
    }

    internal void __MarshalTo(ref __Native @ref)
    {
        @ref.Type = Description.OperatorType;
        @ref.Description = ((IOperatorDescriptionMarshal)Description).__MarshalAlloc();
    }
    #endregion
}
