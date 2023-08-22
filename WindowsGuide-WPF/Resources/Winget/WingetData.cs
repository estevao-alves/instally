using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WindowsGuide_WPF.Components.Items;

namespace WindowsGuide_WPF.Resources.Winget
{
    public class Package
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public List<string> Tags { get; set; }
        public string Description { get; set; }
        public bool SiteBool { get; set; }
        public string Site { get; set; }
        public int VersionsLength { get; set; }
        public string LatestVersion { get; set; }

        public string UpdatedAt { get; set; }
    }

    public class WingetData
    {
        public List<Package> Packages { get; set; }

        public WingetData()
        {
            string text = File.ReadAllText(@"./packages.json");
            Packages = JsonSerializer.Deserialize<List<Package>>(text);
        }

        public Package CapturarPacote(string NomeDoPacote)
        {
            Package pkg = Packages.Find(pkg => pkg.Name.ToLower() == NomeDoPacote.ToLower());

            return pkg;
        }

        public List<Package> CapturarPacotes(string ParteDoNomeDoPacote)
        {
            List<Package> pkgs = Packages.FindAll(pkg => pkg.Name.ToLower().Contains(ParteDoNomeDoPacote.ToLower())).OrderByDescending(pkg => pkg.VersionsLength).ToList();
            Debug.WriteLine(pkgs[0].VersionsLength);
            return pkgs;
        }

        public Image CapturarFaviconDoPacote(string NomeDoPacote)
        {
            Package pkg = CapturarPacote(NomeDoPacote);

            var urlDoFavicon = $"https://www.google.com/s2/favicons?sz=32&domain_url={pkg.Site}";

            isFaviconnull(pkg);

            Image image = new Image()
            {
                Margin = new Thickness(0, 0, 5, 0),
                Name = "IconImage",
                Height = 30,
                Width = 30,
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

        public bool isFaviconnull(Package package)
        {
            Debug.WriteLine(package.Site);

                Debug.WriteLine(pkg);

                if (pkg == null)
                {
                    Debug.WriteLine("Passou!");

                    TextBlock textBlock = new TextBlock()
                    {
                        Text = package.Name[0].ToString().ToUpper(),
                        Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontSize = 26,
                    };

                    App.Master.Items.WrapperIcon.Child = textBlock;
                
                    return true;
                }

            return false;
        }
    }
}
