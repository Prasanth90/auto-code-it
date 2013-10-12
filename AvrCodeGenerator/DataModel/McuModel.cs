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
        }

        public static XmegaPeripheralInfoProvider PeripheralInfoProvider { get; set; }
    }
}
