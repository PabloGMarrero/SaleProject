using SaleProject.Entities.Store;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaleProject.Entities.Users
{
    public class User
    {
        public int iduserperson { get; set; }
        [Required]
        public int idrol { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be higher than 3 and less than 50 characters.")]
        public string nameuser { get; set; }
        public string documenttype { get; set; }
        public string numerdocument { get; set; }
        public string addressuser { get; set; }
        public string phone { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public byte[] passwordhash { get; set; }
        [Required]
        public byte[] passwordsalt { get; set; }
        public bool condition { get; set; }
        public Role role { get; set; }
        public ICollection<Income> incomes { get; set; }
    }
}
