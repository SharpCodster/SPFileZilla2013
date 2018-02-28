using SpMigrator.Core.Interfaces;
using System;

namespace SpMigrator.Core.SpFileAndFolders
{
    internal class DownloadReult : ISpDownloadResult
    {
        public bool Success { get; private set; }
        public string SpUrl { get; private set; }
        public Exception Exception { get; private set; }
        public byte[] ByteData { get; private set; }

        public static DownloadReult GetSuccessResult(string spUrl)
        {
            DownloadReult res = new DownloadReult();
            res.Success = true;
            res.SpUrl = spUrl;

            return res;
        }

        public void SetFinisced(byte[] data)
        {
            ByteData = data;
        }

        public void SetError(Exception ex)
        {
            Success = false;
            Exception = ex;
        }
    }
}
