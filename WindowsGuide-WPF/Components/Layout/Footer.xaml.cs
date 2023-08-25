using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WindowsGuide_WPF.Components.Popups;
using WindowsGuide_WPF.Resources.Winget;

namespace WindowsGuide_WPF.Components.Layout
{
    public class AppParaInstalar
    {
        public string Name;
        public string CodeId;

        public AppParaInstalar(string name, string wingetCode)
        {
            Name = name;
            CodeId = wingetCode;
        }
    }

    public partial class Footer : UserControl
    {
        List<AppParaInstalar> ListaDeAppParaInstalar;
        InstalacaoDeApps janelaDeInstalacao;

        public Footer()
        {
            InitializeComponent();
            ListaDeAppParaInstalar = new();
            this.DataContext = this;
        }

        public Border AdicionarApp(Package pkg)
        {
            ListaDeAppParaInstalar.Add(new AppParaInstalar(pkg.Name, pkg.Id));

            Border borderWrapper = new()
            {
                Background = (SolidColorBrush)App.Current.Resources["PrimaryColor"],
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(0, 0, 10, 0),
                Width = 40,
                Height = 40,
                Child = App.Master.Winget.CapturarFaviconDoPacote(pkg.Name),
                Padding = new Thickness(6, 6, 0, 6),
            };

            ListaDeInstalacao.Children.Add(borderWrapper);

            return borderWrapper;
        }
                    
        public void RemoverApp(Border appItemWithBorder, string wingetId)
        {
            ListaDeAppParaInstalar = ListaDeAppParaInstalar.FindAll(item => item.CodeId != wingetId);
            ListaDeInstalacao.Children.Remove(appItemWithBorder);
        }

        public async void VerificarApps(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            janelaDeInstalacao = new();
            App.Master.Main.AreaDePopups.Children.Add(janelaDeInstalacao);

            List<string> appsJaInstalados = new();

            try
            {
                for(int i = 1; i <= ListaDeAppParaInstalar.Count; i++)
                {
                    string appCodeId = ListaDeAppParaInstalar[i-1].CodeId;
                    string appName = ListaDeAppParaInstalar[i-1].Name;

                    Debug.WriteLine(appCodeId);
                    Debug.WriteLine(appName);

                    // Verificar se o app já está instalado
                    janelaDeInstalacao.TextoDetalhes.Text = $"{appName} ({i}/{ListaDeAppParaInstalar.Count})";
                    string result = await Task.Run(() => ExecutarCommand($"winget list -q {appCodeId}"));

                    // Se já tiver instalado, então...
                    if (result.Contains(appCodeId)) appsJaInstalados.Add(appCodeId);
                }

                if (appsJaInstalados.Count > 0) throw new Exception($"Existem {appsJaInstalados.Count} aplicativos já instalados, deseja ignorar e prosseguir com a instalação?");

                InstalarApps(ListaDeAppParaInstalar);
            }
            catch(Exception ex)
            {
                janelaDeInstalacao.Titulo.Text = "Deseja continuar?";
                janelaDeInstalacao.BarraDeProgresso.Visibility = Visibility.Collapsed;
                janelaDeInstalacao.TextoDetalhes.Text = ex.Message;
                janelaDeInstalacao.Botoes.Visibility = Visibility.Visible;
                janelaDeInstalacao.Confirmar.Click += (object sender, RoutedEventArgs e) =>
                {
                    List<AppParaInstalar> appsPermitidosParaInstalar = new();

                    foreach(AppParaInstalar app in ListaDeAppParaInstalar)
                    {
                        string? exit = appsJaInstalados.Find(appStringCodeId => appStringCodeId == app.CodeId);
                        if (exit is null) appsPermitidosParaInstalar.Add(app);
                    }

                    InstalarApps(appsPermitidosParaInstalar);
                };
            }
        }

        public async void InstalarApps(List<AppParaInstalar> apps)
        {
            janelaDeInstalacao.Botoes.Visibility = Visibility.Collapsed;
            janelaDeInstalacao.Titulo.Text = "Instalando aplicativos...";
            janelaDeInstalacao.BarraDeProgresso.Visibility = Visibility.Visible;
            janelaDeInstalacao.BarraDeProgresso.IsIndeterminate = false;
            janelaDeInstalacao.BarraDeProgresso.Value = 0;
            janelaDeInstalacao.TextoDetalhes.Text = "Instalando aplicativos...";

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
                    string result = await Task.Run(() => ExecutarCommand($"winget install {appCodeId}"));

                    // Se der algum erro na instalacao
                    if (appsJaInstalados.Count > 0) throw new Exception($"Erro ao instalar {appName}.");
                    janelaDeInstalacao.BarraDeProgresso.IsIndeterminate = false;

                    janelaDeInstalacao.BarraDeProgresso.Value = (i*100)/apps.Count;
                    janelaDeInstalacao.TextoDetalhes.Text = $"{appName} ({i}/{apps.Count})";
                }
            }
            catch (Exception ex)
            {
                janelaDeInstalacao.Titulo.Text = "Erro na instalacao";
                janelaDeInstalacao.BarraDeProgresso.Visibility = Visibility.Collapsed;
                janelaDeInstalacao.TextoDetalhes.Text = ex.Message;
                janelaDeInstalacao.Botoes.Visibility = Visibility.Visible;
                janelaDeInstalacao.Confirmar.Click += (object sender, RoutedEventArgs e) =>
                {
                    // Continuar a instalação ?
                    janelaDeInstalacao.Botoes.Visibility = Visibility.Collapsed;
                    janelaDeInstalacao.Titulo.Text = "Instalando aplicativos...";
                    janelaDeInstalacao.BarraDeProgresso.Visibility = Visibility.Visible;
                    janelaDeInstalacao.TextoDetalhes.Text = "Instalando aplicativos...";
                };
            }

        }

        public string ExecutarCommand(string command)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $@"/c powershell; {command}",

                //UseShellExecute = true,
                RedirectStandardOutput = true,

                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            string? output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }

        private void InstallButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            InstallButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1ba34c"));
        }

        private void InstallButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            InstallButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1db453"));
        }
    }
}
