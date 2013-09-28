using DataModel.PortModel;

namespace PeripheralConfig.ViewModel.IOPort
{
    public class PortViewModel
    {
        public Port Port;
        public PortViewModel(Port port)
        {
            Port = port;
        }
    }
}
