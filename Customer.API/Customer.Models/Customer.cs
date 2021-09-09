using System;
using System.Collections.Generic;

namespace Customer.Models
{
    public class Customer
    {
        public Customer()
        {
        }

        public Guid Id { get; set; }
        public string ForeName { get; set; }
        public string Surename { get; set; }          
        public string DateOfBirth { get; set; }
        public Address Address { get; set; }
        public ICollection<ContactInformation> ContactInformation { get; set; }

      
    }
}
