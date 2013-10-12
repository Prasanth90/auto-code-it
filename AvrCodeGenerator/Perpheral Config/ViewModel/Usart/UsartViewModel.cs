using System.Collections.ObjectModel;
using DataModel;
using DataModel.UsarModel;

namespace PeripheralConfig.ViewModel.Usart
{
    public class UsartViewModel : ViewModelBase
    {
        private readonly DataModel.UsarModel.Usart _usartModel;

        public UsartViewModel(DataModel.UsarModel.Usart usartModel)
        {
            _usartModel = usartModel;
            Modes = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetSupportedUsartModes());
            DataBitLengths = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetSupportedUsartCharLengths());
            BaudRates = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetSupportedBaudRates());
            ParityModes = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetSupportedParityModes());
            InteruptLevels = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetSupportedUsartIntLevels());
            Demos = new ObservableCollection<string>();
        }

        private ObservableCollection<string> _modes;
        private ObservableCollection<string> _dataBitLengths;
        private ObservableCollection<string> _baudRates;
        private ObservableCollection<string> _parityModes;
        private ObservableCollection<string> _interuptLevels;
        private ObservableCollection<string> _demos;
        public ObservableCollection<string> Modes
        {
            get { return _modes; }
            set
            {
                _modes = value;
            }
        }

        public ObservableCollection<string> DataBitLengths
        {
            get { return _dataBitLengths; }
            set
            {
                _dataBitLengths = value;
            }
        }

        public ObservableCollection<string> BaudRates
        {
            get { return _baudRates; }
            set { _baudRates = value; }
        }

        public ObservableCollection<string> ParityModes
        {
            get { return _parityModes; }
            set { _parityModes = value; }
        }

        public ObservableCollection<string> InteruptLevels
        {
            get { return _interuptLevels; }
            set { _interuptLevels = value; }
        }

        public ObservableCollection<string> Demos
        {
            get { return _demos; }
            set { _demos = value; }
        }

        public string SelectedMode
        {
            get { return _usartModel.UsartSettings.SelectedMode; }
            set
            {
                _usartModel.UsartSettings.SelectedMode = value;
                OnPropertyChanged("SelectedMode");
            }
        }

        public string SelectedDataBitLength
        {
            get { return _usartModel.UsartSettings.SelectedDataBitLength; }
            set
            {
                _usartModel.UsartSettings.SelectedDataBitLength = value;
            OnPropertyChanged("SelectedDataBitLength");
            }
        }

        public string SelectedBaudRate
        {
            get { return _usartModel.UsartSettings.SelectedBaudRate; }
            set
            {
                _usartModel.UsartSettings.SelectedBaudRate = value;
            OnPropertyChanged("SelectedBaudRate");
            }
        }

        public string SelectedParityMode
        {
            get { return _usartModel.UsartSettings.SelectedParityMode; }
            set
            {
                _usartModel.UsartSettings.SelectedParityMode = value;
            OnPropertyChanged("SelectedParityMode");
            }
        }

        public bool IsTwoStopBits
        {
            get { return _usartModel.UsartSettings.IsTwoStopBits; }
            set { _usartModel.UsartSettings.IsTwoStopBits = value;
            OnPropertyChanged("IsTwoStopBits");
            }
        }

        public bool RxCompleteIntEnabled
        {
            get { return _usartModel.UsartSettings.RxCompleteIntEnabled; }
            set
            {
                _usartModel.UsartSettings.RxCompleteIntEnabled = value;
            OnPropertyChanged("RxCompleteIntEnabled");
            }
        }

        public bool TxCompleteIntEnabled
        {
            get { return _usartModel.UsartSettings.TxCompleteIntEnabled; }
            set
            {
                _usartModel.UsartSettings.TxCompleteIntEnabled = value;
            OnPropertyChanged("TxCompleteIntEnabled");
            }
        }

        public bool DataReceivedIntEnabled
        {
            get { return _usartModel.UsartSettings.DataReceivedIntEnabled; }
            set
            {
                _usartModel.UsartSettings.DataReceivedIntEnabled = value;
            OnPropertyChanged("DataReceivedIntEnabled");
            }
        }

        public string SelectedRxInteruptLevel
        {
            get { return _usartModel.UsartSettings.SelectedRxInteruptLevel; }
            set
            {
                _usartModel.UsartSettings.SelectedRxInteruptLevel = value;
            OnPropertyChanged("SelectedRxInteruptLevel");
            }
        }

        public string SelectedTxInteruptLevel
        {
            get { return _usartModel.UsartSettings.SelectedTxInteruptLevel; }
            set
            {
                _usartModel.UsartSettings.SelectedTxInteruptLevel = value;
            OnPropertyChanged("SelectedTxInteruptLevel");
            }
        }

        public string SelectedDreInteruptLevel
        {
            get { return _usartModel.UsartSettings.SelectedDreInteruptLevel; }
            set
            {
                _usartModel.UsartSettings.SelectedDreInteruptLevel = value;
            OnPropertyChanged("SelectedDreInteruptLevel");
            }
        }

        public string SelectedDemo
        {
            get { return _usartModel.UsartSettings.SelectedDemo; }
            set
            {
                _usartModel.UsartSettings.SelectedDemo = value;
            OnPropertyChanged("SelectedDemo");
            }
        }
    }
}
