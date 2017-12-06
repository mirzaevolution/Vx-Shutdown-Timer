using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
namespace VxShutdownTimer.GUI
{

    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonHelp_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ButtonTray_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TrayIconInfo.Visibility = System.Windows.Visibility.Visible;
            Hide();
        }

        private void ButtonAbout_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ButtonShowWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TrayIconInfo.Visibility = System.Windows.Visibility.Collapsed;
            Show();
        }

        private async void ButtonExitWindow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TrayIconInfo.Visibility = System.Windows.Visibility.Collapsed;
            Show();
            if (await this.ShowMessageAsync("Exit","Are you sure want to exit?",MessageDialogStyle.AffirmativeAndNegative,new MetroDialogSettings
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
                ColorScheme = MetroDialogColorScheme.Inverted
            }) == MessageDialogResult.Affirmative)
            {
              
                App.Current.Shutdown();
            }
        }
    }
}
