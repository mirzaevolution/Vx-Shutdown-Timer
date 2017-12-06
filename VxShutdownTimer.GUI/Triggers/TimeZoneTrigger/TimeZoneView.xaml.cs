using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace VxShutdownTimer.GUI.Triggers.TimeZoneTrigger
{

    public partial class TimeZoneView : UserControl
    {
        private MetroWindow _metroWindow;
        public TimeZoneView()
        {
            InitializeComponent();
            _metroWindow = Application.Current.MainWindow as MetroWindow;
        }

        private async void TimeZoneViewModel_Info(object sender, string e)
        {
            if (_metroWindow != null)
            {
                await _metroWindow.ShowMessageAsync("Information", e);
            }
            else
            {
                MessageBox.Show(e, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void TimeZoneViewModel_Error(object sender, string e)
        {
            if (_metroWindow != null)
            {
                await _metroWindow.ShowMessageAsync("Error", e);
            }
            else
            {
                MessageBox.Show(e, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
