using System;

namespace InstallySetup.Application.Functions
{
    class Processor
    {
        public static string Arquitetura = Environment.Is64BitOperatingSystem ? "x64" : "x86";
    }
}
