using System;
using System.Collections.ObjectModel;
using CodeWizard.DataModel.PeripheralInfo;

namespace Company.AvrCodeGenerator.ViewModel.PeripheralTreeViewModel
{
    public class McuPeripheralsViewModel
    {
        private readonly Action<PeripheralViewModel> _treeViewSelectionCahnged;
        private ObservableCollection<PeripheralViewModel> _peripheralViewModels = new ObservableCollection<PeripheralViewModel>();

        public McuPeripheralsViewModel(ObservableCollection<Peripheral> peripherals, Action<PeripheralViewModel> treeViewSelectionCahnged)
        {

            
            _treeViewSelectionCahnged = treeViewSelectionCahnged;
            foreach (var peripheral in peripherals)
            {
                PeripheralViewModels.Add(new PeripheralViewModel(peripheral,null,SelectionChanged)); 
            }
        }

        public ObservableCollection<PeripheralViewModel> PeripheralViewModels
        {
            get { return _peripheralViewModels; }
            set { _peripheralViewModels = value; }
        }

        private void SelectionChanged(PeripheralViewModel peripheralViewModel)
        {
            _treeViewSelectionCahnged(peripheralViewModel);
        }
    }
}
