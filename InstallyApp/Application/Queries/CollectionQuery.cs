using MediatR;
using System.ComponentModel.DataAnnotations;

namespace InstallyApp.Application.Queries
{
    public class CollectionQuery
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public int? AppId { get; set; }
        public List<AppQuery>? App { get; set; }
    }
}
