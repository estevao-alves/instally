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

            List<InstallyCollection> collections = InstallyCollections.CarregarLista();

            if (collections.Count < 1)
            {
                // Backend collection
                InstallyCollection coll = new InstallyCollection("My Collection");
                InstallyCollections.All.Add(coll);
                InstallyCollections.AtualizarColecao(coll, 0);

                // Frontend collection
                Collection collection = new Collection(coll, 0);
                CollectionList.Children.Add(collection);
            }
            else
            {
                for(int i = 0; i < collections.Count; i++)
                {
                    Collection collection = new Collection(collections[i], i);
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
            string? app = ListaDeAplicativosAdicionados.Apps.Find(name => name == appId);
            if (app is not null) return true;
            else return false;
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
