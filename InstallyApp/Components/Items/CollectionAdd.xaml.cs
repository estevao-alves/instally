using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace InstallyApp.Components.Items
{
    public partial class CollectionAdd : UserControl
    {
        public int collectionNumber { get; set; } = 0;
        public CollectionAdd()
        {
            InitializeComponent();
        }
        private void AddCollection_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dirCollections = new("Collections");
            FileInfo[] collections = dirCollections.GetFiles();

            collectionNumber += 1;

            string collectionName = $"My Collection {collectionNumber}";
            if(collections.Where(file => file.Name == $"{collectionName}.txt").Any()) {
                collectionName = $"My Collection {collectionNumber+1}";
            }

            Collection collection = new(collectionName);

            App.Master.Main.CollectionList.Children.Add(collection);
            Grid.SetColumn(collection, collections.Length);

            collection.Apps.Children.Clear();

            Grid.SetColumn(this, collections.Length+1);

            if ((collections.Length + 1) > 3)
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
