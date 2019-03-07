using SpMigrator.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpMigrator.Core.WindowsFileSystem
{
    public class WfsInspector
    {


        public List<WfsObject> GetDrivers()
        {
            var drivers = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.VolumeLabel != null).ToList();
            var dirs = (from driver in drivers select new DirectoryInfo(driver.Name)).ToList();

            return (from dir in dirs select new WfsObject() { HasLoaded = false, Name = dir.Name, Path = dir.FullName, Type = WfsType.Driver }).ToList();
        }


        public List<WfsObject> GetItems(string path)
        {
            string[] firstDirs;
            try
            {
                firstDirs = Directory.GetDirectories(path);
            }
            catch (Exception ex)
            {
                // no access
                return null;
            }

            var dirs = (from subPath in firstDirs
                        select new DirectoryInfo(subPath));


            var dirObj = (from dir in dirs
                    where dir.Attributes.HasFlag(FileAttributes.System) == false
                    && dir.Attributes.HasFlag(FileAttributes.NotContentIndexed) == false
                    select new WfsObject() { HasLoaded = false, Name = dir.Name, Path = dir.FullName, Type = WfsType.Folder }).ToList();


            var firstFiles = Directory.GetFiles(path);


            var files = (from subPath in firstFiles
                         select new FileInfo(subPath));

            var fileObj = (from file in files
                          select new WfsObject() { HasLoaded = true, Name = file.Name, Path = file.FullName, Type = WfsType.File }).ToList();


            List<WfsObject> res = new List<WfsObject>();
            res.AddRange(dirObj);
            res.AddRange(fileObj);

            return res;

               
        }
    }
}
