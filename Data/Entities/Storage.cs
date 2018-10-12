using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace restore.Data.Entities
{
    public class Storage
    {
        public int Id { get; set; }
        [Required]
        public int Capacity { get; set; }

        public List<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }
}
