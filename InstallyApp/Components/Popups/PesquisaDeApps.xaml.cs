using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Controls;
using InstallyApp.Components.Items;
using InstallyApp.Components.Layout;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using InstallyApp.Components.Selectors;
using InstallyApp.Application.Functions;

namespace InstallyApp.Components.Popups
{
    public partial class PesquisaDeApps : UserControl
    {
        
        string? TextoPadraoSearch;
        string? CategoriaEscolhida;

        List<Package> PacotesEncontrados { get; set; } = new();
        int LimiteDeResultados = 0;

        public event System.EventHandler<ScrollChangedEventArgs> ViewChanged;

        public List<AppParaInstalar> ListaDeAppsParaColecionar;

        public PesquisaDeApps()
        {
            InitializeComponent();
            DataContext = this;

            AppList.Children.Clear();

            TextoPadraoSearch = SearchTextBox.Text;

            BarraDeRolagem.ScrollChanged += BarraDeRolagem_ScrollChanged;
        }

        private void BarraDeRolagem_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0)
            {
                if (e.VerticalOffset + e.ViewportHeight > (e.ExtentHeight - 200)) PesquisarPacotes();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            App.Master.Main.AreaDePopups.Children.Clear();
            ListaDeInstalacao.Children.Clear();
        }

        public void PesquisarPacotes()
        {
            string TextoDigitado = SearchTextBox.Text;
            string? filtro = TextoDigitado.Length > 0 ? (TextoDigitado != TextoPadraoSearch ? TextoDigitado : null) : null;

            PacotesEncontrados = WingetData.CapturarPacotes(filtro, CategoriaEscolhida, LimiteDeResultados, 42);
            LimiteDeResultados += 42;

            foreach (Package pacote in PacotesEncontrados)
            {
                AppInSearchList app = new(pacote.Name, App.Master.Main.VerificarSeAplicativoJaFoiAdicionado(pacote.Name));
                AppList.Children.Add(app);
            }
        }

        public void BuscarPorCategoria(string categoriaEscolhida)
        {
            AppList.Children.Clear();

            LimiteDeResultados = 0;

            if (categoriaEscolhida == "all") CategoriaEscolhida = null;
            else CategoriaEscolhida = categoriaEscolhida;

            foreach(Button button in DropdownCategoria.ListItems.Children)
            {
                button.Visibility = Visibility.Visible;

                TextBlock? textBlock = button.Content as TextBlock;
                if (textBlock?.Text.ToLower() == categoriaEscolhida) button.Visibility = Visibility.Collapsed;
            }

            PesquisarPacotes();
            BarraDeRolagem.ScrollToTop();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Cursor = Cursors.Arrow;

            if (string.IsNullOrEmpty(SearchTextBox.Text))
                Placeholder.Visibility = Visibility.Visible;
            else
                Placeholder.Visibility = Visibility.Collapsed;
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LimiteDeResultados = 0;
                AppList.Children.Clear();

                PesquisarPacotes();
                BarraDeRolagem.ScrollToTop();

                Keyboard.ClearFocus();
            }
        }

        private void Search_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LimiteDeResultados = 0;
            AppList.Children.Clear();

            PesquisarPacotes();
            BarraDeRolagem.ScrollToTop();
        }

        public void AppList_ChangeColumns()
        {
            if (ActualWidth < 1100) AppList.Columns = 4;
            else if (ActualWidth < 1600) AppList.Columns = 6;
            else AppList.Columns = 8;

            if (DetalhesDoApp.Visibility == Visibility.Visible) AppList.Columns = AppList.Columns - 2;
        }

        private void AppList_SizeChanged(object sender, SizeChangedEventArgs e) => AppList_ChangeColumns();

        public Button AdicionarApp(Package pkg)
        {
            ListaDeAppsParaColecionar.Add(new AppParaInstalar(pkg.Name, pkg.Id, App.Master.Main.ColecaoSelecionada.Title));

            Button borderWrapper = new()
            {
                Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"],
                Margin = new Thickness(0, 0, 10, 0),
                Width = 40,
                Height = 40,
                Style = (Style)App.Current.Resources["HoverEffect"],
                Content = WingetData.CapturarFaviconDoPacote(pkg.Name),
                Padding = new Thickness(6, 6, 0, 6),
            };

            ListaDeInstalacao.Children.Add(borderWrapper);

            return borderWrapper;
        }

        public void RemoverApp(Button appItemWithBorder, string wingetId)
        {
            ListaDeAppsParaColecionar = ListaDeAppsParaColecionar.FindAll(item => item.CodeId != wingetId);
            ListaDeInstalacao.Children.Remove(appItemWithBorder);
        }

        private void Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Master.Main.AdicionarAplicativosACollection(ListaDeAppsParaColecionar, App.Master.Main.ColecaoSelecionada);

            App.Master.Main.JanelaDePesquisa.ListaDeInstalacao.Children.Clear();

            App.Master.Main.AreaDePopups.Children.Clear();
        }

        private void SearchTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            App.Master.Main.MouseDown += (object sender, MouseButtonEventArgs e) => { SearchTextBox.Focusable = false; BarraDeRolagem.Focus(); };
        }

        private void SearchTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            SearchTextBox.Focusable = true;
        }
        private void DropdownCategoria_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DropdownCategoria.isActive)
            {
                // Verificar se o objeto clicado é um componente dropdown
                Dropdown objClicado = e.Source as Dropdown;

                // Se não for, fechar o componente dropdown
                if (objClicado is null) DropdownCategoria.Fechar();
            }
        }
    }
}
