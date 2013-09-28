using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using WpfApplication1;

namespace DataModel.DataProvider.PeripheralInfoProviders
{
    public class XmegaPeripheralInfoProvider : IPeripheralInfoProvider
    {
        public ObservableCollection<Peripheral> PeripheralsInfo;

        public string McuName { get; set; }

        public XmegaPeripheralInfoProvider(string mcuName)
        {
            McuName = mcuName;
            PeripheralsInfo = GetPerpheralInfo();
        }

        private ObservableCollection<Peripheral> GetPerpheralInfo()
        {
            var peripheralInfo = new Peripheral()
                {
                    new Peripheral()
                        {
                            Icon = "china",
                            Name = PeripheralNames.Spis,
                            ChildPeripherals = GetSpis(),
                        },
                    new Peripheral()
                        {
                            Icon = "uk",
                            Name = PeripheralNames.Usarts,
                            ChildPeripherals = GetUsarts(),
                        },
                    new Peripheral()
                        {
                            Icon = "japan",
                            Name = PeripheralNames.ADC,
                            ChildPeripherals = GetAdcs(),
                        },
                    new Peripheral()
                        {
                            Icon = "somalia",
                            Name = PeripheralNames.DAC,
                            ChildPeripherals = GetDacs(),
                        },
                    new Peripheral()
                        {
                            Icon = "mexico",
                            Name = PeripheralNames.AnalogComparator,
                            ChildPeripherals = GetAnalogComparators(),
                        },
                    new Peripheral()
                        {
                                 Icon = "usa",
                                Name = PeripheralNames.Port,
                                ChildPeripherals = GetPorts()
                        }

                };

            return peripheralInfo;
        }

        public  List<string> GetPinsList()
        {
            return new List<string>() { "0","1","2","3","4","5","6","7" };
        }

        public List<string> GetPinDirections()
        {
            return new List<string>() { "IOPORT_DIR_INPUT", "IOPORT_DIR_OUTPUT" };
        }

        public List<string> GetPinOutputValues()
        {
            return new List<string>(){"true","false"};
        }

        public List<string> GetOuputPullConfigs()
        {
            return new List<string>()
                {
                    "IOPORT_MODE_TOTEM",
                    "IOPORT_MODE_BUSKEEPER",
                    "IOPORT_MODE_PULLDOWN",
                    "IOPORT_MODE_PULLUP",
                    "IOPORT_MODE_WIREDOR",
                    "IOPORT_MODE_WIREDAND",
                    "IOPORT_MODE_WIREDORPULL",
                    "IOPORT_MODE_WIREDANDPULL",
                };
        }

        public List<string> GetInputSenseModes()
        {
            return new List<string>()
                {
                    "IOPORT_SENSE_BOTHEDGES",
                    "IOPORT_SENSE_RISING",   
                    "IOPORT_SENSE_FALLING",  
                };
        }

        public string GetInvertedPinMode()
        {
            return "IOPORT_MODE_INVERT_PIN";
        }

        public string GetReducedSlewRateMode()
        {
            return "IOPORT_MODE_SLEW_RATE_LIMIT";
        }

        public List<string> GetSupportedUsartCharLengths()
        {
            return new List<string>()
                {
                    "USART_CHSIZE_5BIT_gc",
                    "USART_CHSIZE_6BIT_gc",
                    "USART_CHSIZE_7BIT_gc",
                    "USART_CHSIZE_8BIT_gc",
                    "USART_CHSIZE_9BIT_gc"
                };
        }

        public List<string> GetSupportedUsartIntLevels()
        {
            return new List<string>()
                {
                    "USART_INT_LVL_OFF",
                    "USART_INT_LVL_LO ",
                    "USART_INT_LVL_MED",
                    "USART_INT_LVL_HI ",
                };
        }

        public List<string> GetSupportedBaudRates()
        {
            return new List<string>()
                {
                    "1200",
                    "2400",
                    "4800",
                    "9600",
                    "19200",
                    "38400",
                    "57600"
                };
        }

