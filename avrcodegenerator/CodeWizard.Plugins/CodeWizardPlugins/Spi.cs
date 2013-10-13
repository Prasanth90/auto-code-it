using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using CodeWizard.DataModel;
using CodeWizard.DataModel.ICodeWizardPlugin;
using CodeWizard.DataModel.SPI;
using CodeWizard.Plugins.CodeGeneration;
using CodeWizard.Plugins.CodeGeneration.CodeGenerators;
using CodeWizard.Plugins.View.Spi;

namespace CodeWizard.Plugins.CodeWizardPlugins
{
    [Export(typeof(ICodeWizardPlugin))]
    [ExportMetadata(CodeWizardPluginType.General, CodeWizardPluginNames.Spi)]
    public class Spi : ICodeWizardPlugin
    {
        private SpiModel _spiModel;
        public PluginInfo GetPluginInfo()
        {
            return new PluginInfo()
            {
                Icon = "brazil",
                Name = CodeWizardPluginNames.Spi
            };
        }

        public Dictionary<string, UserControl> CreateUserControl(string name)
        {
            var userControls = new Dictionary<string, UserControl>();
            var spiModel = new SpiModel();
            _spiModel = spiModel;
            foreach (var spi in spiModel.Spis)
            {
                userControls.Add(spi.SpiName,new SpiControl(spi));
            }
            return userControls;
        }

        public ICodeGenerator CodeGenerator()
        {
            var filesContentStore = new FilesContentStore();
            new CreateRawInput(filesContentStore).LoadResourceFile();
            return new SpiCodeGenerator(_spiModel, filesContentStore);
        }
    }
}
