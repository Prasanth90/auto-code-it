using System.Windows.Controls;
using PeripheralConfig.ViewModel.Usart;

namespace PeripheralConfig.View.Usart
{
    /// <summary>
    /// Interaction logic for Usart.xaml
    /// </summary>
    public partial class Usart : UserControl
    {
        public Usart(UsartViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
