using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DomainModels
{
    public class Product
    {
        // relationships
        public int Id { get; set; }        

        // fields
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool New { get; set; }
        public bool Available { get; set; }
        public ProductType ProductType { get; set; }
        public ProductAvailability ProductAvailability { get; set; }
        public bool Hidden { get; set; }
        public string Ingredients { get; set; }
    }
}
