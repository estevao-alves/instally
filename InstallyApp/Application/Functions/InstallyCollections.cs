using System.IO;

namespace InstallyApp.Application.Functions
{
    // Arquivo de classe que contém a inteligência das coleções
    public class InstallyCollection
    {
        public string Title { get; set; }
        public List<string> Packages { get; set; }

        public InstallyCollection(string title) {
            Title = title;
            Packages = new List<string>();
        }
    }

    public class InstallyCollections
    {
        public static List<InstallyCollection> All { get; set; }

        public static string collectionsPath = "Collections.json";

        public static List<InstallyCollection> CarregarLista()
        {
            string arquivoJson = File.ReadAllText(collectionsPath);
            All = Json.JsonParaClasse<List<InstallyCollection>>(arquivoJson);

            return All;
        }

        public static void AtualizarColecao(InstallyCollection collectionUpdated, int collectionIndex)
        {
            if (collectionIndex < 0) collectionIndex = 0;
            All[collectionIndex] = collectionUpdated;
            AtualizarArquivo();
        }

        public static void AtualizarArquivo()
        {
            string arquivoJson = Json.ClasseParaJson(All);
            File.WriteAllText(collectionsPath, arquivoJson);
        }
    }
}
