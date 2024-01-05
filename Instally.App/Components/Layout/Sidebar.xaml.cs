namespace Instally.App.Components.Layout
{
    public partial class Sidebar : UserControl
    {
        public Sidebar()
        {
            InitializeComponent();
        }

        private void SidebarSettings_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SidebarSettings.Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"];
        }

        private void SidebarSettings_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SidebarSettings.Background = (SolidColorBrush)App.Current.Resources["TertiaryColor"];
        }
    }
}
