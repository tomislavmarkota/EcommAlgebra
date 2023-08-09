using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommAlgebra.Models
{
    public class Product
    {
    
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
      
        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }

        public string? ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [ForeignKey("ProductId")]
        public List<ProductCategory>? ProductCategories { get; set; }

       

    }
}
