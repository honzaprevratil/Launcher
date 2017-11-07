using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    [DelimitedRecord(";")]
    public sealed class PathClass
    {
        public string FullPath { get; set; }
        public string FileName { get; set; }

        public PathClass() { }
        public PathClass(string fullPath)
        {
            this.FullPath = fullPath;
        }
        public PathClass(string fullPath, string fileName)
        {
            this.FullPath = fullPath;
            this.FileName = fileName;
        }
        public override string ToString()
        {
            return FullPath;
            //return FullPath + "    Název: " + FileName;
        }
    }
}
