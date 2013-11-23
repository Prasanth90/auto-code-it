using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CodeWizard.DataModel;
using CodeWizard.DataModel.Timer;

namespace CodeWizard.Plugins.ViewModel.Timer
{
    public class TimerViewModel
    {
        private readonly DataModel.Timer.Timer _timer;
        

        public TimerViewModel(DataModel.Timer.Timer timer)
        {
            _timer = timer;        
        }

        public string TimerName
        {
            get { return _timer.TimerName + "  Settings  -"; }
            set { _timer.TimerName = value; }
        }

        public bool IsTimerEnabled
        {
            get { return _timer.TimerSettings.IsTimerEnabled; }
            set { _timer.TimerSettings.IsTimerEnabled = value; }
        }

        public ObservableCollection<string> ClockSources
        {
            get { return _timer.TimerSettings.ClockSources; }
            set { _timer.TimerSettings.ClockSources = value; }
        }

        public string SelectedClockSource
        {
            get { return _timer.TimerSettings.TimerClockSource; }
            set { _timer.TimerSettings.TimerClockSource = value; }
        }

        public int Period
        {
            get { return _timer.TimerSettings.PeriodValue; }
            set { _timer.TimerSettings.PeriodValue = value; }
        }

        public int Count
        {
            get { return _timer.TimerSettings.Count ; }
            set { _timer.TimerSettings.Count = value; }
        }

        public bool IsCCAAvailable
        {
            get { return _timer.TimerSettings.CCAChannel.IsAvailable; }
            set { _timer.TimerSettings.CCAChannel.IsAvailable = value; }
        }

        public bool IsCCBAvailable
        {
            get { return _timer.TimerSettings.CCBChannel.IsAvailable; }
            set { _timer.TimerSettings.CCBChannel.IsAvailable = value; }
        }

        public bool IsCCCAvailable
        {
            get { return _timer.TimerSettings.CCCChannel.IsAvailable; }
            set { _timer.TimerSettings.CCCChannel.IsAvailable = value; }
        }

        public bool IsCCDAvailable
        {
            get { return _timer.TimerSettings.CCDChannel.IsAvailable; }
            set { _timer.TimerSettings.CCDChannel.IsAvailable = value; }
        }

        public int CCAValue
        {
            get { return _timer.TimerSettings.CCAChannel.ChannelValue; }
            set { _timer.TimerSettings.CCAChannel.ChannelValue = value; }
        }

        public int CCBValue
        {
            get { return _timer.TimerSettings.CCBChannel.ChannelValue; }
            set { _timer.TimerSettings.CCBChannel.ChannelValue = value; }
        }

        public int CCCValue
        {
            get { return _timer.TimerSettings.CCCChannel.ChannelValue; }
            set { _timer.TimerSettings.CCCChannel.ChannelValue = value; }
        }

        public int CCDValue
        {
            get { return _timer.TimerSettings.CCDChannel.ChannelValue; }
            set { _timer.TimerSettings.CCDChannel.ChannelValue = value; }
        }

        public ObservableCollection<string> TimerModes
        {
            get { return _timer.TimerSettings.TimerModes; }
            set { _timer.TimerSettings.TimerModes = value; }
        }

        public string SelectedTimerMode
        {
            get { return _timer.TimerSettings.TimerMode; }
            set { _timer.TimerSettings.TimerMode = value; }
        }

        public ObservableCollection<string> EventSources
        {
            get { return _timer.TimerSettings.EventSources; }
            set { _timer.TimerSettings.EventSources = value; }
        }

        public string SelectedEventSource
        {
            get { return _timer.TimerSettings.SelectedEventSource; }
            set { _timer.TimerSettings.SelectedEventSource = value; }
        }

        public ObservableCollection<string> EventActions
        {
            get { return _timer.TimerSettings.EventActions; }
            set { _timer.TimerSettings.EventActions = value; }
        }

        public string SelectedEventAction
        {
            get { return _timer.TimerSettings.SelectedEventAction; }
            set { _timer.TimerSettings.SelectedEventAction = value; }
        }


        public bool IsCCAEnabled
        {
            get { return _timer.TimerSettings.CCAChannel.IsEnabled; }
            set { _timer.TimerSettings.CCAChannel.IsEnabled = value; }
        }

        public bool IsCCBEnabled
        {
            get { return _timer.TimerSettings.CCBChannel.IsEnabled; }
            set { _timer.TimerSettings.CCBChannel.IsEnabled = value; }
        }

        public bool IsCCCEnabled
        {
            get { return _timer.TimerSettings.CCCChannel.IsEnabled; }
            set { _timer.TimerSettings.CCCChannel.IsEnabled = value; }
        }

        public bool IsCCDEnabled
        {
            get { return _timer.TimerSettings.CCDChannel.IsEnabled; }
            set { _timer.TimerSettings.CCDChannel.IsEnabled = value; }
        }



        public bool OverflowIntEnabled
        {
            get { return _timer.TimerSettings.OverFlowInterupt.IsEnabled; }
            set { _timer.TimerSettings.OverFlowInterupt.IsEnabled = value; }
        }

        public bool CCAIntEnabled
        {
            get { return _timer.TimerSettings.CCAChannel.ChannelInterupt.IsEnabled; }
            set { _timer.TimerSettings.CCAChannel.ChannelInterupt.IsEnabled = value; }
        }

        public bool CCBIntEnabled
        {
            get { return _timer.TimerSettings.CCBChannel.ChannelInterupt.IsEnabled;; }
            set { _timer.TimerSettings.CCBChannel.ChannelInterupt.IsEnabled = value; }
        }

        public bool CCCIntEnabled
        {
            get { return _timer.TimerSettings.CCCChannel.ChannelInterupt.IsEnabled;; }
            set { _timer.TimerSettings.CCCChannel.ChannelInterupt.IsEnabled = value; }
        }

        public bool CCDIntEnabled
        {
            get { return _timer.TimerSettings.CCDChannel.ChannelInterupt.IsEnabled;; }
            set { _timer.TimerSettings.CCDChannel.ChannelInterupt.IsEnabled = value; }
        }

        public ObservableCollection<string> InteruptLevels
        {
            get { return _timer.TimerSettings.InteruptLevels; }
            set { _timer.TimerSettings.InteruptLevels = value; }
        }

        public string SelectedOvfInteruptLevel
        {
            get { return _timer.TimerSettings.OverFlowInterupt.Level; }
            set { _timer.TimerSettings.OverFlowInterupt.Level = value; }
        }

         public string SelectedCCAInteruptLevel
        {
            get { return _timer.TimerSettings.CCAChannel.ChannelInterupt.Level; }
            set { _timer.TimerSettings.CCAChannel.ChannelInterupt.Level = value; }
        }

         public string SelectedCCBInteruptLevel
        {
           get { return _timer.TimerSettings.CCBChannel.ChannelInterupt.Level; }
            set { _timer.TimerSettings.CCBChannel.ChannelInterupt.Level = value; }
        }

         public string SelectedCCCInteruptLevel
        {
           get { return _timer.TimerSettings.CCCChannel.ChannelInterupt.Level; }
            set { _timer.TimerSettings.CCCChannel.ChannelInterupt.Level = value; }
        }

         public string SelectedCCDInteruptLevel
        {
            get { return _timer.TimerSettings.CCDChannel.ChannelInterupt.Level; }
            set { _timer.TimerSettings.CCDChannel.ChannelInterupt.Level = value; }
        }
    }
}
