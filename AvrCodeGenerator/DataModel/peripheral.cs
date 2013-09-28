using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WpfApplication1
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
