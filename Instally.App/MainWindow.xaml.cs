using Instally.App.Application.Entities;
using Instally.App.Application.Queries.Interfaces;
using Instally.App.Components.Items;
using Instally.App.Components.Janelas;
using Instally.App.Components.Layout;
using Microsoft.Extensions.DependencyInjection;

namespace Instally.App
{
    public partial class MainWindow : Window
    {
        public Login Login;
        public PesquisaDeApps JanelaDePesquisa;
        public CollectionItem ColecaoSelecionada;
        public AddCollection ElementCollectionAdd;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            JanelaDePesquisa = new();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login = new();

            UserEntity usuario = Master.ServiceProvider.GetService<IUserQuery>().GetAll().FirstOrDefault();

            if (usuario is not null) Master.UsuarioAutenticado = usuario;
            else Master.Main.Janelas.Children.Add(Login);

            var packagesQuery = Master.ServiceProvider.GetService<IPackageQuery>();
            Master.Packages = packagesQuery.GetAll().ToList();

            CarregarCollections();
        }

        public async void CarregarCollections()
        {
            Master.Collections = Master.ServiceProvider.GetService<ICollectionQuery>().GetAll().ToList();

            CollectionList.Children.Clear();

            ElementCollectionAdd = new();
            ElementCollectionAdd.Visibility = Visibility.Collapsed;
            CollectionList.Children.Add(ElementCollectionAdd);

            if (!Master.Collections.Any() && Master.UsuarioAutenticado is not null)
            {
                ElementCollectionAdd.AdicionarCollectionPadrao();

                ElementCollectionAdd.Visibility = Visibility.Visible;
                Grid.SetColumn(ElementCollectionAdd, 1);
            }
            else
            {
                for (int i = 0; i < Master.Collections.Count; i++)
                {
                    CollectionItem collectionAtual = new(i, Master.Collections[i].Title, Master.Collections[i].Packages, Master.Collections[i]);
                    ColecaoSelecionada = collectionAtual;
                    CollectionList.Children.Add(collectionAtual);

                    Grid.SetColumn(collectionAtual, i);
                }

                if(Master.Collections.Count < 4)
                {
                    ElementCollectionAdd.Visibility = Visibility.Visible;
                    Grid.SetColumn(ElementCollectionAdd, Master.Collections.Count);
                }
            }
        }

        public void AdicionarAplicativosACollection(List<AppParaInstalar> list, CollectionItem componentCollection, Guid collectionId)
        {
            foreach (AppParaInstalar app in list) componentCollection.AnexarAplicativoAColecao(app.Name, app.CodeId, collectionId, true);
        }

        public bool VerificarSeAplicativoJaFoiAdicionado(string appId)
        {
            /*
            string? app = ListaDeAplicativosAdicionados.Apps.Find(name => name == appId);
            if (app is not null) return true;
            else return false;
            */
            return false;
        }

        private void Window_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                LayoutMain.Margin = new Thickness(5);
                LayoutMainOpacityMask.RadiusX = 0;
                LayoutMainOpacityMask.RadiusY = 0;
            }
            {
                LayoutMainOpacityMask.RadiusX = 10;
                LayoutMainOpacityMask.RadiusY = 10;
            }
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
                DragMove();

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
                    Master.Debug = new();
                    Master.Debug.Show();
                }
            }
        }
    }
}
