using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Data.Models
{
    public class DbBot
    {
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public string PublicKey { get; set; }
    }
}
