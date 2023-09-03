using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace InstallyApp.Components.Items
{
    public partial class Collection : UserControl
    {
        public string dirName { get; set; }
        public string Title { get; set; }
        public string collectionFile { get; set; }

        public bool isActive = false;

        public Collection()
        {
            InitializeComponent();
        }

        public Collection(string title)
        {
            // Inicia
            InitializeComponent();
            Apps.Children.Clear();

            // Define
            dirName = "Collections";
            collectionFile = @$"{dirName}\{title}.txt";
            Title = title;
            CollectionTextBox.Text = title;

            VerOpcoesConfiguracao();

            // Carrega
            CarregarArquivo();
        }

        private void CarregarArquivo()
        {

            if (!File.Exists(collectionFile))
            {
                File.Create(collectionFile);
                return;
            }

            using (StreamReader reader = new(collectionFile))
            {
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    MenuAppItem newApp = new(line);

                    newApp.OnExcluir += () =>
                    {
                        Apps.Children.Remove(newApp);
                        AtualizarArquivo(line);
                        App.Master.AppsJaAdicionados.Remove(line);
                    };

                    Apps.Children.Add(newApp);
                    App.Master.AppsJaAdicionados.Add(line);
                }

                reader.Close();
            }
        }

        public void AtualizarArquivo(string appName)
        {
            string tempFile = Path.GetTempFileName();

            using (var sr = new StreamReader(collectionFile))
            using (var sw = new StreamWriter(tempFile))
            {
                string? line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line != appName) sw.WriteLine(line);
                }

                sw.Close();
                sr.Close();
            }

            File.Delete(collectionFile);
            File.Move(tempFile, collectionFile);
        }

        private void AdicionarApp_Click(object sender, RoutedEventArgs e)
        {
            App.Master.Main.JanelaDePesquisa = new();
            App.Master.Main.ColecaoSelecionada = this;

            App.Master.Main.AreaDePopups.Children.Add(App.Master.Main.JanelaDePesquisa);
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
            App.Master.Main.MouseDown -= ClickOutside;
            App.Master.Main.MouseDown += ClickOutside;
        }

        public void ChangeIcon()
        {
            App.Master.Main.MouseEnter += (object sender, MouseEventArgs e) => { EditPen.Visibility = System.Windows.Visibility.Visible; };

            if (CollectionTextBox.IsEnabled) EditPen.Visibility = System.Windows.Visibility.Visible;
            if (!CollectionTextBox.IsEnabled) EditPen.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CollectionButton_MouseEnter(object sender, MouseEventArgs e)
        {
            EditPen.Visibility = System.Windows.Visibility.Visible;
        }

        private void CollectionButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!CollectionTextBox.IsEnabled) EditPen.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void AtualizarNome()
        {
            CollectionTextBox.IsEnabled = false;
            ChangeIcon();
            Keyboard.ClearFocus();

            string oldTitle = Title;
            string newTitle = CollectionTextBox.Text;

            DirectoryInfo dirCollections = new(dirName);

            if(dirCollections.GetFiles($"{newTitle}.txt").Length > 0)
            {
                CollectionTextBox.Text = oldTitle;
                return;
            }

            Title = newTitle;

            File.Move(collectionFile, $@"{dirName}\{CollectionTextBox.Text}.txt");
            File.Delete($@"{dirName}\{oldTitle}.txt");
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

        private async void RemoveCollection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ColunaAtual = Grid.GetColumn(this);

                CollectionRemoveButton.Content = "Removendo...";
                CollectionRemoveButton.Opacity = .6;
                CollectionRemoveButton.Cursor = Cursors.Wait;

                File.Delete(collectionFile);

                await Task.Delay(3000);
                if (File.Exists(collectionFile)) throw new Exception("Erro, o arquivo ainda não foi excluído!");

                App.Master.Main.CollectionList.Children.Remove(this);

                foreach (UIElement coll in App.Master.Main.CollectionList.Children)
                {
                    int colunaDoElemento = Grid.GetColumn(coll);

                    if (colunaDoElemento > ColunaAtual) Grid.SetColumn(coll, colunaDoElemento - 1);
                }

                DirectoryInfo dirCollections = new("Collections");
                FileInfo[] collections = dirCollections.GetFiles();

                if (collections.Length <= 3) App.Master.Main.ElementCollectionAdd.Visibility = Visibility.Visible;

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
