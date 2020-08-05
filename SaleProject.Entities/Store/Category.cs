using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaleProject.Entities.Store
{
    public class Category
    {
        public int idcategory { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be higher than 3 and less than 50 characters.")]
        public string namecategory { get; set; }
        [StringLength(100)]
        public string categorydescription { get; set; }
        public bool condition { get; set; }

        public ICollection<Item> items { get; set; }
    }
}
