using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace restore.Data.Entities
{
    public class Color
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }
}
