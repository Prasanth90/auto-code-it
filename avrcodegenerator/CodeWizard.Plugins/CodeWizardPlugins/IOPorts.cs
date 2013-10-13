using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using CodeWizard.DataModel;
using CodeWizard.DataModel.ICodeWizardPlugin;
using CodeWizard.DataModel.PortModel;
using CodeWizard.Plugins.View.IOPort;

namespace CodeWizard.Plugins.CodeWizardPlugins
{
    [Export(typeof(ICodeWizardPlugin))]
    [ExportMetadata(CodeWizardPluginType.General, CodeWizardPluginNames.Port)]
    public class IOPorts : ICodeWizardPlugin
    {
        private ICodeWizardPlugin _portPinPlugin;
        public PluginInfo GetPluginInfo()
        {
            return new PluginInfo()
            {
                Icon = "china",
                Name = CodeWizardPluginNames.Port
            };
        }

        public  Dictionary<string, UserControl> CreateUserControl(string name)
        {
            var portPinplugin = PluginManager.PluginManager.GetPlugins(CodeWizardPluginType.Reusable, CodeWizardPluginNames.PortPin);
            _portPinPlugin = portPinplugin;
            var usercontrols = new Dictionary<string, UserControl>();
            var ioPortModel = new IOPortModel();
            foreach (var port in ioPortModel.Ports)
            {
                var portControl = new PortControl();
                portControl.Port.Header = port.PortName;
                foreach (var pin in port.Pins)
                {
                    var pinControl =portPinplugin.CreateUserControl(string.Format("{0},{1}", port.PortName, pin.PinName));
                    if (pinControl != null)
                    {
                        DockPanel.SetDock(pinControl[pin.PinName], Dock.Top);
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
