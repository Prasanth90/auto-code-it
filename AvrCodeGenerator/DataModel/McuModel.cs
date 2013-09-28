using System.Collections.ObjectModel;
using DataModel.DataProvider;
using DataModel.DataProvider.PeripheralInfoProviders;
using DataModel.PortModel;
using DataModel.SPI;
using DataModel.UsarModel;

namespace DataModel
{
    public class McuModel
    {
        public McuModel(string mcuName)
        {
            PeripheralInfoProvider = new XmegaPeripheralInfoProvider(mcuName);
            IOPortModel = new IOPortModel();
            UsartModels = GetUsartModels();
            SpiModels = GetSpiModels();
        }

        private ObservableCollection<SpiModel> GetSpiModels()
        {
            var spiModels = new ObservableCollection<SpiModel>();
            foreach (var spi in PeripheralInfoProvider.GetSpis())
            {
                spiModels.Add(new SpiModel(spi.Name));
            }
            return spiModels;
        }

        private ObservableCollection<UsartModel> GetUsartModels()
        {
            var usarModels = new ObservableCollection<UsartModel>();
            foreach (var usart in PeripheralInfoProvider.GetUsarts())
            {
                usarModels.Add(new UsartModel(usart.Name));
            }
            return usarModels;
        }

        public static XmegaPeripheralInfoProvider PeripheralInfoProvider { get; set; }
        public IOPortModel IOPortModel { get; set; }
        public ObservableCollection<UsartModel> UsartModels { get; set; }
        public ObservableCollection<SpiModel> SpiModels { get; set; } 
    }
}
