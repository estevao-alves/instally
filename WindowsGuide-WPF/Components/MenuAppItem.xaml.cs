using System;
using System.Windows.Controls;
using System.Windows.Media;
using WindowsGuide_WPF.Resources.Winget;

namespace WindowsGuide_WPF.Components
{
    public partial class MenuAppItem : UserControl
    {
        string appName;
        Border BorderAppFooter;
        Package PacoteWingetSelecionado;

        public string AppName
        {
            get => appName;
            set
            {
                appName = value;
                Title.Text = value;
            }
        }

        public bool IsActive { get; set; }

        public MenuAppItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public MenuAppItem(string name)
        {
            InitializeComponent();
            AppName = name;
            AdicionarIcone();
        }

        public void AdicionarIcone()
        {
            Image image = App.Master.Winget.CapturarFaviconDoPacote(AppName);
            WrapperAppIcon.Child = image;
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsActive = !IsActive;

            if (IsActive)
            {
                BorderWrapper.Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"];

                // Adicionar ao rodapé de instalação
                PacoteWingetSelecionado = App.Master.Winget.CapturarPacote(AppName);
                BorderAppFooter = App.Master.Main.Footer.AdicionarApp(PacoteWingetSelecionado);
            }
            else
            {
                BorderWrapper.Background = (SolidColorBrush)new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                // Remover do rodapé de instalação
                if(PacoteWingetSelecionado is not null) App.Master.Main.Footer.RemoverApp(BorderAppFooter, PacoteWingetSelecionado.Id);
            }
        }
    }
}
