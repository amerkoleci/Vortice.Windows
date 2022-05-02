// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Multimedia;

/// <summary>
/// Generic sound input stream supporting WAV (Pcm,Float), ADPCM, xWMA sound file formats.
/// </summary>
public class SoundStream : Stream
{
    private Stream _input;
    private long _startPositionOfData;
    private long _length;

    /// <summary>
    /// Initializes a new instance of the <see cref="SoundStream"/> class.
    /// </summary>
    /// <param name="stream">The sound stream.</param>
    public SoundStream(Stream stream)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        _input = stream;
        Initialize(stream);
    }

    /// <summary>
    /// Initializes the specified stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    private unsafe void Initialize(Stream stream)
    {
        var parser = new RiffParser(stream);

        FileFormatName = "Unknown";

        // Parse Header
        if (!parser.MoveNext() || parser.Current == null)
        {
            ThrowInvalidFileFormat();
            return;
        }

        // Check that WAVE or XWMA header is present
        FileFormatName = parser.Current.Type;
        if (FileFormatName != "WAVE" && FileFormatName != "XWMA")
            throw new InvalidOperationException("Unsupported " + FileFormatName + " file format. Only WAVE or XWMA");

        // Parse inside the first chunk
        parser.Descend();

        // Get all the chunk
        var chunks = parser.GetAllChunks();

        // Get "fmt" chunk
        var fmtChunk = Chunk(chunks, "fmt ");
        if (fmtChunk.Size < sizeof(WaveFormat.__PcmNative))
            ThrowInvalidFileFormat();

        try
        {
            Format = WaveFormat.MarshalFrom(fmtChunk.GetData());
        }
        catch (InvalidOperationException ex)
        {
            ThrowInvalidFileFormat(ex);
        }

        // If XWMA
        if (FileFormatName == "XWMA")
        {
            // Check that format is Wma
            if (Format!.Encoding != WaveFormatEncoding.WindowsMediaAudio &&
                Format!.Encoding != WaveFormatEncoding.WindowsMediaAudioProfessional)
            {
                ThrowInvalidFileFormat();
            }

            // Check for "dpds" chunk
            // Get the dpds decoded packed cumulative bytes
            RiffChunk? dpdsChunk = Chunk(chunks, "dpds");
            DecodedPacketsInfo = dpdsChunk!.GetDataAsArray<uint>();
        }
        else
        {
            switch (Format!.Encoding)
            {
                case WaveFormatEncoding.Pcm:
                case WaveFormatEncoding.IeeeFloat:
                case WaveFormatEncoding.Extensible:
                case WaveFormatEncoding.Adpcm:
                    break;
                default:
                    ThrowInvalidFileFormat();
                    break;
            }
        }

        // Check for "data" chunk
        RiffChunk? dataChunk = Chunk(chunks, "data");
        _startPositionOfData = dataChunk!.DataPosition;
        _length = dataChunk.Size;

        _input.Position = _startPositionOfData;
    }

    protected void ThrowInvalidFileFormat(Exception? nestedException = null)
    {
        throw new InvalidOperationException("Invalid " + FileFormatName + " file format", nestedException);
    }

    /// <summary>
    /// Gets the decoded packets info.
    /// </summary>
    /// <remarks>
    /// This property is only valid for XWMA stream.</remarks>
    public uint[]? DecodedPacketsInfo { get; private set; }

    /// <summary>
    /// Gets the wave format of this instance.
    /// </summary>
    public WaveFormat? Format { get; protected set; }

    /// <summary>
    /// Converts this stream to a DataStream by loading all the data from the source stream.
    /// </summary>
    /// <returns></returns>
    public DataStream ToDataStream()
    {
        byte[] buffer = new byte[Length];
        if (Read(buffer, 0, (int)Length) != Length)
        {
            throw new InvalidOperationException("Unable to get a valid DataStream");
        }

        return DataStream.Create(buffer, true, true);
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="SoundStream"/> to <see cref="DataStream"/>.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator DataStream(SoundStream stream) => stream.ToDataStream();

    /// <inheritdoc />
    public override bool CanRead => true;

    /// <inheritdoc />
    public override bool CanSeek => true;

    /// <inheritdoc />
    public override bool CanWrite => false;

    /// <inheritdoc />
    public override long Position
    {
        get
        {
            return _input.Position - _startPositionOfData;
        }
        set
        {
            Seek(value, SeekOrigin.Begin);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (_input != null)
        {
            _input.Dispose();
        }

        base.Dispose(disposing);
    }

    protected RiffChunk Chunk(IEnumerable<RiffChunk> chunks, string id)
    {
        RiffChunk? chunk = null;
        foreach (RiffChunk? riffChunk in chunks)
        {
            if (riffChunk.Type == id)
            {
                chunk = riffChunk;
                break;
            }
        }

        if (chunk == null || chunk.Type != id)
        {
            throw new InvalidOperationException("Invalid " + FileFormatName + " file format");
        }

        return chunk;
    }

    private string? FileFormatName { get; set; }

    /// <inheritdoc />
    public override void Flush()
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin)
    {
        long newPosition = _input.Position;
        switch (origin)
        {
            case SeekOrigin.Begin:
                newPosition = _startPositionOfData + offset;
                break;
            case SeekOrigin.Current:
                newPosition = _input.Position + offset;
                break;
            case SeekOrigin.End:
                newPosition = _startPositionOfData + _length + offset;
                break;
        }

        if (newPosition < _startPositionOfData || newPosition > (_startPositionOfData + _length))
            throw new InvalidOperationException("Cannot seek outside the range of this stream");

        return _input.Seek(newPosition, SeekOrigin.Begin);
    }

    /// <inheritdoc />
    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
        return _input.Read(buffer, offset, Math.Min(count, (int)Math.Max(_startPositionOfData + _length - _input.Position, 0)));
    }

    /// <inheritdoc />
    public override long Length => _length;

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException();
    }
}
