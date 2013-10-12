using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using CodeGenerator;
using DataModel;
using DataModel.PortModel;
using PeripheralConfig.View.IOPort.IOPin;
using PeripheralConfig.ViewModel.IOPort.IOPin;
using CreateRawInput = PeripheralConfig.CodeGeneration.CreateRawInput;
using FilesContentStore = PeripheralConfig.CodeGeneration.FilesContentStore;
using PortCodeGenerator = PeripheralConfig.CodeGeneration.CodeGenerators.PortCodeGenerator;

namespace PeripheralConfig.CodeWizardPlugins
{
    [Export(typeof(ICodeWizardPlugin))]
    [ExportMetadata(CodeWizardPluginType.Reusable, CodeWizardPluginNames.PortPin)]
    public class IOPortPin : ICodeWizardPlugin
    {
        private readonly IOPortModel _ioPortModel;
        public IOPortPin()
        {
            _ioPortModel = new IOPortModel();
        }

        public Dictionary<string, UserControl> CreateUserControl(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            var portPinNames = name.Split(',');
            if (portPinNames.Length == 2)
            {
                string portName = portPinNames[0];
                string pinName = portPinNames[1];
                foreach (var port in _ioPortModel.Ports)
                {
                    if (port.PortName.Equals(portName, StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (var pin in port.Pins)
                        {
                            if (pin.PinName.Equals(pinName, StringComparison.OrdinalIgnoreCase))
                            {
                                var pinViewModel = new PinViewModel(pin);
                                return new Dictionary<string, UserControl>()
                                    {
                                       {pin.PinName,new PinControl(pinViewModel)}
                                    };
                            }
                        }
                    }
                }
            }
            return null;
        }

        public ICodeGenerator CodeGenerator()
        {
            var filesContentStore = new FilesContentStore();
            new CreateRawInput(filesContentStore).LoadResourceFile();
            return new PortCodeGenerator(_ioPortModel, filesContentStore);
        }
    }
}
