// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct3D12
{
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
}
