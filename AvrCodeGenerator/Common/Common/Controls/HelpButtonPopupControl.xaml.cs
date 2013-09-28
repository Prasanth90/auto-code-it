using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication3.Common.Controls
{
    /// <summary>
    /// Interaction logic for HelpButtonPopupControl.xaml
    /// </summary>
    public partial class HelpButtonPopupControl : UserControl, INotifyPropertyChanged
    {
        public HelpButtonPopupControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private string _helpString = "No Help";
        public string HelpString
        {
            get { return _helpString; }
            set
            {
                _helpString = value;
                OnPropertyChanged("HelpString");
            }
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            this.popMsg.IsOpen = !this.popMsg.IsOpen;

            var popupSize = new Size(popMsg.ActualWidth, popMsg.ActualHeight);

            // Return the offset vector for the TextBlock object.
            Vector vector = VisualTreeHelper.GetOffset(this.btnHelp);

            // Convert the vector to a point value.
            var currentPoint = new Point(this.btnHelp.ActualWidth, this.btnHelp.ActualHeight - vector.Y);

            popMsg.PlacementRectangle = new Rect(currentPoint, popupSize);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
