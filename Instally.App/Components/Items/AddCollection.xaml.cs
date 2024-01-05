using Instally.App.Application.Commands.UserCommands;
using Instally.App.Application.Entities;
using Instally.App.Application.Queries;
using Instally.App.Application.Queries.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Packaging;

namespace Instally.App.Components.Items
{
    public partial class AddCollection : UserControl
    {
        private string defaultName = "My Collection";
        private UserEntity usuarioAtual;

        public AddCollection()
        {
            InitializeComponent();
        }

        private async void AddCollection_Click(object sender, RoutedEventArgs e)
        {
            bool resultado = await AdicionarCollection();

            if (resultado) return;

            int novoIndex = (usuarioAtual.Collections != null ? usuarioAtual.Collections.Count : 0)  - 1;

            Collection collection = new(novoIndex);

            Master.Main.CollectionList.Children.Add(collection);
            Grid.SetColumn(collection, novoIndex + 1);

            collection.Apps.Children.Clear();

            Grid.SetColumn(this, novoIndex);

            if (novoIndex > 3)
            {
                Visibility = Visibility.Collapsed;
                return;
            }
        }

        private async Task<bool> AdicionarCollection()
        {
            usuarioAtual = await Master.ServiceProvider.GetService<IUserQuery>().GetById(Master.Usuario.Id);

            AddCollectionCommand command = new(defaultName, Master.Usuario.Id, null);
            bool resultado = await Master.Mediator.Send(command);

            return resultado;
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
