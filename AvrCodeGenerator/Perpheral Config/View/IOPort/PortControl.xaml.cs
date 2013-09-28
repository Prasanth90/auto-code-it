using System.Windows.Controls;
using PeripheralConfig.View.IOPort.IOPin;
using PeripheralConfig.ViewModel.IOPort;
using PeripheralConfig.ViewModel.IOPort.IOPin;

namespace PeripheralConfig.View.IOPort
{


    /// <summary>
    /// Interaction logic for Port.xaml
    /// </summary>
    public partial class PortControl : UserControl
    {

        public PortControl(PortViewModel portViewModel)
        {
            InitializeComponent();
            Port.Header = portViewModel.Port.PortName;
            foreach (var pin in portViewModel.Port.Pins)
            {
                var pincontrolViewModel = new PinViewModel(pin);
                var pincontrol = new PinControl(pincontrolViewModel);
                DockPanel.SetDock(pincontrol, Dock.Top);
                PinsContainer.Children.Add(pincontrol);
            }
        }
    }

    
}
