using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using CodeGenerator;

namespace DataModel
{
    public interface ICodeWizardPlugin
    {
        Dictionary<string,UserControl> CreateUserControl(string name);
        ICodeGenerator CodeGenerator();
    }
}
