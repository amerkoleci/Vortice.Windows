// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectWrite;

public partial class IDWriteFactory
{
    private readonly List<IDWriteFontCollectionLoader> _fontCollectionLoaderCallbacks = new();
    private readonly List<IDWriteFontFileLoader> _fontFileLoaderCallbacks = new();

    internal IDWriteFactory()
    {
    }

    public IDWriteTextFormat CreateTextFormat(string fontFamilyName, float fontSize)
    {
        return CreateTextFormat(fontFamilyName, null, FontWeight.Normal, FontStyle.Normal, FontStretch.Normal, fontSize, "");
    }

    public IDWriteTextFormat CreateTextFormat(
        string fontFamilyName,
        FontWeight fontWeight,
        FontStyle fontStyle,
        float fontSize)
    {
        return CreateTextFormat(fontFamilyName, null, fontWeight, fontStyle, FontStretch.Normal, fontSize, "");
    }

    public IDWriteTextFormat CreateTextFormat(
        string fontFamilyName,
        FontWeight fontWeight,
        FontStyle fontStyle,
        FontStretch fontStretch,
        float fontSize)
    {
        return CreateTextFormat(fontFamilyName, null, fontWeight, fontStyle, fontStretch, fontSize, "");
    }

    public IDWriteTextFormat CreateTextFormat(
        string fontFamilyName, 
        IDWriteFontCollection fontCollection, 
        FontWeight fontWeight, 
        FontStyle fontStyle, 
        FontStretch fontStretch, float fontSize)
    {
        return CreateTextFormat(fontFamilyName, fontCollection, fontWeight, fontStyle, fontStretch, fontSize, "");
    }

    /// <summary>
    /// Creates an object that represents a font face.
    /// </summary>
    /// <param name="fontFaceType">A value that indicates the type of file format of the font face.</param>
    /// <param name="fontFiles">A font file object representing the font face. Because <see cref="IDWriteFontFace"/> maintains its own references to the input font file objects, you may release them after this call.</param>
    /// <param name="faceIndex">The zero-based index of a font face, in cases when the font files contain a collection of font faces. If the font files contain a single face, this value should be zero.</param>
    /// <param name="fontFaceSimulationFlags">A value that indicates which, if any, font face simulation flags for algorithmic means of making text bold or italic are applied to the current font face.</param>
    /// <returns>Instance of <see cref="IDWriteFontFace"/> or null if failed.</returns>
    public IDWriteFontFace? CreateFontFace(FontFaceType fontFaceType, IDWriteFontFile[] fontFiles, uint faceIndex = 0, FontSimulations fontFaceSimulationFlags = FontSimulations.None)
    {
        Result result = CreateFontFace(
            fontFaceType,
            (uint)fontFiles.Length, fontFiles,
            faceIndex,
            fontFaceSimulationFlags,
            out IDWriteFontFace? fontFace);

        return result.Failure ? null : fontFace;
    }

    public IDWriteFontFile CreateFontFileReference(string filePath)
    {
        return CreateFontFileReference(filePath, null);
    }

    /// <summary>	
    /// Registers a custom font collection loader with the factory object. 	
    /// </summary>
    /// <param name="fontCollectionLoader">Reference to a <see cref="IDWriteFontCollectionLoader"/> object to be registered.</param>
    public void RegisterFontCollectionLoader(IDWriteFontCollectionLoader fontCollectionLoader)
    {
        //IDWriteFontCollectionLoaderShadow.SetFactory(fontCollectionLoader, this);
        RegisterFontCollectionLoader_(fontCollectionLoader);
        _fontCollectionLoaderCallbacks.Add(fontCollectionLoader);
    }

    /// <summary>	
    /// Unregisters a custom font collection loader that was previously registered using <see cref="RegisterFontCollectionLoader"/>	method.
    /// </summary>	
    /// <param name="fontCollectionLoader">Instance of <see cref="IDWriteFontCollectionLoader"/> object to be unregistered.</param>
    public void UnregisterFontCollectionLoader(IDWriteFontCollectionLoader fontCollectionLoader)
    {
        if (!_fontCollectionLoaderCallbacks.Contains(fontCollectionLoader))
        {
            throw new ArgumentException("This font collection loader is not registered", nameof(fontCollectionLoader));
        }

        UnregisterFontCollectionLoader_(fontCollectionLoader);
        _fontCollectionLoaderCallbacks.Remove(fontCollectionLoader);
    }


    /// <summary>	
    /// Registers a font file loader with DirectWrite. 	
    /// </summary>
    /// <param name="fontFileLoader">Instance of <see cref="IDWriteFontFileLoader"/> object for a particular file resource type. </param>
    public void RegisterFontFileLoader(IDWriteFontFileLoader fontFileLoader)
    {
        RegisterFontFileLoader_(fontFileLoader);
        _fontFileLoaderCallbacks.Add(fontFileLoader);
    }

    /// <summary>	
    /// Unregisters a font file loader that was previously registered with the DirectWrite font system using {{RegisterFontFileLoader}}. 	
    /// </summary>	
    /// <remarks>	
    /// This function unregisters font file loader callbacks with the DirectWrite font system. You should implement the font file loader interface by a singleton object. Note that font file loader implementations must not register themselves with DirectWrite inside their constructors and must not unregister themselves in their destructors, because registration and unregistration operations increment and decrement the object reference count respectively. Instead, registration and unregistration of font file loaders with DirectWrite should be performed outside of the font file loader implementation.  	
    /// </remarks>	
    /// <param name="fontFileLoader">Pointer to the file loader that was previously registered with the DirectWrite font system using {{RegisterFontFileLoader}}. </param>
    /// <returns>If the method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code. </returns>
    /// <unmanaged>HRESULT IDWriteFactory::UnregisterFontFileLoader([None] IDWriteFontFileLoader* fontFileLoader)</unmanaged>
    public void UnregisterFontFileLoader(IDWriteFontFileLoader fontFileLoader)
    {
        if (!_fontFileLoaderCallbacks.Contains(fontFileLoader))
        {
            throw new ArgumentException("This font file loader is not registered", nameof(fontFileLoader));
        }

        UnregisterFontFileLoader_(fontFileLoader);
        _fontFileLoaderCallbacks.Remove(fontFileLoader);
    }
}
