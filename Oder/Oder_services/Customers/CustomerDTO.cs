using Oder.Domain.Adresses;

namespace Oder.Services.Customers
{
    public class CustomerDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Adress AdressOfCustomer { get; set; }
        public string PhoneNumber { get; set; }
    }
}