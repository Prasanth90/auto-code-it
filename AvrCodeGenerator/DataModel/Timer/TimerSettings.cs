namespace CodeWizard.DataModel.Timer
{
    public class TimerSettings
    {
        public TimerSettings()
        {
            CCAChannel = new TimerChannel("TC_CCA","cca");
            CCBChannel = new TimerChannel("TC_CCB", "ccb");
            CCCChannel = new TimerChannel("TC_CCC", "ccc");
            CCDChannel = new TimerChannel("TC_CCD", "ccd");
            OverFlowInterupt = new TimerInterupt("overflow");
        }

        public bool IsTimerEnabled { get; set;}
        public string TimerClockSource { get; set;}
        public string TimerMode { get; set;}
        public string PeriodValue { get; set;}
        public string Count { get; set; }
        public TimerInterupt ErrorInterupt { get; set; }
        public TimerInterupt OverFlowInterupt { get; set; }
        public TimerChannel CCAChannel { get; set; }
        public TimerChannel CCBChannel { get; set; }
        public TimerChannel CCCChannel { get; set; }
        public TimerChannel CCDChannel { get; set; }
        public string SelectedEventSource  { get; set; }
        public string  SelectedEventAction { get; set; }
    }
    public class TimerInterupt
    {
        public TimerInterupt(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public string Callback { get; set; }
        public string Level { get; set; }
    }
    public class TimerChannel
    {
        public TimerChannel(string name, string interruptName)
        {
            Name = name;
            ChannelInterupt = new TimerInterupt(interruptName);
            IsAvailable = true;
        }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsEnabled { get; set; }
        public string ChannelValue { get; set; }
        public TimerInterupt ChannelInterupt { get; set; }
    }
}
