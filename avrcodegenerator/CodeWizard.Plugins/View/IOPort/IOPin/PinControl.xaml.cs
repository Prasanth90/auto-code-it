using System.Windows.Controls;
using PeripheralConfig.ViewModel.IOPort.IOPin;

namespace PeripheralConfig.View.IOPort.IOPin
{
    /// <summary>
    /// Interaction logic for IOPortConfigControl.xaml
    /// </summary>
    public partial class PinControl : UserControl
    {
        public PinControl(PinViewModel pinControlViewModel)
        {
            InitializeComponent();
            this.DataContext = pinControlViewModel;
        }
    }
}
