using System;

namespace SpMigrator.Core.Interfaces
{
    public interface ISpUploadResult
    {
        bool Success { get; }
        bool AlredyExists { get; }
        string FileSystemPath { get; }
        string SpUrl { get; }
        Exception Exception { get; }
    }
}
