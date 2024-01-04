using System.IO;

namespace InstallyApp.Application.Functions
{
    // Arquivo de classe que contém a inteligência das coleções

    public class InstallyCollection
    {
        public string Title { get; set; }
        public List<string> Packages { get; set; }

        public InstallyCollection(string title)
        {
            Title = title;
            Packages = new List<string>();
        }
    }
}
