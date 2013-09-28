using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DataModel;
using DataModel.SPI;

namespace PeripheralConfig.ViewModel.Spi
{
    public class SpiViewModel : ViewModelBase
    {
        private readonly SpiModel _spiModel;

        public SpiViewModel(SpiModel spiModel)
        {
            _spiModel = spiModel;
            SpiModes = McuModel.PeripheralInfoProvider.GetSupportedSpiModes();
            SpiComModes = McuModel.PeripheralInfoProvider.GetSupportedSpiComModes();
            BaudRates = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetSupportedBaudRates());
            DataOrders = McuModel.PeripheralInfoProvider.GetSupportedDataOrders();
            SpiInteruptLevels = McuModel.PeripheralInfoProvider.GetSupportedSpiInteruptLevels();
            Ports = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetPorts().Select(p => p.Name));
            Pins = new ObservableCollection<string>(McuModel.PeripheralInfoProvider.GetPinsList());
        }

        public bool IsSpiEnabled
        {
            get { return _spiModel.IsEnabled; }
            set
            {
                _spiModel.IsEnabled = value;
                OnPropertyChanged("IsSpiEnabled");
            }
        }

        public ObservableCollection<string> SpiModes { get; set; }

        public ObservableCollection<string> SpiComModes { get; set; }

        public ObservableCollection<string> BaudRates { get; set; }

        public ObservableCollection<string> DataOrders { get; set; }

        public ObservableCollection<string> SpiInteruptLevels { get; set; }

        public string SelectedSpiMode
        {
            get { return _spiModel.SpiSettings.SpiMode; }
            set
            {
                _spiModel.SpiSettings.SpiMode = value;
                OnPropertyChanged("SelectedSpiMode");
            }
        }

        public string SelectedBaudRate
        {
            get { return _spiModel.SpiSettings.BaudRate; }
            set
            {
                _spiModel.SpiSettings.BaudRate = value;
                OnPropertyChanged("SelectedBaudRate");
            }
        }

        public string SelectedSpiComMode
        {
            get { return _spiModel.SpiSettings.SpiComMode; }
            set
            {
                _spiModel.SpiSettings.SpiComMode = value;
                OnPropertyChanged("SelectedSpiComMode");
            }
        }

        public string SelectedInterruptLevel
        {
            get { return _spiModel.SpiSettings.InterruptLevel; }
            set
            {
                _spiModel.SpiSettings.InterruptLevel = value;
            this.OnPropertyChanged("SelectedInterruptLevel");
            }
        }

        public string SelectedDataOrder
        {
            get { return _spiModel.SpiSettings.DataOrder; }
            set
            {
                _spiModel.SpiSettings.DataOrder = value;
                OnPropertyChanged("SelectedDataOrder");
            }
        }

        public ObservableCollection<string> Ports { get; set; }

        public ObservableCollection<string> Pins { get; set; }

        public string SelectedCsPort
        {
            get { return _spiModel.SpiSettings.CsPort; }
            set
            {
                _spiModel.SpiSettings.CsPort = value;
                OnPropertyChanged("SelectedCsPort");
            }
        }

        public string SelectedCsPin
        {
            get { return _spiModel.SpiSettings.CsPin; }
            set
            {
                _spiModel.SpiSettings.CsPin = value;
                OnPropertyChanged("SelectedCsPin");
            }
        }
    }
}
