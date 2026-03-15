using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.CLI
{
    public class NamedObject
    {
        public NamedObject(string name)
        {
            ReportNum = name;
        }

        public string ReportNum
        {
            get; set;
        }
    }
}