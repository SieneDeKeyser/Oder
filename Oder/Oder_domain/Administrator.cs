using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain
{
   public class Administrator
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Administrator()
        {
            Username = "Admin";
            Password = "AdminPassword";
        }
    }
}
