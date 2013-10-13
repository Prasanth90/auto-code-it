using CodeWizard.DataModel.DataProvider.PeripheralInfoProviders;

namespace CodeWizard.DataModel
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
