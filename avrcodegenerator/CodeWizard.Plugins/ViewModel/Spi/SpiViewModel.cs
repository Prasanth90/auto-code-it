﻿using System.Collections.ObjectModel;
using System.Linq;
using CodeWizard.DataModel;

namespace CodeWizard.Plugins.ViewModel.Spi
{
    public class SpiViewModel : ViewModelBase
    {
        private readonly CodeWizard.DataModel.SPI.Spi _spiModel;

        public SpiViewModel(CodeWizard.DataModel.SPI.Spi spiModel)
        {
            _spiModel = spiModel;
        }

        public string SpiName
        {
            get { return _spiModel.SpiName; }
        }

        public ObservableCollection<string> SpiModes
        {
            get { return _spiModel.SpiSettings.SpiModes; }
            set { _spiModel.SpiSettings.SpiModes = value; }
        }

        public ObservableCollection<string> SpiComModes
        {
            get { return _spiModel.SpiSettings.SpiComModes; }
            set { _spiModel.SpiSettings.SpiComModes = value; }
        }

        public ObservableCollection<string> BaudRates
        {
            get { return _spiModel.SpiSettings.BaudRates; }
            set { _spiModel.SpiSettings.BaudRates = value; }
        }

        public ObservableCollection<string> DataOrders
        {
            get { return _spiModel.SpiSettings.DataOrders; }
            set { _spiModel.SpiSettings.DataOrders = value; }
        }

        public ObservableCollection<string> SpiInteruptLevels
        {
            get { return _spiModel.SpiSettings.SpiInteruptLevels; }
            set { _spiModel.SpiSettings.SpiInteruptLevels = value; }
        }

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

        public ObservableCollection<string> Ports
        {
            get { return _spiModel.SpiSettings.Ports; }
            set { _spiModel.SpiSettings.Ports = value; }
        }

        public ObservableCollection<string> Pins
        {
            get { return _spiModel.SpiSettings.Pins; }
            set { _spiModel.SpiSettings.Pins = value; }
        }

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
