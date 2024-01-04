using System.Text.Json;
using InstallyApp.Application.Entities;
using InstallyApp.Application.Queries;

namespace InstallyApp.Application.Functions
{
    public class Json
    {
        public static List<PackageEntity> JsonParaClasse(string data)
        {
            if (data == string.Empty) data = "[]";

            List<PackageEntity> dataClasse = null;

            try
            {
                dataClasse = JsonSerializer.Deserialize<List<PackageEntity>>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dataClasse;
        }

        public static string ClasseParaJson(List<PackageEntity> colecoes)
        {
            return JsonSerializer.Serialize(colecoes);
        }
    }
}