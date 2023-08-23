using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Controls;
using WindowsGuide_WPF.Components.Items;
using WindowsGuide_WPF.Resources.Winget;
using System.Linq;
using System.Diagnostics;
using System.Windows.Media;

namespace WindowsGuide_WPF.Components.Popups
{
    public partial class PesquisaDeApps : UserControl
    {
        List<Package> PacotesEncontrados { get; set; } = new();
        int LimiteDeResultados = 42;

        public event System.EventHandler<ScrollChangedEventArgs> ViewChanged;

        public PesquisaDeApps()
        {
            InitializeComponent();
            DataContext = this;
            AppList.Children.Clear();

            PesquisarPacotes();
            BarraDeRolagem.ScrollChanged += BarraDeRolagem_ScrollChanged;
        }

        private void BarraDeRolagem_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0)
            {
                if (e.VerticalOffset + e.ViewportHeight > (e.ExtentHeight - 200))
                {
                    BuscarMaisPacotes(42);
                }
            }
        }

        private void Times_Close(object sender, MouseButtonEventArgs e)
        {
            App.Master.Main.AreaDePopups.Children.Clear();
        }

        private void TextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;
        }

        public void PesquisarPacotes()
        {
            AppList.Children.Clear();
            PacotesEncontrados = App.Master.Winget.CapturarPacotes(SearchTextBox.Text).Take<Package>(LimiteDeResultados).ToList();

            foreach (Package pacote in PacotesEncontrados) AppList.Children.Add(new AppInSearchList(pacote.Name));
            if (PacotesEncontrados.Count < LimiteDeResultados) LimiteDeResultados = PacotesEncontrados.Count;
        }

        public void BuscarMaisPacotes(int qtd)
        {
            LimiteDeResultados += qtd;
            PesquisarPacotes();
        }

        private void SearchTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LimiteDeResultados = 42;
                BarraDeRolagem.ScrollToTop();
                PesquisarPacotes();
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LimiteDeResultados = 42;
            BarraDeRolagem.ScrollToTop();
            PesquisarPacotes();
        }

        private void AppList_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
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
        }
    }
}
