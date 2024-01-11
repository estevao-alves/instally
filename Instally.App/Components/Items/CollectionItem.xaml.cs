using Instally.App.Application.Commands.UserCommands;
using Instally.App.Application.Entities;
using Instally.App.Application.Functions;
using Instally.App.Application.Queries.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Instally.App.Components.Items
{
    public partial class CollectionItem : UserControl
    {
        public List<PackageEntity> CollectionPackages { get; set; }
        public CollectionEntity Collection { get; set; }
        public int collectionIndex { get; set; }

        public bool isActive = false;

        public CollectionItem()
        {
            InitializeComponent();
        }

        public CollectionItem(int collIndex, string collectionTitulo, List<PackageEntity> collectionPackages, CollectionEntity collection)
        {
            InitializeComponent();
            Apps.Children.Clear();

            collectionIndex = collIndex;

            Collection = collection;
            CollectionTitulo.Text = collectionTitulo;
            CollectionPackages = collectionPackages;

            VerOpcoesConfiguracao();
        }

        public async void AnexarAplicativoAColecao(string appName, string wingetId, Guid collectionId, bool updateCollection)
        {
            PackageEntity package = Master.Packages.Where(p => p.WingetId == wingetId).FirstOrDefault();

            MenuAppItem newApp = new(appName, wingetId, collectionId);

            newApp.OnExcluir += () =>
            {
                // package.LimparCollection();

                Apps.Children.Remove(newApp);
                Collection.Packages.RemoveAll(p => p.WingetId == wingetId);
            };

            if (updateCollection)
            {
                AddToCollectionCommand command = new(package.Id, Collection.Id);
                bool resultado = await Master.Mediator.Send(command);

                if (resultado)
                {
                    Apps.Children.Add(newApp);
                }
            }
        }

        private void AdicionarApp_Click(object sender, RoutedEventArgs e)
        {
            Master.Main.ColecaoSelecionada = this;
            Master.Main.Janelas.Children.Add(Master.Main.JanelaDePesquisa);

            Master.Main.JanelaDePesquisa.AppList.Children.Clear();
            Master.Main.JanelaDePesquisa.BuscarPorCategoria("all");
            Master.Main.JanelaDePesquisa.ListaDeAppsParaColecionar = new();
        }

        private async void EditName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CollectionTitulo.IsEnabled = true;
            await Task.Delay(10);

            CollectionTitulo.Focus();
            CollectionTitulo.Select(CollectionTitulo.Text.Length, 0);

            ChangeIcon();
        }

        private void CollectionTitulo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) AtualizarNome();
        }

        private void ClickOutside(object sender, MouseButtonEventArgs e) => AtualizarNome();

        private void CollectionTitulo_MouseLeave(object sender, MouseEventArgs e)
        {
            Master.Main.MouseDown -= ClickOutside;
            Master.Main.MouseDown += ClickOutside;
        }

        public void ChangeIcon()
        {
            Master.Main.MouseEnter += (object sender, MouseEventArgs e) => { EditPen.Visibility = Visibility.Visible; };

            if (CollectionTitulo.IsEnabled) EditPen.Visibility = Visibility.Visible;
            if (!CollectionTitulo.IsEnabled) EditPen.Visibility = Visibility.Collapsed;
        }

        private void CollectionButton_MouseEnter(object sender, MouseEventArgs e)
        {
            EditPen.Visibility = Visibility.Visible;
        }

        private void CollectionButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!CollectionTitulo.IsEnabled) EditPen.Visibility = Visibility.Collapsed;
        }

        private void AtualizarNome()
        {
            /*
            CollectionTitulo.IsEnabled = false;
            ChangeIcon();
            Keyboard.ClearFocus();

            string newTitle = CollectionTitulo.Text;

            if (InstallyCollections.All.Where(coll => coll.Title == newTitle).ToList().Count > 0)
            {
                CollectionTitulo.Text = collection.Title;
                return;
            }

            collection.Title = newTitle;
            
            InstallyCollections.AtualizarColecao(collection, collectionIndex);
            */
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
            DeleteCollectionCommand command = new(Collection.Id);
            bool resultado = await Master.Mediator.Send(command);

            if (resultado)
            {
                int ColunaAtual = Grid.GetColumn(this);

                Master.Main.Footer.RemoverAppsPorColecao(Collection.Id);
                Master.Main.CollectionList.Children.Remove(this);

                 foreach (UIElement coll in Master.Main.CollectionList.Children)
                {
                    int colunaDoElemento = Grid.GetColumn(coll);
                    if (colunaDoElemento > ColunaAtual) Grid.SetColumn(coll, colunaDoElemento - 1);
                }

                if (Master.Collections.Count <= 3) Master.Main.ElementCollectionAdd.Visibility = Visibility.Visible;

                var collectionQuery = Master.ServiceProvider.GetService<ICollectionQuery>();
                Master.Collections = collectionQuery.GetAll().ToList();
            }
        }
    }
}
