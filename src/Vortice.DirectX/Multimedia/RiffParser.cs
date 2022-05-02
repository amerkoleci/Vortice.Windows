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

using System.Collections;

namespace Vortice.Multimedia;

/// <summary>
/// Riff chunk enumerator.
/// </summary>
public class RiffParser : IEnumerator<RiffChunk>, IEnumerable<RiffChunk>
{
    private readonly Stream _input;
    private readonly long startPosition;
    private readonly BinaryReader reader;
    private readonly Stack<RiffChunk> _chunckStack = new();
    private bool descendNext;
    private bool isEndOfRiff;
    private bool isErrorState;
    private RiffChunk? _current;

    /// <summary>
    /// Initializes a new instance of the <see cref="RiffParser"/> class.
    /// </summary>
    /// <param name="input">The input.</param>
    public RiffParser(Stream input)
    {
        _input = input;
        this.startPosition = input.Position;
        this.reader = new BinaryReader(input);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        // Nothing to dispose.
    }

    /// <summary>
    /// Advances the enumerator to the next element of the collection.
    /// </summary>
    /// <returns>
    /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// The collection was modified after the enumerator was created.
    ///   </exception>
    public bool MoveNext()
    {
        CheckState();

        if (_current != null)
        {
            // By default, set the starting position to the data of the chunk
            long nextOffset = _current.DataPosition;
            // If we descend
            if (descendNext)
            {
                // Next time, proceed chunk sequentially
                descendNext = false;
            }
            else
            {
                // Else, go to next chunk
                nextOffset += _current.Size;
                // Pad DWORD
                if ((nextOffset & 1) != 0)
                    nextOffset++;
            }
            _input.Position = nextOffset;

            // Check that moveNext is not going outside a parent chunk.
            // If yes, pop the last chunk from the stack
            var currentChunkContainer = _chunckStack.Peek();
            long endOfOuterChunk = currentChunkContainer.DataPosition + currentChunkContainer.Size;
            if (_input.Position >= endOfOuterChunk)
                _chunckStack.Pop();

            // If there are no more chunk in the 
            if (_chunckStack.Count == 0)
            {
                isEndOfRiff = true;
                return false;
            }
        }

        var fourCC = ((FourCC)reader.ReadUInt32());
        bool isList = (fourCC == "LIST");
        bool isHeader = (fourCC == "RIFF");
        uint chunkSize = 0;

        if (_input.Position == (startPosition + 4) && !isHeader)
        {
            isErrorState = true;
            throw new InvalidOperationException("Invalid RIFF file format");
        }

        // Read chunk size
        chunkSize = reader.ReadUInt32();

        // If list or header
        if (isList || isHeader)
        {
            // Check file size
            if (isHeader && chunkSize > (_input.Length - 8))
            {
                isErrorState = true;
                throw new InvalidOperationException("Invalid RIFF file format");
            }
            chunkSize -= 4;
            fourCC = reader.ReadUInt32();
        }

        // Read RIFF type and create chunk
        _current = new RiffChunk(_input, fourCC, chunkSize, (uint)_input.Position, isList, isHeader);
        return true;
    }

    private void CheckState()
    {
        if (isEndOfRiff)
            throw new InvalidOperationException("End of Riff. Cannot MoveNext");

        if (isErrorState)
            throw new InvalidOperationException("The enumerator is in an error state");
    }

    /// <summary>
    /// Gets the current stack of chunks.
    /// </summary>
    public Stack<RiffChunk> ChunkStack => _chunckStack;

    /// <summary>
    /// Sets the enumerator to its initial position, which is before the first element in the collection.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// The collection was modified after the enumerator was created.
    ///   </exception>
    public void Reset()
    {
        CheckState();
        _current = default;
        _input.Position = startPosition;
    }

    /// <summary>
    /// Ascends to the outer chunk.
    /// </summary>
    public void Ascend()
    {
        CheckState();
        var outerChunk = _chunckStack.Pop();
        _input.Position = outerChunk.DataPosition + outerChunk.Size;
    }

    /// <summary>
    /// Descends to the current chunk.
    /// </summary>
    public void Descend()
    {
        CheckState();
        _chunckStack.Push(_current!);
        descendNext = true;
    }

    /// <summary>
    /// Gets all chunks.
    /// </summary>
    /// <returns></returns>
    public IList<RiffChunk> GetAllChunks()
    {
        var chunks = new List<RiffChunk>();
        foreach (RiffChunk riffChunk in this)
        {
            chunks.Add(riffChunk);
        }
        return chunks;
    }

    /// <summary>
    /// Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    /// <returns>
    /// The element in the collection at the current position of the enumerator.
    ///   </returns>
    public RiffChunk Current
    {
        get
        {
            CheckState();
            return _current!;
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<RiffChunk> GetEnumerator()
    {
        return this;
    }

    object IEnumerator.Current
    {
        get
        {
            CheckState();
            return Current;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
