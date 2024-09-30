// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Vortice.XAudio2.Fx;

namespace Vortice.XAudio2;

public unsafe partial class IXAudio2Voice
{
    /// <summary>	
    /// Gets or Sets the overall volume level for the voice.	
    /// </summary>	
    public float Volume
    {
        get => GetVolume();
        set => SetVolume(value).CheckError();
    }

    /// <summary>
    /// Sets the overall volume level for the voice.
    /// </summary>
    /// <param name="volume">Overall volume level to use.</param>
    public Result SetVolume(float volume) => SetVolume(volume, 0);

    /// <summary>
    /// Sets the volume levels for the voice, per channel.
    /// </summary>
    /// <param name="channels">Number of channels in the voice.</param>
    /// <param name="volumes">Array containing the new volumes of each channel in the voice. The array must have Channels elements.</param>
    public Result SetChannelVolumes(uint channels, float[] volumes) => SetChannelVolumes(channels, volumes, 0);

    /// <summary>	
    /// Replaces the effect chain of the voice.	
    /// </summary>	
    /// <param name="effectDescriptors">
    /// An array of <see cref="EffectDescriptor"/> that describes the new effect chain to use. 
    /// If null is passed, the current effect chain is removed. If array is non null, its length must be at least of 1.
    /// </param>
    public Result SetEffectChain(params EffectDescriptor[] effectDescriptors)
    {
        if (effectDescriptors != null)
        {
            var tempSendDescriptor = new EffectChain();
            var effectDescriptorNatives = new EffectDescriptor.__Native[effectDescriptors.Length];
            for (int i = 0; i < effectDescriptorNatives.Length; i++)
                effectDescriptors[i].__MarshalTo(ref effectDescriptorNatives[i]);
            tempSendDescriptor.EffectCount = (uint)effectDescriptorNatives.Length;
            fixed (void* pEffectDescriptors = &effectDescriptorNatives[0])
            {
                tempSendDescriptor.EffectDescriptorPointer = (IntPtr)pEffectDescriptors;
                return SetEffectChain(tempSendDescriptor);
            }
        }
        else
        {
            return SetEffectChain((EffectChain?)null);
        }
    }

