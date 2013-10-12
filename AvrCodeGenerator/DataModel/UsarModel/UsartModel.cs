using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DataModel.SPI;

namespace DataModel.UsarModel
{

    public class UsartModel
    {
       private ObservableCollection<Usart> _usarts = GetUsarts();
        public ObservableCollection<Usart> Usarts
        {
            get { return _usarts; }
            set {_usarts = value; }
        }

        private static ObservableCollection<Usart> GetUsarts()
        {
            var usartlist = new ObservableCollection<Usart>();
            var usarts =  McuModel.PeripheralInfoProvider.GetUsarts();
            foreach (var usart in usarts)
            {
                usartlist.Add(new Usart(usart.Name));
            }
            return usartlist;
        }

    }
    public class Usart
    {
        public Usart(string usartName)
        {
            this.UsartName = usartName;
            UsartSettings = new UsartSettings();
        }
        public string UsartName { get; set; }

        public UsartSettings UsartSettings { get; set; }
    }
}
