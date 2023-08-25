using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WindowsGuide_WPF.Components;
using WindowsGuide_WPF.Components.Layout;
using WindowsGuide_WPF.Components.Popups;

namespace WindowsGuide_WPF
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
    }
}
