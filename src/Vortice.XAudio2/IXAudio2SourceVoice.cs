// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.XAudio2
{
    public partial class IXAudio2SourceVoice
    {
        /// <summary>
        /// Gets the voice state.
        /// </summary>
        public VoiceState State => GetState(0);

        /// <summary>
        /// Gets the voice state without samples played.
        /// </summary>
        public VoiceState StateNoSamplesPlayed => GetState(NoSamplesPlayed);

        /// <summary>
        /// Gets or Sets the frequency adjustment ratio of the voice.
        /// </summary>
        public float FrequencyRatio
        {
            get
            {
                GetFrequencyRatio(out float ratio);
                return ratio;
            }
            set
            {
                SetFrequencyRatio(value, 0);
            }
        }

        /// <summary>	
        /// Starts consumption and processing of audio by the voice. Delivers the result to any connected submix or mastering voices, or to the output device, with CommitNow changes.
        /// </summary>	
        public void Start()
        {
            Start(0, 0);
        }

        /// <summary>	
        /// Starts consumption and processing of audio by the voice. Delivers the result to any connected submix or mastering voices, or to the output device.	
        /// </summary>	
        /// <param name="operationSet">Identifies this call as part of a deferred batch.</param>
        public void Start(int operationSet)
        {
            Start(0, operationSet);
        }

        public void Stop()
        {
            Stop(PlayFlags.None, 0);
        }

        public void Stop(int operationSet)
        {
            Stop(PlayFlags.None, operationSet);
        }

        /// <summary>
        /// Adds a new audio buffer to the voice queue.
        /// </summary>
        /// <param name="buffer"></param>
        public void SubmitSourceBuffer(AudioBuffer buffer)
        {
            SubmitSourceBuffer(buffer, (BufferWma?)null);
        }

        /// <summary>
        /// Adds a new audio buffer to the voice queue.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="decodedXMWAPacketInfo"></param>
        public void SubmitSourceBuffer(AudioBuffer buffer, uint[] decodedXMWAPacketInfo)
        {
            unsafe
            {
                fixed (void* pBuffer = decodedXMWAPacketInfo)
                {
                    var bufferWma = new BufferWma
                    {
                        PacketCount = decodedXMWAPacketInfo.Length,
                        DecodedPacketCumulativeBytesPointer = (IntPtr)pBuffer
                    };
                    SubmitSourceBuffer(buffer, bufferWma);
                }
            }
        }
    }
}
