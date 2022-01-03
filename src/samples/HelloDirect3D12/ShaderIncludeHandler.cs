// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Dxc;

namespace HelloDirect3D12;

public class ShaderIncludeHandler : CallbackBase, IDxcIncludeHandler
{
    private readonly string[] _includeDirectories;
    private readonly Dictionary<string, SourceCodeBlob> _sourceFiles = new Dictionary<string, SourceCodeBlob>();

    public ShaderIncludeHandler(params string[] includeDirectories)
    {
        _includeDirectories = includeDirectories;
    }

    protected override void DisposeCore(bool disposing)
    {
        foreach (var pinnedObject in _sourceFiles.Values)
            pinnedObject?.Dispose();

        _sourceFiles.Clear();
    }

    public Result LoadSource(string fileName, out IDxcBlob includeSource)
    {
        if (fileName.StartsWith("./"))
            fileName = fileName.Substring(2);

        var includeFile = GetFilePath(fileName);

        if (string.IsNullOrEmpty(includeFile))
        {
            includeSource = default;

            return Result.Fail;
        }

        if (!_sourceFiles.TryGetValue(includeFile, out SourceCodeBlob sourceCodeBlob))
        {
            byte[] data = NewMethod(includeFile);

            sourceCodeBlob = new SourceCodeBlob(data);
            _sourceFiles.Add(includeFile, sourceCodeBlob);
        }

        includeSource = sourceCodeBlob.Blob;

        return Result.Ok;
    }

    private static byte[] NewMethod(string includeFile) => File.ReadAllBytes(includeFile);

    private string? GetFilePath(string fileName)
    {
        for (int i = 0; i < _includeDirectories.Length; i++)
        {
            var filePath = Path.GetFullPath(Path.Combine(_includeDirectories[i], fileName));

            if (File.Exists(filePath))
                return filePath;
        }

        return null;
    }


    private class SourceCodeBlob : IDisposable
    {
        private byte[] _data;
        private GCHandle _dataPointer;
        private IDxcBlobEncoding? _blob;

        internal IDxcBlob? Blob { get => _blob; }

        public SourceCodeBlob(byte[] data)
        {
            _data = data;

            _dataPointer = GCHandle.Alloc(data, GCHandleType.Pinned);

            _blob = DxcCompiler.Utils.CreateBlob(_dataPointer.AddrOfPinnedObject(), data.Length, Dxc.DXC_CP_UTF8);
        }

        public void Dispose()
        {
            //_blob?.Dispose();
            _blob = null;

            if (_dataPointer.IsAllocated)
                _dataPointer.Free();
            _dataPointer = default;
        }
    }
}
