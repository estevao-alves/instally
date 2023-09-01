using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace InstallyApp.Components.Items
{
    public partial class Collection : UserControl
    {
        public string dir { get; set; }
        public string Title { get; set; }
        public string collectionFile { get; set; }

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
            dir = "Collections";
            collectionFile = @$"{dir}\{title}.txt";
            Title = title;
            CollectionTextBox.Text = title;

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
                        AtualizarArquivo(newApp.Title.Text);
                    };

                    Apps.Children.Add(newApp);
                }
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
            }

            File.Delete(collectionFile);
            File.Move(tempFile, collectionFile);
        }

        private void AdicionarApp_MouseDown(object sender, MouseButtonEventArgs e)
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

        public void ChangeIcon()
        {
            App.Master.Main.MouseEnter += (object sender, MouseEventArgs e) => { EditPen.Visibility = System.Windows.Visibility.Visible; };

            if (CollectionTextBox.IsEnabled) EditPen.Visibility = System.Windows.Visibility.Visible;
            if (!CollectionTextBox.IsEnabled) EditPen.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ClickOutSide(object sender, MouseButtonEventArgs e) => AtualizarNome();

        private void CollectionTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            App.Master.Main.MouseDown -= ClickOutSide;
            App.Master.Main.MouseDown += ClickOutSide;
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

            DirectoryInfo dirCollections = new(dir);

            if(dirCollections.GetFiles($"{newTitle}.txt").Length > 0)
            {
                CollectionTextBox.Text = oldTitle;
                return;
            }

            Title = newTitle;

            File.Move(collectionFile, $@"{dir}\{CollectionTextBox.Text}.txt");
            File.Delete($@"{dir}\{oldTitle}.txt");
        }

        public bool VerificarSeAplicativoJaExiste(string appName)
        {
            try
            {
                foreach (MenuAppItem item in Apps.Children)
                {
                    if (item.AppName == appName) throw new Exception("Aplicativo já pertence a coleção!");
                }

                return false;
            }
            catch(Exception ex)
            {
                return true;
            }
        }
    }
}
