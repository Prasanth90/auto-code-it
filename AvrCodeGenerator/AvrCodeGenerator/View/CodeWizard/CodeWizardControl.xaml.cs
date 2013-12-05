using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using CodeWizard.DataModel;
using CodeWizard.DataModel.ICodeWizardPlugin;
using CodeWizard.DataModel.PeripheralInfo;
using CodeWizard.PluginManager;
using CodeWizard.Plugins.CodeGeneration;
using Company.AvrCodeGenerator.ViewModel.PeripheralTreeViewModel;

namespace Company.AvrCodeGenerator.View.CodeWizard
{
    /// <summary>
    /// Interaction logic for CodeWizardControl.xaml
    /// </summary>
    public partial class CodeWizardControl : UserControl , INotifyPropertyChanged
    {
        private McuModel _mcuModel;
        private McuPeripheralsViewModel _mcuPeripheralsViewModel;
        private List<ICodeWizardPlugin> _plugins;
        private readonly Dictionary<string,UserControl> _controlContainer =new Dictionary<string, UserControl>();

        public CodeWizardControl()
        {
            InitializeComponent();
            InitilaizePlugins();
            InitilaizePeripheralView();
            this.DataContext = this;
        }

        private void InitilaizePlugins()
        {
            _mcuModel = new McuModel("xmega128a1");
            var pluginManager = new PluginManager();
            _plugins = PluginManager.GeneralPlugins;
        }

        private void InitilaizePeripheralView()
        {
            ObservableCollection<Peripheral> peripheralsInfo = GetPeripheralsInfo();
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
            Dictionary<string, UserControl> controls = codeWizardPlugin.CreateUserControl(codeWizardPlugin.GetPluginInfo().Name);
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
    }
}
