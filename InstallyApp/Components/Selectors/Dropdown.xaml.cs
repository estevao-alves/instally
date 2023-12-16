namespace InstallyApp.Components.Selectors
{
    public partial class Dropdown : UserControl
    {
        public bool isActive = false;
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
                    List<string> itemsHandled = new();

                    foreach(string categoryItems in value.Split(","))
                    {
                        string item = categoryItems.Trim();
                        itemsHandled.Add(item);
                    }
                    
                    items = itemsHandled.ToArray();
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

            foreach (string item in items)
            {

                string firstItem = item.Split(" ")[0];

                Button borderWrapper = new()
                {
                    Margin = new Thickness(2, 0, 2, 2),
                    Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"],
                    Style = (Style)App.Current.Resources["HoverEffect"],
                };

                borderWrapper.PreviewMouseDown += (object sender, MouseButtonEventArgs e) =>
                {
                    DropDownTitle.Text = firstItem.ToUpper();
                    Callback(item);
                    
                    Fechar();
                };

                TextBlock textBlock = new()
                {
                    Foreground = new SolidColorBrush(Colors.White),
                    Padding = new Thickness(15, 10, 15, 10),
                    FontWeight = FontWeights.SemiBold,
                    Text = firstItem.ToUpper()
                };

                borderWrapper.Content = textBlock;

                ListItems.Children.Add(borderWrapper);
            }
        }

        public void Abrir()
        {
            ListItems.Visibility = Visibility.Visible;
            isActive = true;
            Arrow.RenderTransform = new RotateTransform(180);
        }

        public void Fechar()
        {
            ListItems.Visibility = Visibility.Collapsed;
            isActive = false;
            Arrow.RenderTransform = new RotateTransform(0);
        }

        private void TagButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isActive) Fechar();
            else Abrir();
        }
    }
}
