using System.Windows.Controls;
using DataModel.SPI;
using PeripheralConfig.ViewModel.Spi;

namespace PeripheralConfig.View.Spi
{
    /// <summary>
    /// Interaction logic for SpiControl.xaml
    /// </summary>
    public partial class SpiControl : UserControl
    {
        public SpiControl(SpiModel spiModel)
        {
            InitializeComponent();
            this.DataContext = new SpiViewModel(spiModel);
        }
    }
}
