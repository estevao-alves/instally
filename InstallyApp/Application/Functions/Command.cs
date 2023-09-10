using System;
using System.Diagnostics;

namespace InstallyApp.Application.Functions
{
    public static class Command
    {
        public static string wingetExe = @"%LOCALAPPDATA%\Microsoft\WindowsApps\winget.exe";

        public static string Executar(string fileName, string arguments)
        {
            try
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

                return output;
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