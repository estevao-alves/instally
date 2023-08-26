using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Security.Policy;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using InstallyApp.Components.Items;

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
    }

    public class WingetData
    {
        public List<Package> Packages { get; set; }

        public WingetData()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "packages.json";
            Debug.WriteLine(path);
            string text = File.ReadAllText("./packages.json");
            
            // string text = File.ReadAllText(@"./packages.json");
            Packages = JsonSerializer.Deserialize<List<Package>>(text);
        }

        public Package CapturarPacote(string NomeDoPacote)
        {
            Package pkg = Packages.Find(pkg => pkg.Name.ToLower() == NomeDoPacote.ToLower());
            return pkg;
        }

        public List<Package> CapturarPacotes(string? ParteDoNomeDoPacote, string? categoria)
        {
            List<Package> pkgs;

            if (ParteDoNomeDoPacote is null) ParteDoNomeDoPacote = "";

            if (categoria is not null) pkgs = Packages.FindAll(pkg => pkg.Name.ToLower().Contains(ParteDoNomeDoPacote.ToLower()) && pkg.Tags.Contains(categoria)).OrderByDescending(pkg => pkg.VersionsLength).ToList();
            else pkgs = Packages.FindAll(pkg => pkg.Name.ToLower().Contains(ParteDoNomeDoPacote.ToLower())).OrderByDescending(pkg => pkg.VersionsLength).ToList();

            return pkgs;
        }

        public UIElement CapturarFaviconDoPacote(string NomeDoPacote)
        {
            Package pkg = CapturarPacote(NomeDoPacote);

            if(pkg.Site is null)
            {
                TextBlock textBlock = new TextBlock()
                {
                    Text = pkg.Name[0].ToString().ToUpper(),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    Margin = new Thickness(0, 0, 5, 0),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 30,
                };

                return textBlock;
            }
            else
            {
                var urlDoFavicon = $"https://www.google.com/s2/favicons?sz=32&domain_url={pkg.Site}";


                Image image = new Image()
                {
                    Margin = new Thickness(0, 0, 5, 0),
                    Name = "IconImage",
                    Height = 25,
                    Width = 25,
                };
                image.Stretch = Stretch.Fill;
                image.ClipToBounds = true;

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
