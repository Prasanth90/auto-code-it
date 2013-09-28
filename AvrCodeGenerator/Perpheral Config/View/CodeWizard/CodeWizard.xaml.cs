using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DataModel;
using DataModel.PortModel;
using DataModel.SPI;
using DataModel.UsarModel;
using PeripheralConfig.View.IOPort;
using PeripheralConfig.View.Spi;
using PeripheralConfig.ViewModel.IOPort;
using PeripheralConfig.ViewModel.PeripheralTreeViewModel;
using PeripheralConfig.ViewModel.Spi;
using PeripheralConfig.ViewModel.Usart;

namespace PeripheralConfig.View.CodeWizard
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
            var peripheralsInfo = McuModel.PeripheralInfoProvider.PeripheralsInfo;
            McuPeripheralsViewModel = new McuPeripheralsViewModel(peripheralsInfo, TreeViewSelectionChanged);
            this.DataContext = this;
            LoadControls();
            
            //var control = new Usart.Usart(new UsartViewModel(_mcuModel.UsartModels.FirstOrDefault()));
            //HostControl.Children.Add(control);
        }

        private void LoadControls()
        {
            var ports = _mcuModel.IOPortModel.Ports;
            foreach (Port port in ports)
            {
                _controlContainer.Add(port.PortName , new PortControl(new PortViewModel(port)));
            }
            foreach (UsartModel usartModel in _mcuModel.UsartModels)
            {
                _controlContainer.Add(usartModel.UsartName,new Usart.Usart(new UsartViewModel(usartModel)));
            }
            foreach (SpiModel spiModel in _mcuModel.SpiModels)
            {
                _controlContainer.Add(spiModel.SpiName , new SpiControl(spiModel));
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
            //PageTransitionControl.ShowPage(control);
           // transitioning.Content = control;
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

        //private UIElement GetControl(string parentPeripheralName, string childPeripheralName)
        //{
        //    switch (parentPeripheralName)
        //    {
        //        case PeripheralNames.Port:
        //            var ports = _mcuModel.IOPortModel.Ports;
        //            foreach (Port port in ports)
        //            {
        //                if (port.PortName.Equals(childPeripheralName))
        //                {
        //                    return new PortControl(new PortViewModel(port));
        //                }
        //            }
        //            break;

        //       case PeripheralNames.Spis:
        //            var spis = _mcuModel.SpiModels;
        //            foreach (SpiModel spi in spis)
        //            {
        //                if (spi.SpiName.Equals(childPeripheralName))
        //                {
        //                    return new SpiControl(spi);
        //                }
        //            }
        //            break;

        //       case PeripheralNames.Usarts:
        //            var usarts = _mcuModel.UsartModels;
        //            foreach (UsartModel usart in usarts)
        //            {
        //                if (usart.UsartName.Equals(childPeripheralName))
        //                {
        //                    return new Usart.Usart(new UsartViewModel(usart));
        //                }
        //            }
        //            break;

        //    }
        //    return null;
        //}

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
            var codegen = new CodeGenerator.CodeGenerator(this._mcuModel);
            string generatedCode =  codegen.GetGeneratedCode();
            using (var streamWriter = new StreamWriter(@"D:\testt.txt"))
            {
                streamWriter.Write(generatedCode);              
            }
        }
    }
}
