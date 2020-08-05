using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleProject.Web.Models.Users.Role
{
    public class RoleViewModel
    {
        public int idrole { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool condition { get; set; }
    }
}
