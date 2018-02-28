using System;

namespace SpMigrator.Core.Interfaces
{
    public interface ISpDownloadResult
    {
        bool Success { get; }
        string SpUrl { get; }
        Exception Exception { get; }
        byte[] ByteData { get; }
    }
}
