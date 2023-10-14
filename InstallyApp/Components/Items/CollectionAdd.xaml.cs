using InstallyApp.Application.Functions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace InstallyApp.Components.Items
{
    public partial class CollectionAdd : UserControl
    {
        public CollectionAdd()
        {
            InitializeComponent();
        }

        private void AddCollection_Click(object sender, RoutedEventArgs e)
        {
            string newCollectionName = $"My Collection";

            int defaultsName = InstallyCollections.All.Where(coll => coll.Title.ToLower().Contains(newCollectionName.ToLower())).ToList().Count;

            if (defaultsName > 0) {
                newCollectionName = $"My Collection {defaultsName + 1}";
            }

            int newCollectionIndex = InstallyCollections.All.Count - 1;
            InstallyCollection newCollection = new InstallyCollection(newCollectionName);
            InstallyCollections.All.Add(newCollection);
            InstallyCollections.AtualizarArquivo();

            Collection collection = new(newCollection, newCollectionIndex);

            App.Master.Main.CollectionList.Children.Add(collection);
            Grid.SetColumn(collection, newCollectionIndex+1);

            collection.Apps.Children.Clear();

            Grid.SetColumn(this, InstallyCollections.All.Count);

            if (InstallyCollections.All.Count > 3)
            {
                Visibility = Visibility.Collapsed;
                return;
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
