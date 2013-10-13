using System.Collections.ObjectModel;

namespace CodeWizard.DataModel.PeripheralInfo
{
    public class Peripheral : ObservableCollection<Peripheral>
    {
        public Peripheral()
        {
            Name = "";
            Icon = "";
        }

        private ObservableCollection<Peripheral> _childPeripherals = new ObservableCollection<Peripheral>();
        public string Name { get; set; }

        public string Icon { get; set; }

        public ObservableCollection<Peripheral> ChildPeripherals
        {
            get { return _childPeripherals; }
            set { _childPeripherals = value; }
        }
    }
}
