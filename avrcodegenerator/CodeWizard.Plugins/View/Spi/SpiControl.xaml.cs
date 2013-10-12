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
        public SpiControl(DataModel.SPI.Spi spiModel)
        {
            InitializeComponent();
            this.DataContext = new SpiViewModel(spiModel);
        }
    }
}
