using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using CodeGenerator;
using DataModel;
using DataModel.UsarModel;
using CreateRawInput = PeripheralConfig.CodeGeneration.CreateRawInput;
using FilesContentStore = PeripheralConfig.CodeGeneration.FilesContentStore;
using UartCodeGenerator = PeripheralConfig.CodeGeneration.CodeGenerators.UartCodeGenerator;

namespace PeripheralConfig.CodeWizardPlugins
{
    [Export(typeof(ICodeWizardPlugin))]
    [ExportMetadata(CodeWizardPluginType.General, CodeWizardPluginNames.Usart)]
    public class Usart : ICodeWizardPlugin
    {
        private UsartModel _usartModel;
        public Dictionary<string, UserControl> CreateUserControl(string name)
        {
            var userControls = new Dictionary<string, UserControl>();
            var usartModel = new UsartModel();
            _usartModel = usartModel;
            foreach (var usart in usartModel.Usarts)
            {
                userControls.Add(usart.UsartName, new View.Usart.UsartControl(usart));
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
