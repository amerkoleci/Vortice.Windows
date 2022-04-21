// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Vortice.DirectML;
public abstract partial class TensorDescription
{
    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public TensorType Type;
        public IntPtr Description;
    }

    internal virtual void __MarshalFree(ref __Native @ref)
    {

    }

    internal virtual void __MarshalFrom(ref __Native @ref)
    {
        throw new NotImplementedException();
    }

    internal abstract void __MarshalTo(ref __Native @ref);
    #endregion
}
