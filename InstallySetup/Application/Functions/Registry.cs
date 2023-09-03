using Microsoft.Win32;

namespace InstallySetup.Application.Functions
{
    class Registry
    {
        public static bool Verificar(string subkey)
        {
            var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey);

            if (ndpKey != null) return true;
            else return false;
        }

        public static string[]? VerificarVersao(string subkey)
        {
            var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey);
            return ndpKey?.GetValueNames();
        }

        public static string[]? VerificarPastas(string subkey)
        {
            var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey);
            return ndpKey?.GetSubKeyNames();
        }
    }
}
