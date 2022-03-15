/************************************************************************
*                                                                       *
*   dstorage.h -- This module defines the DirectStorage for Windows API *
*                                                                       *
*   Copyright (c) Microsoft Corp. All rights reserved.                  *
*                                                                       *
************************************************************************/

#if !defined(__cplusplus)
    #error C++11 required
#endif

#pragma once

#include <unknwn.h>
#include <d3d12.h>
#include <dstorageerr.h>

#define DSTORAGE_SDK_VERSION 1

interface ID3D12Resource;
interface ID3D12Fence;
interface IDStorageStatusArray;

/// <summary>
/// The priority of a DStorage queue.
/// </summary>
enum DSTORAGE_PRIORITY : INT8 {
    DSTORAGE_PRIORITY_LOW      = -1,
    DSTORAGE_PRIORITY_NORMAL   = 0,
    DSTORAGE_PRIORITY_HIGH     = 1,
    DSTORAGE_PRIORITY_REALTIME = 2,

    /// <summary>
    /// The following values can be used for iterating over all priority levels.
    /// </summary>
    DSTORAGE_PRIORITY_FIRST    = DSTORAGE_PRIORITY_LOW,
    DSTORAGE_PRIORITY_LAST     = DSTORAGE_PRIORITY_REALTIME,

    DSTORAGE_PRIORITY_COUNT    = 4
};

#define DSTORAGE_MIN_QUEUE_CAPACITY             0x80
#define DSTORAGE_MAX_QUEUE_CAPACITY             0x2000

/// <summary>
/// The source type of a DStorage request.
/// </summary>
enum DSTORAGE_REQUEST_SOURCE_TYPE : UINT64 {
    /// <summary>
    /// The source of the DStorage request is a file.
    /// </summary>
    DSTORAGE_REQUEST_SOURCE_FILE = 0,

    /// <summary>
    /// The source of the DStorage request is a block of memory.
    /// </summary>
    DSTORAGE_REQUEST_SOURCE_MEMORY = 1,
};

/// <summary>
/// The destination type of a DStorage request.
/// </summary>
enum DSTORAGE_REQUEST_DESTINATION_TYPE : UINT64 {
    /// <summary>
    /// The destination of the DStorage request is a block of memory.
    /// </summary>
    DSTORAGE_REQUEST_DESTINATION_MEMORY = 0,

    /// <summary>
    /// The destination of the DStorage request is an ID3D12Resource
    /// that is a buffer.
    /// </summary>
    DSTORAGE_REQUEST_DESTINATION_BUFFER = 1,

    /// <summary>
    /// The destination of the DStorage request is an ID3D12Resource
    /// that is a texture.
    /// </summary>
    DSTORAGE_REQUEST_DESTINATION_TEXTURE_REGION = 2,

    /// <summary>
    /// The destination of the DStorage request is an ID3D12Resource
    /// that is a texture which will recieve all subresources in a
    /// single request.
    /// </summary>
    DSTORAGE_REQUEST_DESTINATION_MULTIPLE_SUBRESOURCES = 3,

    /// <summary>
    /// The destination of the DStorage request is an ID3D12Resource
    /// that is tiled.
    /// </summary>
    DSTORAGE_REQUEST_DESTINATION_TILES = 4
};

/// <summary>
/// The DSTORAGE_QUEUE_DESC structure contains the properties of a DStorage
/// queue for the queue's creation.
/// </summary>
struct DSTORAGE_QUEUE_DESC {
    /// <summary>
    /// The source type of requests this DStorage queue can accept.
    /// </summary>
    DSTORAGE_REQUEST_SOURCE_TYPE SourceType;

    /// <summary>
    /// The maximum number of requests the queue can hold.
    /// </summary>
    UINT16 Capacity;

    /// <summary>
    /// The priority of the requests in this queue.
    /// </summary>
    DSTORAGE_PRIORITY Priority;

