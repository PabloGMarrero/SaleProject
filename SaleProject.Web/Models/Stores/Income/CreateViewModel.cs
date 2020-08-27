using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleProject.Web.Models.Stores.Income
{
    public class CreateViewModel
    {
        [Required]
        public int idsupplier { get; set; }
        [Required]
        public int iduser { get; set; }
        [Required]
        public string vouchertype { get; set; }
        public string voucherserie { get; set; }
        [Required]
        public string vouchernumber { get; set; }
        [Required]
        public decimal tax { get; set; }
        [Required]
        public decimal total { get; set; }
        [Required]
        public List<DetailIncomeViewModel> details { get; set; }
    }
}
