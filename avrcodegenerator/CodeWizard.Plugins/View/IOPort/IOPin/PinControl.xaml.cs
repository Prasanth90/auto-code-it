using System.Windows.Controls;
using CodeWizard.Plugins.ViewModel.IOPort.IOPin;

namespace CodeWizard.Plugins.View.IOPort.IOPin
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
