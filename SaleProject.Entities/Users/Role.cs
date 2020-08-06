using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaleProject.Entities.Users
{
    public class Role
    {
        public int idrol { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be higher than 3 and less than 50 characters.")]
        public string rolname { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be higher than 3 and less than 50 characters.")]
        public string descriptionrol { get; set; }
        public bool condition { get; set; }
        public ICollection<User> users { get; set; }

    }
}
