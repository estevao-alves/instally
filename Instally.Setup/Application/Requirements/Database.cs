using System;
using System.IO.Compression;
using System.IO;
using System.Threading.Tasks;
using Instally.Setup.Application.Functions;

namespace Instally.Setup.Application.Requirements
{
    class Database
    {
        public static string ServiceId = @"SYSTEM\CurrentControlSet\Services\MySQL";
        public static string MySqlInstallationDirectory = @"C:\Program Files\MySQL\MySQL Server 8.0";

        public static bool Verificar()
        {
            bool service = Registry.Verificar(ServiceId);
            if (service) return true;

            return false;
        }

        public static async Task<bool> Instalar()
        {
            try
            {
                string fileName = $"mysql-8.1.0-winx64.zip";
                string downloadLink = $"{Configs.CdnUtilsUrl}/{fileName}";
                string sourceArquiveFileName = Configs.AppUtilsPath + fileName;

                if (Directory.Exists(MySqlInstallationDirectory)) await Desinstalar();

                Master.InstallationStatus = "Baixando sistema de dados...";

                await Task.Run(async () =>
                {
                    if (!File.Exists(sourceArquiveFileName))
                    {
                        await Command.Download(downloadLink, sourceArquiveFileName);
                        if (!File.Exists(sourceArquiveFileName)) throw new Exception("Erro ao baixar o sistema de dados.");
                    }

                    Directory.CreateDirectory(MySqlInstallationDirectory);

                    ZipFile.ExtractToDirectory(sourceArquiveFileName, MySqlInstallationDirectory);

                    File.Delete(sourceArquiveFileName);

                });

                Master.InstallationStatus = "Configurando banco de dados local...";

                await Task.Run(() => Command.Executar(MySqlInstallationDirectory + @"\bin\mysqld", "--install"));

                if (!Directory.Exists(MySqlInstallationDirectory + @"\data"))
                    await Task.Run(() => Command.Executar(MySqlInstallationDirectory + @"\bin\mysqld", "--initialize-insecure"));

                if (!Verificar()) throw new Exception("A configuração do banco de dados falhou. Tente novamente ou contate o suporte!");

                await Task.Delay(1000);
                Master.InstallationStatus = "Iniciando o sistema de dados...";
                await Task.Run(() => Command.Executar("cmd.exe", "/c; sc start MySQL"));
                await Task.Delay(4000);

                Master.InstallationStatus = "Criando base de dados local...";
                Master.Main.MainProgressBar.IsIndeterminate = false;
                Master.Main.MainProgressBar.Value = 30;
                await Task.Delay(500);
                Master.Main.MainProgressBar.Value = 50;
                await Task.Delay(500);
                Master.Main.MainProgressBar.Value = 80;

                string query = "ALTER USER 'root'@'localhost' IDENTIFIED BY '1234'";
                await Task.Run(() => Command.ExecutarSQL(MySqlInstallationDirectory, "root", "", query));

                Master.Main.MainProgressBar.Value = 100;
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
                    await DesligarServico();
                    await RemoverServico();
                });

                Command.FinalizarProcessos("mysql");

                Master.InstallationStatus = "Removendo arquivos do computador...";

                await Task.Run(() =>
                {
                    if (Directory.Exists(MySqlInstallationDirectory)) Command.RemoverDiretorio(MySqlInstallationDirectory);
                });
                
                return true;
            }
            catch(Exception e)
            {
                Master.InstallationStatus = "Erro ao tentar desinstalar o banco de dados!";
                return false;
            }
        }

        public static async Task<string> DesligarServico()
        {
            string output = Command.Executar("cmd.exe", "/c; sc stop MySQL");
            return await Task.FromResult(output);
        }

        public static async Task<string> RemoverServico()
        {
            string output = Command.Executar("cmd.exe", "/c; sc delete MySQL");
            return await Task.FromResult(output);
        }
    }
}
