using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using InstallyApp.Components;
using InstallyApp.Components.Layout;
using InstallyApp.Components.Popups;

namespace InstallyApp
{
    public partial class MainWindow : Window
    {
        public PesquisaDeApps JanelaDePesquisa;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            CarregarAplicativos();
        }

        public void CarregarAplicativos()
        {
            AppsCategoria1.Children.Clear();

            try
            {
                StreamReader reader = new StreamReader("Apps.txt");

                string line = reader.ReadLine();

                while (line != null)
                {
                    MenuAppItem newApp = new(line);
                    AppsCategoria1.Children.Add(newApp);

                    line = reader.ReadLine();
                }

                reader.Close();
            } catch(Exception ex)
            {

            }
        }

        public void AdicionarAplicativosACategoria(List<AppParaInstalar> list)
        {
            StreamWriter writer = new StreamWriter("Apps.txt", true);

            foreach (AppParaInstalar app in list)
            {
                writer.WriteLine(app.Name);
                MenuAppItem newApp = new(app.Name);
                AppsCategoria1.Children.Add(newApp);
            }

            writer.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Search_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            JanelaDePesquisa = new();

            App.Master.Main.AreaDePopups.Children.Add(JanelaDePesquisa);
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(WindowState == WindowState.Maximized) LayoutMain.Margin = new Thickness(7);
            else LayoutMain.Margin = new Thickness(0);
        }

        private void TopBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Normal) WindowState = WindowState.Maximized;
            else WindowState = WindowState.Normal;
        }

        private void TopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (WindowState == WindowState.Maximized) WindowState = WindowState.Normal;
                DragMove();
            }
        }
    }
}
