using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace InstallyApp.Components.Selectors
{
    public partial class Dropdown : UserControl
    {
        public bool IsActive = false;
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
                Button borderWrapper = new()
                {
                    Margin = new Thickness(2, 0, 2, 2),
                    Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"],
                    Style = (Style)App.Current.Resources["HoverEffect"],
                };

                borderWrapper.PreviewMouseDown += (object sender,MouseButtonEventArgs e) =>
                {
                    DropDownTitle.Text = item.ToUpper();
                    Callback(item);

                    // Close DropDown
                    IsActive = false;
                    ListItems.Visibility = Visibility.Collapsed;
                };

                TextBlock textBlock = new()
                {
                    Foreground = new SolidColorBrush(Colors.White),
                    Padding = new Thickness(15, 10, 15, 10),
                    FontWeight = FontWeights.SemiBold,
                    Text = item.ToUpper()
                };

                borderWrapper.Content = textBlock;

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
