using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StudentenAdministratieApp.Convertor
{
    public class clsManVrouwToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "Man";
                else
                    return "Vrouw";
            }
            return "Vrouw";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString() == "System.Windows.Controls.ComboBoxItem: Man")
            {
                return true;
            }
            else
            {
                return false;
            }
 
        }
    }
}

