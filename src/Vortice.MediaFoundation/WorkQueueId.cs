// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;

namespace Vortice.MediaFoundation;

/// <summary>
/// A Work Queue Identifier
/// </summary>
/// <msdn-id>ms703102</msdn-id>	
[StructLayout(LayoutKind.Sequential)]
public readonly struct WorkQueueId : IEquatable<WorkQueueId>
{
    /// <summary>
    /// The default queue associated to the <see cref="WorkQueueType.Standard"/>.
    /// </summary>
    public static readonly WorkQueueId Standard = new(WorkQueueType.Standard);

    /// <summary>
    /// The identifier.
    /// </summary>
    public readonly int Id;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkQueueId"/> struct.
    /// </summary>
    /// <param name="id">The id.</param>
    public WorkQueueId(int id)
    {
        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkQueueId"/> struct.
    /// </summary>
    /// <param name="id">The id.</param>
    public WorkQueueId(WorkQueueType id)
    {
        Id = (int)id;
    }

    /// <inheritdoc/>
		public override bool Equals(object? obj) => obj is WorkQueueId workQueue && Equals(workQueue);

    public bool Equals(WorkQueueId other) => Id == other.Id;


    public override int GetHashCode() => Id;

    public static bool operator ==(WorkQueueId left, WorkQueueId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(WorkQueueId left, WorkQueueId right)
    {
        return !left.Equals(right);
    }

    public override string ToString()
    {
        return $"Id: {Id} (Type: {(WorkQueueType)Id})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="int"/> to <see cref="WorkQueueId"/>.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator WorkQueueId(int id) => new(id);

    /// <summary>
    /// Performs an implicit conversion from <see cref="WorkQueueType"/> to <see cref="WorkQueueId"/>.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator WorkQueueId(WorkQueueType type) => new(type);

    /// <summary>
    /// Performs an explicit conversion from <see cref="WorkQueueId"/> to <see cref="int"/>.
    /// </summary>
    /// <param name="workQueueId">The work queue Id.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator int(WorkQueueId workQueueId) => workQueueId.Id;

    /// <summary>
    /// Performs an explicit conversion from <see cref="WorkQueueId"/> to <see cref="WorkQueueType"/>.
    /// </summary>
    /// <param name="workQueueId">The work queue Id.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator WorkQueueType(WorkQueueId workQueueId) => (WorkQueueType)workQueueId.Id;
}
