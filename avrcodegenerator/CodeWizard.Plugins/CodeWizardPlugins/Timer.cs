using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using CodeGenerator;
using DataModel;
using DataModel.ICodeWizardPlugin;

namespace PeripheralConfig.CodeWizardPlugins
{
    class Timer : ICodeWizardPlugin
    {
        public PluginInfo GetPluginInfo()
        {
            throw new NotImplementedException();
        }

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
