using System.IO;
using System.Text.Json;
using Instally.App.Application.Entities;
using Instally.App.Application.Queries;

namespace Instally.App.Application.Functions
{
    /*
    public class FuncoesJson
    {
        public static T JsonParaClasse<T>(string caminhoArquivoJson)
        {
            string json = File.ReadAllText(caminhoArquivoJson);
            return JsonSerializer.Deserialize<T>(json);
        }

        public static string ClasseParaJson<T>(T classe)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // Indentação para melhor legibilidade
            };

            return JsonSerializer.Serialize(classe, options);
        }
    }
    */

    public class FuncoesJson
    {
        public static List<PackageEntity> JsonParaClasse(string data)
        {
            if (data == string.Empty) data = "[]";

            List<PackageEntity> dataClasse;

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