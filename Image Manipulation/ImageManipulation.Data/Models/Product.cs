using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulation.Data.Models
{
    [Table("Product")]
    public class Product
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ProductName { get; set; }

        [MaxLength(50)]
        public string? ProductImage { get; set; }



    }
}
