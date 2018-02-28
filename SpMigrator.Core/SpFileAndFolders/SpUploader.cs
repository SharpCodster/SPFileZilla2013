using Microsoft.SharePoint.Client;
using SpMigrator.Core.Interfaces;
using SpMigrator.Core.Utilities;
using System;
using System.IO;
using Sp = Microsoft.SharePoint.Client;

namespace SpMigrator.Core.SpFileAndFolders
{
    public class SpUploader
    {
        public SpConnectionManager Connection { get; private set; }

        public SpUploader(SpConnectionManager spConnection)
        {
            Connection = spConnection;
        }

        public ISpUploadResult UploadFileToSharePoint(string filePath, string spFolderUrl, bool overwrite, DateTime? dtCreated, DateTime? dtModified)
        {
            UploadResult res = UploadResult.GetSuccessResult(filePath, spFolderUrl);

            try
            {
                using (var ctx = Connection.GetContext())
                {
                    string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                    string newServerRelPath = PathHelper.CombinePaths(spFolderUrl, PathHelper.CleanFilenameForSP(fileName, ""));

                    // check if file exists before trying to upload (optimized, so the file is not opened and stream not sent to SP if already exists)
                    if (!overwrite)
                    {
                        Sp.File file = ctx.Web.GetFileByServerRelativeUrl(newServerRelPath);
                        ctx.Load(file, f => f.Exists);
                        ctx.ExecuteQuery();

                        if (file.Exists)
                        {
                            res.SetAlredyExist();
                        }
                    }

                    if (!res.AlredyExists)
                    {
                        using (var fs = new FileStream(filePath, FileMode.Open))
                        {
                            if (!Connection.IsSpOnline)
                            {
                                Sp.File.SaveBinaryDirect(ctx, newServerRelPath, fs, true);
                            }
                            else
                            {
                                var folder = ctx.Web.GetFolderByServerRelativeUrl(spFolderUrl);

                                var fci = new FileCreationInformation();
                                fci.ContentStream = fs;
                                fci.Url = PathHelper.CleanFilenameForSP(fileName, "");
                                fci.Overwrite = true;

                                folder.Files.Add(fci);
                                ctx.ExecuteQuery();
                            }
                        }

                        if (dtCreated.HasValue || dtModified.HasValue)
                        {
                            // update spfile dates
                            try
                            {
                                var file = ctx.Web.GetFileByServerRelativeUrl(newServerRelPath);
                                var item = file.ListItemAllFields;
                                if (dtCreated.HasValue)
                                    item["Created"] = dtCreated.Value.ToString("MM/dd/yyyy HH:mm:ss");
                                if (dtModified.HasValue)
                                    item["Modified"] = dtModified.Value.ToString("MM/dd/yyyy HH:mm:ss");
                                item.Update();
                                ctx.ExecuteQuery();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("File uploaded but error setting created/modified dates: " + ex.Message);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                res.SetError(ex);
            }

            return res;
        }


        public ISpUploadResult UploadFileToSharePoint(string spFileServerRelUrl, byte[] fileData)
        {
            UploadResult res = UploadResult.GetSuccessResult("[blob]", spFileServerRelUrl);


            try
            {
                using (var ctx = Connection.GetContext())
                {
                    using (var ms = new MemoryStream(fileData))
                    {
                        if (!Connection.IsSpOnline)
                        {
                            Sp.File.SaveBinaryDirect(ctx, spFileServerRelUrl, ms, true);
                        }
                        else
                        {
                            var spFolderUrl = spFileServerRelUrl.Substring(0, spFileServerRelUrl.LastIndexOf('/')).TrimEnd("/".ToCharArray());
                            var fileName = spFileServerRelUrl.Substring(spFileServerRelUrl.LastIndexOf('/') + 1).TrimStart("/".ToCharArray());

                            var folder = ctx.Web.GetFolderByServerRelativeUrl(spFolderUrl);

                            var fci = new FileCreationInformation();
                            fci.ContentStream = ms;
                            fci.Url = fileName;
                            fci.Overwrite = true;

                            folder.Files.Add(fci);
                            ctx.ExecuteQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.SetError(ex);
            }

            return res;
        }
    }
}
