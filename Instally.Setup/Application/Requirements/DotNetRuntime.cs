
using Instally.Setup.Application.Functions;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Instally.Setup.Application.Requirements
{
    class DotNetRuntime
    {
        public static string ServiceId = @"SOFTWARE\WOW6432Node\dotnet\Setup\InstalledVersions\" + Processor.Arquitetura + @"\sharedfx\Microsoft.WindowsDesktop.App";
        public static int MinimalVersion = 6;

        public static bool Verificar()
        {
            string[]? dotNetRuntimeVersions = Registry.VerificarVersao(ServiceId);
            if (dotNetRuntimeVersions is not null && dotNetRuntimeVersions.Where(version => version[0] >= MinimalVersion).Count() > 0) return true;

            return false;
        }

        public static async Task<bool> Instalar()
        {
            try
            {
                string fileName = $"windowsdesktop-runtime-6.0.21-win-{Processor.Arquitetura}.exe";
                string downloadLink = $"{Configs.CdnUtilsUrl}/{fileName}";
                string sourceArquiveFileName = Configs.AppUtilsPath + fileName;

                if (!File.Exists(sourceArquiveFileName))
                {
                    Master.InstallationStatus = "Baixando .NET Runtime...";
                    await Command.Download(downloadLink, sourceArquiveFileName);
                }

                Master.InstallationStatus = "Aguardando você finalizar a instalação do .NET Runtime";

                await Task.Delay(200);

                var p = new Process();
                p.StartInfo = new ProcessStartInfo(sourceArquiveFileName);
                p.Start();
                p.WaitForExit();

                if (!Verificar()) throw new Exception("Você não completou a instalação do .NET Runtime, tente novamente.");

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
