using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using CodeGenerator;
using DataModel;
using DataModel.PortModel;
using PeripheralConfig.View.IOPort;

namespace PeripheralConfig.CodeWizardPlugins
{
    [Export(typeof(ICodeWizardPlugin))]
    [ExportMetadata(CodeWizardPluginType.General, CodeWizardPluginNames.Port)]
    public class IOPorts : ICodeWizardPlugin
    {
        private ICodeWizardPlugin _portPinPlugin;
        public  Dictionary<string, UserControl> CreateUserControl(string name)
        {
            var portPinplugin = PluginManager.PluginManager.GetPlugins(CodeWizardPluginType.Reusable, CodeWizardPluginNames.PortPin);
            var usercontrols = new Dictionary<string, UserControl>();
            var ioPortModel = new IOPortModel();
            foreach (var port in ioPortModel.Ports)
            {
                var portControl = new PortControl();
                foreach (var pin in port.Pins)
                {
                    var pinControl =portPinplugin.CreateUserControl(string.Format("{0},{1}", port.PortName, pin.PinName));
                    if (pinControl != null)
                    {
                        portControl.PinsContainer.Children.Add(pinControl[pin.PinName]);
                    }
                }
                usercontrols.Add(port.PortName, portControl);
            }
            return usercontrols;
        }

        public ICodeGenerator CodeGenerator()
        {
           return _portPinPlugin.CodeGenerator();
        }
    }
}
