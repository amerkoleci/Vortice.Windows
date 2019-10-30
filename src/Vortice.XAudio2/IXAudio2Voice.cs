// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.XAudio2
{
    public partial class IXAudio2Voice
    {
        public static XAudio2Version Version { get; internal set; }

        /// <summary>	
        /// Gets or Sets the overall volume level for the voice.	
        /// </summary>	
        public float Volume
        {
            get
            {
                GetVolume(out var value);
                return value;
            }
            set
            {
                SetVolume(value);
            }
        }

        /// <summary>	
        /// Returns information about the creation flags, input channels, and sample rate of a voice.
        /// </summary>	
        public VoiceDetails VoiceDetails
        {
            get
            {
                GetVoiceDetails(out var details);

                // Handle 2.7 version changes here
                if (Version == XAudio2Version.Version27)
                {
                    details.InputSampleRate = details.InputChannels;
                    details.InputChannels = details.ActiveFlags;
                    details.ActiveFlags = 0;
                }

                return details;
            }
        }

        /// <summary>
        /// Sets the overall volume level for the voice.
        /// </summary>
        /// <param name="volume">Overall volume level to use.</param>
        public void SetVolume(float volume)
        {
            SetVolume(volume, 0);
        }

        /// <summary>
        /// Sets the volume levels for the voice, per channel.
        /// </summary>
        /// <param name="channels">Number of channels in the voice.</param>
        /// <param name="volumes">Array containing the new volumes of each channel in the voice. The array must have Channels elements.</param>
        public void SetChannelVolumes(int channels, float[] volumes)
        {
            SetChannelVolumes(channels, volumes, 0);
        }

        /// <summary>	
        /// Designates a new set of submix or mastering voices to receive the output of the voice.	
        /// </summary>	
        /// <param name="outputVoices">
        /// Array of <see cref="VoiceSendDescriptor"/> structure pointers to destination voices. If outputVoices is null, the voice will send its output to the current mastering voice. To set the voice to not send its output anywhere set an array of length 0. All of the voices in a send list must have the same input sample rate, see {{XAudio2 Sample Rate Conversions}} for additional information.
        /// </param>
        public void SetOutputVoices(params VoiceSendDescriptor[] outputVoices)
        {
            unsafe
            {
                if (outputVoices != null)
                {
                    var tempSendDescriptor = new VoiceSendDescriptors { SendCount = outputVoices.Length };

                    if (outputVoices.Length > 0)
                    {
                        var nativeDescriptors = new VoiceSendDescriptor.__Native[outputVoices.Length];

                        for (int i = 0; i < outputVoices.Length; i++)
                        {
                            outputVoices[i].__MarshalTo(ref nativeDescriptors[i]);
                        }

                        fixed (void* pVoiceSendDescriptors = &nativeDescriptors[0])
                        {
                            tempSendDescriptor.SendPointer = (IntPtr)pVoiceSendDescriptors;
                            SetOutputVoices(tempSendDescriptor);
                        }
                    }
                    else
                    {
                        tempSendDescriptor.SendPointer = IntPtr.Zero;
                    }
                }
                else
                {
                    SetOutputVoices((VoiceSendDescriptors?)null);
                }
            }
        }

        /// <summary>	
        /// Sets the volume level of each channel of the final output for the voice. These channels are mapped to the input channels of a specified destination voice.	
        /// </summary>	
        /// <param name="sourceChannels">Confirms the output channel count of the voice. This is the number of channels that are produced by the last effect in the chain.</param>
        /// <param name="destinationChannels">Confirms the input channel count of the destination voice.</param>
        /// <param name="levelMatrix">
        /// Array of [sourceChannels x destinationChannels] volume levels sent to the destination voice. 
        /// The level sent from source channel S to destination channel D is specified in the form levelMatrix[sourceChannels x D + S]. 
        /// For example, when rendering two-channel stereo input into 5.1 output that is weighted toward the front channels-but is absent from the center and low-frequency channels-the matrix might have the values shown in the following table.
        /// </param>
        public void SetOutputMatrix(int sourceChannels, int destinationChannels, float[] levelMatrix)
        {
            SetOutputMatrix(sourceChannels, destinationChannels, levelMatrix, 0);
        }

        /// <summary>	
        /// Sets the volume level of each channel of the final output for the voice. These channels are mapped to the input channels of a specified destination voice.	
        /// </summary>	
        /// <param name="destinationVoice">
        /// A destination of <see cref="IXAudio2Voice"/> for which to set volume levels. 
        /// Note If the voice sends to a single target voice then specifying null will cause SetOutputMatrix to operate on that target voice.
        /// </param>
        /// <param name="sourceChannels">
        /// Confirms the output channel count of the voice. This is the number of channels that are produced by the last effect in the chain.
        /// </param>
        /// <param name="destinationChannels">
        /// Confirms the input channel count of the destination voice.
        /// </param>
        /// <param name="levelMatrix">
        /// Array of [sourceChannels x destinationChannels] volume levels sent to the destination voice. 
        /// The level sent from source channel S to destination channel D is specified in the form levelMatrix[sourceChannels x D + S]. 
        /// For example, when rendering two-channel stereo input into 5.1 output that is weighted toward the front channels-but is absent from the center and low-frequency channels-the matrix might have the values shown in the following table.
        /// </param>
        public void SetOutputMatrix(IXAudio2Voice destinationVoice, int sourceChannels, int destinationChannels, float[] levelMatrix)
        {
            SetOutputMatrix(destinationVoice, sourceChannels, destinationChannels, levelMatrix, 0);
        }

        /// <summary>	
        /// Sets the volume level of each channel of the final output for the voice. These channels are mapped to the input channels of a specified destination voice.	
        /// </summary>	
        /// <param name="sourceChannels">
        /// Confirms the output channel count of the voice. This is the number of channels that are produced by the last effect in the chain.
        /// </param>
        /// <param name="destinationChannels">
        /// Confirms the input channel count of the destination voice.
        /// </param>
        /// <param name="levelMatrix">
        /// Array of [sourceChannels x destinationChannels] volume levels sent to the destination voice. 
        /// The level sent from source channel S to destination channel D is specified in the form levelMatrix[sourceChannels x D + S]. 
        /// For example, when rendering two-channel stereo input into 5.1 output that is weighted toward the front channels-but is absent from the center and low-frequency channels-the matrix might have the values shown in the following table.
        /// </param>
        /// <param name="operationSet">Identifies this call as part of a deferred batch. See the {{XAudio2 Operation Sets}} overview for more information.</param>
        public void SetOutputMatrix(int sourceChannels, int destinationChannels, float[] levelMatrix, int operationSet)
        {
            SetOutputMatrix(null, sourceChannels, destinationChannels, levelMatrix, operationSet);
        }
    }
}
