using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Adresses
{
   public class Adress
    {
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Streetname { get; set; }
        public int HouseNumber { get; set; }
    }
}
