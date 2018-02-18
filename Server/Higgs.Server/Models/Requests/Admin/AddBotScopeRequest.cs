using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class AddBotScopeRequest
    {
        /// <summary>
        ///     Bot to add the scope to
        /// </summary>
        [Required]
        public int BotId { get; set; }

        /// <summary>
        ///     Scope to add
        /// </summary>
        [Required]
        public string Scope { get; set; }
    }
}