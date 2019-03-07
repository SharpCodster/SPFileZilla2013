using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using SpToolBox.ViewModels.FileSystemViews;
using SpToolBox.ViewModels.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpToolBox.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            RegisterViewModels();
            RegisterMessengers();

            
        }

        private void RegisterViewModels()
        {
            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register<FileExplorerViewModel>();
            SimpleIoc.Default.Register<FileSystemStructureViewModel>();
            SimpleIoc.Default.Register<FolderContentViewModel>();
        }

        private void RegisterMessengers()
        {
            Messenger.Default.Register<NotificationMessage>(this, NotifyUserMethod);
        }


        //private INavigationService CreateNavigationService()
        //{
        //    //var navigationService = new NavigationService();
        //    //navigationService.Configure("Details", typeof(DetailsPage));
        //    // navigationService.Configure("key1", typeof(OtherPage1));
        //    // navigationService.Configure("key2", typeof(OtherPage2));

        //    return navigationService;
        //}

        public MainWindowViewModel MainWindowViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainWindowViewModel>(); }
        }

        public FileExplorerViewModel FileExplorerViewModel
        {
            get { return ServiceLocator.Current.GetInstance<FileExplorerViewModel>(); }
        }

        public FileSystemStructureViewModel FileSystemStructureViewModel
        {
            get { return ServiceLocator.Current.GetInstance<FileSystemStructureViewModel>(); }
        }

        public FolderContentViewModel FolderContentViewModel
        {
            get { return ServiceLocator.Current.GetInstance<FolderContentViewModel>(); }
        }


        private void NotifyUserMethod(NotificationMessage message)
        {
            MessageBox.Show(message.Notification);
        }



    }
}
