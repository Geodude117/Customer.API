using System;

namespace Customer.Models
{
    public class Address
    {
        public Address()
        {
          
        }

        public int HouseNo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
    }
}
