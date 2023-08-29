using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InstallyApp.Components;
using InstallyApp.Components.Items;
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

        /*
        public async void VerificarInstalacaoWinget()
        {
            string wingetVersionCommand = await Task.Run(() => this.Footer.ExecutarCommand($"winget --version").Substring(0, 4).TrimStart('v'));
            double wingetVersion = Convert.ToDouble(wingetVersionCommand);

            if (wingetVersion >= 1.5)
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.WingetAvisoDeInstalacao.Visibility = Visibility.Collapsed;
                });
            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.WingetAvisoDeInstalacao.Visibility = Visibility.Visible;
                });
            }
        }
        */

        public void CarregarAplicativos()
        {
            Collection1.Apps.Children.Clear();

            try
            {
                StreamReader reader = new StreamReader("Apps.txt");

                string line = reader.ReadLine();

                while (line != null)
                {
                    MenuAppItem newApp = new(line);
                    Collection1.Apps.Children.Add(newApp);

                    line = reader.ReadLine();
                }

                reader.Close();
            } catch(Exception ex){ }
        }

        public void AdicionarAplicativosACollection(List<AppParaInstalar> list)
        {
            StreamWriter writer = new StreamWriter("Apps.txt", true);

            foreach (AppParaInstalar app in list)
            {
                writer.WriteLine(app.Name);
                MenuAppItem newApp = new(app.Name);

                Collection1.Apps.Children.Add(newApp);
            }

            writer.Close();
        }

        public void RemoverAplicativosDaCollection(List<AppParaInstalar> list)
        {
            try
            {
                StreamReader reader = new StreamReader("Apps.txt");

                string line = reader.ReadLine();

                while (line != null)
                {
                    MenuAppItem newApp = new(line);
                    Collection1.Apps.Children.Remove(newApp);

                    line = reader.ReadLine();
                }

                reader.Close();
            }
            catch (Exception ex) { }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(WindowState == WindowState.Maximized) LayoutMain.Margin = new Thickness(7);
            else LayoutMain.Margin = new Thickness(0);

            if (WindowState == WindowState.Maximized || ActualHeight == SystemParameters.WorkArea.Height)
            {
                LayoutMainOpacityMask.RadiusX = 0;
                LayoutMainOpacityMask.RadiusY = 0;

                LayoutMain.Margin = new Thickness(7);
            }
            else
            {
                LayoutMainOpacityMask.RadiusX = 10;
                LayoutMainOpacityMask.RadiusY = 10;

                LayoutMain.Margin = new Thickness(0);
            }
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();

                MoveWindowToMousePosition();
            }
        }

        private void MoveWindowToMousePosition()
        {
            if (WindowState == WindowState.Maximized)
            {
                Point getMousePosition = Mouse.GetPosition(this);
                var mouse = TranslatePoint(getMousePosition, null);

                Left = mouse.X - (Width / 2);
                Top = mouse.Y - (TopBar.Height / 2);
            }
        }

        private void AddCollection_Click(object sender, RoutedEventArgs e)
        {
            Collection collection = new();

            collection.Apps.Children.Clear();
            CollectionList.Children.Add(collection);

            if (CollectionList.Children.Count >= 4)
            {
                AddCollection.Visibility = Visibility.Collapsed;
            }
        }

        private void Collection1_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
