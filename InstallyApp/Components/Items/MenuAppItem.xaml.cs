using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InstallyApp.Resources.Winget;
using InstallyApp.Application.Contexts;

namespace InstallyApp.Components
{
    public partial class MenuAppItem : UserControl
    {
        string appName;
        public Package PacoteWingetSelecionado;

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

        public string CollectionName { get; set; }

        public bool IsActive { get; set; }

        public MenuAppItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public MenuAppItem(string name, string collectionName)
        {
            InitializeComponent();

            AppName = name;
            CollectionName = collectionName;
            AdicionarIcone();
        }

        public void AdicionarIcone()
        {
            UIElement image = App.Master.Winget.CapturarFaviconDoPacote(AppName);
            WrapperAppIcon.Child = image;
        }

        public void AdicionarRemoverItemDoRodape(bool IsActive)
        {
            if (IsActive)
            {
                BorderWrapper.Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"];

                // Adicionar ao rodapé de instalação
                PacoteWingetSelecionado = App.Master.Winget.CapturarPacote(AppName);
                App.Master.Main.Footer.AdicionarApp(PacoteWingetSelecionado, CollectionName);
            }
            else
            {
                BorderWrapper.Background = (SolidColorBrush)new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                
                // Remover do rodapé de instalação
                if (PacoteWingetSelecionado is not null) App.Master.Main.Footer.RemoverApp(PacoteWingetSelecionado.Name);
            }
        }

        private void AppItem_MouseDown(object sender, MouseButtonEventArgs e) { IsActive = !IsActive; AdicionarRemoverItemDoRodape(IsActive); }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Remover do rodapé de existir
            AdicionarRemoverItemDoRodape(false);

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
