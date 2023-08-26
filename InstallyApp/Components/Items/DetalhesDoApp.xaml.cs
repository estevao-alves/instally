using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InstallyApp.Resources.Winget;

namespace InstallyApp.Components.Items
{
    public partial class DetalhesDoApp : UserControl
    {
        public DetalhesDoApp()
        {
            InitializeComponent();
        }

        public void AtualizarInformacoes(Package pkg)
        {
            Description_ChangeShowText(true);

            // Remover IsActive (InfoIcon) de todos os outros pacotes
            foreach (AppInSearchList appItem in App.Master.Main.JanelaDePesquisa.AppList.Children)
            {
                if(appItem.Titulo.Text != pkg.Name)
                {
                    appItem.InfoIcon.IsActive = false;
                    appItem.InfoIcon.Visibility = Visibility.Collapsed;
                }
            }

            // Carregar as informações do pacote em tela
            UIElement icon = App.Master.Winget.CapturarFaviconDoPacote(pkg.Name);

            WrapperIcon.Child = icon;
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

        public void Description_ChangeShowText(bool? setDefault)
        {
            if(setDefault is null && TextShowDescription.Text == "Full Description")
            {
                Description.MaxHeight = double.PositiveInfinity;

                TextShowDescription.Text = "Show Less";
                IconShowDescription.RenderTransform = new RotateTransform(180);
            } else
            {
                Description.MaxHeight = 46;

                TextShowDescription.Text = "Full Description";
                IconShowDescription.RenderTransform = new RotateTransform(0);
            }
        }

        private void BorderShowMore_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => Description_ChangeShowText(null);
    }
}
