using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace VxShutdownTimer.GUI.ShutdownSchedule
{

    public partial class ShutdownScheduleView : UserControl
    {
        private MetroWindow _metroWindow;
        public ShutdownScheduleView()
        {
            InitializeComponent();
            _metroWindow = Application.Current.MainWindow as MetroWindow;
        }

        private async void ShutdownScheduleViewModel_Info(object sender, string e)
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

        private async void ShutdownScheduleViewModel_Error(object sender, string e)
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

        private void ShutdownScheduleViewModel_Tick(object sender, TimeSpan e)
        {
            Dispatcher.Invoke(() =>
            {
                TextTimer.Text = e.ToString(@"hh\:mm\:ss");
            });
        }
    }
}
