using GalaSoft.MvvmLight;
using SpToolBox.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SpToolBox.ViewModels.FileSystemViews
{
    public class FileSystemStructureViewModel : ViewModelBase
    {

        public FileSystemStructureViewModel()
        {
            //Drivers = new ObservableCollection<FolderTreeViewItem>(FolderTreeViewItem.GetDrivers());
            Drivers = FolderTreeViewItem.GetDrivers();
        }

        private List<FolderTreeViewItem> _drivers;
        public List<FolderTreeViewItem> Drivers
        {
            get { return _drivers; }
            set
            {
                Set<List<FolderTreeViewItem>>(() => this.Drivers, ref _drivers, value);
            }
        }


        //private object _drivers;
        //public object Drivers
        //{
        //    get { return _drivers; }
        //    set
        //    {
        //        Set<object>(() => this.Drivers, ref _drivers, value);
        //    }
        //}


        private string _url;
        public string Url
        {
            get { return _url; }
            set {
                Set<string>(() => this.Url, ref _url, value);
            }
        }


        //public FileSystemStructureViewModel()
        //{
        //    _url = "Ciao";
        //}






    }
}
