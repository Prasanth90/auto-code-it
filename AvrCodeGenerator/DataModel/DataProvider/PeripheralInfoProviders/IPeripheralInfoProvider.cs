using System.Collections.Generic;
using System.Collections.ObjectModel;
using CodeWizard.DataModel.PeripheralInfo;

namespace CodeWizard.DataModel.DataProvider.PeripheralInfoProviders
{
    public interface IPeripheralInfoProvider
    {
        ObservableCollection<Peripheral> GetPorts();
        List<string> GetPinsList();
        List<string> GetPinDirections();
        List<string> GetPinOutputValues();
        List<string> GetOuputPullConfigs();
        List<string> GetInputSenseModes();
        string GetInvertedPinMode();
        string GetReducedSlewRateMode();
    }
}
