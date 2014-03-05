using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Atmel.Studio.Library.Wizard;
using Atmel.Studio.Services;
using CodeWizard.DataModel;
using CodeWizard.DataModel.ICodeWizardPlugin;
using CodeWizard.DataModel.PeripheralInfo;
using CodeWizard.PluginManager;
using CodeWizard.Plugins.CodeGeneration;
using CodeWizard.Plugins.ViewModel;
using Company.AvrCodeGenerator.ViewModel.PeripheralTreeViewModel;

namespace Company.AvrCodeGenerator.ViewModel.CodeWizardViewModel
{
    public class CodeWizardViewModel : ViewModelBase
    {
        private ObservableCollection<string> _devices;
        private Dictionary<string, UserControl> _controlContainer = new Dictionary<string, UserControl>();
        private string _selectedDevice;
        private McuPeripheralsViewModel _mcuPeripheralsViewModel;
        private McuModel _mcuModel;
        private List<ICodeWizardPlugin> _plugins;
        private string _code;
        private ICommand _generateCode;
        private ICommand _generateMainCode;

        public CodeWizardViewModel()
        {
            Devices = new ObservableCollection<string>();
            GenerateCode = new RelayCommand(GenerateCodeClickedHandler, CanExecute);
            GenerateMainCode = new RelayCommand(GenerateMainCodeClickedHandler, CanExecute);
            Task.Factory.StartNew(LoadDevices);
        }

        private bool CanExecute()
        {
            return !string.IsNullOrEmpty(this.SelectedDevice);
        }

        public delegate void TreeSelectionChangedEventHandler(object sender, SelectionChangedEventArgs myArgs);
        public event TreeSelectionChangedEventHandler TreeSelectionChanged;
        public event EventHandler DeviceSelectionChanged;
     
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }

        public ICommand GenerateCode
        {
            get { return _generateCode; }
            set
            {
                _generateCode = value;
                this.OnPropertyChanged("GenerateCode");
            }
        }

        public ICommand GenerateMainCode
        {
            get { return _generateMainCode; }
            set
            {
                _generateMainCode = value;
                this.OnPropertyChanged("GenerateMainCode");
            }
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

        public string SelectedDevice
        {
            get { return _selectedDevice; }
            set
            {
                _selectedDevice = value;
                OnPropertyChanged("SelectedDevice");
                Code = string.Empty;
                OnDeviceSelectionChanged();
                Initialize();
            }
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

        protected virtual void OnDeviceSelectionChanged()
        {
            EventHandler handler = DeviceSelectionChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnTreeSelectionChanged(object sender, SelectionChangedEventArgs myargs)
        {
            TreeSelectionChangedEventHandler handler = TreeSelectionChanged;
            if (handler != null) handler(sender, myargs);
        }

        private void InitilaizePlugins()
        {
            _mcuModel = new McuModel(SelectedDevice);
            var pluginManager = new PluginManager();
            _plugins = PluginManager.GeneralPlugins;
        }

        private void LoadDevices()
        {
            var deviceNames = new ObservableCollection<string>();
            var service = ATServiceProvider.DeviceService;
            if (service != null)
            {

                var devices = service.GetDevices();
                foreach (var device in devices)
                {
                    if (device.Family.ToLower().Contains("xmega"))
                        deviceNames.Add(device.Name);
                }
            }
            Devices = deviceNames;
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

        private void TreeViewSelectionChanged(PeripheralViewModel peripheralViewModel)
        {
            if (peripheralViewModel.Parent != null)
            {
                var uiControl = GetControl(peripheralViewModel.Parent.Name, peripheralViewModel.Name);
                OnTreeSelectionChanged(this,new SelectionChangedEventArgs(uiControl));
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

        private void GenerateCodeClickedHandler()
        {
            var projectName = GetProjectName();
            var projectData = new ProjectData()
            {
                Device = this.SelectedDevice,
                Name = projectName,
                Path = Path.Combine(Path.GetTempPath(), "AsfPrototype",projectName)
            };
            var codeCompose = new CodeComposeSteps.ComposeSteps(projectData,this);
            codeCompose.Run();
        }


        private string GetProjectName()
        {
            int variable = 0;
            const string defaultProjectName = "TestProject";

            var projectName = defaultProjectName + variable.ToString();
            var projectParentDir = Path.Combine(Path.GetTempPath(), "AsfPrototype");
            
            var projectPath = Path.Combine(projectParentDir, projectName);

            while (true)
            {
                projectPath = Path.Combine(projectParentDir, projectName);
                if(!Directory.Exists(projectPath))
                {
                    break;
                }
                variable++;
                projectName = defaultProjectName + variable.ToString();
            }

            return projectName;

        }
        private void GenerateMainCodeClickedHandler()
        {
            var projectData = new ProjectData()
            {
                Device = this.SelectedDevice,
                Name = "TestProject",
                Path = @"D:\project"
            };
            var codeCompose = new CodeComposeSteps.ComposeSteps(projectData, this);
            codeCompose.RunMainContents();
        }

        private void Initialize()
        {
            _controlContainer = new Dictionary<string, UserControl>();
            InitilaizePlugins();
            var peripheralsInfo = GetPeripheralsInfo();
            InitilaizePeripheralView(peripheralsInfo);
        }
    }

    public class SelectionChangedEventArgs : EventArgs
    {
        public SelectionChangedEventArgs(UserControl userControl)
        {
            UserControl = userControl;
        }
        public UserControl UserControl { get; set; }
    }
}
