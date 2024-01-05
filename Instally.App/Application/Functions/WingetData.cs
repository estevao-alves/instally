using Instally.App.Application.Commands.UserCommands;
using Instally.App.Application.Entities;
using Instally.App.Application.Queries.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Instally.App.Application.Functions
{
    public static class WingetData
    {
        public static List<PackageEntity> Packages { get; set; }

        public static async Task<bool> CarregarPacotesDaAPI()
        {
            string responseBody = await Task.Run(async () =>
            {
                return await API.Get("/packages");
            });
            Packages = FuncoesJson.JsonParaClasse(responseBody);

            AddPackageCommand command = new(Packages);
            bool resultado = await Master.Mediator.Send(command);

            return resultado;
        }

        public static async void DownloadFavicon(string? url, string size, string fileName, string extension)
        {
            if (url is null) return;

            var urlDoFavicon = $"https://www.google.com/s2/favicons?domain={url}&sz={size}";
            string dest = @"C:\Favicons";

            string pathDest = Path.Combine(dest, extension);
            string fileDestPath = Path.Combine(dest, extension, $"{fileName}.{extension}");

            if (!Directory.Exists(pathDest)) Directory.CreateDirectory(pathDest);
            if (!File.Exists(fileDestPath))
            {
                await Command.Download(urlDoFavicon, fileDestPath);
                Debug.WriteLine(fileName + " foi baixado!");
            }

            await Task.Delay(10);
        }

        public static PackageEntity? CapturarPacote(string NomeDoPacote)
        {
            PackageEntity? pkg = Packages.Find(pkg => pkg.Name.ToLower() == NomeDoPacote.ToLower());
            return pkg;
        }

        public static PackageEntity? CapturarPacotePorId(string id)
        {
            PackageEntity? pkg = Packages.Find(pkg => pkg.WingetId == id);
            return pkg;
        }

        public static List<PackageEntity> CapturarPacotes(string? ParteDoNomeDoPacote, string? categoria, int offset, int limit)
        {
            string BuscaPorNome = string.Empty;

            if (ParteDoNomeDoPacote is not null) BuscaPorNome = ParteDoNomeDoPacote;

            bool Filter(PackageEntity pkg)
            {
                bool pkgFilteredByName = pkg.Name.ToLower().Contains(BuscaPorNome.ToLower());

                if (categoria is not null) {
                    // string[] categorias = categoria.Split(' ');

                    bool pkgFilteredByTag = pkg.Tags.Where(tag => categoria.Contains(tag)).Count() > 0;

                    return pkgFilteredByName && pkgFilteredByTag;
                }

                return pkgFilteredByName;
            }

            List<PackageEntity> pkgs = Master.ServiceProvider.GetService<IPackageQuery>().GetAll().ToList().OrderByDescending(pkg => pkg.Score).Skip(offset).Take(limit).ToList();

            return pkgs;
        }

        public static UIElement? CapturarFaviconDoPacote(string NomeDoPacote)
        {
            if (Packages is null) return null;

            PackageEntity? pkg = CapturarPacote(NomeDoPacote);
            if (pkg is null) return null;

            if (pkg.Site is null)
            {
                TextBlock textBlock = new TextBlock()
                {
                    Text = pkg.Name[0].ToString().ToUpper(),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 30,
                };

                return textBlock;
            }
            else
            {
                var urlDoFavicon = $"{API.SiteUrl}/icons/{pkg.Id}.png";

                Image image = new Image()
                {
                    Name = "IconImage",
                    Height = 30,
                    Width = 30,
                };

                BitmapImage bitmap = new BitmapImage();

                bitmap.BeginInit();
                bitmap.UriSource = new Uri(urlDoFavicon, UriKind.Absolute);
                bitmap.EndInit();

                image.Source = bitmap;

                return image;
            }
        }
    }
}
