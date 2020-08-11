using System.ComponentModel.DataAnnotations;

namespace SaleProject.Web.Models.Sales.Person
{
    public class CreateViewModel
    {
        [Required]
        public string typeperson { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be higher than 3 and less than 50 characters.")]
        public string name { get; set; }
        public string documenttype { get; set; }
        public string documentnumber { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}
