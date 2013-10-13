using System;
using System.Windows.Data;

namespace CodeWizard.Plugins.Utils
{
    public class CustomImagePathConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
                                        System.Globalization.CultureInfo culture)
        {
            return "../Resources/Images/" + GetImageName(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                        System.Globalization.CultureInfo culture)
        {
            return "";
        }

        #endregion

        private string GetImageName(string text)
        {
            string name = "";
            name = text.ToLower() + ".png";
            return name;
        }
    } 
}
