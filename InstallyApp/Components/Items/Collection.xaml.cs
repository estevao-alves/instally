using InstallyApp.Application.Queries;

namespace InstallyApp.Components.Items
{
    public partial class Collection : UserControl
    {
        public InstallyCollection collection { get; set; }
        public int collectionIndex { get; set; }

        public bool isActive = false;

        public Collection()
        {
            InitializeComponent();
        }

        public Collection(InstallyCollection coll, int collIndex)
        {
            // Inicia
            InitializeComponent();
            Apps.Children.Clear();

            collection = coll;
            collectionIndex = collIndex;

            // Define
            CollectionTextBox.Text = coll.Title;

            VerOpcoesConfiguracao();

            // Carrega
            CarregarApps();
        }

        private void CarregarApps()
        {
            var appName = "Blender";
            foreach (string appId in collection.Packages) AnexarAplicativoAColecao(appName, appId, false);
        }

        public void AnexarAplicativoAColecao(string appName, string appId, bool updateCollection)
        {
            Package? pkg = WingetData.CapturarPacotePorId(appId);
            if (pkg is null) return;

            MenuAppItem newApp = new(pkg.Name, appId, collection.Title);

            newApp.OnExcluir += () =>
            {
                // Frontend changes
                Apps.Children.Remove(newApp);
                collection.Packages.Remove(appId);

                // Contexto Apps Adicionados
                ListaDeAplicativosAdicionados.Remover(newApp.AppName);

                // Backend changes
                InstallyCollections.AtualizarColecao(collection, collectionIndex);
            };

            // Frontend changes
            Apps.Children.Add(newApp);

            // Contexto Apps Adicionados
            ListaDeAplicativosAdicionados.Adicionar(appId);

            // Backend changes
            if(updateCollection)
            {
                collection.Packages.Add(appId);
                InstallyCollections.AtualizarColecao(collection, collectionIndex);
            }
        }

        private void AdicionarApp_Click(object sender, RoutedEventArgs e)
        {
            Master.Main.ColecaoSelecionada = this;
            Master.Main.AreaDePopups.Children.Add(Master.Main.JanelaDePesquisa);

            Master.Main.JanelaDePesquisa.AppList.Children.Clear();
            Master.Main.JanelaDePesquisa.BuscarPorCategoria("all");
            Master.Main.JanelaDePesquisa.ListaDeAppsParaColecionar = new();
        }

        private async void EditName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CollectionTextBox.IsEnabled = true;
            await Task.Delay(10);

            CollectionTextBox.Focus();
            CollectionTextBox.Select(CollectionTextBox.Text.Length, 0);

            ChangeIcon();
        }

        private void CollectionTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) AtualizarNome();
        }

        private void ClickOutside(object sender, MouseButtonEventArgs e) => AtualizarNome();

        private void CollectionTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            Master.Main.MouseDown -= ClickOutside;
            Master.Main.MouseDown += ClickOutside;
        }

        public void ChangeIcon()
        {
            Master.Main.MouseEnter += (object sender, MouseEventArgs e) => { EditPen.Visibility = Visibility.Visible; };

            if (CollectionTextBox.IsEnabled) EditPen.Visibility = Visibility.Visible;
            if (!CollectionTextBox.IsEnabled) EditPen.Visibility = Visibility.Collapsed;
        }

        private void CollectionButton_MouseEnter(object sender, MouseEventArgs e)
        {
            EditPen.Visibility = Visibility.Visible;
        }

        private void CollectionButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!CollectionTextBox.IsEnabled) EditPen.Visibility = Visibility.Collapsed;
        }

        private void AtualizarNome()
        {
            CollectionTextBox.IsEnabled = false;
            ChangeIcon();
            Keyboard.ClearFocus();

            string newTitle = CollectionTextBox.Text;

            if (InstallyCollections.All.Where(coll => coll.Title == newTitle).ToList().Count > 0)
            {
                CollectionTextBox.Text = collection.Title;
                return;
            }

            collection.Title = newTitle;
            
            InstallyCollections.AtualizarColecao(collection, collectionIndex);
        }

        private void VerOpcoesConfiguracao()
        {
            if (isActive)
            {
                CollectionGearButton.Background = new SolidColorBrush(Color.FromArgb(255, 45, 45, 51));
                CollectionRemoveButton.Visibility = Visibility.Visible;
                isActive = false;
            }
            else
            {
                CollectionGearButton.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                CollectionRemoveButton.Visibility = Visibility.Collapsed;
                isActive = true;
            }
        }

        private void GearButton_Click(object sender, RoutedEventArgs e) => VerOpcoesConfiguracao();

        private async void RemoveCollection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int ColunaAtual = Grid.GetColumn(this);

                Master.Main.Footer.RemoverAppsPorColecao(collection.Title);
                Master.Main.CollectionList.Children.Remove(this);

                InstallyCollections.All.RemoveAt(collectionIndex);
                InstallyCollections.AtualizarArquivo();

                foreach (UIElement coll in Master.Main.CollectionList.Children)
                {
                    int colunaDoElemento = Grid.GetColumn(coll);
                    if (colunaDoElemento > ColunaAtual) Grid.SetColumn(coll, colunaDoElemento - 1);
                }

                if (InstallyCollections.All.Count <= 3) Master.Main.ElementCollectionAdd.Visibility = Visibility.Visible;

            }
            catch(Exception ex)
            {
                await Task.Delay(1000);
                CollectionRemoveButton.Opacity = 1;
                CollectionRemoveButton.Cursor = Cursors.Hand;

                Debug.WriteLine(ex.Message);
            }
        }
    }
}
