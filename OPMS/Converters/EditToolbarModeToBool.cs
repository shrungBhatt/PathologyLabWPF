using OPMS.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OPMS.Converters
{
    public class EditToolbarModeToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mode = (EditToolbarMode)value;
            string convertedParam = "";
            if(parameter != null)
            {
                convertedParam = parameter as string;
            }

            if (convertedParam.Equals("DataGrid") || convertedParam.Equals("Button") || convertedParam.Equals("Invert"))
            {
                if (mode == EditToolbarMode.EDIT || mode == EditToolbarMode.ADD)
                {
                    return false;
                }
                else if (mode == EditToolbarMode.IDLE)
                {
                    return true;
                }
            }
            else
            {
                if (mode == EditToolbarMode.EDIT || mode == EditToolbarMode.ADD)
                {
                    return true;
                }
                else if (mode == EditToolbarMode.IDLE)
                {
                    return false;
                }
            }
            

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
