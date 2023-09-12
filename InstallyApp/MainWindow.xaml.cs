using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InstallyApp.Application.Contexts;
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
        public CollectionAdd ElementCollectionAdd;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            JanelaDePesquisa = new();
            CarregarCollections();
        }

        public void CarregarCollections()
        {
            CollectionList.Children.Clear();

            ElementCollectionAdd = new();
            ElementCollectionAdd.Visibility = Visibility.Collapsed;
            CollectionList.Children.Add(ElementCollectionAdd);

            DirectoryInfo dirCollections = new("Collections");

            if(!dirCollections.Exists) Directory.CreateDirectory("Collections");

            FileInfo[] collections = dirCollections.GetFiles();
            ElementCollectionAdd.collectionNumber = collections.Length;

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
                ElementCollectionAdd.Visibility = Visibility.Visible;
                Grid.SetColumn(ElementCollectionAdd, dirCollections.GetFiles().Length);
            }
        }

        public void AdicionarAplicativosACollection(List<AppParaInstalar> list, Collection collection)
        {
            StreamWriter writer = new StreamWriter(@$"{collection.dirName}\{collection.Title}.txt", true);

            foreach (AppParaInstalar app in list)
            {
                writer.WriteLine(app.Name);
                
                MenuAppItem newApp = new(app.Name, collection.Title);
                newApp.OnExcluir += () =>
                {
                    collection.Apps.Children.Remove(newApp);
                    collection.AtualizarArquivo(app.Name);
                    ListaDeAplicativosAdicionados.Remover(app.Name);
                };
                collection.Apps.Children.Add(newApp);

                ListaDeAplicativosAdicionados.Adicionar(app.Name);
            }

            writer.Close();
        }

        public bool VerificarSeAplicativoJaFoiAdicionado(string appName)
        {
            string? app = ListaDeAplicativosAdicionados.Apps.Find(name => name == appName);
            if (app is not null) return true;
            else return false;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(WindowState == WindowState.Maximized) LayoutMain.Margin = new Thickness(5);
            else LayoutMain.Margin = new Thickness(0);
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

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (((Keyboard.Modifiers & ModifierKeys.Control) > 0) && (Keyboard.Modifiers & ModifierKeys.Shift) > 0)
            {
                if (e.Key == Key.T)
                {
                    App.Master.Debug = new();
                    App.Master.Debug.Show();
                }
            }
        }
    }
}