    /// <summary>
    /// Optional name of the queue. Used for debugging.
    /// </summary>
    _In_opt_z_ const CHAR *Name;

    /// <summary>
    /// Optional device to use for writing to destination resources and
    /// performing GPU decompression. The destination resource's device
    /// must match this device.
    ///
    /// This member can be null. Only the DSTORAGE_REQUEST_DESTINATION_MEMORY
    /// destination type can be used if a null device is specified.
    /// </summary>
    ID3D12Device* Device;
};

/// <summary>
/// The DSTORAGE_QUEUE_INFO structure contains the properties and current state
/// of a DStorage queue.
/// </summary>
struct DSTORAGE_QUEUE_INFO {
    /// <summary>
    /// The DSTORAGE_QUEUE_DESC structure used for queue's creation.
    /// </summary>
    DSTORAGE_QUEUE_DESC Desc;

    /// <summary>
    /// The number of available empty slots. If a queue is empty, the number
    /// of empty slots equals capacity - 1. The reserved slot is used to
    /// distinguish between empty and full cases.
    /// </summary>
    UINT16 EmptySlotCount;

    /// <summary>
    /// The number of entries that would need to be enqueued to trigger
    /// automatic submission.
    /// </summary>
    UINT16 RequestCountUntilAutoSubmit;
};

/// <summary>
/// The type of compression format used at the decompression stage.
/// The application can register custom decompressors, starting from
/// DSTORAGE_CUSTOM_COMPRESSION_0.
/// </summary>
enum DSTORAGE_COMPRESSION_FORMAT : UINT8 {
    DSTORAGE_COMPRESSION_FORMAT_NONE    = 0,
    DSTORAGE_COMPRESSION_FORMAT_1       = 1,
    DSTORAGE_CUSTOM_COMPRESSION_0       = 0x80,
};

/// <summary>
/// Options for a DStorage request.
/// </summary>
struct DSTORAGE_REQUEST_OPTIONS {
    /// <summary>
    /// DSTORAGE_COMPRESSION_FORMAT indicating how the data is compressed.
    /// </summary>
    DSTORAGE_COMPRESSION_FORMAT CompressionFormat : 8;

    /// <summary>
    /// DSTORAGE_REQUEST_SOURCE_TYPE enum value indicating whether the
    /// source of the request is a file or a block of memory.
    /// </summary>
    DSTORAGE_REQUEST_SOURCE_TYPE SourceType : 1;

    /// <summary>
    /// DSTORAGE_REQUEST_DESTINATION_TYPE enum value indicating the
    /// destination of the request. Block of memory, resource.
    /// </summary>
    DSTORAGE_REQUEST_DESTINATION_TYPE DestinationType : 7;

    /// <summary>
    /// Reserved fields. Must be 0.
    /// </summary>
    UINT64 Reserved : 48;
};

/// <summary>
/// Flags controlling DirectStorage debug layer.
/// </summary>
enum DSTORAGE_DEBUG {
    /// <summary>
    /// DirectStorage debug layer is disabled.
    /// </summary>
    DSTORAGE_DEBUG_NONE                 = 0x00,

    /// <summary>
    /// Print error information to a debugger.
    /// </summary>
    DSTORAGE_DEBUG_SHOW_ERRORS          = 0x01,

    /// <summary>
    /// Trigger a debug break each time an error is detected.
    /// </summary>
    DSTORAGE_DEBUG_BREAK_ON_ERROR       = 0x02,

    /// <summary>
    /// Include IDStorageStatusArray and ID3D12Fence names in ETW events.
    /// </summary>
    DSTORAGE_DEBUG_RECORD_OBJECT_NAMES  = 0x04
};
DEFINE_ENUM_FLAG_OPERATORS(DSTORAGE_DEBUG)

