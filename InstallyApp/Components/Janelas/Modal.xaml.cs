namespace InstallyApp.Components.Janelas
{
    public partial class Modal : UserControl
    {
        public Modal()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Master.Main.Janelas.Children.Clear();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
