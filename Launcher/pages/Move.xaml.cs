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
    /// Interakční logika pro Page1.xaml
    /// </summary>
    public partial class Move : Page
    {
        public Move()
        {
            InitializeComponent();
        }

        private void MoveToPath_Click(object sender, RoutedEventArgs e)
        {
            if(MoveWork.testPaths(SourcePath.Text, DestinationPath.Text))
            {
                MoveWork.MoveProject(SourcePath.Text, DestinationPath.Text);
                MessageBox.Show("Project copyied! \nFrom: '"+ SourcePath.Text +"' \nTo: " + DestinationPath.Text + "\nRefresh data to map it.", "Project copyied", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Error: Check your paths again. Some of them do not exist.", "Error: Path enter", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            SourcePath.Text = (string)Application.Current.Properties["path"];
        }
    }
}