/// <summary>
/// Represents a file to be accessed by DStorage.
/// </summary>
DECLARE_INTERFACE_IID_(IDStorageFile, IUnknown, "5de95e7b-955a-4868-a73c-243b29f4b8da")
{
    /// <summary>
    /// Closes the file, regardless of the reference count on this object.
    ///
    /// After an IDStorageFile object is closed, it can no longer be used in
    /// DStorage requests.
    /// </summary>
    virtual void STDMETHODCALLTYPE Close() = 0;

    /// <summary>
    /// Retrieves file information for an opened file.
    ///
    /// </summary>
    virtual HRESULT STDMETHODCALLTYPE GetFileInformation(BY_HANDLE_FILE_INFORMATION* info) = 0;
};

/// <summary>
/// Describes a source for a request with SourceType
/// DSTORAGE_REQUEST_SOURCE_FILE.
/// </summary>
struct DSTORAGE_SOURCE_FILE {
    /// <summary>
    /// The file to perform this read request from.
    /// </summary>
    IDStorageFile* Source;

    /// <summary>
    /// The offset, in bytes, in the file to start the read request at.
    /// </summary>
    UINT64 Offset;

    /// <summary>
    /// Number of bytes to read from the file.
    /// </summary>
    UINT32 Size;
};

/// <summary>
/// Describes a source for a request with SourceType
/// DSTORAGE_REQUEST_SOURCE_MEMORY.
/// </summary>
struct DSTORAGE_SOURCE_MEMORY {
    /// <summary>
    /// Address of the source buffer to be read from.
    /// </summary>
    void const* Source;

    /// <summary>
    /// Size, in bytes, of the buffer to read from the source.
    /// </summary>
    UINT32 Size;
};

/// <summary>
/// Describes the destination for a request with DestinationType
/// DSTORAGE_REQUEST_DESTINATION_MEMORY.
/// </summary>
struct DSTORAGE_DESTINATION_MEMORY {
    /// <summary>
    /// Address of the buffer to receive the final result of this request.
    /// </summary>
    void* Buffer;

    /// <summary>
    /// Size, in bytes, of the buffer to receive the final result of a
    /// request.
    /// </summary>
    UINT32 Size;
};

/// <summary>
/// Describes the destination for a request with DestinationType
/// DSTORAGE_REQUEST_DESTINATION_BUFFER.
/// </summary>
struct DSTORAGE_DESTINATION_BUFFER {
    /// <summary>
    /// Address of the resource to receive the final result of this request.
    /// </summary>
    ID3D12Resource* Resource;

    /// <summary>
    /// The offset, in bytes, in the buffer resource to write into.
    /// </summary>
    UINT64 Offset;

    /// <summary>
    /// Size, in bytes, of the buffer to receive the final result of a
    /// request.
    /// </summary>
    UINT32 Size;
};

/// <summary>
/// Describes the destination for a request with DestinationType
/// DSTORAGE_REQUEST_DESTINATION_TEXTURE_REGION.
/// </summary>
struct DSTORAGE_DESTINATION_TEXTURE_REGION {
    /// <summary>
    /// Address of the resource to receive the final result of this request.
    /// </summary>
    ID3D12Resource* Resource;

    /// <summary>
    /// Describes the destination texture copy location. The subresource
    /// referred to must be in the D3D12_RESOURCE_STATE_COMMON state.
    /// </summary>
    UINT SubresourceIndex;

    /// <summary>
    /// Coordinates and size of the destination region to copy in pixels.
    /// </summary>
    D3D12_BOX Region;
};

/// <summary>
/// Describes the destination for a request with DestinationType
/// DSTORAGE_REQUEST_DESTINATION_MULTIPLE_SUBRESOURCES.
/// </summary>
struct DSTORAGE_DESTINATION_MULTIPLE_SUBRESOURCES {
    /// <summary>
    /// Address of the resource to receive the final result of this request. The
    /// source is expected to contain full data for all subresources, starting
    /// from FirstSubresource.
    /// </summary>
    ID3D12Resource* Resource;

