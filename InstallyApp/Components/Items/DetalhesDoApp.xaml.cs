using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InstallyApp.Resources.Winget;

namespace InstallyApp.Components.Items
{
    public partial class DetalhesDoApp : UserControl
    {
        bool showMoreActive = true;
        public DetalhesDoApp()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void AtualizarInformacoes(Package pkg)
        {
            // Remover IsActive (InfoIcon) de todos os outros pacotes
            foreach (AppInSearchList appItem in App.Master.Main.JanelaDePesquisa.AppList.Children)
            {
                if (pkg.Description.Length < 80)
                {
                    BorderShowMore.Visibility = Visibility.Collapsed;
                }
                else
                {
                    BorderShowMore.Visibility = Visibility.Visible;
                }

                if (appItem.Titulo.Text != pkg.Name)
                {
                    appItem.InfoIcon.IsActive = false;
                    appItem.InfoIcon.Visibility = Visibility.Collapsed;
                }
            }

            // Carregar as informações do pacote em tela
            UIElement appIcon = WingetData.CapturarFaviconDoPacote(pkg.Name);
            appIcon.RenderTransform = new TranslateTransform(-2.5F, 0.0F);
            WrapperIcon.Child = appIcon;
            WrapperIcon.Padding = new Thickness(5, 0, 0, 0);

            Description.Text = pkg.Description;
            Publisher.Text = pkg.Publisher;
            LatestVersion.Text = pkg.LatestVersion;

            TagsList.Children.Clear();

            foreach (string tag in pkg.Tags)
            {
                Border border = new();

                TextBlock textBlock = new()
                { 
                    Text = tag,
                    Style = (Style)TagsList.Resources["TextBlockItem"]
                };

                border.Child = textBlock;

                TagsList.Children.Add(border);
            }
        }

        public void Description_ChangeShowText(bool? showMoreActive)
        {
            if (Description.Text.Length > 200)
            {
                BorderShowMore.Visibility = Visibility.Visible;
            }

            if (showMoreActive is true && TextShowDescription.Text == "Full Description")
            {
                Description.MaxHeight = double.PositiveInfinity;

                TextShowDescription.Text = "Show Less";
                IconShowDescription.RenderTransform = new RotateTransform(180);
            } else
            {
                Description.MaxHeight = 80;

                TextShowDescription.Text = "Full Description";
                IconShowDescription.RenderTransform = new RotateTransform(0);
            }
        }

        private void BorderShowMore_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(!showMoreActive)
            {
                showMoreActive = false;
                Description_ChangeShowText(showMoreActive);
            }
            {
                showMoreActive = true;
                Description_ChangeShowText(showMoreActive);
            }
        }
    }
}
