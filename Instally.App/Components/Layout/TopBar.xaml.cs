using Instally.App.Application.Commands.UserCommands;
using Instally.App.Application.Entities;
using Instally.App.Application.Repository.Interfaces;

namespace Instally.App.Components.Layout
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
            Master.Main.Janelas.Children.Add(Master.Main.Login);
        }
    }
}
