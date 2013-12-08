using System.Collections.ObjectModel;
using System.Linq;

namespace CodeWizard.DataModel.Timer
{
    public class TimerSettings
    {
        public TimerSettings(string timerName)
        {
            ClockSources = McuModel.PeripheralInfoProvider.GetTimerClockSources();
            TimerModes = McuModel.PeripheralInfoProvider.GetTimerModes();
            EventActions = McuModel.PeripheralInfoProvider.GetTimerEventActions();
            EventSources = McuModel.PeripheralInfoProvider.GetTimerEventSources();
            InteruptLevels = McuModel.PeripheralInfoProvider.GetTimerInteruptLevels();
            TimerClockSource = ClockSources[1];
            TimerMode = TimerModes.FirstOrDefault();
            Count = 0;
            PeriodValue = 0;
            CCAChannel = new TimerChannel("TC_CCA","cca",string.Format("{0}_{1}_callback",timerName,"TC_CCA"));
            CCBChannel = new TimerChannel("TC_CCB", "ccb", string.Format("{0}_{1}_callback", timerName, "TC_CCB"));
            CCCChannel = new TimerChannel("TC_CCC", "ccc", string.Format("{0}_{1}_callback", timerName, "TC_CCC"));
            CCDChannel = new TimerChannel("TC_CCD", "ccd", string.Format("{0}_{1}_callback", timerName, "TC_CCD"));
            OverFlowInterupt = new TimerInterupt("overflow", string.Format("{0}_{1}_callback", timerName, "overflow"));
            SelectedEventAction = EventActions.FirstOrDefault();
            SelectedEventSource = EventSources.FirstOrDefault();
        }

        public bool IsTimerEnabled { get; set;}
        public string TimerClockSource { get; set;}
        public string TimerMode { get; set;}
        public int PeriodValue { get; set;}
        public int Count { get; set; }
        public TimerInterupt ErrorInterupt { get; set; }
        public TimerInterupt OverFlowInterupt { get; set; }
        public TimerChannel CCAChannel { get; set; }
        public TimerChannel CCBChannel { get; set; }
        public TimerChannel CCCChannel { get; set; }
        public TimerChannel CCDChannel { get; set; }
        public string SelectedEventSource  { get; set; }
        public string  SelectedEventAction { get; set; }

        public ObservableCollection<string> ClockSources { get; set; }
        public ObservableCollection<string> TimerModes { get; set; }
        public ObservableCollection<string> InteruptLevels { get; set; }
        public ObservableCollection<string> EventActions { get; set; }
        public ObservableCollection<string> EventSources { get; set; }

    }
    public class TimerInterupt
    {
        public TimerInterupt(string name, string interuptCallback)
        {
            Name = name;
            Callback = interuptCallback;
            Level = McuModel.PeripheralInfoProvider.GetTimerInteruptLevels()[1];
        }

        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public string Callback { get; set; }
        public string Level { get; set; }
    }
    public class TimerChannel
    {
        public TimerChannel(string name, string interruptName,string interuptCallback)
        {
            Name = name;
            ChannelInterupt = new TimerInterupt(interruptName, interuptCallback);
            IsEnabled = true;
            IsAvailable = true;
            ChannelValue = 0;
        }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsEnabled { get; set; }
        public int ChannelValue { get; set; }
        public TimerInterupt ChannelInterupt { get; set; }
    }
}
