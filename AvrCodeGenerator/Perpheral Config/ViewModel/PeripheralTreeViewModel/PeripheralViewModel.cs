using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using PeripheralConfig.View.CodeWizard;
using WpfApplication1;

namespace PeripheralConfig.ViewModel.PeripheralTreeViewModel
{
    public class PeripheralViewModel : INotifyPropertyChanged
    {
        private readonly Action<PeripheralViewModel> _selectionChanged;
        private readonly Peripheral _peripheral;
        private bool _isSelected;
        private bool _isExpanded;
        private PeripheralViewModel _parent;
        private ObservableCollection<PeripheralViewModel> _childrenPeripherals;

        public PeripheralViewModel(Peripheral peripheral, PeripheralViewModel parent, Action<PeripheralViewModel> selectionChanged)
        {
            _peripheral = peripheral;
            _parent = parent;
            _selectionChanged = selectionChanged;
            List<PeripheralViewModel> list = new List<PeripheralViewModel>();
            foreach (Peripheral child in _peripheral.ChildPeripherals)
                list.Add(new PeripheralViewModel(child, this,selectionChanged));
            _childrenPeripherals = new ObservableCollection<PeripheralViewModel>(
                    list);
        }

        public string Icon
        {
            get { return _peripheral.Icon; }
            set { _peripheral.Icon = value;
            this.OnPropertyChanged("Icon");}

        }

        public string Name
        {
            get { return _peripheral.Name; }
        }

        public ObservableCollection<PeripheralViewModel> ChildrenPeripherals
        {
            get { return _childrenPeripherals; }
            set
            {
                _childrenPeripherals = value;
                this.OnPropertyChanged("ChildrenPeripherals");
            }
        }

        #region IsExpanded

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                if (_isExpanded && _parent != null)
                    _parent.IsExpanded = true;
            }
        }

        #endregion // IsExpanded

        #region IsSelected

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    if (_isSelected)
                    {
                        _selectionChanged(this);
                    }
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion // IsSelected

        #region NameContainsText

        public bool NameContainsText(string text)
        {
            if (String.IsNullOrEmpty(text) || String.IsNullOrEmpty(this.Name))
                return false;

            return this.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1;
        }

        #endregion // NameContainsText

        #region Parent

        public PeripheralViewModel Parent
        {
            get { return _parent; }
        }

        #endregion // Parent

         #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members
    }
}
