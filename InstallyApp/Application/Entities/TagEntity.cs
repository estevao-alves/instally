using System.ComponentModel.DataAnnotations.Schema;

namespace InstallyApp.Application.Entities
{
    public class TagEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Category { get; set; }

        [ForeignKey("Package")]
        public int? PackageId { get; set; }
        public PackageEntity Package { get; set; }

        public TagEntity(string title)
        {
            Title = title;
        }
    }
}
