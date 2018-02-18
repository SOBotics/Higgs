﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Higgs.Server.Data.Models
{
    public class DbReport
    {
		[Key]
		public int Id { get; set; }

		public int PostId { get; set; }
		[ForeignKey("PostId")]
		public DbPost Post { get; set; }

		public int BotId { get; set; }
		[ForeignKey("BotId")]
		public DbBot Bot { get; set; }

		public double Confidence { get; set; }
    }
}