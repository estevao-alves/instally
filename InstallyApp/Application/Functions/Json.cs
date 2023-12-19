using System.Text.Json;
using InstallyApp.Application.Queries;

namespace InstallyApp.Application.Functions
{
    public class Json
    {

        public static T JsonParaClasse<T>(string data)
        {
            if (data == string.Empty) data = "[]";

            return JsonSerializer.Deserialize<T>(data);
        }

        public static string ClasseParaJson(List<InstallyCollection> colecoes)
        {
            return JsonSerializer.Serialize(colecoes);
        }
    }
}
