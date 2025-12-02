// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Dxc.Test;

internal static class ShaderCodeHelper
{
    internal unsafe struct DxilMinimalHeader
    {
        public uint four_cc;
        public fixed uint hash_digest[4];
    };

    internal static unsafe bool IsCodeSigned(byte[] code)
    {
        fixed (byte* pcode = code)
        {
            var header = *(DxilMinimalHeader*)(pcode);

            bool has_digest = false;

            has_digest |= header.hash_digest[0] != 0x0;
            has_digest |= header.hash_digest[1] != 0x0;
            has_digest |= header.hash_digest[2] != 0x0;
            has_digest |= header.hash_digest[3] != 0x0;

            return has_digest;
        }
    }
}
