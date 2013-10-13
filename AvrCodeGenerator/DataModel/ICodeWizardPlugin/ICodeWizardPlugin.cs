using System.Collections.Generic;
using System.Windows.Controls;

namespace CodeWizard.DataModel.ICodeWizardPlugin
{
    public interface ICodeWizardPlugin
    {
        PluginInfo GetPluginInfo();
        Dictionary<string,UserControl> CreateUserControl(string name);
        ICodeGenerator CodeGenerator();
    }

    public class PluginInfo
    {
        public string Name { get; set; }
        public string Icon { get; set; }

    }
}
