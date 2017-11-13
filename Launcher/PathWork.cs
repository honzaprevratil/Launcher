using FileHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Launcher
{
    public class PathWork
    {
        public ObservableCollection<DataClass> PathsToFindIn = new ObservableCollection<DataClass>();
        public string PathsFileName = @"paths.csv";
        private FileHelperEngine<DataClass> engine = new FileHelperEngine<DataClass>();

        public void ReadPaths()
        {
            DataClass[] res;

            CheckPathsFile();
            res = engine.ReadFile(PathsFileName);

            PathsToFindIn.Clear();
            foreach (var record in res)
            {
                PathsToFindIn.Add(record);
            }
        }
        public void WritePath(string newPath)
        {
            DirectoryInfo dirPath = new DirectoryInfo("none");
            try
            {
                dirPath = new DirectoryInfo(newPath);
            } catch {};
            if (dirPath.Exists)
            {
                DataClass newRow = new DataClass("path", newPath);
                PathsToFindIn.Add(newRow);
                CheckPathsFile();
                engine.WriteFile(PathsFileName, PathsToFindIn);
            } else
            {
                MessageBox.Show("Error: Fill your path again, this path do not exist.", "Error: Path enter", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void RemovePath(DataClass RemoveThis)
        {
            PathsToFindIn.Remove(RemoveThis);
            CheckPathsFile();
            engine.WriteFile(PathsFileName, PathsToFindIn);
        }
        public void CheckPathsFile()
        {
            if (!File.Exists(PathsFileName))
            {
                FileStream x = File.Create(PathsFileName);
                x.Close();
            }
        }
    }
}