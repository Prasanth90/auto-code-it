namespace ClassLibrary1
{
    public class TimerSettings
    {
        public TimerSettings()
        {
            CCAChannel = new TimerChannel();
            CCBChannel = new TimerChannel();
            CCCChannel = new TimerChannel();
            CCDChannel = new TimerChannel();
        }

        public bool IsTimerEnabled { get; set;}
        public string TimerClockSource { get; set;}
        public string TimerMode { get; set;}
        public string PeriodValue { get; set;}
        public TimerInterupt ErrorInterupt { get; set; }
        public TimerInterupt OverFlowInterupt { get; set; }
        public TimerChannel CCAChannel { get; set; }
        public TimerChannel CCBChannel { get; set; }
        public TimerChannel CCCChannel { get; set; }
        public TimerChannel CCDChannel { get; set; }
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
        }
        public bool IsEnabled { get; set; }
        public string ChannelValue { get; set; }
        public TimerInterupt ChannelInterupt { get; set; }
    }
}
