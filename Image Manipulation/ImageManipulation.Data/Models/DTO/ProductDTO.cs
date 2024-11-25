using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulation.Data.Models.DTO
{
    public class ProductDTO
    {
        [Required]
        [MaxLength(50)]
        public string? ProductName { get; set; }

        [Required]
        public IFormFile? ImageFile { get; set; }
    }

    public class ProductUpdateDTO
    {

        [Required]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string? ProductName { get; set; }

        [Required, MaxLength(50)]
        public string? ProductImage { get; set; }

        [Required]
        public IFormFile? ImageFile { get; set; }

    }
}
