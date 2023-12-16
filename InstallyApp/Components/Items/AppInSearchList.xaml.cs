namespace InstallyApp.Components.Items
{
    public partial class AppInSearchList : UserControl
    {
        public bool IsActive = false;

        public string AppName;
        public string AppId;
        Button appInListaDeInstalacao;

        public AppInSearchList()
        {
            InitializeComponent();
            InfoIcon.Visibility = Visibility.Collapsed;
        }

        public AppInSearchList(string pacoteName, string pacoteId, bool pacoteJaAdicionado)
        {
            InitializeComponent();
            InfoIcon.Visibility = Visibility.Collapsed;

            AppName = pacoteName;
            AppId = pacoteId;
            CarregarInformacoesDoApp(pacoteName);
            IconeJaAdicionado(pacoteJaAdicionado);
        }

        public void IconeJaAdicionado(bool state)
        {
            IconIsAdded.Visibility = state ? Visibility.Visible : Visibility.Collapsed;
        }

        public void CarregarInformacoesDoApp(string pkgName)
        {
            UIElement? appIcon = WingetData.CapturarFaviconDoPacote(pkgName);
            if(appIcon is not null)
            {
                appIcon.RenderTransform = new TranslateTransform(-2.5F, 0.0F);
                WrapperIcon.Child = appIcon;
                WrapperIcon.Padding = new Thickness(5, 0, 0, 0);
            }

            Titulo.Text = pkgName;
        }

        private async void WrapperAppItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Master.Main.VerificarSeAplicativoJaFoiAdicionado(AppId))
            {
                this.AlertDropdownText.Text = $"App already added in: {App.Master.Main.ColecaoSelecionada.collection.Title}";

                AlertDropdownCanvas.Visibility = Visibility.Visible;
                await Task.Delay(3000);
                AlertDropdownCanvas.Visibility = Visibility.Collapsed;

                return;
            }

            Package pkg = WingetData.CapturarPacotePorId(AppId);

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
                Package pkg = WingetData.CapturarPacote(AppName);
                App.Master.Main.JanelaDePesquisa.DetalhesDoApp.AtualizarInformacoes(pkg);

                App.Master.Main.JanelaDePesquisa.DetalhesDoApp.Visibility = Visibility.Visible;
            }
            else App.Master.Main.JanelaDePesquisa.DetalhesDoApp.Visibility = Visibility.Collapsed;
            
            App.Master.Main.JanelaDePesquisa.AppList_ChangeColumns();

            e.Handled = true;
        }
    }
}
