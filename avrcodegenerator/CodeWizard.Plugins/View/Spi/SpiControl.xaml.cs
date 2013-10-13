using System.Windows.Controls;
using CodeWizard.Plugins.ViewModel.Spi;

namespace CodeWizard.Plugins.View.Spi
{
    /// <summary>
    /// Interaction logic for SpiControl.xaml
    /// </summary>
    public partial class SpiControl : UserControl
    {
        public SpiControl(CodeWizard.DataModel.SPI.Spi spiModel)
        {
            InitializeComponent();
            this.DataContext = new SpiViewModel(spiModel);
        }
    }
}
