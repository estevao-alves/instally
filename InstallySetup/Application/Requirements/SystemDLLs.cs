
using InstallySetup.Application.Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace InstallySetup.Application.Requirements
{
    class DLL
    {
        public string Name { get; set; }
        public string Directory { get; set; }
        public string CompleteSource { get; set; }

        public DLL(string name, string dir)
        {
            Name = name;
            Directory = dir;
            CompleteSource = $@"{dir}\{name}.dll";
        }
    }

    class SystemDLLs
    {
        static List<DLL> dlls = new()
        {
            new DLL("msvcp140", @"C:\Windows\System32"),
            new DLL("vcruntime140", @"C:\Windows\System32"),
            new DLL("vcruntime140_1", @"C:\Windows\System32")
        };

        public static bool Verificar()
        {
            try
            {
                foreach(DLL dll in dlls)
                {
                    if (!File.Exists(dll.CompleteSource)) throw new Exception(dll.Name + ".dll não encontrada!");
                }

                return true;
            }
            catch(Exception ex)
            {
                WriteLine(ex.Message);
                return false;
            }
        }

        public static async Task<bool> Instalar()
        {
            try
            {
                foreach (DLL dll in dlls)
                {
                    if (!File.Exists(dll.CompleteSource)) await Command.Download(Configs.CdnDLLsUrl + $@"/{dll.Name}.dll", dll.CompleteSource);
                }

                return Verificar();
            }
            catch (Exception ex)
            {
                Master.InstallationStatus = ex.Message;
                return false;
            }
        }
    }
}
