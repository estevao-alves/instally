using System.Windows;
using System.Windows.Input;
using WindowsGuide_WPF.Components;

namespace WindowsGuide_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            CarregarAplicativos();
        }

        public void CarregarAplicativos()
        {
            AppsCategoria1.Children.Clear();

            MenuAppItem app1 = new("Blender");
            MenuAppItem app2 = new("Insomnia");
            MenuAppItem app3 = new("Inkscape");
            MenuAppItem app4 = new("Discord");
            MenuAppItem app5 = new("Obsidian");

            AppsCategoria1.Children.Add(app1);
            AppsCategoria1.Children.Add(app2);
            AppsCategoria1.Children.Add(app3);
            AppsCategoria1.Children.Add(app4);
            AppsCategoria1.Children.Add(app5);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
