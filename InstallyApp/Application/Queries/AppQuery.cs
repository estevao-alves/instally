using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstallyApp.Application.Queries
{
    public class AppQuery
    {
        [Key]
        public int Id { get; set; }

        public string Packages { get; set; }

        [ForeignKey("Collection")]
        public int? CollectionId { get; set; }
        public CollectionQuery CollectionQuery { get; set; }
    }
}
