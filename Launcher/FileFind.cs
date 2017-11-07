using FileHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Launcher
{
    class FileFind
    {
        public ObservableCollection<PathClass> DataList = new ObservableCollection<PathClass>();
        public List<PathClass> SlnDataList = new List<PathClass>();

        public void ExeFilesFromSlnFiles()
        {
            foreach (PathClass Cesta in SlnDataList)
            {
                string cestaStr = Cesta.FullPath.ToString().Substring(0, (Cesta.FullPath.ToString()).Length -4);
                string nazevStr = Cesta.FileName.ToString();
                string slozka = nazevStr.Substring(0, nazevStr.Length - 4);
                // string cestaStrBin = cestaStr.Substring(0, cestaStr.Length - 4) + "\\bin";
                string csprojFile = cestaStr + "\\" + slozka + ".csproj";
                Console.WriteLine(csprojFile);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(csprojFile);
                XmlNamespaceManager mgr = new XmlNamespaceManager(xmldoc.NameTable);
                mgr.AddNamespace("x", "http://schemas.microsoft.com/developer/msbuild/2003");

                foreach (XmlNode item in xmldoc.SelectNodes("//x:OutputPath", mgr))
                {
                    string exePath = cestaStr + "\\" + item.InnerText.ToString()+ slozka + ".exe"; //the path of exe
                    FileInfo exeFile = new FileInfo(exePath);
                    if (exeFile.Exists)
                    {
                        DataList.Add(new PathClass(exeFile.FullName, exeFile.Name));
                    }
                }
            }
        }
        public void SlnFilesInDirs(string dirRoot = "0")
        {
            if (dirRoot == "0")
            {
                dirRoot = @"D:\prevrja15";
                //dirRoot = @"E:\Honzovo\Škola\SPS\3ITB\VAH\MVS Repos";
                SlnDataList.Clear();
            }

            List<DirectoryInfo> RootDirs = new List<DirectoryInfo>(); //všechny podsložky root složky
            foreach (DirectoryInfo OneDir in new DirectoryInfo(dirRoot).GetDirectories())
            {
                RootDirs.Add(OneDir);
            }
            foreach (DirectoryInfo oneRootDir in RootDirs)// projde podsložky
            {
                List<FileInfo> slnFilesInDir = FindFilesInDir(oneRootDir.FullName, ".sln");// sln soubory v dané podsložce
                if (slnFilesInDir.Any()) //obsahuje alespoň jeden .sln
                {
                    foreach (FileInfo file in slnFilesInDir) // pro každý sln
                    {
                        SlnDataList.Add(new PathClass(file.FullName, file.Name));
                    }
                }
                else //neobsahuje .sln
                {
                    List<DirectoryInfo> DirsOfRootDir = new List<DirectoryInfo>(oneRootDir.GetDirectories());// podsložky jedné root složky
                    if (DirsOfRootDir.Any())
                    {
                        string Path = oneRootDir.FullName;
                        SlnFilesInDirs(Path);// rekurze
                    }

                }
            }
        }
        public List<FileInfo> FindFilesInDir(string directionary, string fileType = ".exe")
        {
            List<FileInfo> returnData = new List<FileInfo>();

            var dir = new DirectoryInfo(directionary);
            FileInfo[] files = dir.GetFiles();
            var carMake = files
            .Where(item => item.Extension == fileType)
            .Select(item => item);

            foreach (var item in carMake)
            {
                returnData.Add(item);
            }
            return returnData;
        }
    }
}
