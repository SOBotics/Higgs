using System;
using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Data.Models
{
    public class DbPost
    {
		[Key]
		public int Id { get; set; }
		public string PostUrl { get; set; }

		public string QuestionTitle { get; set; }

		public string PostBody { get; set; }

		public DateTime PostTime { get; set; }

		public string AuthorUrl { get; set; }
	    public string AuthorName { get; set; }
		public int AuthorReputation { get; set; }
    }
}
