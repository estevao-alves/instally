using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using InstallySetup.Application.Functions;

namespace InstallySetup.Application.Requirements
{
    internal class Winget
    {
        static string wingetExe = @"%LOCALAPPDATA%\Microsoft\WindowsApps\winget.exe";

        public static bool Verificar()
        {
            try
            {
                string result = Command.Executar("cmd.exe", $"/c; {wingetExe} --version");
                
                if (result[0] == 'v') return true;
                else return false;

            } catch
            {
                return false;
            }
        }

        public static async Task<bool> Instalar()
        {
            try
            {
                Master.Main.MainProgressBar.IsIndeterminate = false;
                Master.Main.MainProgressBar.Value = 20;
                Master.InstallationStatus = "Aguardando a instalação do gerenciador de pacotes...";

                await Task.Run(() =>
                {
                Command.Executar("cmd.exe", $"/c; powershell; Start-Process ms-appinstaller:?source=https://aka.ms/getwinget; $proc=Get-Process AppInstaller; Wait-Process -InputObject $proc");
                });

                string result = await Task.Run(() => Command.Executar("cmd.exe", $"/c; echo Y | {wingetExe} show winget"));

                Debug.WriteLine(result);

                Master.InstallationStatus = "Instalando o aplicativo...";

                Master.Main.MainProgressBar.Value = 40;
                await Task.Delay(500);
                Master.Main.MainProgressBar.Value = 60;
                await Task.Delay(500);
                Master.Main.MainProgressBar.Value = 80;
                await Task.Delay(500);

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
