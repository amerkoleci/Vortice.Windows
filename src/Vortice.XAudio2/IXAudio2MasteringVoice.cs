// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.XAudio2
{
    public partial class IXAudio2MasteringVoice
    {
        /// <summary>	
        /// Returns the channel mask for this voice. (Only valid for XAudio 2.8 or higher, returns 0 otherwise)
        /// </summary>	
        public int ChannelMask
        {
            get
            {
                GetChannelMask(out var mask);
                return mask;
            }
        }
    }
}
