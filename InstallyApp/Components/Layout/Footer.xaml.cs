using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InstallyApp.Application.Functions;
using InstallyApp.Components.Popups;
using InstallyApp.Resources.Winget;

namespace InstallyApp.Components.Layout
{
    public class AppParaInstalar
    {
        public string Name;
        public string CodeId;
        public string CollectionName;

        public AppParaInstalar(string name, string wingetCode, string collectionName)
        {
            Name = name;
            CodeId = wingetCode;
            CollectionName = collectionName;
        }
    }

    public partial class Footer : UserControl
    {
        public List<AppParaInstalar> ListaDeAppParaInstalar;
        public InstalacaoDeApps janelaDeInstalacao;

        public bool isInstalling = false;
        public string titleStatus = "Installing...";
        public string textProgressDetail { get; set; }

        public Footer()
        {
            InitializeComponent();
            ListaDeAppParaInstalar = new();
            this.DataContext = this;
        }

        private Button ConstruirAppIconeRodape(string pkgName)
        {
            // Constroi o icone visualmente para ficar na lista do rodapé
            Button borderWrapper = new()
            {
                Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"],
                Style = (Style)App.Current.Resources["HoverEffect"],
                Width = 40,
                Height = 40,
                Content = App.Master.Winget.CapturarFaviconDoPacote(pkgName),
                Padding = new Thickness(6),
                Margin = new Thickness(8, 0, 0, 0)
            };

            return borderWrapper;
        }

        public Button AdicionarApp(Package pkg, string collectionName)
        {
            // Adiciona na variavel lista de apps
            ListaDeAppParaInstalar.Add(new AppParaInstalar(pkg.Name, pkg.Id, collectionName));

            // Adiciona visualmente nos icones do rodape
            Button Icone = ConstruirAppIconeRodape(pkg.Name);
            ListaDeInstalacao.Children.Add(Icone);

            return Icone;
        }
                    
        public void RemoverApp(string appName)
        {
            // Remove da variavel lista de apps
            ListaDeAppParaInstalar = ListaDeAppParaInstalar.FindAll(item => item.Name != appName);

            // Atualiza visualmente os icones selecionados no rodapé
            AtualizarListaRodape();
        }

        public void RemoverAppsPorColecao(string collectionName)
        {
            // Atualiza na variável lista de apps
            ListaDeAppParaInstalar = ListaDeAppParaInstalar.FindAll(item => item.CollectionName != collectionName);

            // Atualiza visualmente os icones selecionados no rodapé
            AtualizarListaRodape();
        }

        private void AtualizarListaRodape()
        {
            // Atualiza visualmente os icones selecionados no rodapé
            ListaDeInstalacao.Children.Clear();

            foreach (AppParaInstalar app in ListaDeAppParaInstalar)
            {
                Button Icone = ConstruirAppIconeRodape(app.Name);
                ListaDeInstalacao.Children.Add(Icone);
            }
        }

        public async void VerificarApps_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine(ListaDeAppParaInstalar);

            janelaDeInstalacao = new();
            App.Master.Main.AreaDePopups.Children.Add(janelaDeInstalacao);

            List<string> appsJaInstalados = new();

