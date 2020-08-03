using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleProject.Web.Models.Stores.Category
{
    public class UpdateViewModel
    {
        public int idcategory { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be higher than 3 and less than 50 characters.")]
        public string namecategory { get; set; }
        [StringLength(100)]
        public string categorydescription { get; set; }
    }
}
