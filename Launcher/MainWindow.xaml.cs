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
        public static LauncherApp LaunchApp = new LauncherApp();
        public MainWindow()
        {
            InitializeComponent();
            LaunchApp.RunModule();
            dataListView.ItemsSource = LaunchApp.FileFinder.DataList;
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = dataListView.SelectedItem.ToString();
                Process.Start(path);
            }
            catch
            {
                MessageBox.Show("Error: Select a file to run it.", "Error: File Select", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Navigate_Move(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = (string)Application.Current.Properties["path"];
                if (path.Length > 0)
                {
                    PagesFrame.Navigate(new Uri("/pages/Move.xaml", UriKind.Relative));
                }
            }
            catch
            {
                MessageBox.Show("Error: Select a project to move it.", "Error: File Select", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Navigate_PathSettings(object sender, RoutedEventArgs e)
        {
            PagesFrame.Navigate(new Uri("/pages/PathSettings.xaml", UriKind.Relative)); //change Uri
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            LaunchApp.RunModule();
            dataListView.ItemsSource = LaunchApp.FileFinder.DataList;
        }

        private void dataListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string path = dataListView.SelectedItem.ToString();
                Application.Current.Properties["path"] = path;
                PagesFrame.NavigationService.Navigate(new Uri("/pages/EditCvs.xaml", UriKind.Relative), path, true); //change Uri
            }
            catch { }
        }

        private void Navigate_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = (string)Application.Current.Properties["path"];
                if (path.Length > 0)
                {
                    PagesFrame.Navigate(new Uri("/pages/DeletePage.xaml", UriKind.Relative));
                }
            }
            catch
            {
                MessageBox.Show("Error: Select a project to delete it.", "Error: File Select", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
