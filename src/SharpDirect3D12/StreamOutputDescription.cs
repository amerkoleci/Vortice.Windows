// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace SharpDirect3D12
{
    /// <summary>
    /// Describes a streaming output buffer.
    /// </summary>
    public partial class StreamOutputDescription
    {
        public StreamOutputDescription() { }

        public StreamOutputDescription(params StreamOutputElement[] elements)
        {
            Elements = elements;
        }

        /// <summary>	
        /// An array of <see cref="StreamOutputElement"/>.
        /// </summary>	
        public StreamOutputElement[] Elements { get; set; }

        /// <summary>
        /// An array of buffer strides; each stride is the size of an element for that buffer.
        /// </summary>
        public int[] Strides { get; set; }

        /// <summary>
        /// Implicitely converts to an <see cref="StreamOutputDescription"/> from an array of <see cref="StreamOutputElement"/>
        /// </summary>
        /// <param name="elements">Array of <see cref="StreamOutputElement"/>.</param>
        public static implicit operator StreamOutputDescription(StreamOutputElement[] elements)
        {
            return new StreamOutputDescription(elements);
        }
    }
}
