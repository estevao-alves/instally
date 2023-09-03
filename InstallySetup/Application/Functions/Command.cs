using InstallySetup.Application.Requirements;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace InstallySetup.Application.Functions
{
    class Command
    {

        public static async Task<bool> Download(string url, string pathDest)
        {
            try
            {
                WebClient client = new();
                await Task.Run(() => client.DownloadFile(url, pathDest));
                return true;
            }
            catch(Exception)
            {
                Debug.WriteLine("Erro no download.");
                return false;
            }
        }

        public static async Task<string> Executar(string fileName, string arguments)
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
                    CreateNoWindow = true
                }
            };
            p.Start();
            string? output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            return await Task.FromResult(output);
        }

        public static async Task<string> ExecutarSQL(string MySqlInstallationDirectory, string user, string password, string query)
        {
            return await Executar(MySqlInstallationDirectory + @"\bin\mysql", $@"-u {user} --password=""{password}"" --execute=""{query}""");
        }

        public static void FinalizarProcessos(string processNameWithoutExtension)
        {
            var processes = Process.GetProcessesByName(processNameWithoutExtension);
            foreach (var p in processes)
            {
                p.Kill();
                p.WaitForExit();
            }
        }

        public static void RemoverDiretorio(string path)
        {
            try
            {
                DirectoryInfo dirInfo = new(path);
                dirInfo.Delete(true);
            }
            catch (UnauthorizedAccessException ex)
            {

            }
        }
    }
}
