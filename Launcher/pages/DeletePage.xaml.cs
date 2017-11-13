using System;
using System.Collections.Generic;
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

namespace Launcher.pages
{
    /// <summary>
    /// Interakční logika pro Delete.xaml
    /// </summary>
    public partial class Delete : Page
    {
        public Delete()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string pathToDelete = SourcePath.Text;
            if (Directory.Exists(pathToDelete))
            {
                MessageBoxResult result = MessageBox.Show("Do you really wish to remove all the files in:\n" + pathToDelete + "\n?\n\nThis will be PERNAMENT and there is no undo!!!", "Watning: Deleting all files", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    Directory.Delete(pathToDelete, true);
                    SourcePath.Text = "";
                }
            } else
            {
                MessageBox.Show("Error: This path was not found. Check it again.", "Error: Path input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Page_Initialized(object sender, EventArgs e)
        {
            SourcePath.Text = (string)Application.Current.Properties["path"];
        }
    }
}
