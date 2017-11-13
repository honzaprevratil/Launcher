using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Launcher
{
    public class LauncherApp
    {
        public FileFind FileFinder = new FileFind();
        public PathWork PathWorker = new PathWork();

        public void RunModule()
        {
            FileFinder.DataList.Clear();
            FileFinder.SlnDataList.Clear();
            PathWorker.ReadPaths(); // fills: PathsToFindIn
            if (PathWorker.PathsToFindIn.Any())
            {
                foreach (DataClass path in PathWorker.PathsToFindIn)
                {
                    FileFinder.SlnFilesInDirs(path.Content.ToString());
                    FileFinder.ExeFilesFromSlnFiles();
                    FileFinder.SlnDataList.Clear();
                }
                if (FileFinder.DataList.Count <= 0)
                {
                    MessageBox.Show("Error: Change (Edit or Add) loading path of programs. Nothing was found.", "Error: Path settings", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } else
            {
                MessageBox.Show("Error: Change (Edit or Add) loading path of programs. Nothing was found.", "Error: Path settings", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
