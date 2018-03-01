using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Higgs.Server.Data.Models;

namespace Higgs.Server.Models.Requests.Admin
{
    public class DbContentFragment
    {
        [Key]
        public int Id { get; set; }

        public int Order { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public string RequiredScope { get; set; }

        public int ReportId { get; set; }

        [ForeignKey("ReportId")]
        public DbReport Report { get; set; }
    }
}
