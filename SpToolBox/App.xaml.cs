using SpMigrator.Core.WindowsFileSystem;
using SpToolBox.Views.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SpToolBox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {

#if DEBUG
            VariusTestMethod();
#endif

            MainWindow window = new MainWindow();

            this.MainWindow = window;
            this.MainWindow.Show();
        }





        private void VariusTestMethod()
        {
            WfsInspector ins = new WfsInspector();

            var ddd = ins.GetDrivers();

            ddd[0].Load();

            var t = ddd[0].Childs;

            List <DriveInfo> g = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.VolumeLabel != null).ToList();
            var dirs = GetChildDirs(g[1].Name);
            var dirs2 = GetChildDirs(dirs[3].FullName);
            int i = 0;
        }
        


        private List<DirectoryInfo> GetChildDirs(string path)
        {
            string[] firstDirs = Directory.GetDirectories(path);

            var dirs = (from subPath in firstDirs
                        select new DirectoryInfo(subPath));

            return dirs.Where(v => !v.Attributes.HasFlag(FileAttributes.System)).ToList();
        }
    }
}
