using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    class PathApp
    {
        public FileFind FileFinder = new FileFind();
        public PathWork PathWorker = new PathWork();

        public void RunModule()
        {
            PathWorker.ReadPaths(); // fills: PathsToFindIn
            if (PathWorker.PathsToFindIn.Any())
            {
                foreach (DataClass path in PathWorker.PathsToFindIn)
                {
                    FileFinder.SlnFilesInDirs(path.ToString());
                    FileFinder.ExeFilesFromSlnFiles();
                }
            }
        }
    }
}
