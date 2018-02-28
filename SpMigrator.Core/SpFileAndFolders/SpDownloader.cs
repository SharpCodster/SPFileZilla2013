using SpMigrator.Core.Interfaces;
using System;
using System.IO;
using Sp = Microsoft.SharePoint.Client;

namespace SpMigrator.Core.SpFileAndFolders
{
    public class SpDownloader
    {
        private SpConnectionManager _connection;

        public SpDownloader(SpConnectionManager spConnection)
        {
            _connection = spConnection;
        }

        public ISpDownloadResult DownloadFileFromSharePoint(string fileServerRelUrl)
        {
            byte[] fileData = null;

            DownloadReult res = DownloadReult.GetSuccessResult(fileServerRelUrl);

            try
            {
                using (var ctx = _connection.GetContext())
                {
                    var fi = Sp.File.OpenBinaryDirect(ctx, fileServerRelUrl);
                    fileData = ReadFully(fi.Stream);
                }
                res.SetFinisced(fileData);
            }
            catch (Exception ex)
            {
                res.SetError(ex);
            }

            return res;
        }

        public byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
