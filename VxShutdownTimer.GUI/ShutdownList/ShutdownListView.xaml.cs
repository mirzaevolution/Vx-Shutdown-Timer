using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Windows.Media.Imaging;

namespace VxShutdownTimer.GUI.ShutdownList
{
    
    public partial class ShutdownListView : UserControl
    {
       
        public ShutdownListView()
        {
            InitializeComponent();
        }

        private void ShutdownListViewModel_Info(object sender, string e)
        {

            BitmapImage image = new BitmapImage(new Uri("/Contents/info.png", UriKind.Relative));
            ImageNotification.Source = image;
            TextStatus.Text = e;
        }

        private void ShutdownListViewModel_Error(object sender, string e)
        {
            BitmapImage image = new BitmapImage(new Uri("/Contents/close.png", UriKind.Relative));
            ImageNotification.Source = image;
            TextStatus.Text = e;
        }
    }
}
