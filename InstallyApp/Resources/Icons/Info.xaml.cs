namespace InstallyApp.Resources.Icons
{
    public partial class Info : UserControl
    {
        bool active;

        public bool IsActive
        {
            get => active;
            set => ChangeActive(value);
        }

        public Info()
        {
            InitializeComponent();
        }

        public void ChangeActive(bool value)
        {
            active = value;
            if (value) CircleButton.Fill = new SolidColorBrush(Color.FromRgb(53, 109, 160));
            else CircleButton.Fill = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        private void InfoButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsActive = !IsActive;
        }
    }
}
