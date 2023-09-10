using System.Diagnostics;
using System;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Threading.Tasks;
using System.IO.Compression;
using InstallySetup.Application.Requirements;
using InstallySetup.Application.Functions;
using InstallySetup.Application;
using System.Windows.Interop;

namespace InstallySetup
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainText.Text = Configs.Phrases.BoasVindas;
            TopBar.Title.Text = $"{Configs.AppName} - Setup";
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            LayoutMainClipMask.Rect = new Rect(0, 0, ActualWidth, ActualHeight);

            if (WindowState == WindowState.Maximized || ActualHeight == SystemParameters.WorkArea.Height)
            {
                LayoutMainClipMask.RadiusX = 0;
                LayoutMainClipMask.RadiusY = 0;

                Cursor = Cursors.Arrow;
                LayoutMain.Margin = new Thickness(7);
            }
            else
            {
                LayoutMainClipMask.RadiusX = 8;
                LayoutMainClipMask.RadiusY = 8;

                LayoutMain.Margin = new Thickness(0);
            }
        }

        private void TopBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Normal) WindowState = WindowState.Maximized;
            else WindowState = WindowState.Normal;
        }

        private void TopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (WindowState == WindowState.Maximized)
                {
                    MoveWindowToMousePosition();
                    WindowState = WindowState.Normal;
                }

                DragMove();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed) Cursor = Cursors.Hand;
        }

        private void TopBar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void MoveWindowToMousePosition()
        {
            if (WindowState == WindowState.Maximized)
            {
                Point getMousePosition = Mouse.GetPosition(this);
                var mouse = TranslatePoint(getMousePosition, null);

                double WindowWidth = Width;

                Left = mouse.X - (WindowWidth / 2);
                Top = mouse.Y - (TopBar.Height / 2);
            }
        }

        private void ExibirBotaoCancelar()
        {
            BtnCancelar.Content = "Cancelar";
            BtnCancelar.Visibility = Visibility.Visible;
            BtnCancelar.IsEnabled = false;
            BtnCancelar.Opacity = .5;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            BtnInstalar.Visibility = Visibility.Visible;
            BtnCancelar.Visibility = Visibility.Collapsed;

            MainProgressBar.Visibility = Visibility.Collapsed;
            Master.InstallationStatus = Configs.Phrases.BoasVindas;
        }

        private void BtnInstalar_Click(object sender, RoutedEventArgs e)
        {
            BtnInstalar.Visibility = Visibility.Collapsed;
            BtnConcluir.Visibility = Visibility.Collapsed;
            
            Master.InstallationStatus = Configs.Phrases.Instalando;
            MainProgressBar.Visibility = Visibility.Visible;
            ExibirBotaoCancelar();
            
            IniciarInstalacao();
        }

        private void BtnReparar_Click(object sender, RoutedEventArgs e)
        {
            BtnReparar.Visibility = Visibility.Collapsed;
            BtnConcluir.Visibility = Visibility.Collapsed;
            

            Master.InstallationStatus = Configs.Phrases.Reparando;
            MainProgressBar.Visibility = Visibility.Visible;
            ExibirBotaoCancelar();

            try
            {
                Command.FinalizarProcessos(Configs.AppFileExe.Replace(".exe", ""));

                Master.InstallationStatus = Configs.Phrases.Reparando;
                Command.RemoverDiretorio(Configs.AppPath);
                IniciarInstalacao();
            }
            catch(Exception ex)
            {
                Master.InstallationStatus = "Feche o aplicativo para continuar a reparação.";
            }
        }
        private void BtnConcluir_Click(object sender, RoutedEventArgs e) => Close();

        private async void AbrirApp(object sender, RoutedEventArgs e)
        {
            try
            {
                string result = await Task.Run(() => Command.Executar("cmd.exe", @$"/c; start C:\Saturnia\Instally\InstallyApp.exe"));
                
                string installedAppPath = Path.Combine(Configs.AppPath + Configs.AppFileExe);

                Debug.WriteLine(Configs.AppPath + Configs.AppFileExe);
                Debug.WriteLine(result);

                var p = new Process();

                p.StartInfo = new ProcessStartInfo(installedAppPath);
                p.Start();
                p.WaitForExit();
            }
            catch(Exception ex)
            {
                // Lidar com
            }
        }

        private void BtnDesinstalar_Click(object sender, RoutedEventArgs eventArgs)
        {
            BtnDesinstalar.Visibility = Visibility.Collapsed;
            BtnReparar.Visibility = Visibility.Collapsed;
            BtnConcluir.Visibility = Visibility.Collapsed;
            BtnConcluir.Content = "Fechar";
            BtnConcluir.MouseDown -= AbrirApp;

            ExibirBotaoCancelar();
            Desinstalar();
        }

        public void VerificarAplicativo()
        {
            var directory = new DirectoryInfo(Configs.AppPath);
            if (directory.Exists && directory.GetFiles().Length > 1)
            {
                Master.InstallationStatus = Configs.Phrases.AplicativoJaInstalado;

                // Ocultar botão instalar
                BtnInstalar.Visibility = Visibility.Collapsed;

                // Exibir botão desinstalar
                BtnDesinstalar.Visibility = Visibility.Visible;
                
                // Exibir botão reparar
                BtnReparar.Visibility = Visibility.Visible;

                // Exibir botão de abertura
                BtnConcluir.Visibility = Visibility.Visible;
                BtnConcluir.Content = "Abrir";
                BtnConcluir.Click += AbrirApp;
            }
        }

        private async Task<bool> VerificarRequisitos()
        {
            if (!Directory.Exists(Configs.AppPath)) Directory.CreateDirectory(Configs.AppPath);

            try
            {
                if (!Directory.Exists(Configs.AppUtilsPath)) Directory.CreateDirectory(Configs.AppUtilsPath);

                if (!Winget.Verificar())
                {
                    bool install = await Winget.Instalar();
                    if (!install) throw new Exception();
                }

                if (!DotNetRuntime.Verificar())
                {
                    bool install = await DotNetRuntime.Instalar();
                    if (!install) throw new Exception();
                }

                if (!SystemDLLs.Verificar())
                {
                    bool install = await SystemDLLs.Instalar();
                    if (!install) throw new Exception();
                }

                /*
                if (!Database.Verificar())
                {
                    bool install = await Database.Instalar();
                    if (!install) throw new Exception();
                }
                */

                return true;
            }
            catch (Exception ex)
            {
                MainProgressBar.Value = 0;
                MainProgressBar.Visibility = Visibility.Collapsed;
                
                BtnCancelar.Visibility = Visibility.Collapsed;
                BtnInstalar.Visibility = Visibility.Visible;
                BtnInstalar.Content = "Tente novamente";

                return false;
            }
        }

        public async void IniciarInstalacao()
        {
            BtnDesinstalar.Visibility = Visibility.Collapsed;
            BtnConcluir.Content = "Fechar";

            bool isAuthorized = await VerificarRequisitos();
            if (!isAuthorized) return;

            Master.InstallationStatus = Configs.Phrases.Instalando;

            try
            {
                string sourceArquiveFileName = Configs.AppPath + @"\lastversion.zip";

                await Command.Download(Configs.CdnUrl, sourceArquiveFileName);

                if (!File.Exists(sourceArquiveFileName)) throw new Exception("Erro ao baixar a última versão do aplicativo.");

                ZipFile.ExtractToDirectory(sourceArquiveFileName, Configs.AppPath);

                File.Delete(sourceArquiveFileName);

                Master.InstallationStatus = Configs.Phrases.InstalacaoSucesso;

                string installedAppPath = Path.Combine(Configs.AppPath, Configs.AppFileExe);

                if (File.Exists(installedAppPath)) BtnConcluir.Click += AbrirApp;

                createShortcut();

                // Finally
                MainProgressBar.IsIndeterminate = false;
                MainProgressBar.Value = 100;

                BtnCancelar.Visibility = Visibility.Collapsed;
                BtnConcluir.Visibility = Visibility.Visible;
                BtnConcluir.Content = "Abrir INSTALLY";

            }
            catch (Exception ex)
            {
                MainProgressBar.IsIndeterminate = false;
                MainProgressBar.Visibility = Visibility.Collapsed;

                BtnCancelar.Visibility = Visibility.Collapsed;
                BtnConcluir.Visibility = Visibility.Visible;
                
                Master.InstallationStatus = ex.Message;
            }
        }

        public async void Desinstalar()
        {
            try
            {
                Master.InstallationStatus = "Desligando serviços de dados...";
                MainProgressBar.Visibility = Visibility.Visible;
                MainProgressBar.IsIndeterminate = true;

                if (Directory.Exists(Database.MySqlInstallationDirectory)) await Database.Desinstalar();

                Command.FinalizarProcessos(Configs.AppFileExe.Replace(".exe", ""));
                Command.RemoverDiretorio(Configs.AppPath);

                Master.InstallationStatus = Configs.Phrases.Desinstalando;
                
                MainProgressBar.IsIndeterminate = false;
                MainProgressBar.Value = 10;
                await Task.Delay(1000);
                MainProgressBar.Value = 40;
                await Task.Delay(1000);
                MainProgressBar.Value = 70;
                await Task.Delay(500);
                MainProgressBar.Value = 90;
                await Task.Delay(500);
                MainProgressBar.Value = 100;
                
                Master.InstallationStatus = "O aplicativo foi removido deste computador!";

                Shortcut.Remover(Configs.AppName, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs"));
                Shortcut.Remover(Configs.AppName, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

                BtnCancelar.Visibility = Visibility.Collapsed;
                BtnConcluir.Visibility = Visibility.Visible;
                BtnConcluir.Content = "Fechar";
            }
            catch (Exception ex)
            {
                Master.InstallationStatus = ex.Message;
                MainProgressBar.Visibility = Visibility.Collapsed;
                
                BtnCancelar.Visibility = Visibility.Collapsed;
                
                BtnDesinstalar.Visibility = Visibility.Visible;
                BtnDesinstalar.Content = "Tentar novamente";

                BtnConcluir.Visibility = Visibility.Visible;
                BtnConcluir.Content = "Fechar";

                BtnReparar.Visibility = Visibility.Visible;
            }
        }

        public void createShortcut()
        {
            string exePath = $@"{Configs.AppPath}\{Configs.AppFileExe}";
            Shortcut.Criar(Configs.AppName, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs"), exePath, Configs.AppSlogan);
            Shortcut.Criar(Configs.AppName, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), exePath, Configs.AppSlogan);
        }

    }
}
