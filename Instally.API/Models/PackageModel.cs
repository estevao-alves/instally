using System.ComponentModel.DataAnnotations.Schema;

namespace Instally.API.Models
{
    public class PackageModel : BaseModel
    {
        public string WingetId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }

        [NotMapped]
        public string[] Tags
        {
            get { return TagsString?.Split(','); }
            set { TagsString = string.Join(',', value); }
        }
        public string TagsString { get; set; }
        public string Description { get; set; }
        public string? Site { get; set; }
        public int VersionsLength { get; set; }
        public string LatestVersion { get; set; }
        public double Score { get; set; }

        [ForeignKey("Collection")]
        public Guid CollectionId { get; set; }

        private PackageModel() { }

        public PackageModel(string wingetId, string name, string publisher, string[] tags, string description, string site, int versionsLength, string latestVersion, double score)
            : this()
        {
            WingetId = wingetId;
            Name = name;
            Publisher = publisher;
            Tags = tags;
            Description = description;
            Site = site;
            VersionsLength = versionsLength;
            LatestVersion = latestVersion;
            Score = score;
        }
        public void AtualizarCollection(Guid collectionId)
        {
            CollectionId = collectionId;
        }
    }
}
