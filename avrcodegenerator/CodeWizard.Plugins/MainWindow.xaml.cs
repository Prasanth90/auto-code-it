using System.IO;
using System.Linq;
using System.Windows;
using DataModel;
using MahApps.Metro.Controls;
using PeripheralConfig.View.Usart;
using PeripheralConfig.ViewModel.Usart;

namespace PeripheralConfig
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly McuModel _mcuModel;
        public MainWindow()
        {
            InitializeComponent();
             _mcuModel = new McuModel("xmega128a1");
            var control = new Usart(new UsartViewModel(_mcuModel.UsartModels.FirstOrDefault()));
            HostControl.Children.Add(control);
            //List<Port> ports = _mcuModel.IOPortModel.Ports;
            //foreach (Port port in ports)
            //{
            //    if (port.PortName.Equals("PORTB"))
            //    {
            //        var portviewModel = new PortViewModel(port);
            //        var control = new PortControl(portviewModel);
            //        DockPanel.SetDock(control, Dock.Top);
            //        HostControl.Children.Add(control);
            //    }
            //}
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(McuModel));
            using (var filestream = new StreamWriter("D:\\MyStore.xml"))
            {
                x.Serialize(filestream, _mcuModel);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var codegenerator = new CodeGenerator.CodeGenerator(this._mcuModel);
            var t = codegenerator.GetGeneratedCode();
            using (StreamWriter streamWriter = new StreamWriter(@"D:\dummy.c"))
            {
                streamWriter.Write(t);
            }
        }
    }
}
