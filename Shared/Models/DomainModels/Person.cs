using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.DomainModels
{
    public class Person
    {
        public Person()
        {
            Addresses = new HashSet<Address>();
        }

        // relationships
        public int Id { get; set; }
        public ICollection<Address> Addresses { get; set; }

        // fields
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsRegistered { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? Birthdate { get; set; }
        public PersonType PersonType { get; set; }
    }
}
