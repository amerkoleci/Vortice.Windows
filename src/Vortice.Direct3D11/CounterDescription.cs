// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D11
{
    /// <summary>
    /// Describes a counter.
    /// </summary>
    public partial struct CounterDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CounterDescription"/> struct.
        /// </summary>
        /// <param name="counterKind">Type of query (see <see cref="CounterKind"/>).</param>
        /// <param name="miscFlags">Miscellaneous flags.</param>
        public CounterDescription(CounterKind counterKind, int miscFlags = 0)
        {
            CounterKind = counterKind;
            MiscFlags = miscFlags;
        }
    }
}
