using System.Windows.Controls;
using System.Windows;

namespace InstallySetup.UI.Components.Layout
{
    public partial class TopBar : UserControl
    {
        public bool AllowMaximize
        {
            set
            {
                if(value) BtnMaximize.Visibility = Visibility.Visible;
                else BtnMaximize.Visibility = Visibility.Collapsed;
            }
        }

        public TopBar()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (App.Current.MainWindow.WindowState == WindowState.Normal)
            {
                App.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                App.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.WindowState = WindowState.Minimized;
        }

    }
}
