using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SaleProject.Entities.Store
{
    public class DetailIncome
    {
        public int iddetailincome { get; set; }
        [Required]
        public int idincome { get; set; }
        [Required]
        public int iditem { get; set; }
        [Required]
        public int amount { get; set; }
        [Required]
        public decimal price { get; set; }
        public Income income { get; set; }
    }
}
