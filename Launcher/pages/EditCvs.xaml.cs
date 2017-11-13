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
    /// Interakční logika pro EditCvs.xaml
    /// </summary>
    public partial class EditCvs : Page
    {
        public static CsvWork CsvWorker = new CsvWork();
        public EditCvs()
        {
            InitializeComponent();
            Page_Initialized();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            CsvWorker.WriteCsv(DescriptionField.Text);
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            foreach (DataClass oneProjData in CsvWorker.AboutProjData)
            {
                if (oneProjData.ParametrName == (string)Application.Current.Properties["path"])
                {
                    DescriptionField.Text = oneProjData.Content;
                    found = true;
                }
            }
            if (!found)
            {
                DescriptionField.Text = "None";
            }
        }
        private void Page_Initialized()
        {
            CsvWorker.ReadCsv();
            bool found = false;
            foreach (DataClass oneProjData in CsvWorker.AboutProjData)
            {
                if (oneProjData.ParametrName == (string)Application.Current.Properties["path"])
                {
                    DescriptionField.Text = oneProjData.Content;
                    found = true;
                }
            }
            if (!found)
            {
                DescriptionField.Text = "None";
            }
        }
    }
}
