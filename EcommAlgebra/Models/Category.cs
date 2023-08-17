using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommAlgebra.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }

        [ForeignKey("CategoryId")]
        public List<ProductCategory>? ProductCategories { get; set; }
    }
}