        public List<string> GetSupportedParityModes()
        {
            return new List<string>()
                {
                    "USART_PMODE_DISABLED_gc",
                    "USART_PMODE_EVEN_gc",
                    "USART_PMODE_ODD_gc",
                };
        }

        public List<string> GetSupportedUsartModes()
        {
            return new List<string>()
                {
                    "USART_CMODE_ASYNCHRONOUS_gc",
                    "USART_CMODE_SYNCHRONOUS_gc",
                    "USART_CMODE_IRDA_gc",
                    "USART_CMODE_MSPI_gc"
                };
        }

        public ObservableCollection<Peripheral> GetPorts()
        {
            var pheriperals = new ObservableCollection<Peripheral>
                {
                    new Peripheral()
                        {
                            Name = "PORTA",
                        },
                            new Peripheral()
                        {
                            Name = "PORTB"
                        },
                           new Peripheral()
                        {
                            Name = "PORTC"
                        },
                            new Peripheral()
                        {
                            Name = "PORTD"
                        },
                             new Peripheral()
                        {
                            Name = "PORTE"
                        },
                };
            return pheriperals;
        }

        public ObservableCollection<Peripheral> GetUsarts()
        {
            var pheriperals = new ObservableCollection<Peripheral>
                {
                    new Peripheral()
                        {
                            Name = "USARTC0",
                            Icon = "denmark"
                        },
                };
            return pheriperals;
        }

        public ObservableCollection<Peripheral> GetSpis()
        {
            var pheriperals = new ObservableCollection<Peripheral>
                {
                    new Peripheral()
                        {
                            Name = "SPIC",
                            Icon = "denmark"
                        },
                };
            return pheriperals;
        }

        public ObservableCollection<Peripheral> GetAdcs()
        {
            var pheriperals = new ObservableCollection<Peripheral>
                {
                    new Peripheral()
                        {
                            Name = "ADC0",
                            Icon = "denmark"
                        },

                    new Peripheral()
                        {
                            Name = "ADC1",
                            Icon = "denmark"
                        }
                };
            return pheriperals;
        }

        public ObservableCollection<Peripheral> GetDacs()
        {
            var pheriperals = new ObservableCollection<Peripheral>
                {
                   new Peripheral()
                        {
                            Name = "DAC0",
                            Icon = "denmark"
                        },
                        new Peripheral()
                        {
                            Name = "DAC1",
                            Icon = "denmark"
                        }
                };
            return pheriperals;
        }

        public ObservableCollection<Peripheral> GetTimers()
        {
            var pheriperals = new ObservableCollection<Peripheral>
                {
                     new Peripheral()
                        {
                            Name = "TIMER0",
                            Icon = "denmark"
                        },
                        new Peripheral()
                        {
                            Name = "TIMER1",
                            Icon = "denmark"
                        }
                };
            return pheriperals;
        }

        public ObservableCollection<Peripheral> GetAnalogComparators()
        {
            var pheriperals = new ObservableCollection<Peripheral>
                {
                        new Peripheral()
                        {
                            Name = "AC0",
                            Icon = "denmark"
                        },
                        new Peripheral()
                        {
                            Name = "AC1",
                            Icon = "denmark"
                        }
                };
            return pheriperals;
        }

        public ObservableCollection<string> GetSupportedSpiInteruptLevels()
        {
            return new ObservableCollection<string>();
        }

        public ObservableCollection<string> GetSupportedSpiModes()
        {

            return new ObservableCollection<string>()
                {
                    "SPI_MODE_0",
                    "SPI_MODE_1",
                    "SPI_MODE_2",
                    "SPI_MODE_3",
                };
           
        }

        public ObservableCollection<string> GetSupportedSpiComModes()
        {
            return new ObservableCollection<string>()
                {
                    "master",
                    "slave"
                };
        }

        public ObservableCollection<string> GetSupportedDataOrders()
        {
            return new ObservableCollection<string>();

            return new ObservableCollection<string>()
                {
                    "MSB First",
                    "LSB First",
                };
        }
    }

    

}
