using Entities;
using System.Collections.Generic;

namespace BusinessService
{
    public interface IProductServices
    {
        ProductEntity GetProductById(int productId);
        IEnumerable<ProductEntity> GetAllProducts();
        int CreateProduct(ProductEntity productEntity);
        int UpdateProduct(int productId, ProductEntity productEntity);
        int DeleteProduct(string productId);
    }
}
