using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SampleTestApplication
{
    public class SpiViewModel
    {
        private ObservableCollection<string> _baudRates;
        private ObservableCollection<string> _spiComModes;
        private ObservableCollection<string> _interuptLevels;
        private ObservableCollection<string> _spiModes;
        private bool _isSpiEnabled;
        private ObservableCollection<string> _dataOrders;
        private string _selectedSpiComMode = string.Empty;

        public bool IsSpiEnabled
        {
            get { return _isSpiEnabled; }
            set { _isSpiEnabled = value; }
        }

        public ObservableCollection<string> BaudRates
        {
            get { return _baudRates; }
            set { _baudRates = value; }
        }

        public ObservableCollection<string> SpiComModes
        {
            get { return _spiComModes; }
            set { _spiComModes = value; }
        }

        public string SelectedSpiComMode
        {
            get { return _selectedSpiComMode; }
            set { _selectedSpiComMode = value; }
        }

        public ObservableCollection<string> SpiModes
        {
            get { return _spiModes; }
            set { _spiModes = value; }
        }

        public ObservableCollection<string> DataOrders
        {
            get { return _dataOrders; }
            set { _dataOrders = value; }
        }

        public ObservableCollection<string> SpiInteruptLevels
        {
            get { return _interuptLevels; }
            set { _interuptLevels = value; }
        }
    }
}
