using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace WindowsGuide_WPF.Components.Layout
{

    public partial class TopBar : UserControl
    {
        public TopBar()
        {
            InitializeComponent();
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.MainWindow.Close();
        }

        private void Maximize_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void Minimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
