using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    static class MoveWork
    {
        public static void MoveProject(string SourcePath, string DestinationPath)
        {
            if(SourcePath.Substring(SourcePath.Length - 1, 1) != "\\" )
            {
                SourcePath = SourcePath + "\\";
            }
            if (DestinationPath.Substring(DestinationPath.Length - 1, 1) != "\\")
            {
                DestinationPath = DestinationPath + "\\";
            }
            //Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));
            
            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
        }
        public static bool testPaths(string SourcePath, string DestinationPath)
        {
            if(!Directory.Exists(SourcePath))
            {
                return false;
            }
            if (!Directory.Exists(DestinationPath))
            {
                return false;
            }
            return true;
        }
    }
}