    /// <summary>
    /// Describes the first subresource of the destination texture copy
    /// location. The subresource referred to must be in the
    /// D3D12_RESOURCE_STATE_COMMON state.
    /// </summary>
    UINT FirstSubresource;
};

/// <summary>
/// Describes the destination for a request with DestinationType
/// DSTORAGE_REQUEST_DESTINATION_TILES.
/// </summary>
struct DSTORAGE_DESTINATION_TILES {
    /// <summary>
    /// Address of the resource to receive the final result of this request. The
    /// source buffer is expected to contain data arranged as if it were the
    /// source to a CopyTiles call with these parameters.
    /// </summary>
    ID3D12Resource* Resource;

    /// <summary>
    /// The starting coordinates of the tiled region.
    /// </summary>
    D3D12_TILED_RESOURCE_COORDINATE TiledRegionStartCoordinate;

    /// <summary>
    /// The size of the tiled region.
    /// </summary>
    D3D12_TILE_REGION_SIZE TileRegionSize;
};

/// <summary>
/// Describes the source specified for a DStorage request.
/// </summary>
union DSTORAGE_SOURCE {
    DSTORAGE_SOURCE_MEMORY Memory;
    DSTORAGE_SOURCE_FILE File;
};

/// <summary>
/// Describes the destination for a DStorage request.
/// </summary>
union DSTORAGE_DESTINATION {
    DSTORAGE_DESTINATION_MEMORY Memory;
    DSTORAGE_DESTINATION_BUFFER Buffer;
    DSTORAGE_DESTINATION_TEXTURE_REGION Texture;
    DSTORAGE_DESTINATION_MULTIPLE_SUBRESOURCES MultipleSubresources;
    DSTORAGE_DESTINATION_TILES Tiles;
};

/// <summary>
/// Represents a DStorage request.
/// </summary>
struct DSTORAGE_REQUEST {
    /// <summary>
    /// Combination of decompression and other options for this request.
    /// </summary>
    DSTORAGE_REQUEST_OPTIONS Options;

    /// <summary>
    /// The source for this request.
    /// </summary>
    DSTORAGE_SOURCE Source;

    /// <summary>
    /// The destination for this request.
    /// </summary>
    DSTORAGE_DESTINATION Destination;

    /// <summary>
    /// The uncompressed size in bytes for the destination
    /// for this request.
    /// </summary>
    UINT32 UncompressedSize;

    /// <summary>
    /// An arbitrary UINT64 number used for cancellation matching.
    /// </summary>
    UINT64 CancellationTag;

    /// <summary>
    /// Optional name of the request. Used for debugging. If specified, the
    /// string should be accessible until the request completes.
    /// </summary>
    _In_opt_z_ const CHAR *Name;
};

#define DSTORAGE_REQUEST_MAX_NAME       64

enum DSTORAGE_COMMAND_TYPE {
    DSTORAGE_COMMAND_TYPE_NONE = -1,
    DSTORAGE_COMMAND_TYPE_REQUEST = 0,
    DSTORAGE_COMMAND_TYPE_STATUS = 1,
    DSTORAGE_COMMAND_TYPE_SIGNAL = 2,
};

/// <summary>
/// The parameters passed to the EnqueueRequest call and optional
/// filename if the request is for a file source.
/// </summary>
struct DSTORAGE_ERROR_PARAMETERS_REQUEST {
    /// <summary>
    /// For a file source request, the name of the file the request was
    /// targeted to.
    /// </summary>
    WCHAR Filename[MAX_PATH];

    /// <summary>
    /// The name of the request if one was specified.
    /// </summary>
    CHAR RequestName[MAX_PATH];

    /// <summary>
    /// The parameters passed to the EnqueueRequest call.
    /// </summary>
    DSTORAGE_REQUEST Request;
};

/// <summary>
/// The parameters passed to the EnqueueStatus call.
/// </summary>
struct DSTORAGE_ERROR_PARAMETERS_STATUS {
    IDStorageStatusArray* StatusArray;
    UINT32 Index;
};

