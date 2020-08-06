using System.ComponentModel.DataAnnotations;

namespace SaleProject.Web.Models.Users.User
{
    public class UpdatePassViewModel
    {
        [Required]
        public int iduser { get; set; }
        public string password { get; set; }
    }
}
