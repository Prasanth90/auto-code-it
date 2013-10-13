using System.Windows.Controls;
using CodeWizard.Plugins.ViewModel.Usart;

namespace CodeWizard.Plugins.View.Usart
{
    /// <summary>
    /// Interaction logic for Usart.xaml
    /// </summary>
    public partial class UsartControl : UserControl
    {
        public UsartControl(CodeWizard.DataModel.UsarModel.Usart usartModel)
        {
            InitializeComponent();
            this.DataContext = new UsartViewModel(usartModel);
        }
    }
}
