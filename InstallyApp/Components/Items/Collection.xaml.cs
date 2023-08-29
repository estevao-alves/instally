using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace InstallyApp.Components.Items
{
    public partial class Collection : UserControl
    {
        string MudarNomeDaCollection;
        public Collection()
        {
            InitializeComponent();
        }

        private void Search_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Master.Main.JanelaDePesquisa = new();

            App.Master.Main.AreaDePopups.Children.Add(App.Master.Main.JanelaDePesquisa);
        }

        private void EditName_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MudarNomeDaCollection = NomeDaCollection.Text;
            Debug.WriteLine(MudarNomeDaCollection);
        }
    }
}
