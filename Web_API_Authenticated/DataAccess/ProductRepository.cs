using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess
{
    public class ProductRepository
    {
        public ProductEntity GetByID(int pid)
        {
            var product = new ProductEntity() { ProductId = 1, ProductName = "P name" };
            return product;
        }

        public int InsertProduct(ProductEntity t)
        {
            return 1;
        }

        public List<ProductEntity> GetAll()
        {
            var prod = new ProductEntity() { ProductId = 1, ProductName = "P Name" };
            var a = new List<ProductEntity>();
            a.Add(prod);
            return a;
        }

        public int UpdateProduct(ProductEntity t)
        {
            return 1;
        }

        public int DeleteProduct(string id)
        {
            return 1;
        }
    }
}
