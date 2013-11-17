namespace ClassLibrary1
{
    public class Timer
    {
        public Timer(string timerName)
        {
            TimerName = timerName;
            TimerSettings = new TimerSettings();
            if (timerName.EndsWith("1"))
            {
                TimerSettings.CCCChannel = null;
                TimerSettings.CCDChannel = null;
            }
        }
        public string TimerName { get; set; }
        public TimerSettings TimerSettings { get; set; }
    }
}