            if (ListaDeAppParaInstalar.Count != 0)
            {
                try
                {
                    for (int i = 1; i <= ListaDeAppParaInstalar.Count; i++)
                    {
                        string appCodeId = ListaDeAppParaInstalar[i - 1].CodeId;
                        string appName = ListaDeAppParaInstalar[i - 1].Name;

                        Debug.WriteLine(appCodeId);
                        Debug.WriteLine(appName);

                        // Verificar se o app já está instalado
                        janelaDeInstalacao.TextoDetalhes.Text = $"{appName} ({i}/{ListaDeAppParaInstalar.Count})";

                        Debug.WriteLine(isInstalling);

                        if (titleStatus == "Installing...")
                        {
                            titleStatus = "Checking...";
                            janelaDeInstalacao.TextoDetalhes.Text = textProgressDetail;
                            Debug.WriteLine(textProgressDetail);
                        }

                        if (!isInstalling)
                        {
                            janelaDeInstalacao.Titulo.Text = titleStatus;

                            string result = await Task.Run(() => Command.Executar("cmd.exe", $"/c; {Command.wingetExe} list -q {appCodeId}"));

                            // Se já tiver instalado, então...
                            if (result.Contains(appCodeId)) appsJaInstalados.Add(appCodeId);
                        }
                    }

                    if (ListaDeAppParaInstalar.Count == appsJaInstalados.Count)
                    {
                        janelaDeInstalacao.Botoes.Visibility = Visibility.Visible;
                        janelaDeInstalacao.ConfirmarTextBlock.Text = "✔️";
                        janelaDeInstalacao.Titulo.Visibility = Visibility.Collapsed;
                        janelaDeInstalacao.TextoDetalhes.FontSize = 16;

                        janelaDeInstalacao.Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => janelaDeInstalacao.Visibility = Visibility.Collapsed;

                        throw new Exception($"All selected apps are already installed and updated");
                    }

                    string appSingularPlural;
                    if (appsJaInstalados.Count > 0)
                    {
                        appSingularPlural = "apps";
                        if (appsJaInstalados.Count == 1) appSingularPlural = "app";
                        throw new Exception($"{appsJaInstalados.Count} {appSingularPlural} is already installed \n continue the installation?");
                    }

                    InstalarApps(ListaDeAppParaInstalar);
                }
                catch (Exception ex)
                {
                    janelaDeInstalacao.Titulo.Visibility = Visibility.Collapsed;
                    janelaDeInstalacao.BarraDeProgresso.Visibility = Visibility.Collapsed;
                    janelaDeInstalacao.TextoDetalhes.Width = 300;
                    janelaDeInstalacao.TextoDetalhes.FontSize = 18;
                    janelaDeInstalacao.TextoDetalhes.Text = ex.Message;
                    janelaDeInstalacao.Botoes.Visibility = Visibility.Visible;
                    janelaDeInstalacao.Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) =>
                    {
                        List<AppParaInstalar> appsPermitidosParaInstalar = new();

                        foreach (AppParaInstalar app in ListaDeAppParaInstalar)
                        {
                            string? exit = appsJaInstalados.Find(appStringCodeId => appStringCodeId == app.CodeId);
                            if (exit is null) appsPermitidosParaInstalar.Add(app);
                        }

                        InstalarApps(appsPermitidosParaInstalar);
                    };
                }
            }
            else
            {
                janelaDeInstalacao.Titulo.Visibility = Visibility.Collapsed;
                janelaDeInstalacao.BarraDeProgresso.Visibility = Visibility.Collapsed;
                janelaDeInstalacao.TextoDetalhes.FontSize = 18;
                janelaDeInstalacao.ConfirmarTextBlock.Text = "✔️";
                janelaDeInstalacao.Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => janelaDeInstalacao.Visibility = Visibility.Collapsed;

                janelaDeInstalacao.TextoDetalhes.Text = "Select at least one app to install";
                janelaDeInstalacao.TextoDetalhes.Width = 300;
            }
        }

        public async void InstalarApps(List<AppParaInstalar> apps)
        {
            isInstalling = true;

            janelaDeInstalacao.CloseX.Visibility = Visibility.Collapsed;
            janelaDeInstalacao.CloseLine.Visibility = Visibility.Visible;

            janelaDeInstalacao.Titulo.Visibility = Visibility.Visible;
            janelaDeInstalacao.Botoes.Visibility = Visibility.Collapsed;
            janelaDeInstalacao.Titulo.Text = "Installing...";
            janelaDeInstalacao.BarraDeProgresso.Visibility = Visibility.Visible;
            janelaDeInstalacao.BarraDeProgresso.IsIndeterminate = false;
            janelaDeInstalacao.BarraDeProgresso.Value = 0;
            janelaDeInstalacao.TextoDetalhes.Text = "Installing...";

            List<string> appsJaInstalados = new();

            try
            {
                for (int i = 1; i <= apps.Count; i++)
                {
                    string appCodeId = apps[i-1].CodeId;
                    string appName = apps[i-1].Name;

                    // Verificar se o app já está instalado
                    janelaDeInstalacao.BarraDeProgresso.IsIndeterminate = true;
                    janelaDeInstalacao.TextoDetalhes.Text = $"{appName} ({i-1}/{apps.Count})";
                    string result = await Task.Run(() => Command.Executar("cmd.exe", $"/c; {Command.wingetExe} install {appCodeId}"));

                    // Se der algum erro na instalacao
                    if (appsJaInstalados.Count > 0) throw new Exception($"Error installing {appName}.");
                    janelaDeInstalacao.BarraDeProgresso.IsIndeterminate = false;

                    janelaDeInstalacao.BarraDeProgresso.Value = (i*100)/apps.Count;

                    textProgressDetail = $"{appName} ({i}/{apps.Count})";
                    janelaDeInstalacao.TextoDetalhes.Text = textProgressDetail;
                }

                janelaDeInstalacao.Titulo.Text = "All apps successfully installed!";
                janelaDeInstalacao.TextoDetalhes.Visibility = Visibility.Collapsed;
                janelaDeInstalacao.Botoes.Visibility = Visibility.Visible;
                janelaDeInstalacao.ConfirmarTextBlock.Text = "✔️";
                janelaDeInstalacao.Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => janelaDeInstalacao.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                janelaDeInstalacao.Titulo.Text = "Installation error";
                janelaDeInstalacao.BarraDeProgresso.Visibility = Visibility.Collapsed;
                janelaDeInstalacao.TextoDetalhes.Text = ex.Message;
                janelaDeInstalacao.Botoes.Visibility = Visibility.Visible;
                janelaDeInstalacao.Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => App.Master.Main.AreaDePopups.Children.Clear();
                {
                    // Continuar a instalação ?
                    janelaDeInstalacao.Botoes.Visibility = Visibility.Collapsed;
                    janelaDeInstalacao.Titulo.Text = "Installing...";
                    janelaDeInstalacao.BarraDeProgresso.Visibility = Visibility.Visible;
                    janelaDeInstalacao.TextoDetalhes.Text = "Installing...";
                };
            }

            isInstalling = false;
        }
    }
}
