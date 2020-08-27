using System.ComponentModel.DataAnnotations;

namespace SaleProject.Web.Models.Stores.Income
{
    public class DetailIncomeViewModel
    {
        [Required]
        public int iditem { get; set; }
        [Required]
        public int amount { get; set; }
        [Required]
        public decimal total { get; set; }
    }
}
