using InstallyApp.Application.Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InstallyApp.Resources.Winget
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

        public static async void CarregarPacotesDaAPI()
        {
            string responseBody = await API.Get("/packages");
            Packages = JsonSerializer.Deserialize<List<Package>>(responseBody);

            await Task.Delay(5000);
            foreach(Package pkg in Packages)
            {
                if (pkg.Site is null) return;

                var urlDoFavicon = $"https://t0.gstatic.com/faviconV2?client=SOCIAL&type=FAVICON&fallback_opts=TYPE,SIZE,URL&url={pkg.Site}&size=256";
                string dest = @"C:\Favicons";

                if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);
                if (!Directory.Exists(Path.Combine(dest, "ico"))) Directory.CreateDirectory(Path.Combine(dest, "ico"));
                if (!Directory.Exists(Path.Combine(dest, "png"))) Directory.CreateDirectory(Path.Combine(dest, "png"));

                try
                {
                    await Task.Run(() => Command.Download(urlDoFavicon, Path.Combine(dest, "ico", $"{pkg.Name}.ico")));
                    await Task.Run(() => Command.Download(urlDoFavicon, Path.Combine(dest, "png", $"{pkg.Name}.png")));
                    Debug.WriteLine(pkg.Name + " foi baixado!");
                }
                catch(Exception ex)
                {
                    return;
                }
            }
        }

        public static Package CapturarPacote(string NomeDoPacote)
        {
            Package pkg = Packages.Find(pkg => pkg.Name.ToLower() == NomeDoPacote.ToLower());
            return pkg;
        }

        public static List<Package> CapturarPacotes(string? ParteDoNomeDoPacote, string? categoria, int offset, int limit)
        {
            string BuscaPorNome = "";

            if (ParteDoNomeDoPacote is not null) BuscaPorNome = ParteDoNomeDoPacote;

            bool Filter(Package? pkg) {
                if(categoria is not null) return pkg.Name.ToLower().Contains(BuscaPorNome.ToLower()) && pkg.Tags.Contains(categoria);
                return pkg.Name.ToLower().Contains(BuscaPorNome.ToLower());
            }

            List<Package> pkgs = Packages.FindAll(Filter).OrderByDescending(pkg => pkg.VersionsLength).OrderByDescending(pkg => pkg.Score).Skip(offset).Take(limit).ToList();

            return pkgs;
        }

        public static UIElement CapturarFaviconDoPacote(string NomeDoPacote)
        {
            Package pkg = CapturarPacote(NomeDoPacote);

            if(pkg.Site is null)
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
                var urlDoFavicon = $"https://t0.gstatic.com/faviconV2?client=SOCIAL&type=FAVICON&fallback_opts=TYPE,SIZE,URL&url={pkg.Site}&size=256";

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
