using Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DTOs
{

    public class ProductFormDTO
    {
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public bool IsNew { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public ProductAvailability ProductAvailability { get; set; }

        [Required]
        public bool IsHidden { get; set; }

        [StringLength(100, MinimumLength = 0)]
        public string Ingredients { get; set; }
    }

    public class ProductCreateDTO : ProductFormDTO
    {
    }

    public class ProductEditDTO : ProductFormDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
