using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleProject.Web.Models.Stores.Income
{
    public class IncomeViewModel
    {
        public int idincome { get; set; }
        public int idsupplier { get; set; }
        public string nameSupplier { get; set; }
        public int iduser { get; set; }
        public string nameUsuer { get; set; }
        public string vouchertype { get; set; }
        public string voucherserie { get; set; }
        public string vouchernumber { get; set; }
        public DateTime dateincome { get; set; }
        public decimal tax { get; set; }
        public decimal total { get; set; }
        public string stateincome { get; set; }
   
    }
}
