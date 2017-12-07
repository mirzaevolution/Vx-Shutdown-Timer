using System.Windows.Controls;
using VxShutdownTimer.GUI.Triggers.BatteryPercentTrigger;
using VxShutdownTimer.GUI.Triggers.DayTrigger;
using VxShutdownTimer.GUI.Triggers.DirectoryTrigger;
using VxShutdownTimer.GUI.Triggers.FileTrigger;
using VxShutdownTimer.GUI.Triggers.NetConnectivityTrigger;
using VxShutdownTimer.GUI.Triggers.ProcessTrigger;
using VxShutdownTimer.GUI.Triggers.TimeZoneTrigger;
namespace VxShutdownTimer.GUI
{
    public class ViewFactory
    {
        private static BatteryPercentView _batteryPercentView;
        private static DayView _dayView;
        private static DirectoryView _dirView;
        private static FileView _fileView;
        private static NetConnectivityView _netView;
        private static ProcessView _procView;
        private static TimeZoneView _tzView;
        public static UserControl CreateView(string value)
        {
            switch (value.ToString())
            {
                case "Triggered by Battery Percent Change":
                    if (_batteryPercentView == null)
                        _batteryPercentView = new BatteryPercentView();
                    return _batteryPercentView;
                case "Triggered by Day Change":
                    if(_dayView == null)
                        _dayView = new DayView();
                    return _dayView;
                case "Triggered by Directory Change":
                    if(_dirView == null)
                        _dirView = new DirectoryView();
                    return _dirView;
                case "Triggered by File Change":
                    if(_fileView == null)
                        _fileView = new FileView();
                    return _fileView;
                case "Triggered by Internet Connectivity":
                    if(_netView == null)
                        _netView = new NetConnectivityView();
                    return _netView;
                case "Triggered by Process":
                    if(_procView == null)
                        _procView = new ProcessView();
                    return _procView;
                case "Triggered by Timezone Change":
                    if(_tzView == null)
                        _tzView = new TimeZoneView();
                    return _tzView;
            }
            return null;
        }

    }
}
