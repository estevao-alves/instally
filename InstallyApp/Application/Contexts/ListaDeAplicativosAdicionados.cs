using InstallyApp.Components.Items;
using System.Collections.Generic;

namespace InstallyApp.Application.Contexts
{
    public static class ListaDeAplicativosAdicionados
    {
        public static List<string> Apps = new();

        public static void Adicionar(string appName)
        {
            Apps.Add(appName);

            if (App.Master.Main is null) return;

            foreach (AppInSearchList app in App.Master.Main.JanelaDePesquisa.AppList.Children)
            {
                if (app.AppName == appName) app.IconeJaAdicionado(true);
            }
        }

        public static void Remover(string appName)
        {
            Apps.Remove(appName);

            foreach (AppInSearchList app in App.Master.Main.JanelaDePesquisa.AppList.Children)
            {
                if (app.AppName == appName) app.IconeJaAdicionado(false);
            }
        }
        
    }
}
