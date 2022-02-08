// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class ID3D12Resource1
{
    private ID3D12ProtectedResourceSession _protectedResourceSession;

    public ID3D12ProtectedResourceSession? ProtectedResourceSession
    {
        get
        {
            if (_protectedResourceSession != null)
                return _protectedResourceSession;

            if (GetProtectedResourceSession(typeof(ID3D12ProtectedResourceSession).GUID, out IntPtr nativePtr).Failure)
            {
                return null;
            }

            _protectedResourceSession = new ID3D12ProtectedResourceSession(nativePtr);
            return _protectedResourceSession;
        }
    }
}
