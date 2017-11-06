using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    [DelimitedRecord(";")]
    public sealed class DataClass
    {
        public String FullPath { get; set; }
        public String FileName { get; set; }
        public override string ToString()
        {
            return FullPath;
            //return FullPath + "    Název: " + FileName;
        }
    }
}
