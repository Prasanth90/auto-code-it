using System;
using System.Collections.Generic;
using System.Windows.Controls;
using CodeWizard.DataModel.ICodeWizardPlugin;

namespace CodeWizard.Plugins.CodeWizardPlugins
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
