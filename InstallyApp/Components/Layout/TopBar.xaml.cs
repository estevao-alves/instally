using InstallyApp.Application.Commands.UserCommands;
using InstallyApp.Application.Entities;
using InstallyApp.Application.Repository.Interfaces;

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
            if (Master.Main.WindowState == WindowState.Normal) Master.Main.WindowState = WindowState.Maximized;
            else Master.Main.WindowState = WindowState.Normal;
        }

        private void Account_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Master.Main.AreaDePopups.Children.Add(Master.Main.Login);
        }
    }
}
