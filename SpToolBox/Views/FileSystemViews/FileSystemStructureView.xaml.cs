﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpToolBox.Views.FileSystemViews
{
    /// <summary>
    /// Interaction logic for FileSystemStructureViewModel.xaml
    /// </summary>
    public partial class FileSystemStructureView : UserControl
    {
        public FileSystemStructureView()
        {
            InitializeComponent();
            TreeViewItem it = new TreeViewItem();
            //it.Expanded
        }
    }
}
