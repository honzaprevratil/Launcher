using System;
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

namespace Launcher.pages
{
    /// <summary>
    /// Interakční logika pro PathSettings.xaml
    /// </summary>
    public partial class PathSettings : Page
    {
        public PathSettings()
        {
            InitializeComponent();
            MainWindow.LaunchApp.PathWorker.ReadPaths();
            pathWiew.ItemsSource = MainWindow.LaunchApp.PathWorker.PathsToFindIn;
        }

        private void Add_Path_Click(object sender, RoutedEventArgs e)
        {
            if (PathInput.Text.Length > 0)
            {
                MainWindow.LaunchApp.PathWorker.WritePath(PathInput.Text);
                PathInput.Text = "";
            }
            else
            {
                MessageBox.Show("Error: Fill the path name.", "Error: Path enter", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void pathWiew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                DataClass RemoveThisObj = new DataClass("path", (pathWiew.SelectedItem.ToString()));
                MainWindow.LaunchApp.PathWorker.RemovePath(RemoveThisObj);
            } catch {}
        }
    }
}
