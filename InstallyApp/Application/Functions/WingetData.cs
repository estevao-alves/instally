using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InstallyApp.Application.Functions
{
    public class Package
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public List<string> Tags { get; set; }
        public string Description { get; set; }
        public string Site { get; set; }
        public int VersionsLength { get; set; }
        public string LatestVersion { get; set; }
        public double Score { get; set; }
    }

    public static class WingetData
    {
        public static List<Package> Packages { get; set; }

        public static async Task<bool> CarregarPacotesDaAPI()
        {
            string responseBody = await API.Get("/packages");
            Packages = Json.JsonParaClasse<List<Package>>(responseBody);

            // Baixar favicons
            // foreach (Package pkg in Packages) DownloadFavicon(pkg.Site, "256", pkg.Id, "png");

            return true;
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

        public static Package? CapturarPacote(string NomeDoPacote)
        {
            Package? pkg = Packages.Find(pkg => pkg.Name.ToLower() == NomeDoPacote.ToLower());
            return pkg;
        }

        public static Package? CapturarPacotePorId(string id)
        {
            Package? pkg = Packages.Find(pkg => pkg.Id == id);
            return pkg;
        }

        public static List<Package> CapturarPacotes(string? ParteDoNomeDoPacote, string? categoria, int offset, int limit)
        {
            string BuscaPorNome = "";

            if (ParteDoNomeDoPacote is not null) BuscaPorNome = ParteDoNomeDoPacote;

            bool Filter(Package pkg)
            {
                bool pkgFilteredByName = pkg.Name.ToLower().Contains(BuscaPorNome.ToLower());

                if (categoria is not null) {
                    string[] categorias = categoria.Split(' ');

                    bool pkgFilteredByTag = pkg.Tags.Where(tag => categorias.Contains(tag)).Count() > 0;

                    return pkgFilteredByName && pkgFilteredByTag;
                }

                return pkgFilteredByName;
            }

            List<Package> pkgs = Packages.FindAll(Filter).OrderByDescending(pkg => pkg.VersionsLength).OrderByDescending(pkg => pkg.Score).Skip(offset).Take(limit).ToList();

            return pkgs;
        }

        public static UIElement? CapturarFaviconDoPacote(string NomeDoPacote)
        {
            if (Packages is null) return null;

            Package? pkg = CapturarPacote(NomeDoPacote);
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
