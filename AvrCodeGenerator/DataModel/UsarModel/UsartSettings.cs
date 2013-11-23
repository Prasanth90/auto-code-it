using System.Collections.ObjectModel;
using System.Linq;

namespace CodeWizard.DataModel.UsarModel
{
    public class UsartSettings
    {

        public UsartSettings()
        {
            Modes = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetSupportedUsartModes());
            DataBitLengths = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetSupportedUsartCharLengths());
            BaudRates = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetSupportedBaudRates());
            ParityModes = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetSupportedParityModes());
            InteruptLevels = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetSupportedUsartIntLevels());
            SelectedMode = Modes.FirstOrDefault();
            SelectedDataBitLength = DataBitLengths.FirstOrDefault();
            SelectedBaudRate = BaudRates.FirstOrDefault();
            SelectedParityMode = ParityModes.FirstOrDefault();
            SelectedRxInteruptLevel = InteruptLevels[1];         
            SelectedTxInteruptLevel = InteruptLevels[1];
            SelectedDreInteruptLevel = InteruptLevels[1];
        }

        public ObservableCollection<string> Modes { get; set; }

        public ObservableCollection<string> DataBitLengths { get; set; }

        public ObservableCollection<string> BaudRates { get; set; }

        public ObservableCollection<string> ParityModes { get; set; }

        public ObservableCollection<string> InteruptLevels { get; set; }

        public ObservableCollection<string> Demos { get; set; }

        public string SelectedMode
        {
            get;
            set;
        }

        public string SelectedDataBitLength
        {
            get;
            set;
        }

        public string SelectedBaudRate
        {
            get;
            set;
        }

        public string SelectedParityMode
        {
            get;
            set;
        }

        public bool IsTwoStopBits { get; set; }

        public bool RxCompleteIntEnabled
        {
            get;
            set;
        }

        public bool TxCompleteIntEnabled
        {
            get;
            set;
        }

        public bool DataReceivedIntEnabled
        {
            get;
            set;
        }

        public string SelectedRxInteruptLevel
        {
            get;
            set;
        }

        public string SelectedTxInteruptLevel
        {
            get;
            set;
        }

        public string SelectedDreInteruptLevel
        {
            get;
            set;
        }

        public string SelectedDemo
        {
            get;
            set;
        }
    }
}
