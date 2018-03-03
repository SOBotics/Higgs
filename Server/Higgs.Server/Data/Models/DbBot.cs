﻿using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Data.Models
{
    public class DbBot
    {
        [Key]
        public int Id { get; set; }
        public string PublicKey { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
        public string LogoUrl { get; set; }

        public string FavIcon { get; set; }
        public string TabTitle { get; set; }
    }
}