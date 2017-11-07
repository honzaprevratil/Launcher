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
        public string ParametrName { get; set; }
        public string Content { get; set; }

        public DataClass(string parametrName, string content)
        {
            this.ParametrName = parametrName;
            this.Content = content;
        }
        public override string ToString()
        {
            return Content;
        }
    }
}
