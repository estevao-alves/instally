using System.Windows.Input;
using System;

namespace InstallySetup.Application
{
    public class PhrasesDictionary
    {
        public string BoasVindas { get; set; }
        public string AplicativoJaInstalado { get; set; }
        public string InstalacaoSucesso { get; set; }
        public string Instalando { get; set; }
        public string Reparando { get; set; }
        public string Desinstalando { get; set; }

        public PhrasesDictionary() {}
    }

    public static class Configs
    {
        // Recursos
        public static string CdnUrl = "https://storage.googleapis.com/app-solutions/Instally/Instally.zip";
        public static string CdnUtilsUrl = "https://storage.googleapis.com/app-solutions/Utils";
        public static string CdnDLLsUrl = "https://storage.googleapis.com/app-solutions/Utils/DLLs";

        public static string AppPath = @"c:\Saturnia\Instally";
        public static string AppUtilsPath = AppPath + @"\Utils\";

        public static string AppName = "Instally";
        public static string AppFileExe = "InstallyApp.exe";

        public static string AppSlogan = "Instally com um click!";

        // Frases
        public static PhrasesDictionary Phrases = new()
        {
            BoasVindas = "Boas-vindas ao instalador da Instally.",
            InstalacaoSucesso = "Instalação concluída com sucesso!",
            AplicativoJaInstalado = "O aplicativo já está instalado nesse computador. Deseja reparar a instalação? \nA reparação excluirá todos os arquivos instalados!",

            Instalando = "Instalando...",
            Reparando = "Reparando a instalação...",
            Desinstalando = "Desinstalando..."
        };
    }
}
