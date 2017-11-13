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
    class DataWork
    {
        public ObservableCollection<DataClass> DataList = new ObservableCollection<DataClass> { };
        public List<DataClass> AllData = new List<DataClass>();
        public string FileName = @"cesty.txt";

        public void FindAllExeFiles()
        {
            AllData.Clear();
            foreach (DataClass Cesta in DataList)
            {
                string cestaStr = Cesta.FullPath.ToString();
                string nazevStr = Cesta.FileName.ToString();
                string slozka = nazevStr.Substring(0, nazevStr.Length - 4);
                // string cestaStrBin = cestaStr.Substring(0, cestaStr.Length - 4) + "\\bin";
                string csprojFile = cestaStr.Substring(0, cestaStr.Length - 4) + "\\" + slozka + ".csproj";

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(csprojFile);
                XmlNamespaceManager mgr = new XmlNamespaceManager(xmldoc.NameTable);

                foreach (XmlNode item in xmldoc.SelectNodes("OutputPath[last()]", mgr))
                {
                    string exePath = cestaStr + "\\" + item.InnerText.ToString()+".exe"; //fix the path !! 
                    FileInfo file = new FileInfo(exePath);
                    DataClass x = new DataClass();
                    x.FullPath = file.FullName;
                    x.FileName = file.Name;
                    AllData.Add(x);
                }
            }
            DataList.Clear();
            foreach (DataClass Cesta in AllData)
            {
                DataList.Add(Cesta);
            }
        }
        public void FindAllSlnFilesInDirs(string directionary = "0")
        {
            var engine = new FileHelperEngine<DataClass>();
            DataClass[] res;
            List<String> returnData = new List<String> { };

            //@"D:\prevrja15"
            if (directionary == "0")
            {
                directionary = @"D:\prevrja15";
            }
            else
            {
                res = engine.ReadFile(FileName);
                directionary = res[0].FullPath;
            }

            DataList.Clear();
            DirectoryInfo[] dirs = new DirectoryInfo(directionary).GetDirectories(); //všechny podsložky zadané složky
            foreach(DirectoryInfo oneDir in dirs)// projde podsložky
            {
                List<FileInfo> slnFilesInDir = FindAllTypeFilesInDir(oneDir.FullName, ".sln");// sln soubory v dané podsložce
                if (slnFilesInDir.Any())
                {
                    foreach (FileInfo file in slnFilesInDir) // pro každý sln
                    {
                        DataClass x = new DataClass();
                        x.FullPath = file.FullName;
                        x.FileName = file.Name;
                        DataList.Add(x);
                    }
                }
                else
                {
                    foreach ( DirectoryInfo oneDirInDir in oneDir.GetDirectories()) // projde pod-podsložky
                    {
                        slnFilesInDir = FindAllTypeFilesInDir(oneDirInDir.FullName, ".sln");
                        foreach (FileInfo file in slnFilesInDir) // všechny soubory v pod-podsložkách
                        {
                            DataClass x = new DataClass();
                            x.FullPath = file.FullName;
                            x.FileName = file.Name;
                            DataList.Add(x);
                        }

                    }
                }
            }
        }
        public List<FileInfo> FindAllTypeFilesInDir(string directionary = "0", string type = ".exe")
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
            .Where(item => item.Extension == type)
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
                AllData.Add(record);

                var engine = new FileHelperEngine<DataClass>();
                engine.WriteFile(@"fileTXT.txt", AllData);
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
            AllData.Clear();
            foreach (var record in res)
            {
                DataList.Add(record);
                AllData.Add(record);
            }
        }
    }
}
