namespace CodeWizard.DataModel.Timer
{
    public class TimerSettings
    {
        public TimerSettings()
        {
            CCAChannel = new TimerChannel();
            CCBChannel = new TimerChannel();
            CCCChannel = new TimerChannel();
            CCDChannel = new TimerChannel();
            OverFlowInterupt = new TimerInterupt();
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
        public bool IsEnabled { get; set; }
        public string Callback { get; set; }
        public string Level { get; set; }
    }
    public class TimerChannel
    {
        public TimerChannel()
        {
            ChannelInterupt = new TimerInterupt();
            IsAvailable = true;
        }
        public bool IsAvailable { get; set; }
        public bool IsEnabled { get; set; }
        public string ChannelValue { get; set; }
        public TimerInterupt ChannelInterupt { get; set; }
    }
}
