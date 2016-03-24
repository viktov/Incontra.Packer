using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Model
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Code { get; set; }
        public int CountryID { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }        
    }
}
