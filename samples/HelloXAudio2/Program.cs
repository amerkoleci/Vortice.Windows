// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Multimedia;
using Vortice.XAudio2;

namespace HelloXAudio2;

public static class Program
{
    static async Task Main()
    {
        // Create the XAudio2 engine.
        using IXAudio2 audio = XAudio2.XAudio2Create(ProcessorSpecifier.DefaultProcessor);

        // The engine is already in the started state when initialized.
        // In other cases, the following call will start the audio engine.
        // XAudio.StartEngine();

        // Create mastering voice.
        using IXAudio2MasteringVoice masterVoice = audio.CreateMasteringVoice();

        // Define wave format.
        WaveFormatExtensible format = new(96000, 24, 2);

        // Create source voice for buffer submission.
        using IXAudio2SourceVoice voice = audio.CreateSourceVoice(format, false);

        // Load sound effect into audio buffer.
        string filePath = "Assets\\SpaceVehicleFlyby.wav";
        using Stream stream = new MemoryStream(File.ReadAllBytes(filePath));
        SoundStream soundStream = new(stream);
        using AudioBuffer effectBuffer = new(soundStream.ToDataStream());

        // Play buffer.
        voice.SubmitSourceBuffer(effectBuffer, null);
        voice.Start(0);

        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Playing sound effect 'SpaceVehicleFlyby.wav'");

        // XAudio2 is a non-blocking API, so wait for the buffer to finish
        // playing before allowing the program to complete.
        bool isPlaying = true;
        while (isPlaying)
        {
            isPlaying = voice.State.BuffersQueued != 0;
            Console.Write('.');
            await Task.Delay(100);
        }
    }
}
