using FileHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Launcher
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileFind FileFinder = new FileFind();
        public MainWindow()
        {
            InitializeComponent();
            dataListView.ItemsSource = FileFinder.DataList;
            FileFinder.SlnFilesInDirs();
            FileFinder.ExeFilesFromSlnFiles();
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            string path = dataListView.SelectedItem.ToString();
            Process.Start(path);
        }
    }
}
