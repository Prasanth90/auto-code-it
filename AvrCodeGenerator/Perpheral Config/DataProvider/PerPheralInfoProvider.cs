using System.Collections.Generic;

namespace PeripheralConfig.DataProvider
{
    public  class DataProvider
    {
        public static List<string> GetPorts()
        {
            // TODO: Implement this by parsing the device xml files.
            return new List<string>() { "PORTA", "PORTB", "PORTC", "PORTD", "PORTE", "PORTF", "PORTG" };
        }

        public static List<string> GetPinNames()
        {
            return new List<string>() { "Bit 0", "Bit 1", "Bit 2", "Bit 3", "Bit 4", "Bit 5", "Bit 6", "Bit 7" };
        }
    }
}
