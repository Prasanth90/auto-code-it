using System.Collections.Generic;
using System.Linq;

namespace CodeWizard.DataModel.PortModel
{
    public class IOPortModel
    {
        private List<Port> _ports = GetPorts();
        public List<Port> Ports
        {
            get { return _ports; }
            set { _ports = value; }
        }

        private static List<Port> GetPorts()
        {
            var portNames = McuModel.PeripheralInfoProvider.GetPorts();
            return portNames.Select(port => new Port { PortName = port.Name }).ToList();
        }
    }
    /// <summary>
    /// Object which comprises all the pins of a particular port
    /// </summary>
    public class Port
    {
        public string PortName { get; set; }

        private List<Pin> _pins = GetPins();
        public List<Pin> Pins
        {
            get { return _pins; }
            set { _pins = value; }
        }

        private static List<Pin> GetPins()
        {
            var pinNums = McuModel.PeripheralInfoProvider.GetPinsList();
            return pinNums.Select(pinNum => new Pin { PinNumber = pinNum, PinName = "PIN_" + pinNum }).ToList();
        }
    }

    public class Pin
    {

        public bool HasUserConfigured { get; set; }

        public string PinName
        {
            get;
            set;
        }

        public string PinNumber { get; set; }


        public string SelectedDirection
        {
            get;
            set;
        }



        public string SelectedOutputValue
        {
            get;
            set;
        }



        public string SelectedOutputPullConfig
        {
            get;
            set;
        }



        public string SelectedInputSenseMode
        {
            get;
            set;
        }

        public bool IsInverted
        {
            get;
            set;
        }

        public bool IsOutputSlRateLimited
        {
            get;
            set;
        }
    }

    
}
