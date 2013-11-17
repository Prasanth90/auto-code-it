using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeWizard.DataModel.Timer
{
    public class TimerModel
    {
        public TimerModel()
        {
            Timers =  GetTimers();
        }
        public ObservableCollection<Timer> Timers { get; set; } 

        public static ObservableCollection<Timer> GetTimers()
        {
            var timers = new ObservableCollection<Timer>();
            var timerPherpherals = McuModel.PeripheralInfoProvider.GetTimers();
            foreach (var timerPherpheral in timerPherpherals)
            {
                timers.Add(new Timer(timerPherpheral.Name));
            }
            return timers;
        }
    }
}
