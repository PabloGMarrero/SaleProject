using SaleProject.Entities.Sales;
using SaleProject.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SaleProject.Entities.Store
{
    public class Income
    {
        public int idincome { get; set; }
        [Required]
        public int idsupplier { get; set; }
        [Required]
        public int iduser { get; set; }
        [Required]
        public string vouchertype  { get; set; }
        public string voucherserie { get; set; }
        [Required]
        public string vouchernumber { get; set; }
        [Required]
        public DateTime dateincome { get; set; }
        public decimal tax { get; set; }
        [Required]
        public decimal total { get; set; }
        [Required]
        public string stateincome { get; set; }
        public ICollection<DetailIncome> detailIncomes { get; set; }
        public User user { get; set; }
        public Person person { get; set; }
}
}
