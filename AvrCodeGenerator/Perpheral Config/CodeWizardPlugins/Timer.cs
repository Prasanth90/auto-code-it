using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using CodeGenerator;
using DataModel;

namespace PeripheralConfig.CodeWizardPlugins
{
    class Timer : ICodeWizardPlugin
    {
        public Dictionary<string, UserControl> CreateUserControl(string name)
        {
            throw new NotImplementedException();
        }

        public ICodeGenerator CodeGenerator()
        {
            throw new NotImplementedException();
        }
    }
}
