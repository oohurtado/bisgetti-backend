using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DomainModels
{
    public class Address
    {
        // relationships
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }

        // fields
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string OutdoorNumber { get; set; }
        public string InteriorNumber { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
