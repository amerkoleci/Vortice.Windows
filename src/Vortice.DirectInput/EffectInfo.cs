// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectInput;

public partial class EffectInfo
{
    #region Marshal
    internal static unsafe __Native __NewNative()
    {
        __Native native = default;
        native.Size = sizeof(__Native);
        return native;
    }
    #endregion Marshal
}
