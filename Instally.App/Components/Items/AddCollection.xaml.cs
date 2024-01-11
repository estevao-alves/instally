using Instally.App.Application.Commands.UserCommands;
using Instally.App.Application.Entities;
using Instally.App.Application.Queries.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Instally.App.Components.Items
{
    public partial class AddCollection : UserControl
    {
        public AddCollection()
        {
            InitializeComponent();
        }

        private async void AddCollection_Click(object sender, RoutedEventArgs e)
        {
            AdicionarCollectionPadrao();
        }

        public void AdicionarCollectionPadrao() => AdicionarCollection("My Collection", Master.UsuarioAutenticado.Id, Master.Packages.Take(5).ToList());

        public async void AdicionarCollection(string name, Guid user, List<PackageEntity> packages)
        {
            AddCollectionCommand command = new(name, user, packages);
            bool resultado = await Master.Mediator.Send(command);

            if (resultado)
            {
                int novoIndex = Master.Collections != null ? Master.Collections.Count : 0;

                CollectionItem collection = new(0, name, packages, Master.Collections[Master.Collections.Count]);
                Master.Main.CollectionList.Children.Add(collection);

                Grid.SetColumn(collection, novoIndex);
                collection.Apps.Children.Clear();
                Grid.SetColumn(this, novoIndex + 1);

                if (novoIndex > 4)
                {
                    Visibility = Visibility.Collapsed;
                    return;
                }

                var collectionQuery = Master.ServiceProvider.GetService<ICollectionQuery>();
                Master.Collections = collectionQuery.GetAll().ToList();
            }
        }

        private void NewCollection_MouseEnter(object sender, MouseEventArgs e)
        {
            BorderIconAddCollection.Background = (SolidColorBrush)App.Current.Resources["ActionColor"];
        }

        private void NewCollection_MouseLeave(object sender, MouseEventArgs e)
        {
            BorderIconAddCollection.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

    }
}
