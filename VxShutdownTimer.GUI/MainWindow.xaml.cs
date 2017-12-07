using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Diagnostics;
using System.IO;

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
            string pdf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "help.pdf");
            if(File.Exists(pdf))
            {
                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo("Explorer.exe", pdf)
                };
                process.Start();
                
            }
            else
            {
                Process.Start("https://github.com/mirzaevolution/Vx-Shutdown-Timer");
            }
        }

        private void ButtonTray_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TrayIconInfo.Visibility = System.Windows.Visibility.Visible;
            TrayIconInfo.ShowBalloonTip("VX Shutdown Timer", "Click here to show/exit the app.",Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Info);
            Hide();
        }

        private void ButtonAbout_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            new About.AboutView().ShowDialog();
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
