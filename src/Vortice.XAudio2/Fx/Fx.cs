// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XAudio2.Fx;

public static unsafe partial class Fx
{
    public static ReverbParameters ReverbConvertI3DL2ToNative(ReverbI3DL2Parameters I3DL2Parameters, bool sevenDotOneReverb = true)
    {
        ReverbParameters result = new();

        float reflectionsDelay;
        float reverbDelay;

        // RoomRolloffFactor is ignored

        // These parameters have no equivalent in I3DL2
        if (sevenDotOneReverb)
        {
            result.RearDelay = ReverbDefault7Point1RearDelay; // 20
        }
        else
        {
            result.RearDelay = ReverbDefaultRearDelay; // 5
        }

        result.SideDelay = ReverbDefault7Point1SideDelay; // 5
        result.PositionLeft = ReverbDefaultPosition; // 6
        result.PositionRight = ReverbDefaultPosition; // 6
        result.PositionMatrixLeft = ReverbDefaultPositionMatrix; // 27
        result.PositionMatrixRight = ReverbDefaultPositionMatrix; // 27
        result.RoomSize = ReverbDefaultRoomSize; // 100
        result.LowEQCutoff = 4;
        result.HighEQCutoff = 6;

        // The rest of the I3DL2 parameters map to the native property set
        result.RoomFilterMain = I3DL2Parameters.Room / 100.0f;
        result.RoomFilterHF = I3DL2Parameters.RoomHF / 100.0f;

        if (I3DL2Parameters.DecayHFRatio >= 1.0f)
        {
            int index = (int)(-4.0 * Math.Log10(I3DL2Parameters.DecayHFRatio));
            if (index < -8) index = -8;
            result.LowEQGain = (byte)((index < 0) ? index + 8 : 8);
            result.HighEQGain = 8;
            result.DecayTime = I3DL2Parameters.DecayTime * I3DL2Parameters.DecayHFRatio;
        }
        else
        {
            int index = (int)(4.0 * Math.Log10(I3DL2Parameters.DecayHFRatio));
            if (index < -8) index = -8;
            result.LowEQGain = 8;
            result.HighEQGain = (byte)((index < 0) ? index + 8 : 8);
            result.DecayTime = I3DL2Parameters.DecayTime;
        }

        reflectionsDelay = I3DL2Parameters.ReflectionsDelay * 1000.0f;
        if (reflectionsDelay >= ReverbMaxReflectionsDelay) // 300
        {
            reflectionsDelay = ReverbMaxReflectionsDelay - 1;
        }
        else if (reflectionsDelay <= 1)
        {
            reflectionsDelay = 1;
        }
        result.ReflectionsDelay = (int)reflectionsDelay;

        reverbDelay = I3DL2Parameters.ReverbDelay * 1000.0f;
        if (reverbDelay >= ReverbMaxReverbDelay) // 85
        {
            reverbDelay = (float)(ReverbMaxReverbDelay - 1);
        }
        result.ReverbDelay = (byte)reverbDelay;

        result.ReflectionsGain = I3DL2Parameters.Reflections / 100.0f;
        result.ReverbGain = I3DL2Parameters.Reverb / 100.0f;
        result.EarlyDiffusion = (byte)(15.0f * I3DL2Parameters.Diffusion / 100.0f);
        result.LateDiffusion = result.EarlyDiffusion;
        result.Density = I3DL2Parameters.Density;
        result.RoomFilterFreq = I3DL2Parameters.HFReference;
        result.WetDryMix = I3DL2Parameters.WetDryMix;
        result.DisableLateField = false;

        return result;
    }

    public static Result XAudio2CreateVolumeMeter(out ComObject? volumeMeter) 
    {
        IntPtr reverbPtr = default;
        Result result = XAudio2Native.CreateAudioVolumeMeter(0u, &reverbPtr);
        if (result.Failure)
        {
            volumeMeter = default;
            return result;
        }

        volumeMeter = new(reverbPtr);
        return result;
    }

    public static ComObject XAudio2CreateVolumeMeter()
    {
        IntPtr reverbPtr = default;
        XAudio2Native.CreateAudioVolumeMeter(0u, &reverbPtr).CheckError();
        return new(reverbPtr)!;
    }

    public static Result XAudio2CreateReverb(out ComObject? reverb)
    {
        IntPtr reverbPtr = default;
        Result result = XAudio2Native.CreateAudioReverb(0u, &reverbPtr);
        if (result.Failure)
        {
            reverb = default;
            return result;
        }

        reverb = new(reverbPtr);
        return result;
    }

    public static ComObject XAudio2CreateReverb() 
    {
        IntPtr reverbPtr = default;
        XAudio2Native.CreateAudioReverb(0u, &reverbPtr).CheckError();
        return new ComObject(reverbPtr);
    }
}
