using Instally.App.Application.Entities;
using Instally.App.Components.Janelas;

namespace Instally.App.Components.Layout
{
    public class AppParaInstalar
    {
        public string Name;
        public string CodeId;
        public Guid CollectionId;

        public AppParaInstalar(string name, string wingetCode, Guid collectionId)
        {
            Name = name;
            CodeId = wingetCode;
            CollectionId = collectionId;
        }
    }

    public partial class Footer : UserControl
    {
        public InstalacaoDeApps janelaDeInstalacao;

        public Footer()
        {
            InitializeComponent();
            janelaDeInstalacao = new();
        }

        private Button ConstruirAppIcone(string pkgName)
        {
            // Constroi o icone visualmente para ficar na lista do rodapé
            Button borderWrapper = new()
            {
                Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"],
                Style = (Style)App.Current.Resources["HoverEffect"],
                Width = 40,
                Height = 40,
                Content = WingetData.CapturarFaviconDoPacote(pkgName),
                Padding = new Thickness(6),
                Margin = new Thickness(8, 0, 0, 0)
            };

            return borderWrapper;
        }

        public Button AdicionarApp(PackageEntity pkg, Guid collectionId)
        {
            // Adiciona na variavel lista de apps
            janelaDeInstalacao.ListaDeAppParaInstalar.Add(new AppParaInstalar(pkg.Name, pkg.WingetId, collectionId));

            // Adiciona visualmente nos icones do rodape
            Button Icone = ConstruirAppIcone(pkg.Name);
            ListaDeInstalacao.Children.Add(Icone);

            return Icone;
        }
                    
        public void RemoverApp(string appName)
        {
            // Remove da variavel lista de apps
            janelaDeInstalacao.ListaDeAppParaInstalar = new(janelaDeInstalacao.ListaDeAppParaInstalar.FindAll(item => item.Name != appName));

            AtualizarLista();
        }

        public void RemoverAppsPorColecao(Guid collectionId)
        {
            // Atualiza na variável lista de apps
            janelaDeInstalacao.ListaDeAppParaInstalar = new(janelaDeInstalacao.ListaDeAppParaInstalar.FindAll(item => item.CollectionId != collectionId));

            AtualizarLista();
        }

        private void AtualizarLista()
        {
            // Atualiza visualmente os icones selecionados no rodapé
            ListaDeInstalacao.Children.Clear();

            foreach (AppParaInstalar app in janelaDeInstalacao.ListaDeAppParaInstalar)
            {
                Button Icone = ConstruirAppIcone(app.Name);
                ListaDeInstalacao.Children.Add(Icone);
            }
        }

        private void InstallyButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            janelaDeInstalacao.IniciarVerificacao();
        }
    }
}
