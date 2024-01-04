using InstallyApp.Application.Entities;
using InstallyApp.Application.Queries;
using InstallyApp.Application.Queries.Interfaces;
using InstallyApp.Application.Repository.Interfaces;
using InstallyApp.Components.Items;
using InstallyApp.Components.Janelas;
using InstallyApp.Components.Layout;
using Microsoft.Extensions.DependencyInjection;

namespace InstallyApp
{
    public partial class MainWindow : Window
    {
        public Login Login;
        public PesquisaDeApps JanelaDePesquisa;
        public Collection ColecaoSelecionada;
        public CollectionAdd ElementCollectionAdd;

        List<CollectionEntity> collections { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            JanelaDePesquisa = new();
            Login = new();
            Janelas.Children.Add(Login);
            CarregarCollections();
        }

        public void CarregarCollections()
        {
            CollectionList.Children.Clear();

            ElementCollectionAdd = new();
            ElementCollectionAdd.Visibility = Visibility.Collapsed;
            CollectionList.Children.Add(ElementCollectionAdd);

            collections = Master.ServiceProvider.GetService<ICollectionQuery>().GetAll().ToList();

            if (!collections.Any())
            {
                Collection collection = new(0);
                CollectionList.Children.Add(collection);
            }
            else
            {
                for (int i = 0; i < collections.Count; i++)
                {
                    Collection collection = new(i);
                    CollectionList.Children.Add(collection);

                    Grid.SetColumn(collection, i);
                }
            }

            if(collections.Count < 4)
            {
                ElementCollectionAdd.Visibility = Visibility.Visible;
                Grid.SetColumn(ElementCollectionAdd, collections.Count);
            }
        }

        public void AdicionarAplicativosACollection(List<AppParaInstalar> list, Collection componentCollection)
        {
            foreach (AppParaInstalar app in list) componentCollection.AnexarAplicativoAColecao(app.Name, app.CodeId, true);
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
