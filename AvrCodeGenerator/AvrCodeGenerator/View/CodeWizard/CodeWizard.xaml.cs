using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Company.AvrCodeGenerator.ViewModel.PeripheralTreeViewModel;
using DataModel;
using DataModel.ICodeWizardPlugin;
using DataModel.PeripheralInfo;

namespace Company.AvrCodeGenerator.View.CodeWizard
{
    /// <summary>
    /// Interaction logic for CodeWizard.xaml
    /// </summary>
    public partial class CodeWizard : UserControl , INotifyPropertyChanged
    {
        private readonly McuModel _mcuModel;
        private McuPeripheralsViewModel _mcuPeripheralsViewModel;

        private Dictionary<string,UserControl> _controlContainer =new Dictionary<string, UserControl>();

        public CodeWizard()
        {
            InitializeComponent();
            _mcuModel = new McuModel("xmega128a1");
            var pluginManager = new PluginManager.PluginManager();
            ObservableCollection<Peripheral> peripheralsInfo = GetPeripheralsInfo();
            McuPeripheralsViewModel = new McuPeripheralsViewModel(peripheralsInfo, TreeViewSelectionChanged);
            this.DataContext = this;
            LoadControls();
        }

        private ObservableCollection<Peripheral> GetPeripheralsInfo()
        {
            List<ICodeWizardPlugin> plugins = PluginManager.PluginManager.GeneralPlugins;
            ObservableCollection<Peripheral> peripherals = new ObservableCollection<Peripheral>();
            foreach (var codeWizardPlugin in plugins)
            {
                var peripheral = GetPeripheral(codeWizardPlugin);
                if (peripheral != null)
                {
                    peripherals.Add(peripheral);
                }
            }
            return peripherals;
        }

        private Peripheral GetPeripheral(ICodeWizardPlugin codeWizardPlugin)
        {
            return new Peripheral()
                {
                    Icon = codeWizardPlugin.GetPluginInfo().Icon,
                    Name = codeWizardPlugin.GetPluginInfo().Name,
                    ChildPeripherals = GetChildItems(codeWizardPlugin)
                };
        }

        private ObservableCollection<Peripheral> GetChildItems(ICodeWizardPlugin codeWizardPlugin)
        {
            var peripherals = new ObservableCollection<Peripheral>();
            var cildItems = codeWizardPlugin.CreateUserControl(codeWizardPlugin.GetPluginInfo().Name).Keys;
            foreach (var cildItem in cildItems)
            {
                peripherals.Add(new Peripheral()
                    {
                        Icon = string.Empty,
                        Name = cildItem
                    });
            }
            return peripherals;
        }

        private void LoadControls()
        {
            //var ports = _mcuModel.IOPortModel.Ports;
            //foreach (Port port in ports)
            //{
            //    _controlContainer.Add(port.PortName , new PortControl(new PortViewModel(port)));
            //}
            //foreach (UsartModel usartModel in _mcuModel.UsartModels)
            //{
            //    _controlContainer.Add(usartModel.UsartName,new Usart.Usart(new UsartViewModel(usartModel)));
            //}
            //foreach (SpiModel spiModel in _mcuModel.SpiModels)
            //{
            //    _controlContainer.Add(spiModel.SpiName , new SpiControl(spiModel));
            //}
        }

        public McuPeripheralsViewModel McuPeripheralsViewModel
        {
            get { return _mcuPeripheralsViewModel; }
            set
            {
                _mcuPeripheralsViewModel = value;
                this.OnPropertyChanged("McuPeripheralsViewModel");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetControl(UserControl control)
        {
            HostControl.Children.Clear();
            HostControl.Children.Add(control);
        }

        private void TreeViewSelectionChanged(PeripheralViewModel peripheralViewModel)
        {
            if (peripheralViewModel.Parent != null)
            {
                var uiControl =   GetControl(peripheralViewModel.Parent.Name,peripheralViewModel.Name);
                if (uiControl != null) 
                SetControl(uiControl);
                
            }
        }


        private UserControl GetControl(string parentPeripheralName, string childPeripheralName)
        {
            if (_controlContainer.ContainsKey(childPeripheralName))
            {
                return _controlContainer[childPeripheralName];
            }
            return null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var codegen = new PeripheralConfig.CodeGeneration.CodeGenerator();
            string generatedCode =  codegen.GetGeneratedCode();
            using (var streamWriter = new StreamWriter(@"D:\testt.txt"))
            {
                streamWriter.Write(generatedCode);              
            }
        }
    }
}
