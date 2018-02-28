using SpMigrator.Core.Interfaces;
using System;

namespace SpMigrator.Core.SpFileAndFolders
{
    internal class UploadResult : ISpUploadResult
    {
        public static UploadResult GetSuccessResult(string fileSystem, string spUrl)
        {
            UploadResult res = new UploadResult();
            res.Success = true;
            res.AlredyExists = false;
            res.FileSystemPath = fileSystem;
            res.SpUrl = spUrl;

            return res;
        }

        public void SetAlredyExist()
        {
            Success = false;
            AlredyExists = true;
        }

        public void SetError(Exception ex)
        {
            Success = false;
            AlredyExists = false;
            Exception = ex;
        }

        public bool Success { get; private set; }
        public string FileSystemPath { get; private set; }
        public string SpUrl { get; private set; }
        public bool AlredyExists { get; private set; }
        public Exception Exception { get; private set; }
    }
}
