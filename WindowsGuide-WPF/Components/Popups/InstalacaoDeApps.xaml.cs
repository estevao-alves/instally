using System.Windows.Controls;

namespace WindowsGuide_WPF.Components.Popups
{
    public partial class InstalacaoDeApps : UserControl
    {
        public InstalacaoDeApps()
        {
            InitializeComponent();
        }

        private void Times_Close(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            App.Master.Main.AreaDePopups.Children.Clear();
        }
    }
}
