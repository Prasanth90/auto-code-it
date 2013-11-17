namespace CodeWizard.DataModel.Timer
{
    public class Timer
    {
        public Timer(string timerName)
        {
            TimerName = timerName;
            TimerSettings = new TimerSettings();
            if (timerName.EndsWith("1"))
            {
                TimerSettings.CCCChannel.IsAvailable = false;
                TimerSettings.CCDChannel.IsAvailable = false;
            }
        }
        public string TimerName { get; set; }
        public TimerSettings TimerSettings { get; set; }
    }
}
