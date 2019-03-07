using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.IO;
using SpMigrator.Core.WindowsFileSystem;

namespace SpToolBox.Models
{
    public class FolderTreeViewItem : ObservableObject//, TreeViewItem
    {

        private WfsObject fsObject;




        public static List<FolderTreeViewItem> GetDrivers()
        {
            var drivers = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.VolumeLabel != null).ToList();
            var dirs = (from driver in drivers select new DirectoryInfo(driver.Name)).ToList();

            return (from dir in dirs select new FolderTreeViewItem(dir)).ToList();
        }

        private DirectoryInfo _info;

        private bool subFoldersPopulated = false;

        public FolderTreeViewItem(DirectoryInfo folder)
        {
            _info = folder;

            if (IsDriver)
            {
                Populate();
            }
 
        }


        public string FolderName
        {
            get { return _info.Name; }
        }

        public bool IsDriver
        {
            get { return _info.Parent == null; }
        }

        public List<FolderTreeViewItem> Folders;
        
        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { Set<bool>(() => this.IsExpanded, ref _isExpanded, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set<bool>(() => this.IsSelected, ref _isSelected, value); }
        }


        public void Populate()
        {
            Folders = GetChildDirs(_info.FullName);
            subFoldersPopulated = true;
        }

        private List<FolderTreeViewItem> GetChildDirs(string path)
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

            return (from dir in dirs
                    where dir.Attributes.HasFlag(FileAttributes.System) == false
                    && dir.Attributes.HasFlag(FileAttributes.NotContentIndexed) == false
                    select new FolderTreeViewItem(dir)).ToList();
        }
    }
}
