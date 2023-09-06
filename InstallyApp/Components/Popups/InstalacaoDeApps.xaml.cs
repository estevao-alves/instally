using System.Diagnostics;
using System.Windows.Controls;

namespace InstallyApp.Components.Popups
{
    public partial class InstalacaoDeApps : UserControl
    {
        public InstalacaoDeApps()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Close_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.Master.Main.AreaDePopups.Children.Clear();
        }
    }
}
