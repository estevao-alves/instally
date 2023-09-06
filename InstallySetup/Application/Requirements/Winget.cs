using InstallySetup.Application.Functions;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading;

namespace InstallySetup.Application.Requirements
{
    internal class Winget
    {
        public static async Task<bool> Verificar()
        {
            bool result = await Task.Run(() =>
            {
                string? output = Command.Executar("cmd.exe", "/c; winget --version");
                if (output is not null && output[0].Equals('v')) return true;

                return false;
            });

            return result;
        }

        public static async Task<bool> Instalar()
        {
            string fileName = $"Microsoft.DesktopAppInstaller.msixbundle";
            string downloadLink = $"{Configs.CdnUtilsUrl}/{fileName}";
            string sourceArquiveFileName = Configs.AppUtilsPath + fileName;

            try
            {
                Master.InstallationStatus = "Baixando o gerenciador de pacotes...";

                await Task.Run(async () =>
                {
                    if (!File.Exists(sourceArquiveFileName))
                    {
                        await Command.Download(downloadLink, sourceArquiveFileName);
                        if (!File.Exists(sourceArquiveFileName)) throw new Exception("Erro ao baixar o gerenciador de pacotes.");
                    }
                });

                Master.Main.MainProgressBar.IsIndeterminate = false;
                Master.InstallationStatus = "Aguardando você finalizar a instalação do gerenciador de pacotes...";

                await Task.Run(() =>
                {
                    var p = new Process();
                    p.StartInfo = new ProcessStartInfo()
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/k; {sourceArquiveFileName}",

                        //UseShellExecute = true,

                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true
                    };
                    p.Start();
                    // p.WaitForExit();
                });

                Master.InstallationStatus = "Instalando o aplicativo...";

                Master.Main.MainProgressBar.Value = 30;
                await Task.Delay(500);
                Master.Main.MainProgressBar.Value = 50;
                await Task.Delay(500);
                Master.Main.MainProgressBar.Value = 80;

                Master.Main.MainProgressBar.IsIndeterminate = true;

                return true;
            }
            catch (Exception ex)
            {
                Master.InstallationStatus = ex.Message;
                return false;
            }
        }
    }
}
