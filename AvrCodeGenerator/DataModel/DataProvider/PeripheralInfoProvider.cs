using CodeWizard.DataModel.DataProvider.PeripheralInfoProviders;

namespace CodeWizard.DataModel.DataProvider
{
    public class PeripheralInfoProvider
    {
        private readonly string _mcuName;

        public PeripheralInfoProvider(string mcuName)
        {
            _mcuName = mcuName;
        }

        public IPeripheralInfoProvider GetProvider()
        {
            return new XmegaPeripheralInfoProvider(_mcuName);
        }
    }
}
