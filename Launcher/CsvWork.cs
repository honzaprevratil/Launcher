using FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Launcher
{
    public class CsvWork
    {
        public List<DataClass> AboutProjData = new List<DataClass>();
        public string CsvFileName = @"aboutProj.csv";
        private FileHelperEngine<DataClass> engine = new FileHelperEngine<DataClass>();

        public void ReadCsv()
        {
            DataClass[] res;

            CheckCsvFile();
            res = engine.ReadFile(CsvFileName);

            AboutProjData.Clear();
            foreach (var record in res)
            {
                AboutProjData.Add(record);
            }
        }
        public void WriteCsv(string writeData)
        {
            writeData = writeData.Replace(';',',');
            DataClass newRow = new DataClass((string)Application.Current.Properties["path"], writeData);
            try
            {
                AboutProjData.RemoveAll(x => x.ParametrName.Contains( (string)Application.Current.Properties["path"]) );
                AboutProjData.Add(newRow);
            }
            catch
            {
                AboutProjData.Add(newRow);
            }
            CheckCsvFile();
            engine.WriteFile(CsvFileName, AboutProjData);
        }
        public void CheckCsvFile()
        {
            if (!File.Exists(CsvFileName))
            {
                FileStream x = File.Create(CsvFileName);
                x.Close();
            }
        }
    }
}
