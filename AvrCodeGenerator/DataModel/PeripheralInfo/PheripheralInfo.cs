using System.Collections.ObjectModel;
using System.Windows.Media;

namespace CodeWizard.DataModel.PeripheralInfo
{
    public class PeripheralInfo : ObservableCollection<PeripheralInfo>
    {
        public string Icon { get; set; }

        public string PerpheralTopLevelName { get; set; }

        public ObservableCollection<string> PeripheralsList { get; set; }

        public  Color ColorInUi { get; set; }
    }
}
