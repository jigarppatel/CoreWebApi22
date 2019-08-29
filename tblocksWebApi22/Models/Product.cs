using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebApi22.Models
{
    public partial class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public int ProductCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductTitle { get; set; }

        [Required]
        [StringLength(250)]
        public string ProductDescription { get; set; }

        [Required]
        [Range(1,1000000)]
        public double ProductPrice { get; set; }

        [StringLength(255)]
        public string ProductImagePath { get; set; }

        [ForeignKey("ProductCategoryId")]
        [InverseProperty("Products")]
        public virtual ProductCategory ProductCategory { get; set; }

        
    }
}
