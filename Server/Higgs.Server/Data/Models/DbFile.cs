using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Data.Models
{
    public class DbFile
    {
        [Key]
        public int Id { get; set; }

        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Contents { get; set; }
    }
}
