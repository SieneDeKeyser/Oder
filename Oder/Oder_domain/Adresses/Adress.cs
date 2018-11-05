using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Adresses
{
   public class Adress
    {
        public int PostalCode { get;}
        public string City { get;}
        public string Streetname { get;}
        public int HouseNumber { get;}

        public Adress(int postalCode, string city, string streetName, int houseNumber)
        {
            PostalCode = postalCode;
            City = city;
            Streetname = streetName;
            HouseNumber = houseNumber;
            AddressDB.Adresses.Add(this);
        }
    }
}
