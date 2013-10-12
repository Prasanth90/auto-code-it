using System.Windows.Controls;
using DataModel.UsarModel;
using PeripheralConfig.ViewModel.Usart;

namespace PeripheralConfig.View.Usart
{
    /// <summary>
    /// Interaction logic for Usart.xaml
    /// </summary>
    public partial class UsartControl : UserControl
    {
        public UsartControl(DataModel.UsarModel.Usart usartModel)
        {
            InitializeComponent();
            this.DataContext = new UsartViewModel(usartModel);
        }
    }
}
