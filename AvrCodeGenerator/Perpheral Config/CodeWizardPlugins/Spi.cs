using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using CodeGenerator;
using CodeGenerator.CodeGenerators;
using DataModel;
using DataModel.SPI;
using PeripheralConfig.View.Spi;
using CreateRawInput = PeripheralConfig.CodeGeneration.CreateRawInput;
using FilesContentStore = PeripheralConfig.CodeGeneration.FilesContentStore;
using SpiCodeGenerator = PeripheralConfig.CodeGeneration.CodeGenerators.SpiCodeGenerator;

namespace PeripheralConfig.CodeWizardPlugins
{
    [Export(typeof(ICodeWizardPlugin))]
    [ExportMetadata(CodeWizardPluginType.General, CodeWizardPluginNames.Spi)]
    public class Spi : ICodeWizardPlugin
    {
        private SpiModel _spiModel;
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
