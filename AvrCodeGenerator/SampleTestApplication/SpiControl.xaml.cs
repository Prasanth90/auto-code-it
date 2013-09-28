using System.Windows.Controls;

namespace SampleTestApplication
{
    /// <summary>
    /// Interaction logic for SpiControl.xaml
    /// </summary>
    public partial class SpiControl : UserControl
    {
        public SpiControl()
        {
            InitializeComponent();
            this.DataContext = new SpiViewModel();
        }
    }
}
