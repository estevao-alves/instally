using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Controls;
using InstallyApp.Components.Items;
using InstallyApp.Resources.Winget;
using InstallyApp.Components.Layout;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;

namespace InstallyApp.Components.Popups
{
    public partial class PesquisaDeApps : UserControl
    {
        
        string? TextoPadraoSearch;
        string? CategoriaEscolhida;

        List<Package> PacotesEncontrados { get; set; } = new();
        int LimiteDeResultados = 0;

        public event System.EventHandler<ScrollChangedEventArgs> ViewChanged;

        List<AppParaInstalar> ListaDeAppParaInstalar;

        public PesquisaDeApps()
        {
            InitializeComponent();
            DataContext = this;

            AppList.Children.Clear();

            TextoPadraoSearch = SearchTextBox.Text;

            ListaDeAppParaInstalar = new();
            PesquisarPacotes();

            BarraDeRolagem.ScrollChanged += BarraDeRolagem_ScrollChanged;
        }

        private void BarraDeRolagem_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0)
            {
                if (e.VerticalOffset + e.ViewportHeight > (e.ExtentHeight - 200)) PesquisarPacotes();
            }
        }

        private void Times_Close(object sender, RoutedEventArgs e)
        {
            App.Master.Main.AreaDePopups.Children.Clear();
        }

        public void PesquisarPacotes()
        {
            string TextoDigitado = SearchTextBox.Text;
            string? filtro = TextoDigitado.Length > 0 ? (TextoDigitado != TextoPadraoSearch ? TextoDigitado : null) : null;

            PacotesEncontrados = App.Master.Winget.CapturarPacotes(filtro, CategoriaEscolhida).Skip(LimiteDeResultados).Take(42).ToList();
            LimiteDeResultados = PacotesEncontrados.Count;

            foreach (Package pacote in PacotesEncontrados) AppList.Children.Add(new AppInSearchList(pacote.Name));
        }

        public void BuscarPorCategoria(string categoriaEscolhida)
        {
            LimiteDeResultados = 0;
            AppList.Children.Clear();

            if (categoriaEscolhida == "all") CategoriaEscolhida = null;
            else CategoriaEscolhida = categoriaEscolhida;

            foreach(Button button in Dropdown_Categoria.ListItems.Children)
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
            if (string.IsNullOrEmpty(SearchTextBox.Text))
                Placeholder.Visibility = Visibility.Visible;
            else
                Placeholder.Visibility = Visibility.Collapsed;
        
            SearchTextBox.Select(SearchTextBox.Text.Length, 0);
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LimiteDeResultados = 0;
                AppList.Children.Clear();

                PesquisarPacotes();
                BarraDeRolagem.ScrollToTop();
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
            if (ActualWidth < 1100)
            {
                AppList.Columns = 4;
            }
            else if (ActualWidth < 1600)
            {
                AppList.Columns = 6;
            }
            else
            {
                AppList.Columns = 8;
            }

            if (DetalhesDoApp.Visibility == Visibility.Visible) AppList.Columns = AppList.Columns - 2;
        }

        private void AppList_SizeChanged(object sender, SizeChangedEventArgs e) => AppList_ChangeColumns();

        public Button AdicionarApp(Package pkg)
        {
            ListaDeAppParaInstalar.Add(new AppParaInstalar(pkg.Name, pkg.Id));

            Button borderWrapper = new()
            {
                Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"],
                Margin = new Thickness(0, 0, 10, 0),
                Width = 40,
                Height = 40,
                Style = (Style)App.Current.Resources["HoverEffect"],
                Content = App.Master.Winget.CapturarFaviconDoPacote(pkg.Name),
                Padding = new Thickness(6, 6, 0, 6),
            };

            ListaDeInstalacao.Children.Add(borderWrapper);

            return borderWrapper;
        }

        public void RemoverApp(Button appItemWithBorder, string wingetId)
        {
            ListaDeAppParaInstalar = ListaDeAppParaInstalar.FindAll(item => item.CodeId != wingetId);
            ListaDeInstalacao.Children.Remove(appItemWithBorder);
        }

        private void Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Master.Main.AdicionarAplicativosACollection(ListaDeAppParaInstalar);
            App.Master.Main.AreaDePopups.Children.Clear();
        }
    }
}
