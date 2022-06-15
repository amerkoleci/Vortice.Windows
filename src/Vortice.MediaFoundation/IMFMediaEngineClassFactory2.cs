// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.MediaFoundation;

public partial class IMFMediaEngineClassFactory2
{
    public IMFMediaKeys CreateMediaKeys2(string keySystem, string defaultCdmStorePath)
    {
        return CreateMediaKeys2(keySystem, defaultCdmStorePath, null);
    }
}
