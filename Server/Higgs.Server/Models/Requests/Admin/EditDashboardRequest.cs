using System.ComponentModel.DataAnnotations;

namespace Higgs.Server.Models.Requests.Admin
{
    public class EditDashboardRequest : CreateDashboardRequest
    {
        [Required]
        public int DashboardId { get; set; }
    }
}