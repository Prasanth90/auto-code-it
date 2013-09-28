using System.ComponentModel;
using System.Collections.ObjectModel;
using DataModel;
using DataModel.PortModel;

namespace PeripheralConfig.ViewModel.IOPort.IOPin
{
    public class PinViewModel : INotifyPropertyChanged
    {
        private readonly Pin _pin;
        private ObservableCollection<string> _directions = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetPinDirections());
        private ObservableCollection<string> _outputValues = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetPinOutputValues());
        private ObservableCollection<string> _outputPullConfigValues = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetOuputPullConfigs());
        private ObservableCollection<string> _inputSenseModes = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetInputSenseModes());

        public PinViewModel(Pin pin)
        {
            _pin = pin;
        }

        public string PinName
        {
            get { return _pin.PinName; }
            set
            {
                _pin.PinName = value;
                OnPropertyChanged("PinName");
            }
        }

        public ObservableCollection<string> Directions
        {
            get { return _directions; }
            set { _directions = value; }
        }

        public ObservableCollection<string> OutputValues
        {
            get { return _outputValues; }
            set { _outputValues = value; }
        }

        public ObservableCollection<string> OutputPullConfigValues
        {
            get { return _outputPullConfigValues; }
            set { _outputPullConfigValues = value; }
        }

        public ObservableCollection<string> InputSenseModes
        {
            get { return _inputSenseModes; }
            set { _inputSenseModes = value; }
        }

        public string SelectedDirection
        {
            get { return _pin.SelectedDirection; }
            set
            {
                _pin.SelectedDirection = value;
                OnPropertyChanged("SelectedDirection");
            }
        }

        public string SelectedOutputValue
        {
            get { return _pin.SelectedOutputValue; }
            set
            {
                _pin.SelectedOutputValue = value;
                OnPropertyChanged("SelectedOutputValue");
            }
        }
     
        public string SelectedOutputPullConfig
        {
            get { return _pin.SelectedOutputPullConfig; }
            set
            {
                _pin.SelectedOutputPullConfig = value;
                OnPropertyChanged("SelectedOutputPullConfig");
            }
        }

        public string SelectedInputSenseMode
        {
            get { return _pin.SelectedInputSenseMode; }
            set
            {
                _pin.SelectedInputSenseMode = value;
                OnPropertyChanged("SelectedInputSenseMode");
            }
        }

        public bool IsInverted
        {
            get { return _pin.IsInverted; }
            set
            {
                _pin.IsInverted = value;
                OnPropertyChanged("IsInverted");
            }
        }

        public bool IsOutputSlRateLimited
        {
            get { return _pin.IsOutputSlRateLimited; }
            set
            {
                _pin.IsOutputSlRateLimited = value;
                OnPropertyChanged("IsOutputSlRateLimited");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            _pin.HasUserConfigured = true;
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
