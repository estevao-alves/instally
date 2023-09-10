using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;

namespace InstallyApp.Components.Layout
{
    public partial class TopBar : UserControl
    {
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

        private void MaximizeMinimize_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("MouseClick");
            if (App.Master.Main.WindowState == WindowState.Normal) App.Master.Main.WindowState = WindowState.Maximized;
            else App.Master.Main.WindowState = WindowState.Normal;
        }
    }
}
