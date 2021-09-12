using System;
using System.Collections.Generic;
using System.Linq;

namespace Customer.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Address = new Models.Address();
            this.ContactInformation = new List<Models.ContactInformation>();
        }

        public Guid? Id { get; set; }
        public string ForeName { get; set; }
        public string Surename { get; set; }          
        public string DateOfBirth { get; set; }
        public Address Address { get; set; }
        public IEnumerable<ContactInformation> ContactInformation { get; set; }

        public int CompareTo(Customer obj)
        {
            if (obj.Id != this.Id || obj.ForeName != this.ForeName || obj.Surename != this.Surename || obj.DateOfBirth != this.DateOfBirth ||
                obj.Address != this.Address || !Enumerable.SequenceEqual(obj.ContactInformation, this.ContactInformation))
            {
                return 1;
            }
            return -1;
        }


    }
}
