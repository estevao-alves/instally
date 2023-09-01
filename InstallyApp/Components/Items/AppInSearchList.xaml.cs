using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InstallyApp.Resources.Winget;
using static System.Net.Mime.MediaTypeNames;

namespace InstallyApp.Components.Items
{
    public partial class AppInSearchList : UserControl
    {
        public bool IsActive = false;

        string appName;
        Button appInListaDeInstalacao;

        public AppInSearchList()
        {
            InitializeComponent();

            InfoIcon.Visibility = Visibility.Collapsed;
        }

        public AppInSearchList(string pacoteName)
        {

            InitializeComponent();

            this.appName = pacoteName;
            CarregarInformacoesDoApp(pacoteName);
            InfoIcon.Visibility = Visibility.Collapsed;
        }

        public void CarregarInformacoesDoApp(string pkgName)
        {

            UIElement appIcon = App.Master.Winget.CapturarFaviconDoPacote(pkgName);
            WrapperIcon.Child = appIcon;
            WrapperIcon.Padding = new Thickness(5, 0, 0, 0);
            Debug.WriteLine(appIcon);



            Titulo.Text = pkgName;
        }

        private async void WrapperAppItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Master.Main.ColecaoSelecionada.VerificarSeAplicativoJaExiste(appName))
            {
                AlertDropdownCanvas.Visibility = Visibility.Visible;
                await Task.Delay(3000);
                AlertDropdownCanvas.Visibility = Visibility.Collapsed;

                return;
            }

            Package pkg = App.Master.Winget.CapturarPacote(appName);

            if (IsActive) {
                IsActive = false;
                WrapperAppItem.Background = new SolidColorBrush(Color.FromArgb(0,0,0,0));

                App.Master.Main.JanelaDePesquisa.RemoverApp(appInListaDeInstalacao, pkg.Id);
            }
            else
            {
                IsActive = true;
                WrapperAppItem.Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"];
                
                // Adicionar a lista de instalação
                appInListaDeInstalacao = App.Master.Main.JanelaDePesquisa.AdicionarApp(pkg);
            }
        }

        public void jaAdicionadoIcon(string pacoteName)
        {
           
        }

        private void WrapperAppItem_MouseEnter(object sender, MouseEventArgs e)
        {
            InfoIcon.Visibility = Visibility.Visible;
        }

        private void WrapperAppItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!InfoIcon.IsActive) InfoIcon.Visibility = Visibility.Collapsed;
        }

        private void InfoIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (InfoIcon.IsActive)
            {
                Package pkg = App.Master.Winget.CapturarPacote(appName);
                App.Master.Main.JanelaDePesquisa.DetalhesDoApp.AtualizarInformacoes(pkg);

                App.Master.Main.JanelaDePesquisa.DetalhesDoApp.Visibility = Visibility.Visible;
            }
            else App.Master.Main.JanelaDePesquisa.DetalhesDoApp.Visibility = Visibility.Collapsed;
            
            App.Master.Main.JanelaDePesquisa.AppList_ChangeColumns();

            e.Handled = true;
        }
    }
}
