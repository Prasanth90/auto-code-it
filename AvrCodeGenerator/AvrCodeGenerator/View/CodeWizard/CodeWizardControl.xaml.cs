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
using CodeWizard.PluginManager;
using CodeWizard.Plugins.CodeGeneration;
using Company.AvrCodeGenerator.ViewModel.CodeWizardViewModel;
using Company.AvrCodeGenerator.ViewModel.PeripheralTreeViewModel;
using Microsoft.VisualStudio.PlatformUI;

namespace Company.AvrCodeGenerator.View.CodeWizard
{
    /// <summary>
    /// Interaction logic for CodeWizardControl.xaml
    /// </summary>
    public partial class CodeWizardControl : UserControl
    {
        public CodeWizardControl()
        {
            InitializeComponent();
            CodeWizardViewModel codeWizardViewModel = new CodeWizardViewModel();
            this.DataContext = codeWizardViewModel;
            codeWizardViewModel.DeviceSelectionChanged += new EventHandler(_codeWizardViewModel_DeviceSelectionChanged);
            codeWizardViewModel.TreeSelectionChanged += new CodeWizardViewModel.TreeSelectionChangedEventHandler(_codeWizardViewModel_TreeSelectionChanged);
        }

        private void _codeWizardViewModel_TreeSelectionChanged(object sender, ViewModel.CodeWizardViewModel.SelectionChangedEventArgs eventArgs)
        {
            ClearControl();
            SetControl(eventArgs.UserControl);
        }

        private void _codeWizardViewModel_DeviceSelectionChanged(object sender, EventArgs e)
        {
            ClearControl();
        }

        private void SetControl(UserControl control)
        {
            HostControl.Children.Add(control);
        }

        private void ClearControl()
        {
            HostControl.Children.Clear();
        }

    }
}
