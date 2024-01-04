using InstallyApp.Application.Queries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Packaging;
using System.Security.Policy;

namespace InstallyApp.Application.Entities
{
    public class PackageEntity : BaseEntity
    {
        public string WingetId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public List<TagEntity> Tags { get; set; }
        public string Description { get; set; }
        public string Site { get; set; }
        public int VersionsLength { get; set; }
        public string LatestVersion { get; set; }
        public double Score { get; set; }

        [ForeignKey("Collection")]
        public int? PackageId { get; set; }
        public List<CollectionEntity> Collections { get; set; }

        public PackageEntity(string wingetId, string name, string publisher, string description, string site, int versionsLength, string latestVersion, double score)
        {
            WingetId = wingetId;
            Name = name;
            Publisher = publisher;
            Description = description;
            Site = site;
            VersionsLength = versionsLength;
            LatestVersion = latestVersion;
            Score = score;
        }
    }
}
