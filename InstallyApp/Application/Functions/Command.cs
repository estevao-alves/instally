using System.Net;
using System.Text;
using System.Threading;

namespace InstallyApp.Application.Functions
{
    public static class Command
    {
        public static CancellationTokenSource CancellationTokenSource { get; set; } = new();

        public static string wingetExe = @"%LOCALAPPDATA%\Microsoft\WindowsApps\winget.exe";

        public static async Task<bool> Download(string url, string pathDest)
        {
            try
            {
                WebClient client = new();
                client.DownloadFile(url, pathDest);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<string> Executar(string fileName, string arguments)
        {
            try
            {
                Process p = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = fileName,
                        Arguments = arguments,

                        // UseShellExecute = true,

                        RedirectStandardOutput = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,

                        // Encoding UTF-8
                        StandardOutputEncoding = Encoding.Default
                    }
                };

                string? result = await Task.Run(() =>
                {
                    p.Start();
                    return p.StandardOutput.ReadToEndAsync();
                });

                p.WaitForExit();

                if (App.Master.Debug is not null) App.Master.Debug.CreateInfo(result);

                return result;
            }
            catch (Exception ex)
            {
                if (App.Master.Debug is not null) App.Master.Debug.CreateInfo("Erro ao executar um comando:\nDetalhes: " + ex.Message);
                return "Erro ao executar o comando";
            }

        }
    }
}