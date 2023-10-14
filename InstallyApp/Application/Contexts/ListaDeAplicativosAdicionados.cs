using InstallyApp.Components.Items;
using System.Collections.Generic;
using System.Diagnostics;

namespace InstallyApp.Application.Contexts
{
    public static class ListaDeAplicativosAdicionados
    {
        public static List<string> Apps = new();

        public static void Adicionar(string appId)
        {
            Apps.Add(appId);

            if (App.Master.Main is null) return;

            foreach (AppInSearchList app in App.Master.Main.JanelaDePesquisa.AppList.Children)
            {
                Debug.WriteLine(app.AppId);
                if (app.AppId == appId) app.IconeJaAdicionado(true);
            }
        }

        public static void Remover(string appId)
        {
            Apps.Remove(appId);

            foreach (AppInSearchList app in App.Master.Main.JanelaDePesquisa.AppList.Children)
            {
                Debug.WriteLine(app.AppId);
                if (app.AppId == appId) app.IconeJaAdicionado(false);
            }
        }
        
    }
}
