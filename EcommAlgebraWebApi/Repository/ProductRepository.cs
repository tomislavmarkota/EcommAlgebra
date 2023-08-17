using EcommAlgebra.Data;
using EcommAlgebra.Models;
using EcommAlgebraWebApi.Data;
using EcommAlgebraWebApi.Data.Interface;

namespace EcommAlgebraWebApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id) {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);

            return product;
        
        }

    }
}
