using System;
using System.Collections.Generic;

namespace Customer.Models
{
    public class ContactInformation
    {
        public ContactInformation()
        {
        }

        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
