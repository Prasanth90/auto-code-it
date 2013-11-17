using System.Windows.Controls;
using CodeWizard.Plugins.ViewModel.Timer;

namespace CodeWizard.Plugins.View.Timers
{
    /// <summary>
    /// Interaction logic for Timer.xaml
    /// </summary>
    public partial class TimerControl : UserControl
    {
        public TimerControl(CodeWizard.DataModel.Timer.Timer timer)
        {
            InitializeComponent();
            this.DataContext = new TimerViewModel(timer);
        }
    }
}
