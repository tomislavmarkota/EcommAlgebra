using EcommAlgebra.Models;

namespace EcommAlgebraWebApi.Data.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
    }
}
