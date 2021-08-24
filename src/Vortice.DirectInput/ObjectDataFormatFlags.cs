// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectInput
{
    /// <summary>
    /// Flags for a data format.
    /// </summary>
    [System.Flags]
    public enum ObjectDataFormatFlags : int
    {
        /// <summary>
        /// The data format doesn't report any specific information.
        /// </summary>
        None = 0,
        /// <summary>
        /// The data format must report acceleration information.
        /// </summary>
        Acceleration = ObjectAspect.Acceleration,
        /// <summary>
        /// The data format must report force information.
        /// </summary>
        Force = ObjectAspect.Force,
        /// <summary>
        /// The data format must report position information.
        /// </summary>
        Position = ObjectAspect.Position,
        /// <summary>
        /// The data format must report velocity information.
        /// </summary>
        Velocity = ObjectAspect.Velocity
    }
}
