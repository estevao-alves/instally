using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WindowsGuide_WPF.Resources.Winget;

namespace WindowsGuide_WPF.Components.Items
{
    public partial class AppInSearchList : UserControl
    {
        public bool IsActive = false;

        public AppInSearchList()
        {
            InitializeComponent();
        }

        public AppInSearchList(string appName)
        {
            InitializeComponent();
            CarregarInformacoesDoApp(appName);
        }

        public void CarregarInformacoesDoApp(string pkgName)
        {
            UIElement imgOrText = App.Master.Winget.CapturarFaviconDoPacote(pkgName);
         
            WrapperIcon.Child = imgOrText;
            WrapperIcon.Padding = new Thickness(5, 0, 0, 0);

            Titulo.Text = pkgName;
        }

        private void WrapperAppItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsActive) {
                IsActive = false;
                WrapperAppItem.Background = new SolidColorBrush(Color.FromArgb(0,0,0,0));
            }
            else
            {
                IsActive = true;
                WrapperAppItem.Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"];
            }
        }
    }
}
