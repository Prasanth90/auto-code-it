using System.ComponentModel;
using System.Collections.ObjectModel;
using CodeWizard.DataModel;
using CodeWizard.DataModel.PortModel;

namespace CodeWizard.Plugins.ViewModel.IOPort.IOPin
{
    public class PinViewModel : INotifyPropertyChanged
    {
        private readonly Pin _pin;

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

        public bool IsEnabled
        {
            get { return _pin.HasUserConfigured; }
            set
            {
                _pin.HasUserConfigured = value;
                this.OnPropertyChanged("IsEnabled");
            }
        }

        public ObservableCollection<string> Directions
        {
            get { return _pin.Directions; }
            set { _pin.Directions = value; }
        }

        public ObservableCollection<string> OutputValues
        {
            get { return _pin.OutputValues; }
            set { _pin.OutputValues = value; }
        }

        public ObservableCollection<string> OutputPullConfigValues
        {
            get { return _pin.OutputPullConfigValues; }
            set { _pin.OutputPullConfigValues = value; }
        }

        public ObservableCollection<string> InputSenseModes
        {
            get { return _pin.InputSenseModes; }
            set { _pin.InputSenseModes = value; }
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
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
