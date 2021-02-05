using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DTOs
{
    public class AddressFormDTO
    {
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(10, MinimumLength = 1)]
        public string PostalCode { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string Street { get; set; }

        [StringLength(10, MinimumLength = 1)]
        public string OutdoorNumber { get; set; }

        [StringLength(10, MinimumLength = 0)]
        public string InteriorNumber { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string Suburb { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string City { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string State { get; set; }
    }

    public class AddressCreateDTO : AddressFormDTO
    {
    }

    public class AddressEditDTO : AddressFormDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
