using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Atmel.Studio.Services;
using CodeWizard.DataModel;
using CodeWizard.DataModel.ICodeWizardPlugin;
using CodeWizard.DataModel.PeripheralInfo;
using CodeWizard.PluginManager;
using CodeWizard.Plugins.CodeGeneration;
using Company.AvrCodeGenerator.ViewModel.PeripheralTreeViewModel;
using Microsoft.VisualStudio.PlatformUI;

namespace Company.AvrCodeGenerator.View.CodeWizard
{
    /// <summary>
    /// Interaction logic for CodeWizardControl.xaml
    /// </summary>
    public partial class CodeWizardControl : UserControl , INotifyPropertyChanged
    {
        private McuModel _mcuModel;
        private ObservableCollection<string> _deviceNames;
        private DialogWindow _dialog = new DialogWindow()
                                        {
                                            Width = 300,
                                            Height = 200
                                        };

        private McuPeripheralsViewModel _mcuPeripheralsViewModel;
        private List<ICodeWizardPlugin> _plugins;
        private  Dictionary<string,UserControl> _controlContainer =new Dictionary<string, UserControl>();
        private string _selectedDevice = string.Empty;
        private ObservableCollection<Peripheral> _peripheralsInfo;
        private ObservableCollection<string> _devices;

        public CodeWizardControl()
        {
            InitializeComponent();
            this.DataContext = this;
            Devices = new ObservableCollection<string>()
                {
                    "ATxmega128A1"
                };
        }


        private void Initialize()
        {
            _controlContainer = new Dictionary<string, UserControl>();
            InitilaizePlugins();
            _peripheralsInfo = GetPeripheralsInfo();
            InitilaizePeripheralView(_peripheralsInfo);
        }

        public ObservableCollection<string> Devices
        {
            get { return _devices; }
            set
            {
                _devices = value;
                OnPropertyChanged("Devices");
            }
        }

        private void LoadDevices()
        {
            _deviceNames = new ObservableCollection<string>();
            var service = ATServiceProvider.DeviceService;
            if (service != null)
            {

                var devices = service.GetDevices();
                foreach (var device in devices)
                {
                    if (device.Family.ToLower().Contains("xmega"))
                        _deviceNames.Add(device.Name);
                }
            }
            Devices = _deviceNames;
        }

        public string SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                _selectedDevice = value;
                OnPropertyChanged("SelectedDevice");
                Initialize();
            }
        }

        private void InitilaizePlugins()
        {
            _mcuModel = new McuModel(SelectedDevice);
            var pluginManager = new PluginManager();
            _plugins = PluginManager.GeneralPlugins;
        }

        private void InitilaizePeripheralView(ObservableCollection<Peripheral> peripheralsInfo)
        {         
            McuPeripheralsViewModel = new McuPeripheralsViewModel(peripheralsInfo, TreeViewSelectionChanged);
        }

        private ObservableCollection<Peripheral> GetPeripheralsInfo()
        {
            
            ObservableCollection<Peripheral> peripherals = new ObservableCollection<Peripheral>();
            foreach (var codeWizardPlugin in _plugins)
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
            Dictionary<string, UserControl> controls =
                codeWizardPlugin.CreateUserControl(codeWizardPlugin.GetPluginInfo().Name);
            foreach (KeyValuePair<string, UserControl> keyValuePair in controls)
            {
                peripherals.Add(new Peripheral()
                    {
                        Icon = string.Empty,
                        Name = keyValuePair.Key
                    });
                _controlContainer.Add(keyValuePair.Key, keyValuePair.Value);
            }
               
            return peripherals;
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
            var codegen = new CodeGenerator();
            var enabledModules = GetEnabledModules();
            string generatedCode =  codegen.GetGeneratedCode(enabledModules);
            using (var streamWriter = new StreamWriter(@"E:\testt.txt"))
            {
                streamWriter.Write(generatedCode);              
            }
            CodeContainer.Text = generatedCode;
        }

        private List<string> GetEnabledModules()
        {
            var enabledModules = new List<string>();
            var phViewModel = McuPeripheralsViewModel.PeripheralViewModels;
            foreach (var peripheralViewModel in phViewModel)
            {
                foreach (var childrenPeripheral in peripheralViewModel.ChildrenPeripherals)
                {
                    if (childrenPeripheral.IsModuleEnabled)
                    {
                        enabledModules.Add(childrenPeripheral.Name);
                    }
                }
            }
            return enabledModules;
        }

        private void CodeWizardControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(LoadDevices);


        }
    }
}
