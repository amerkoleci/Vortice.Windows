// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.MediaFoundation;
using static Vortice.MediaFoundation.MediaFactory;

namespace AudioPlay;

public static unsafe class Program
{
    private static IMFMediaEngineEx mediaEngineEx;
    private static readonly ManualResetEvent eventReadyToPlay = new (false);
    private static bool isMusicStopped;

    private static void OnPlaybackCallback(MediaEngineEvent playEvent, nuint param1, int param2)
    {
        Console.Write("PlayBack Event received: {0}", playEvent);
        switch (playEvent)
        {
            case MediaEngineEvent.CanPlay:
                eventReadyToPlay.Set();
                break;
            case MediaEngineEvent.TimeUpdate:
                Console.Write(" {0}", TimeSpan.FromSeconds(mediaEngineEx.CurrentTime));
                break;
            case MediaEngineEvent.Error:
            case MediaEngineEvent.Abort:
            case MediaEngineEvent.Ended:
                isMusicStopped = true;
                break;
        }

        Console.WriteLine();
    }

    static void Main()
    {
        // Initialize MediaFoundation
        if (MFStartup(true).Failure)
        {
            return;
        }

        // Creates the MediaEngineClassFactory
        using IMFMediaEngineClassFactory mediaEngineFactory = new();

        using IMFAttributes attributes = MFCreateAttributes(1);
        attributes.AudioCategory = Vortice.Multimedia.AudioStreamCategory.GameMedia;

        // Creates MediaEngine for AudioOnly 
        using IMFMediaEngine mediaEngine = mediaEngineFactory.CreateInstance(
            MediaEngineCreateFlags.AudioOnly, attributes, OnPlaybackCallback);

        // Query for MediaEngineEx interface
         mediaEngineEx = mediaEngine.QueryInterface<IMFMediaEngineEx>();

        string fileName = Path.Combine(AppContext.BaseDirectory, "ergon.wav");
        using (MFByteStream mfStream = new(fileName))
        {
            // Set the source stream
            mediaEngineEx.SetSourceFromByteStream(mfStream, fileName);

            // Wait for MediaEngine to be ready
            if (!eventReadyToPlay.WaitOne(1000))
            {
                Console.WriteLine("Unexpected error: Unable to play this file");
            }

            // Play the music
            mediaEngineEx.Play();

            // Wait until music is stopped.
            while (!isMusicStopped)
            {
                Thread.Sleep(10);
            }
        }

        MFShutdown().CheckError();
    }
}
