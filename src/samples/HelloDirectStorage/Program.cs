// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using Vortice.Direct3D;
using Vortice.Direct3D12;
using Vortice.DirectStorage;
using Vortice.Win32;
using static Vortice.Direct3D12.D3D12;
using static Vortice.DirectStorage.DirectStorage;

namespace HelloDirectStorage;

public static class Program
{
    public static void Main()
    {
        using ID3D12Device device = D3D12CreateDevice<ID3D12Device>(null, FeatureLevel.Level_12_1);

        using IDStorageFactory factory = DStorageGetFactory<IDStorageFactory>();

        string fileToLoad = "Test.txt";
        Result result = factory.OpenFile(fileToLoad, out IDStorageFile? file);
        if (result.Failure)
        {
            Console.WriteLine($"The file '{fileToLoad}' could not be opened. HRESULT={result}");
            //ShowHelpText();
            return;
        }

        ByHandleFileInformation info = file!.FileInformation;
        uint fileSize = info.FileSizeLow;

        // Create a DirectStorage queue which will be used to load data into a buffer on the GPU.
        QueueDesc queueDesc = new QueueDesc
        {
            Capacity = MaxQueueCapacity,
            Priority = Priority.Normal,
            SourceType = RequestSourceType.File,
            Device = device
        };

        using IDStorageQueue queue = factory.CreateQueue(queueDesc);

        // Create the ID3D12Resource buffer which will be populated with the file's contents
        HeapProperties bufferHeapProps = new HeapProperties(HeapType.Default);
        ResourceDescription bufferDesc = ResourceDescription.Buffer(fileSize);

        using ID3D12Resource bufferResource = device.CreateCommittedResource(
            bufferHeapProps,
            HeapFlags.None,
            bufferDesc,
            ResourceStates.Common
            );

        // Enqueue a request to read the file contents into a destination D3D12 buffer resource.
        // Note: The example request below is performing a single read of the entire file contents.
        Request request = new Request();
        request.Options.SourceType = RequestSourceType.File;
        request.Options.DestinationType = RequestDestinationType.Buffer;
        request.Source.File.Source = file;
        request.Source.File.Offset = 0;
        request.Source.File.Size = fileSize;
        request.UncompressedSize = fileSize;
        request.Destination.Buffer.Resource = bufferResource;
        request.Destination.Buffer.Offset = 0;
        request.Destination.Buffer.Size = request.Source.File.Size;
        queue.EnqueueRequest(request);

        // Configure a fence to be signaled when the request is completed
        using ID3D12Fence fence = device.CreateFence();
        using AutoResetEvent fenceEvent = new AutoResetEvent(false);

        ulong fenceValue = 1;
        fence.SetEventOnCompletion(fenceValue, fenceEvent).CheckError();
        queue.EnqueueSignal(fence, fenceValue);

        // Tell DirectStorage to start executing all queued items.
        queue.Submit();

        // Wait for the submitted work to complete
        Console.WriteLine("Waiting for the DirectStorage request to complete...");
        fenceEvent.WaitOne();

        // Check the status array for errors.
        // If an error was detected the first failure record
        // can be retrieved to get more details.
        ErrorRecord errorRecord = queue.RetrieveErrorRecord();
        if (errorRecord.FirstFailure.HResult.Failure)
        {
            //
            // errorRecord.FailureCount - The number of failed requests in the queue since the last
            //                            RetrieveErrorRecord call.
            // errorRecord.FirstFailure - Detailed record about the first failed command in the enqueue order.
            //
            Console.WriteLine($"The DirectStorage request failed! HRESULT={errorRecord.FirstFailure.HResult}");
        }
        else
        {
            Console.WriteLine("The DirectStorage request completed successfully!");
        }

    }
}
