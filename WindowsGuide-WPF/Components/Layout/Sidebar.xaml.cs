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
        }

        private void Search_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PesquisaDeApps janelaDePesquisa = new();

            App.Master.Main.AreaDePopups.Children.Add(janelaDePesquisa);
        }
    }
}
