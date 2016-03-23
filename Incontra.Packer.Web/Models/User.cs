using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Incontra.Packer.Web.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Email { get; set; }

    }
}