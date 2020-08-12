

using SaleProject.Entities.Store;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaleProject.Entities.Sales
{
    public class Person
    {
        public int idperson { get; set; }
        [Required]
        public string typeperson { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be higher than 3 and less than 50 characters.")]
        public string nameperson { get; set; }
        public string documenttype { get; set; }
        public string numerdocument { get; set; }
        public string addressperson { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public ICollection<Income> incomes {get; set;}
    }
}