/// <summary>
/// The parameters passed to the EnqueueSignal call.
/// </summary>
struct DSTORAGE_ERROR_PARAMETERS_SIGNAL {
    ID3D12Fence* Fence;
    UINT64 Value;
};

/// <summary>
/// Structure to receive the detailed record of the first failed DStorage request.
/// </summary>
struct DSTORAGE_ERROR_FIRST_FAILURE {

    /// <summary>
    /// The HRESULT code of the failure.
    /// </summary>
    HRESULT HResult;

    /// <summary>
    /// Type of the Enqueue command that caused the failure.
    /// </summary>
    DSTORAGE_COMMAND_TYPE CommandType;

    /// <summary>
    /// The parameters passed to the Enqueue call.
    /// </summary>
    union
    {
        DSTORAGE_ERROR_PARAMETERS_REQUEST Request;
        DSTORAGE_ERROR_PARAMETERS_STATUS Status;
        DSTORAGE_ERROR_PARAMETERS_SIGNAL Signal;
    };
};

/// <summary>
/// Structure to receive the detailed record of a failed DStorage request.
/// </summary>
struct DSTORAGE_ERROR_RECORD {
    /// <summary>
    /// The number of failed requests in the queue since the last
    /// RetrieveErrorRecord call.
    /// </summary>
    UINT32 FailureCount;

    /// <summary>
    /// Detailed record about the first failed command in the enqueue order.
    /// </summary>
    DSTORAGE_ERROR_FIRST_FAILURE FirstFailure;
};


/// <summary>
/// Defines the valid staging buffer sizes.
/// </summary>
enum DSTORAGE_STAGING_BUFFER_SIZE : UINT32 {
    DSTORAGE_STAGING_BUFFER_SIZE_0 = 0,
    DSTORAGE_STAGING_BUFFER_SIZE_32MB = 32 * 1048576,
};

/// <summary>
/// A custom decompression request.  Use IDStorageCustomDecompressionQueue to
/// retrieve these requests.
/// </summary>
struct DSTORAGE_CUSTOM_DECOMPRESSION_REQUEST {
    /// <summary>
    /// An identifier provided by DirectStorage.  This should be used to
    /// identify the request in DSTORAGE_CUSTOM_DECOMPRESSION_RESULT.  This
    /// identifier is unique among uncompleted requests, but may be reused after
    /// a request has completed.
    /// </summary>
    UINT64 Id;

    /// <summary>
    /// The compression format.  This will be >= DSTORAGE_CUSTOM_COMPRESSION_0.
    /// </summary>
    DSTORAGE_COMPRESSION_FORMAT CompressionFormat;

    /// <summary>
    /// The size of SrcBuffer in bytes.
    /// </summary>
    UINT64 SrcSize;

    /// <summary>
    /// The compressed source buffer.
    /// </summary>
    void const* SrcBuffer;

    /// <summary>
    /// The size of DstBuffer in bytes.
    /// </summary>
    UINT64 DstSize;

    /// <summary>
    /// The uncompressed destination buffer. SrcBuffer should be decompressed to
    /// DstBuffer.
    /// </summary>
    void* DstBuffer;
};

/// <summary>
/// The result of a custom decompression operation.  If the request failed then
/// the Result code is passed back through the standard DirectStorage
/// status/error reporting mechanism.
/// </summary>
struct DSTORAGE_CUSTOM_DECOMPRESSION_RESULT {
    /// <summary>
    /// The identifier for the request, from DSTORAGE_CUSTOM_DECOMPRESSION_REQUEST.
    /// </summary>
    UINT64 Id;

    /// <summary>
    /// The result of this decompression.  S_OK indicates success.
    /// </summary>
    HRESULT Result;
};

