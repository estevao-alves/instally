using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WindowsGuide_WPF.Components.Selectors
{
    public partial class Dropdown : UserControl
    {
        bool IsActive = false;
        public string[] items = new string[0];

        public string Titulo
        {
            set {
                DropDownTitle.Text = value.ToUpper();
            }
        }

        public string Opcoes
        {
            set
            {
                if (value.Split(",") is not null)
                {
                    items = value.Split(",").ToArray();
                    CarregarLista();
                }
            }
        }

        public delegate void CallbackEvent(string itemEscolhido);
        public CallbackEvent Callback { get; set; }

        public Dropdown()
        {
            InitializeComponent();

            ListItems.Visibility = Visibility.Collapsed;
        }

        public void CarregarLista()
        {
            ListItems.Children.Clear();

            foreach(string item in items)
            {
                Border borderWrapper = new()
                {
                    Margin = new Thickness(2, 0, 2, 2),
                    CornerRadius = new CornerRadius(10),
                    Style = (Style)App.Current.Resources["HoverEffect"]
                };

                borderWrapper.MouseDown += (object sender, MouseButtonEventArgs e) =>
                {
                    DropDownTitle.Text = item.ToUpper();
                    Callback(item);
                };

                TextBlock textBlock = new()
                {
                    Foreground = new SolidColorBrush(Colors.White),
                    Padding = new Thickness(15, 10, 15, 10),
                    FontWeight = FontWeights.SemiBold,
                    Text = item.ToUpper()
                };

                borderWrapper.Child = textBlock;

                ListItems.Children.Add(borderWrapper);
            }
        }

        private void TagButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsActive)
            {
                IsActive = false;
                ListItems.Visibility = Visibility.Collapsed;
            }
            else
            {
                IsActive = true;
                ListItems.Visibility = Visibility.Visible;
            }
        }
    }
}
