﻿using System.Windows.Controls;

namespace CodeWizard.Plugins.View.IOPort
{


    /// <summary>
    /// Interaction logic for Port.xaml
    /// </summary>
    public partial class PortControl : UserControl
    {

        public PortControl()
        {
            InitializeComponent();
            //Port.Header = portViewModel.Port.PortName;
            //foreach (var pin in portViewModel.Port.Pins)
            //{
            //    var pincontrolViewModel = new PinViewModel(pin);
            //    var pincontrol = new PinControl(pincontrolViewModel);
            //    DockPanel.SetDock(pincontrol, Dock.Top);
            //    PinsContainer.Children.Add(pincontrol);
            //}
        }
    }

    
}