/// <summary>
/// A queue of decompression requests. This can be obtained using QueryInterface
/// against the factory.  The application must take requests from this queue,
/// decompress them, and report that decompression is complete. This allows an
/// application to provide its own custom decompression.
/// </summary>
DECLARE_INTERFACE_IID_(IDStorageCustomDecompressionQueue, IUnknown, "97179b2f-2c21-49ca-8291-4e1bf4a160df")
{
    /// <summary>
    /// Obtains an event to wait on. This event is set when there are pending
    /// decompression requests.
    /// </summary>
    virtual HANDLE STDMETHODCALLTYPE GetEvent() = 0;

    /// <summary>
    /// Populates the given array of request structs with new pending requests.
    /// The application must arrange to fulfill all these requests, and then
    /// call SetRequestResults to indicate completion.
    /// <summary>
    virtual HRESULT STDMETHODCALLTYPE GetRequests(
        _In_ UINT32 maxRequests,
        _Out_writes_to_(maxRequests, *numRequests) DSTORAGE_CUSTOM_DECOMPRESSION_REQUEST* requests,
        _Out_ UINT32* numRequests) = 0;

    /// <summary>
    /// The application calls this to indicate requests have been completed.
    /// </summary>
    virtual HRESULT STDMETHODCALLTYPE SetRequestResults(
        _In_ UINT32 numResults,
        _In_reads_(numResults) DSTORAGE_CUSTOM_DECOMPRESSION_RESULT * results) = 0;
};


/// <summary>
/// Represents the static DStorage object used to create DStorage queues, open
/// files for DStorage access, and other global operations.
/// </summary>
DECLARE_INTERFACE_IID_(IDStorageFactory, IUnknown, "6924ea0c-c3cd-4826-b10a-f64f4ed927c1")
{
    /// <summary>
    /// Creates DStorage queue object.
    /// </summary>
    /// <param name="desc">Descriptor to specify the properties of the queue.</param>
    /// <param name="riid">Specifies the DStorage queue interface, such as
    /// __uuidof(IDStorageQueue).</param>
    /// <param name="ppv">Receives the new queue created.</param>
    /// <returns>Standard HRESULT error code.</returns>
    virtual HRESULT STDMETHODCALLTYPE CreateQueue(const DSTORAGE_QUEUE_DESC *desc, REFIID riid, _COM_Outptr_ void **ppv) = 0;

    /// <summary>
    /// Opens a file for DStorage access. The file must be stored on a DStorage
    /// supported NVMe device.
    /// </summary>
    /// <param name="path">Path of the file to be opened.</param>
    /// <param name="riid">Specifies the DStorage file interface, such as
    /// __uuidof(IDStorageFile).</param>
    /// <param name="ppv">Receives the new file opened.</param>
    /// <returns>Standard HRESULT error code.</returns>
    virtual HRESULT STDMETHODCALLTYPE OpenFile(_In_z_ const WCHAR *path, REFIID riid, _COM_Outptr_ void **ppv) = 0;

    /// <summary>
    /// Creates DStorage status array object.
    /// </summary>
    /// <param name="capacity">Specifies the number of status the array can
    /// hold.</param>
    /// <param name="name">Specifies object's name that will appear in
    //  the ETW events if enabled through the debug layer. This is an optional
    //  parameter.</param>
    /// <param name="riid">Specifies the DStorage status interface, such as
    /// __uuidof(IDStorageStatusArray).</param>
    /// <param name="ppv">Receives the new status array object created.</param>
    /// <returns>Standard HRESULT error code.</returns>
    virtual HRESULT STDMETHODCALLTYPE CreateStatusArray(UINT32 capacity, _In_opt_ PCSTR name, REFIID riid, _COM_Outptr_ void **ppv) = 0;

    /// <summary>
    /// Sets flags used to control the debug layer.
    /// </summary>
    /// <param name="flags">A set of flags controlling the debug layer.</param>
    virtual void STDMETHODCALLTYPE SetDebugFlags(UINT32 flags) = 0;

    /// <summary>
    /// Sets the size of staging buffer used to temporarily store content loaded
    /// from the storage device before it is decompressed. If only memory
    /// sourced queues are used, the staging buffer can be 0 sized.
    /// </summary>
    /// <param name="size">Size, in bytes, of the staging buffer.</param>
    virtual HRESULT STDMETHODCALLTYPE SetStagingBufferSize(UINT32 size) = 0;
};