    /// <summary>	
    /// Designates a new set of submix or mastering voices to receive the output of the voice.	
    /// </summary>	
    /// <param name="outputVoices">
    /// Array of <see cref="VoiceSendDescriptor"/> structure pointers to destination voices. If outputVoices is null, the voice will send its output to the current mastering voice. To set the voice to not send its output anywhere set an array of length 0. All of the voices in a send list must have the same input sample rate, see {{XAudio2 Sample Rate Conversions}} for additional information.
    /// </param>
    public Result SetOutputVoices(params Span<VoiceSendDescriptor> outputVoices)
    {
        if (outputVoices.Length > 0)
        {
            VoiceSendDescriptors tempSendDescriptor = new()
            {
                SendCount = (uint)outputVoices.Length
            };

            var nativeDescriptors = new VoiceSendDescriptor.__Native[outputVoices.Length];

            for (int i = 0; i < outputVoices.Length; i++)
            {
                outputVoices[i].__MarshalTo(ref nativeDescriptors[i]);
            }

            fixed (void* pVoiceSendDescriptors = &nativeDescriptors[0])
            {
                tempSendDescriptor.SendPointer = (IntPtr)pVoiceSendDescriptors;
                return SetOutputVoices(tempSendDescriptor);
            }
        }
        else
        {
            return SetOutputVoices((VoiceSendDescriptors?)null);
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
    public Result SetOutputMatrix(uint sourceChannels, uint destinationChannels, float[] levelMatrix)
    {
        return SetOutputMatrix(sourceChannels, destinationChannels, levelMatrix, 0);
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
    public Result SetOutputMatrix(IXAudio2Voice destinationVoice, uint sourceChannels, uint destinationChannels, float[] levelMatrix)
    {
        return SetOutputMatrix(destinationVoice, sourceChannels, destinationChannels, levelMatrix, 0);
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
    public Result SetOutputMatrix(uint sourceChannels, uint destinationChannels, float[] levelMatrix, uint operationSet)
    {
        return SetOutputMatrix(null, sourceChannels, destinationChannels, levelMatrix, operationSet);
    }

    /// <summary>	
    /// Enables the effect at a given position in the effect chain of the voice.	
    /// </summary>	
    /// <param name="effectIndex">Zero-based index of an effect in the effect chain of the voice.</param>
    public Result EnableEffect(uint effectIndex) => EnableEffect(effectIndex, 0);

    /// <summary>	
    /// Disables the effect at a given position in the effect chain of the voice.	
    /// </summary>	
    /// <param name="effectIndex">Zero-based index of an effect in the effect chain of the voice.</param>
    public Result DisableEffect(uint effectIndex) => DisableEffect(effectIndex, 0);

    /// <summary>	
    /// Sets parameters for a given effect in the voice's effect chain.
    /// </summary>	
    /// <param name="effectIndex">[in]  Zero-based index of an effect within the voice's effect chain. </param>
    /// <returns>Returns the current values of the effect-specific parameters.</returns>
    public T GetEffectParameters<T>(uint effectIndex) where T : unmanaged
    {
        var effectParameter = default(T);
        byte* pEffectParameter = stackalloc byte[sizeof(T)];
        GetEffectParameters(effectIndex, (IntPtr)pEffectParameter, (uint)sizeof(T));
        Unsafe.Copy(ref effectParameter, pEffectParameter);
        return effectParameter;
    }

    /// <summary>	
    /// Returns the current effect-specific parameters of a given effect in the voice's effect chain.	
    /// </summary>	
    /// <param name="effectIndex">Zero-based index of an effect within the voice's effect chain.</param>
    /// <param name="effectParameters">Returns the current values of the effect-specific parameters.</param>
    /// <returns>No documentation.</returns>
    public unsafe Result GetEffectParameters(uint effectIndex, Span<byte> effectParameters)
    {
        fixed (void* pEffectParameter = effectParameters)
        {
            return GetEffectParameters(effectIndex, (IntPtr)pEffectParameter, (uint)effectParameters.Length);
        }
    }

    /// <summary>	
    /// Sets parameters for a given effect in the voice's effect chain.
    /// </summary>	
    /// <param name="effectIndex">Zero-based index of an effect within the voice's effect chain.</param>
    /// <param name="effectParameter">The the current values of the effect-specific parameters.</param>
    /// <param name="operationSet">Identifies this call as part of a deferred batch.</param>
    public unsafe Result SetEffectParameters(uint effectIndex, Span<byte> effectParameter, uint operationSet = 0)
    {
        fixed (void* pEffectParameter = effectParameter)
        {
            return SetEffectParameters(effectIndex, pEffectParameter, (uint)effectParameter.Length, operationSet);
        }
    }

    /// <summary>	
    /// Sets parameters for a given effect in the voice's effect chain.
    /// </summary>	
    /// <param name="effectIndex">Zero-based index of an effect within the voice's effect chain.</param>
    /// <param name="effectParameter">The current values of the effect-specific parameters.</param>
    /// <param name="operationSet">Identifies this call as part of a deferred batch.</param>
    public Result SetEffectParameters<T>(uint effectIndex, T effectParameter, uint operationSet = 0) where T : unmanaged
    {
        return SetEffectParameters(effectIndex, &effectParameter, (uint)sizeof(T), operationSet);
    }

    public Result SetEffectParameters(uint effectIndex, ReverbParameters reverbParameters, uint operationSet = 0)
    {
        return SetEffectParameters(effectIndex, &reverbParameters, (uint)sizeof(ReverbParameters), operationSet);
    }

    public Result SetEffectParameters(uint effectIndex, ReverbI3DL2Parameters reverbParameters, bool sevenDotOneReverb = true, uint operationSet = 0)
    {
        ReverbParameters reverbParametersNative = Fx.Fx.ReverbConvertI3DL2ToNative(reverbParameters, sevenDotOneReverb);
        return SetEffectParameters(effectIndex, &reverbParametersNative, (uint)sizeof(ReverbParameters), operationSet);
    }

    public Result SetEffectParameters(uint effectIndex, VolumeMeterLevels meterLevels, uint operationSet = 0)
    {
        VolumeMeterLevels.__Native native = default;
        meterLevels.__MarshalTo(ref native);
        return SetEffectParameters(effectIndex, &native, (uint)sizeof(VolumeMeterLevels.__Native), operationSet);
    }
}
