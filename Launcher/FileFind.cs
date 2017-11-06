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
        public ObservableCollection<DataClass> DataList = new ObservableCollection<DataClass> { };
        public string FileName = @"cesty.txt";

        public void ExeFilesFromSlnFiles()
        {
            List<DataClass> DataTemp = new List<DataClass>();
            foreach (DataClass Cesta in DataList)
            {
                string cestaStr = Cesta.FullPath.ToString().Substring(0, (Cesta.FullPath.ToString()).Length -4);
                string nazevStr = Cesta.FileName.ToString();
                string slozka = nazevStr.Substring(0, nazevStr.Length - 4);
                // string cestaStrBin = cestaStr.Substring(0, cestaStr.Length - 4) + "\\bin";
                string csprojFile = cestaStr + "\\" + slozka + ".csproj";

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
                        DataClass x = new DataClass();
                        x.FullPath = exeFile.FullName;
                        x.FileName = exeFile.Name;
                        DataTemp.Add(x);
                    }
                }
            }
            DataList.Clear();
            foreach (DataClass x in DataTemp)
            {
                DataList.Add(x);
            }
        }
        public void SlnFilesInDirs(string dirRoot = "0")
        {
            if (dirRoot == "0")
            {
                //directionary = @"D:\prevrja15";
                dirRoot = @"E:\Honzovo\Škola\SPS\3ITB\VAH\MVS Repos";
                DataList.Clear();
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
                        DataClass x = new DataClass();
                        x.FullPath = file.FullName;
                        x.FileName = file.Name;
                        DataList.Add(x);
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
        public List<FileInfo> FindFilesInDir(string directionary = "0", string fileType = ".exe")
        {
            List<FileInfo> returnData = new List<FileInfo> { };

            //@"D:\prevrja15"
            if (directionary == "0")
            {
                directionary = @"D:\prevrja15\Launcher\Launcher\bin\Debug";
            }

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
        public void CreateFile(string FileName)
        {
            File.Create(FileName);
        }
        public void WriteData(string cesta)
        {
            if (File.Exists(FileName))
            {
                DataClass record;

                record = new DataClass();
                record.FullPath = cesta;
                DataList.Add(record);

                var engine = new FileHelperEngine<DataClass>();
                engine.WriteFile(@"fileTXT.txt", DataList);
            }
            else
            {
                CreateFile(FileName);
            }
        }
        public void RefreshData()
        {
            var engine = new FileHelperEngine<DataClass>();
            DataClass[] res;

            if (File.Exists(FileName))
            {
                res = engine.ReadFile(FileName);
            }
            else
            {
                CreateFile(FileName);
                res = engine.ReadFile(FileName);
            }

            DataList.Clear();
            foreach (var record in res)
            {
                DataList.Add(record);
            }
        }
    }
}
