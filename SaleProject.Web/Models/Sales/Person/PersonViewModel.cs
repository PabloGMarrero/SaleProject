using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleProject.Web.Models.Sales.Person
{
    public class PersonViewModel
    {
        public int idperson { get; set; }
        public string typeperson { get; set; }
        public string name { get; set; }
        public string documenttype { get; set; }
        public string documentnumber { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}
