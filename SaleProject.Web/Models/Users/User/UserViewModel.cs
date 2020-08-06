    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleProject.Web.Models.Users.User
{
    public class UserViewModel
    {
        public int iduser { get; set; }
        public int idrole { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public string documentType { get; set; }
        public string documentNumber { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public byte[] password { get; set; }
        public bool condition { get; set; }

    }
}
