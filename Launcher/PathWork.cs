using FileHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    class PathWork
    {
        public List<DataClass> PathsToFindIn = new List<DataClass>();
        public string PathsFileName = @"paths.csv";

        public void ReadPaths()
        {
            var engine = new FileHelperEngine<DataClass>();
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
            var engine = new FileHelperEngine<DataClass>();
            PathsToFindIn.Add(new DataClass("path", newPath));

            CheckPathsFile();
            engine.WriteFile(PathsFileName, PathsToFindIn);
        }
        public void CheckPathsFile()
        {
            var engine = new FileHelperEngine<DataClass>();
            DataClass[] res;

            if (!File.Exists(PathsFileName))
            {
                File.Create(PathsFileName);
                res = engine.ReadFile(PathsFileName);
            }
        }
    }
}