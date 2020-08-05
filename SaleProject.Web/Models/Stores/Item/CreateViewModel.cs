using System.ComponentModel.DataAnnotations;

namespace SaleProject.Web.Models.Stores.Item
{
    public class CreateViewModel
    {
        [Required]
        public int idcategory { get; set; }
        public string code { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be higher than 3 and less than 50 characters.")]
        public string nameitem { get; set; }
        [Required]
        public decimal price { get; set; }
        public int stock { get; set; }
        public string itemdescription { get; set; }
    }
}
