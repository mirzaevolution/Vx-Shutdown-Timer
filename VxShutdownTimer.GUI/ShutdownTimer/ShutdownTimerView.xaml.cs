using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace VxShutdownTimer.GUI.ShutdownTimer
{

    public partial class ShutdownTimerView : UserControl
    {
        private MetroWindow _metroWindow;
        public ShutdownTimerView()
        {
            InitializeComponent();
            _metroWindow = Application.Current.MainWindow as MetroWindow;
        }
        private async void ShutdownTimerViewModel_Info(object sender, string e)
        {
            if(_metroWindow!=null)
            {
                await _metroWindow.ShowMessageAsync("Information", e);
            }
            else
            {
                MessageBox.Show(e, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void ShutdownTimerViewModel_Error(object sender, string e)
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

        private void ShutdownTimerViewModel_Tick(object sender, TimeSpan e)
        {
            Dispatcher.Invoke(() =>
            {
                TextTimer.Text = e.ToString(@"hh\:mm\:ss");
            });
        }

        private void NumUpDownHour_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (DataContext != null && NumUpDownHour!=null && NumUpDownMin!=null && NumUpDownSec!=null)
            {
                (DataContext as ShutdownTimerViewModel).TimeOut =
                    new TimeSpan((int)NumUpDownHour.Value, (int)NumUpDownMin.Value, (int)NumUpDownSec.Value);
            }

        }

        private void NumUpDownMin_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (DataContext != null && NumUpDownHour != null && NumUpDownMin != null && NumUpDownSec != null)
            {
                (DataContext as ShutdownTimerViewModel).TimeOut =
                    new TimeSpan((int)NumUpDownHour.Value, (int)NumUpDownMin.Value, (int)NumUpDownSec.Value);
            }
        }

        private void NumUpDownSec_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (DataContext != null && NumUpDownHour != null && NumUpDownMin != null && NumUpDownSec != null)
            {
                (DataContext as ShutdownTimerViewModel).TimeOut =
                    new TimeSpan((int)NumUpDownHour.Value, (int)NumUpDownMin.Value, (int)NumUpDownSec.Value);
            }
        }

        
    }
}
