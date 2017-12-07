using System;
using System.Globalization;
using System.Windows.Data;
using CoreLib.Models;

namespace VxShutdownTimer.GUI.ShutdownList
{
    public class ShutdownTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ShutdownType type;
            if(Enum.TryParse(value.ToString(),out type))
            {
                switch(type)
                {
                    case ShutdownType.Shutdown:
                        return "Shutdown";
                    case ShutdownType.Hibernate:
                        return "Hibernate";
                    case ShutdownType.Sleep:
                        return "Sleep";
                    case ShutdownType.LogOff:
                        return "Log Off";
                    case ShutdownType.Restart:
                        return "Restart";
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ShutdownType shutdownType = ShutdownType.Shutdown;
            switch(value.ToString())
            {
                case "Hibernate":
                    shutdownType = ShutdownType.Hibernate;
                    break;
                case "Sleep":
                    shutdownType = ShutdownType.Sleep;
                    break;
                case "Restart":
                    shutdownType = ShutdownType.Restart;
                    break;
                case "Log Off":
                    shutdownType = ShutdownType.LogOff;
                    break;
            }
            return shutdownType;
        }
    }
}