/// <summary>
/// Represents an array of status entries to receive completion results for the
/// read requests before them.
/// </summary>
/// <remarks>
/// A status entry receives completion status for all the requests in the
/// DStorageQueue between where it is enqueued and the previously enqueued
/// status entry. Only when all requests enqueued before the status entry
/// complete (ie. IsComplete() for the entry returns true), the status entry
/// can be enqueued again.
/// </remarks>
DECLARE_INTERFACE_IID_(IDStorageStatusArray, IUnknown, "82397587-7cd5-453b-a02e-31379bd64656")
{
    /// <summary>
    /// Returns a boolean value indicating all requests enqueued prior to the
    /// specified status entry have completed.
    /// </summary>
    /// <param name="index">Specifies the index of the status entry to retrieve.</param>
    /// <returns>Boolean value indicating completion.</returns>
    /// <remarks>This is equivalent to "GetHResult(index) != E_PENDING".</remarks>
    virtual bool STDMETHODCALLTYPE IsComplete(UINT32 index) = 0;

    /// <summary>
    /// Returns the HRESULT code of all requests between the specified status
    /// entry and the status entry enqueued before it.
    /// </summary>
    /// <param name="index">Specifies the index of the status entry to retrieve.</param>
    /// <returns>HRESULT code of the requests.</returns>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>
    /// If any requests have not completed yet, the return value is E_PENDING.
    /// </description></item>
    /// <item><description>
    /// If all requests have completed, and there were failure(s), the return
    /// value stores the failure code of the first failed request in the enqueue
    /// order.
    /// </description></item>
    /// <item><description>
    /// If all requests have completed successfully, the return value is S_OK.
    /// </description></item>
    /// </list>
    /// </remarks>
    virtual HRESULT STDMETHODCALLTYPE GetHResult(UINT32 index) = 0;
};

