using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DataProvider.PeripheralInfoProviders;

namespace DataModel.DataProvider
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
