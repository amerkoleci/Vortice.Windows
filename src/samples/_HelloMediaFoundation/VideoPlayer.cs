// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice;
using Vortice.DXGI;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using D3D11 = Vortice.Direct3D11;
using Vortice.Direct3D11;
using Vortice.Mathematics;
using Vortice.MediaFoundation;
using System.Drawing;
using SharpGen.Runtime;
using System.Windows.Forms;

namespace HelloMediaFoundation
{
    // NOTE: this follows the tutorial laid down here -> https://docs.microsoft.com/en-us/windows/win32/medfound/how-to-play-unprotected-media-files
    // reference also the MS sample -> https://docs.microsoft.com/en-us/windows/win32/medfound/player-cpp

    class VideoPlayer : IMFAsyncCallback
    {
        protected IMFTopology _playBackTopology;
        protected ID3D11Device1 _parentDevice;
        protected object _lockObject;

        private  VideoPlayer() : base(IntPtr.Zero)
        {

        }

        public VideoPlayer(Window parentForm) : base(parentForm.Handle)
        {
            _lockObject = new object();

            
        }

        public bool Initialise(ID3D11Device1 device, string Url, Window parentForm)
        {
            lock (_lockObject)
            {
                try
                {
                    

                    _parentDevice = device;

                    lock (_lockObject)
                    {
                        MediaFactory.MFStartup();

                        IMFAttributes attributes_ = MediaFactory.MFCreateAttributes();

                        if (MediaFactory.MFCreateMediaSession(attributes_, out IMFMediaSession mediaSession_).Success)
                        {
                            ID3D11Multithread multithread = device.QueryInterface<ID3D11Multithread>();
                            multithread.SetMultithreadProtected(true);

                            if (!File.Exists(Url))
                            {
                                return false;
                            }

                            if (MediaFactory.MFCreateSourceResolver(out IMFSourceResolver resolver_).Success)
                            {
                                IMFMediaSource source_ = resolver_.CreateObjectFromURL(Url);

                                IMFPresentationDescriptor presentationDescriptor_ = source_.CreatePresentationDescriptor();

                                MediaFactory.MFCreateTopology(out _playBackTopology);

                                int prentationCount_ = presentationDescriptor_.StreamDescriptorCount;

                                HandleRef ref_ = new HandleRef(parentForm, parentForm.Handle);

                                for (int i = 0; i < prentationCount_; i++)
                                {                            
                                    AddBranchToPartialTopology(_playBackTopology, source_, presentationDescriptor_, i, ref_);
                                }
                            }
                        }

                        mediaSession_.BeginGetEvent(this, null);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }

        //================================================================================================================================================================================================================

        void AddBranchToPartialTopology(
                                        IMFTopology pTopology,         // Topology.
                                        IMFMediaSource pSource,        // Media source.
                                        IMFPresentationDescriptor pPD, // Presentation descriptor.
                                        int streamIndex,                  // Stream index.
                                        HandleRef hVideoWnd)                 // Window for video playback.
        {
            IMFStreamDescriptor? pSD = null;
            IMFActivate? pSinkActivate = null;
            IMFTopologyNode? pSourceNode = null;
            IMFTopologyNode? pOutputNode = null;

  

            try
            {
                RawBool fSelected = false;

                pPD.GetStreamDescriptorByIndex(streamIndex, out fSelected, out pSD);


                if (fSelected)
                {
                    // Create the media sink activation object.
                    CreateMediaSinkActivate(pSD, hVideoWnd, out pSinkActivate);

                    // Add a source node for this stream.

                    AddSourceNode(pTopology, pSource, pPD, pSD, out pSourceNode);


                    // Create the output node for the renderer.
                    AddOutputNode(pTopology, pSinkActivate, streamIndex, out pOutputNode);

                    // Connect the source node to the output node.
                    pSourceNode.ConnectOutput(0, pOutputNode, 0);
                }
                // else: If not selected, don't add the branch. 

                pSD?.Dispose();
                pOutputNode?.Dispose();
                pSourceNode?.Dispose();
                pSinkActivate?.Dispose();
            }
            catch
            {
                return;
            }
        }

        //================================================================================================================================================================================================================

        void CreateMediaSinkActivate(IMFStreamDescriptor pSourceSD,     // Pointer to the stream descriptor.
                                    HandleRef hVideoWindow,                  // Handle to the video clipping window.
                                    out IMFActivate? ppActivate)
        {
            IMFMediaTypeHandler? pHandler = null;

            ppActivate = null;

            pHandler = pSourceSD.MediaTypeHandler;

            Guid guidMajorType = pHandler.MajorType;

            if (guidMajorType == MediaTypeGuids.Audio)
            {
                MediaFactory.MFCreateAudioRendererActivate(out ppActivate);
                // Create the audio renderer.
                //hr = MFCreateAudioRendererActivate(&pActivate);
            }
            else if (guidMajorType == MediaTypeGuids.Video)
            {
                MediaFactory.MFCreateAudioRendererActivate(out ppActivate);
            }
            else
            {
                return;
            }

            pHandler.Dispose();
        }

        //================================================================================================================================================================================================================
 
        void AddSourceNode(
                            IMFTopology pTopology,           // Topology.
                            IMFMediaSource pSource,          // Media source.
                            IMFPresentationDescriptor pPD,   // Presentation descriptor.
                            IMFStreamDescriptor pSD,         // Stream descriptor.
                            out IMFTopologyNode ppNode)         // Receives the node pointer.
        {
            // Create the node.
            ppNode = MediaFactory.MFCreateTopologyNode(TopologyType.SourcestreamNode);

            ppNode.SetUnknown(TopologyNodeAttributeKeys.Source, pSource);
            ppNode.SetUnknown(TopologyNodeAttributeKeys.PresentationDescriptor, pPD);
            ppNode.SetUnknown(TopologyNodeAttributeKeys.StreamDescriptor, pSD);

            // Add the node to the topology.
            pTopology.AddNode(ppNode);
        }

        //================================================================================================================================================================================================================

        bool AddOutputNode(
                            IMFTopology pTopology,     // Topology.
                            IMFActivate? pStreamSink, // Stream sink.
                            int streamID,                  // Stream index.
                            out IMFTopologyNode ppNode    // Receives the node pointer.
                            )
        {
            ppNode = MediaFactory.MFCreateTopologyNode(TopologyType.OutputNode);

            ppNode.SetObject(pStreamSink);

            ppNode.SetUINT32(TopologyNodeAttributeKeys.Streamid, streamID);

            pTopology.AddNode(ppNode);

            ppNode.SetUINT32(TopologyNodeAttributeKeys.NoshutdownOnRemove, Convert.ToInt32(false));

            return true;
        }

    }
}
