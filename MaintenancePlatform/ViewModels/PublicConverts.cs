using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MaintenancePlatform.ViewModels
{
    [ValueConversion(typeof(System.Enum), typeof(string))]
    public class ConvertForEnableStatus : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((int)value)
            {
                case 2:
                    return "可用";
                case 3:
                    return "不可用";
                case 4:
                    return "其它";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {

            return value;
        }


    }
    #endregion
}