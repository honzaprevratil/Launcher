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
        PathApp PApp = new PathApp();
        public MainWindow()
        {
            InitializeComponent();
            dataListView.ItemsSource = PApp.FileFinder.DataList;
            PApp.RunModule();
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = dataListView.SelectedItem.ToString();
                Process.Start(path);
            } catch
            {
                MessageBoxResult result = MessageBox.Show("Error: Select a file to run it.", "Error: File Select", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Add_Path_Click(object sender, RoutedEventArgs e)
        {
            if (PathInput.ToString().Length > 0)
            {
                PApp.PathWorker.WritePath(PathInput.ToString());
            }
        }
    }
}
