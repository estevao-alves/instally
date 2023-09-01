using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InstallyApp.Components;
using InstallyApp.Components.Items;
using InstallyApp.Components.Layout;
using InstallyApp.Components.Popups;

namespace InstallyApp
{
    public partial class MainWindow : Window
    {
        public PesquisaDeApps JanelaDePesquisa;
        public Collection ColecaoSelecionada;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            CarregarCollections();
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

        public void CarregarCollections()
        {
            CollectionList.Children.Clear();

            DirectoryInfo dirCollections = new("Collections");

            if(!dirCollections.Exists) Directory.CreateDirectory("Collections");

            FileInfo[] collections = dirCollections.GetFiles();

            if (collections.Length < 1)
            {
                Collection collection = new Collection("My Collection");
                CollectionList.Children.Add(collection);
            }
            else
            {
                for(int i = 0; i < collections.Length; i++)
                {
                    Collection collection = new Collection(collections[i].Name.Replace(collections[i].Extension, ""));
                    CollectionList.Children.Add(collection);

                    Grid.SetColumn(collection, i);
                }
            }

            if(dirCollections.GetFiles().Length < 4)
            {
                CollectionAdd collectionAdd = new();
                CollectionList.Children.Add(collectionAdd);

                Grid.SetColumn(collectionAdd, dirCollections.GetFiles().Length);
            }
        }

        public void AdicionarAplicativosACollection(List<AppParaInstalar> list, Collection collection)
        {
            StreamWriter writer = new StreamWriter(@$"{collection.dir}\{collection.Title}.txt", true);

            foreach (AppParaInstalar app in list)
            {
                writer.WriteLine(app.Name);
                MenuAppItem newApp = new(app.Name);
                newApp.OnExcluir += () =>
                {
                    collection.Apps.Children.Remove(newApp);
                    collection.AtualizarArquivo(app.Name);
                };

                collection.Apps.Children.Add(newApp);
            }

            writer.Close();
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
    }
}
