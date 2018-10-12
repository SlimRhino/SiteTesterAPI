using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace restore.Data.Entities
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }

        public int ProductId { get; set; }
       
        public Product Product { get; set; }
    }
}
