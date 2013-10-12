using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel
{
    public class ModuleInfo
    {
        public ModuleInfo(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
