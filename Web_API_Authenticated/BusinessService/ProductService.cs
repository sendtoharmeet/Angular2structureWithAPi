using DataAccess;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessService
{
    public class ProductService : IProductServices
    {
        public ProductEntity GetProductById(int productId)
        {
            return new ProductRepository().GetByID(productId);
        }

        public IEnumerable<ProductEntity> GetAllProducts()
        {
            return new ProductRepository().GetAll().ToList();
        }

        public int CreateProduct(ProductEntity productEntity)
        {
                var pid =new ProductRepository().InsertProduct(productEntity);
                return pid;
        }

        public int UpdateProduct(int productId, ProductEntity productEntity)
        {
            var success = new ProductRepository().UpdateProduct(productEntity);
                        
            return success;
        }

        public int DeleteProduct(string productId)
        {
            var product = new ProductRepository().DeleteProduct(productId);
            return product;
        }
    }
}