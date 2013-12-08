using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using CodeWizard.DataModel;
using CodeWizard.DataModel.ICodeWizardPlugin;
using CodeWizard.DataModel.UsarModel;
using CodeWizard.Plugins.CodeGeneration;
using CodeWizard.Plugins.CodeGeneration.CodeGenerators;
using CodeWizard.Plugins.View.Usart;

namespace CodeWizard.Plugins.CodeWizardPlugins
{
    [Export(typeof(ICodeWizardPlugin))]
    [ExportMetadata(CodeWizardPluginType.General, CodeWizardPluginNames.Usart)]
    public class Usart : ICodeWizardPlugin
    {
        private UsartModel _usartModel;
        public PluginInfo GetPluginInfo()
        {
            return new PluginInfo()
            {
                Icon = "somalia",
                Name = CodeWizardPluginNames.Usart
            };
        }

        public Dictionary<string, UserControl> CreateUserControl(string name)
        {
            var userControls = new Dictionary<string, UserControl>();
            var usartModel = new UsartModel();
            _usartModel = usartModel;
            foreach (var usart in usartModel.Usarts)
            {
                userControls.Add(usart.UsartName, new UsartControl(usart));
            }
            return userControls;
        }

        public ICodeGenerator CodeGenerator()
        {
            var filesContentStore = new FilesContentStore();
            new CreateRawInput(filesContentStore).LoadResourceFile();
            return new UartCodeGenerator(_usartModel, filesContentStore);
        }

    }
}
