using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VxShutdownTimer.GUI.Triggers.BatteryPercentTrigger;
using VxShutdownTimer.GUI.Triggers.DayTrigger;
using VxShutdownTimer.GUI.Triggers.DirectoryTrigger;
using VxShutdownTimer.GUI.Triggers.FileTrigger;
using VxShutdownTimer.GUI.Triggers.NetConnectivityTrigger;
using VxShutdownTimer.GUI.Triggers.ProcessTrigger;
using VxShutdownTimer.GUI.Triggers.TimeZoneTrigger;
namespace VxShutdownTimer.GUI
{
    public class ViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ViewFactory.CreateView(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
