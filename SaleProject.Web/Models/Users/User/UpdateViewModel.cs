using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleProject.Web.Models.Users.User
{
    public class UpdateViewModel
    {
        [Required]
        public int iduser { get; set; }
        [Required]
        public int idrole { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be higher than 3 and less than 50 characters.")]
        public string name { get; set; }
        public string documenttype { get; set; }
        public string documentnumber { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}
