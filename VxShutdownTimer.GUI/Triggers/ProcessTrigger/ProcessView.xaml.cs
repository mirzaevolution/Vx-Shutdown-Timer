using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace VxShutdownTimer.GUI.Triggers.ProcessTrigger
{
    public partial class ProcessView : UserControl
    {
        private MetroWindow _metroWindow;
        public ProcessView()
        {
            InitializeComponent();
            _metroWindow = Application.Current.MainWindow as MetroWindow;
        }

        private async void ProcessViewModel_Info(object sender, string e)
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

        private async void ProcessViewModel_Error(object sender, string e)
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
