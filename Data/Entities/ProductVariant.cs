
using System.ComponentModel.DataAnnotations;

namespace restore.Data.Entities
{
    public class ProductVariant
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int StorageId { get; set; }
        [Required]
        public decimal Price { get; set; }

        public Product Product { get; set; }
        public Color Color { get; set; }
        public Storage Storage { get; set; }

    }
}
