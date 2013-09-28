using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.SPI
{
    public class SpiModel
    {
        public SpiModel(string spiName)
        {
            SpiName = spiName;
            SpiSettings = new SpiSettings();
        }

        public bool IsEnabled { get; set; }

        public string SpiName { get; private set; }

        public SpiSettings SpiSettings { get; set; }
    }
}
