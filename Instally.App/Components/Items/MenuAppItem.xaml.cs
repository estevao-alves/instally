using Instally.App.Application.Entities;

namespace Instally.App.Components
{
    public partial class MenuAppItem : UserControl
    {
        string appName;
        string AppId;
        public PackageEntity PacoteWingetSelecionado;

        public delegate void ExcluirDaColecao();
        public event ExcluirDaColecao OnExcluir;

        public string AppName
        {
            get => appName;
            set
            {
                appName = value;
                Title.Text = value;
            }
        }

        public Guid CollectionId { get; set; }

        public bool IsActive { get; set; }

        public MenuAppItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public MenuAppItem(string appName, string appId, Guid collectionId)
        {
            InitializeComponent();

            AppName = appName;
            AppId = appId;
            CollectionId = collectionId;
            AdicionarIcone();
        }

        public void AdicionarIcone()
        {
            UIElement image = WingetData.CapturarFaviconDoPacote(AppName);
            WrapperAppIcon.Child = image;
        }

        public void AdicionarRemoverItemDoRodape(bool IsActive)
        {
            if (IsActive)
            {
                BorderWrapper.Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"];

                // Adicionar ao rodapé de instalação
                PacoteWingetSelecionado = WingetData.CapturarPacote(AppName);
                Master.Main.Footer.AdicionarApp(PacoteWingetSelecionado, CollectionId);
            }
            else
            {
                BorderWrapper.Background = (SolidColorBrush)new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                
                // Remover do rodapé de instalação
                if (PacoteWingetSelecionado is not null) Master.Main.Footer.RemoverApp(PacoteWingetSelecionado.Name);
            }
        }

        private void AppItem_MouseDown(object sender, MouseButtonEventArgs e) { IsActive = !IsActive; AdicionarRemoverItemDoRodape(IsActive); }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Remover do rodapé de existir
            AdicionarRemoverItemDoRodape(false);

            // ListaDeAplicativosAdicionados.Remover(AppId);

            // Excluir da coleção
            OnExcluir();
        }

        private void BorderWrapper_MouseEnter(object sender, MouseEventArgs e)
        {
            RemoveButton.Visibility = Visibility.Visible;
            
            BorderBackgroundHover.Background = new SolidColorBrush(Color.FromArgb(10, 255, 255, 255));
        }
        private void BorderWrapper_MouseLeave(object sender, MouseEventArgs e)
        {
            RemoveButton.Visibility = Visibility.Collapsed;

            BorderBackgroundHover.Background = null;
        }
    }
}
