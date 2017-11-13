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

        public DataClass() { }
        public DataClass(string parametrName, string content)
        {
            this.ParametrName = parametrName;
            this.Content = content;
        }
        public override string ToString()
        {
            return Content;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            DataClass objAsPart = obj as DataClass;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public bool Equals(DataClass other)
        {
            if (other == null) return false;
            return (this.Content.Equals(other.Content));
        }
    }
}
