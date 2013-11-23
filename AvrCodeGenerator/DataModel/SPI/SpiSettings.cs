using System.Collections.ObjectModel;
using System.Linq;

namespace CodeWizard.DataModel.SPI
{
    public class SpiSettings
    {
        public SpiSettings()
        {
            SpiModes = McuModel.PeripheralInfoProvider.GetSupportedSpiModes();
            SpiComModes = McuModel.PeripheralInfoProvider.GetSupportedSpiComModes();
            BaudRates = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetSupportedBaudRates());
            DataOrders = McuModel.PeripheralInfoProvider.GetSupportedDataOrders();
            SpiInteruptLevels = McuModel.PeripheralInfoProvider.GetSupportedSpiInteruptLevels();

            BaudRate = BaudRates.FirstOrDefault();
            SpiMode = SpiModes.FirstOrDefault();
            SpiComMode = SpiComModes.FirstOrDefault();
            DataOrder = DataOrders.FirstOrDefault();
            InterruptLevel = SpiInteruptLevels.FirstOrDefault();
        }

        public string BaudRate { get; set; }

        public string CsPin { get; set; }

        public string CsPort { get; set; }

        public string SpiMode { get; set; }

        public string SpiComMode { get; set; }

        public string DataOrder { get; set; }

        public string InterruptLevel { get; set; }

        public ObservableCollection<string> SpiModes { get; set; }

        public ObservableCollection<string> SpiComModes { get; set; }

        public ObservableCollection<string> BaudRates { get; set; }

        public ObservableCollection<string> DataOrders { get; set; }

        public ObservableCollection<string> SpiInteruptLevels { get; set; }
    }
}
