using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InstallyApp.Application.Functions
{
    public static class Command
    {
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
                Debug.WriteLine("Erro no download.");
                return false;
            }
        }

        public static async Task<string> Executar(string fileName, string arguments)
        {
            try
            {
                string? result = await Task.Run(() =>
                {
                    Process p = new Process()
                    {
                        StartInfo = new ProcessStartInfo()
                        {
                            FileName = fileName,
                            Arguments = arguments,

                            //UseShellExecute = true,

                            RedirectStandardOutput = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            CreateNoWindow = true,

                            // Encoding UTF-8
                            StandardOutputEncoding = Encoding.Default
                        }
                    };
                    p.Start();
                    string? output = p.StandardOutput.ReadToEnd();
                    p.WaitForExit();

                    return output;
                });

                if (App.Master.Debug is not null) App.Master.Debug.CreateInfo(result);

                return result;
            }
            catch (Exception ex)
            {
                // Lidar
                Debug.WriteLine(ex.Message);
                return "Erro ao executar o comando";
            }

        }
    }
}