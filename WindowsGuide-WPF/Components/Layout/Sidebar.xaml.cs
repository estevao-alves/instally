using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WindowsGuide_WPF.Components.Popups;

namespace WindowsGuide_WPF.Components.Layout
{
    public partial class Sidebar : UserControl
    {
        public Sidebar()
        {
            InitializeComponent();
            System.Windows.Media.Brush InitialBackgroundValue = SidebarSettings.Background.Clone();
        }

        private void SidebarSettings_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SidebarSettings.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#21c25b"));
        }

        private void SidebarSettings_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SidebarSettings.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#282828"));
        }
    }
}
