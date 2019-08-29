using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebApi22.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int ProductCategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCategoryName { get; set; }
        

        [InverseProperty("ProductCategory")]
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
