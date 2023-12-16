namespace InstallyApp
{
    public partial class DebugStatus : Window
    {
        public DebugStatus()
        {
            InitializeComponent();
        }

        public void CreateInfo(string? result)
        {
            TextBlock debugTextBlock = new()
            {
                TextWrapping = TextWrapping.Wrap,
                Text = $"{result}"
            };

            DebugWrapper.Content = debugTextBlock;
            DebugWrapper.ScrollToEnd();
        }
    }
}
