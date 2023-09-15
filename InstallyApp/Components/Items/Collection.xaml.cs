using InstallyApp.Application.Contexts;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
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
                    MenuAppItem newApp = new(line, Title);

                    newApp.OnExcluir += () =>
                    {
                        Apps.Children.Remove(newApp);
                        AtualizarArquivo(newApp.AppName);
                        ListaDeAplicativosAdicionados.Remover(newApp.AppName);
                    };

                    Apps.Children.Add(newApp);
                    ListaDeAplicativosAdicionados.Adicionar(line);
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
            App.Master.Main.ColecaoSelecionada = this;
            App.Master.Main.AreaDePopups.Children.Add(App.Master.Main.JanelaDePesquisa);

            App.Master.Main.JanelaDePesquisa.AppList.Children.Clear();
            App.Master.Main.JanelaDePesquisa.BuscarPorCategoria("all");
            App.Master.Main.JanelaDePesquisa.ListaDeAppsParaColecionar = new();
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
            App.Master.Main.MouseEnter += (object sender, MouseEventArgs e) => { EditPen.Visibility = Visibility.Visible; };

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

        private async void AtualizarNome()
        {
            CollectionTextBox.IsEnabled = false;
            ChangeIcon();
            Keyboard.ClearFocus();

            string newTitle = CollectionTextBox.Text;

            DirectoryInfo dirCollections = new(dirName);

            if (dirCollections.GetFiles($"{newTitle}.txt").Length > 0)
            {
                CollectionTextBox.Text = Title;
                return;
            }

            if (!File.Exists(@$"{dirName}\{newTitle}.txt"))
            {
                File.Move($@"{dirName}\{Title}.txt", $@"{dirName}\{newTitle}.txt");
                File.Delete($@"{dirName}\{Title}.txt");
            }

            Title = newTitle;
            collectionFile = @$"{dirName}\{newTitle}.txt";
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

                TextBlock textBlock = new TextBlock() { Text = "Removing..." };
                CollectionRemoveButton.Opacity = .6;
                CollectionRemoveButton.Cursor = Cursors.Wait;

                StreamReader reader = new(collectionFile);
                if (!reader.BaseStream.CanRead) throw new Exception("O arquivo da coleção não pode ser lido!");

                using (reader)
                {
                    string? line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        MenuAppItem newApp = new(line, Title);
                        Apps.Children.Remove(newApp);

                        ListaDeAplicativosAdicionados.Remover(newApp.AppName);
                    }

                    reader.Close();
                }

                await Task.Delay(100);
                Thread.Sleep(3000);

                File.Delete(collectionFile);

                App.Master.Main.Footer.RemoverAppsPorColecao(Title);
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
                await Task.Delay(1000);
                TextBlock textBlock = new TextBlock() { Text = "Removing..." };
                CollectionRemoveButton.Opacity = 1;
                CollectionRemoveButton.Cursor = Cursors.Hand;

                Debug.WriteLine(ex.Message);
            }
        }

        private void CollectionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
