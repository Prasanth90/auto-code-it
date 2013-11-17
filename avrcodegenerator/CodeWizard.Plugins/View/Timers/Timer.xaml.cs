using System.Windows.Controls;
using CodeWizard.Plugins.ViewModel.Timer;

namespace CodeWizard.Plugins.View.Timers
{
    /// <summary>
    /// Interaction logic for Timer.xaml
    /// </summary>
    public partial class Timer : UserControl
    {
        public Timer(CodeWizard.DataModel.Timer.Timer timer)
        {
            InitializeComponent();
            this.DataContext = new TimerViewModel(timer);
        }
    }
}