/// <summary>
/// Represents a DStorage queue to perform read operations.
/// </summary>
DECLARE_INTERFACE_IID_(IDStorageQueue, IUnknown, "cfdbd83f-9e06-4fda-8ea5-69042137f49b")
{
    /// <summary>
    /// Enqueues a read request to the queue. The request remains in the queue
    /// until Submit is called, or until the queue is 1/2 full.
    /// </summary>
    /// <param name="request">The read request to be queued.</param>
    virtual void STDMETHODCALLTYPE EnqueueRequest(const DSTORAGE_REQUEST *request) = 0;

    /// <summary>
    /// Enqueues a status write. The status write happens when all requests
    /// before the status write entry complete. If there were failure(s)
    /// since the previous status write entry, the HResult of the enqueued
    /// status entry stores the failure code of the first failed request in the
    /// enqueue order.
    /// </summary>
    /// <param name="statusArray">IDStorageStatusArray object.</param>
    /// <param name="index">Index of the status entry in the
    /// IDStorageStatusArray object to receive the status.</param>
    virtual void STDMETHODCALLTYPE EnqueueStatus(IDStorageStatusArray *statusArray, UINT32 index) = 0;

    /// <summary>
    /// Enqueues fence write. The fence write happens when all requests before
    /// the fence entry complete.
    /// </summary>
    /// <param name="fence">An ID3D12Fence to be written.</param>
    /// <param name="value">The value to write to the fence.</param>
    virtual void STDMETHODCALLTYPE EnqueueSignal(ID3D12Fence *fence, UINT64 value) = 0;

    /// <summary>
    /// Submits all requests enqueued so far to DStorage to be executed.
    /// </summary>
    virtual void STDMETHODCALLTYPE Submit() = 0;

    /// <summary>
    /// Attempts to cancel a group of previously enqueued read requests. All
    /// previously enqueued requests whose CancellationTag matches the formula
    /// (CancellationTag & mask ) == value will be cancelled.
    /// A cancelled request may or may not complete its original read request.
    /// A cancelled request is not counted as a failure in either
    /// IDStorageStatusX or DSTORAGE_ERROR_RECORD.
    /// </summary>
    /// <param name="mask">The mask for the cancellation formula.</param>
    /// <param name="value">The value for the cancellation formula.</param>
    virtual void STDMETHODCALLTYPE CancelRequestsWithTag(UINT64 mask, UINT64 value) = 0;

    /// <summary>
    /// Closes the DStorage queue, regardless of the reference count on this
    /// object.
    /// After the Close function returns, the queue will no longer complete any
    /// more requests, even if some are submitted.
    /// </summary>
    virtual void STDMETHODCALLTYPE Close() = 0;

    /// <summary>
    /// Obtains an event to wait on. When there is any error happening for read
    /// requests in this queue, the event will be signalled, and
    /// RetrieveErrorRecord may be called to retrieve diagnostic information.
    /// </summary>
    /// <returns>HANDLE to an event.</returns>
    virtual HANDLE STDMETHODCALLTYPE GetErrorEvent() = 0;

    /// <summary>
    /// When the error event is signaled, this function can be called to
    /// retrieve a DSTORAGE_ERROR_RECORD. Once the error record is retrieved,
    /// this function should not be called until the next time the error event
    /// is signaled.
    /// </summary>
    /// <param name="record">Receives the error record.</param>
    virtual void STDMETHODCALLTYPE RetrieveErrorRecord(_Out_ DSTORAGE_ERROR_RECORD *record) = 0;

    /// <summary>
    /// Obtains information about the queue. It includes the DSTORAGE_QUEUE_DESC
    /// structure used for queue's creation as well as the number of empty slots
    /// and number of entries that need to be enqueued to trigger automatic
    /// submission.
    /// </summary>
    virtual void STDMETHODCALLTYPE Query(_Out_ DSTORAGE_QUEUE_INFO *info) = 0;
};

/// <summary>
/// DirectStorage Configuration. Zero initializing this will result in the default values.
/// </summary>
struct DSTORAGE_CONFIGURATION {
    /// <summary>
    /// Sets the number of threads to use for submitting IO operations.
    /// Specifying 0 means to use the system's best guess at a good value.
    /// Default=0.
    /// </summary>
    UINT32 NumSubmitThreads;

    /// <summary>
    /// Forces the use of the IO mapping layer, even when running on an
    /// operation system that doesn't require it.  This may be useful during
    /// development, but should be set to the FALSE for release. Default=FALSE.
    /// </summary>
    BOOL ForceMappingLayer;

    /// <summary>
    /// Disables the use of the bypass IO optimization, even if it is available.
    /// This may be useful during development, but should be set to the FALSE
    /// for release. Default=FALSE.
    /// </summary>
    BOOL DisableBypassIO;

    /// <summary>
    /// Disables the reporting of telemetry data when set to TRUE.
    /// Telemetry data is enabled by default in the DirectStorage runtime.
    /// Default=FALSE.
    /// </summary>
    BOOL DisableTelemetry;
};

extern "C" {

/// <summary>
/// Configures DirectStorage. This must be called before the first call to
/// DStorageGetFactory.  If this is not called then default values are used.
/// </summary>
HRESULT DStorageSetConfiguration(DSTORAGE_CONFIGURATION const* configuration);

/// <summary>
/// Returns the static DStorage factory object used to create DStorage queues,
/// open files for DStorage access, and other global operations.
/// </summary>
/// <param name="riid">Specifies the DStorage factory interface, such as
/// __uuidof(IDStorageFactory)</param>
/// <param name="ppv">Receives the DStorage factory object.</param>
/// <returns>Standard HRESULT error code.</returns>
HRESULT DStorageGetFactory(REFIID riid, void **ppv);

}
