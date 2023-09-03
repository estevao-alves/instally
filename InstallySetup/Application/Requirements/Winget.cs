using InstallySetup.Application.Functions;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace InstallySetup.Application.Requirements
{
    internal class Winget
    {
        public static bool Verificar()
        {
            Debug.WriteLine(Instalar().Result);

            if (Instalar().Result) return true;

            return false;
        }

        public static async Task<bool> Instalar()
        {
            try
            {
                Debug.WriteLine(Command.Executar("cmd.exe", "winget --version"));

                await Task.Run(() => Command.Executar("cmd.exe", "winget --version"));

                Master.InstallationStatus = "Instalando Winget...";
                Master.Main.MainProgressBar.IsIndeterminate = false;

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

        public static async Task<bool> Desinstalar()
        {
            try
            {
                await Task.Run(async () =>
                {
                    Command.Executar(null, "winget --version");
                });

                Command.FinalizarProcessos("mysql");

                Master.InstallationStatus = "Removendo Winget do computador...";

                return true;
            }
            catch (Exception e)
            {
                Master.InstallationStatus = "Erro ao tentar desinstalar o Winget!";

                return false;
            }
        }
    }
}
