// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.MediaFoundation;

/// <summary>
/// Associate an attribute key with a type used to retrieve keys from a <see cref="IMFAttributes"/> instance.
/// </summary>
public class MediaAttributeKey
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MediaAttributeKey"/> struct.
    /// </summary>
    /// <param name="guid">The attribute GUID.</param>
    /// <param name="type">The attribute type.</param>
    public MediaAttributeKey(Guid guid, Type type)
        : this(guid, type, string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MediaAttributeKey"/> struct.
    /// </summary>
    /// <param name="guid">The attribute GUID.</param>
    /// <param name="type">The attribute type.</param>
    /// <param name="name">The attribute name, useful for debugging.</param>
    public MediaAttributeKey(Guid guid, Type type, string name)
    {
        Guid = guid;
        Type = type;
        Name = name;
    }

    /// <summary>
    /// Gets  the attribute GUID.
    /// </summary>
    /// <value>
    /// The attribute GUID.
    /// </value>
    public Guid Guid { get; }

    /// <summary>
    /// Gets  the attribute type.
    /// </summary>
    /// <value>
    /// The attribute type.
    /// </value>
    public Type Type { get;}

    /// <summary>
    /// Gets the attribute name.
    /// </summary>
    public string Name { get; }
}


/// <summary>
/// Generic version of <see cref="MediaAttributeKey"/>.
/// </summary>
/// <typeparam name="T">Type of the value of this key.</typeparam>
public class MediaAttributeKey<T> : MediaAttributeKey
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MediaAttributeKey{T}"/> class.
    /// </summary>
    /// <param name="guid">The attribute GUID.</param>
    public MediaAttributeKey(string guid)
        : this(guid, string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MediaAttributeKey{T}"/> class.
    /// </summary>
    /// <param name="guid">The GUID.</param>
    /// <param name="name">The attribute name, useful for debugging.</param>
    public MediaAttributeKey(string guid, string name)
        : this(new Guid(guid), name)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MediaAttributeKey{T}"/> class.
    /// </summary>
    /// <param name="guid">The GUID.</param>
    public MediaAttributeKey(Guid guid)
        : this(guid, string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MediaAttributeKey{T}"/> class.
    /// </summary>
    /// <param name="guid">The GUID.</param>
    /// /// <param name="name">The attribute name, useful for debugging.</param>
    public MediaAttributeKey(Guid guid, string name)
        : base(guid, typeof(T), name)
    {
    }
}
