using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleProject.Web.Models.Stores.Item
{
    public class ItemViewModel
    {
        public int iditem { get; set; }
        public int idcategory { get; set; }
        public string namecategory { get; set; }
        public string code { get; set; }
        public string nameitem { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
        public string itemdescription { get; set; }
        public bool condition { get; set; }

    }
}
