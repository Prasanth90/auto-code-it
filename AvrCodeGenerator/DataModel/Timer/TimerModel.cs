using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClassLibrary1
{
    public class TimerModel
    {
        public TimerModel()
        {
            Timers = GetTimers();
        }
        public ObservableCollection<Timer> Timers { get; set; } 

        public static ObservableCollection<Timer> GetTimers()
        {
            var timers = new ObservableCollection<Timer>();
            var timerNames = new List<string>()
                                      {
                                          "TCC0",
                                          "TCC1"
                                      };
            foreach (var timerName in timerNames)
            {
                timers.Add(new Timer(timerName));
            }
            return timers;
        }
    }
}
