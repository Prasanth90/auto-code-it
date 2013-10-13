using System.Collections.ObjectModel;

namespace CodeWizard.DataModel.SPI
{
    public class SpiModel
    {

        private ObservableCollection<Spi> _spis = GetSpiis();
        public ObservableCollection<Spi> Spis
        {
            get { return _spis; }
            set { _spis = value; }
        }

        private static ObservableCollection<Spi> GetSpiis()
        {
            var spilist = new ObservableCollection<Spi>();
            var spis =  McuModel.PeripheralInfoProvider.GetSpis();
            foreach (var spi in spis)
            {
                spilist.Add(new Spi(spi.Name));
            }
            return spilist;
        }
    }

    public class Spi
    {
        public Spi(string spiName)
        {
            SpiName = spiName;
            SpiSettings = new SpiSettings();
        }

        public bool IsEnabled { get; set; }

        public string SpiName { get; private set; }

        public SpiSettings SpiSettings { get; set; }
    }
}
