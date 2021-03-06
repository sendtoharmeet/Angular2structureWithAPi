﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Web.Http;
using Entities;
using BusinessService;
using ResolverTest.Filters;
using ResolverTest.ActionFilters;

namespace ResolverTest.Controllers
{
    [AuthorizationRequired]
    [RoutePrefix("v1/Products/Product")]
    public class ProductController : ApiController
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        // GET api/product
        [HttpGet]
        [Route("allproducts")]
        [Route("all")]
        public HttpResponseMessage Get()
        {
            var products = _productServices.GetAllProducts();
            var productEntities = products as List<ProductEntity> ?? products.ToList();
            if (productEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, productEntities);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }

        // GET api/product/5
        [HttpGet]
        [Route("productid/{id?}")]
        [Route("particularproduct/{id?}")]
        [Route("myproduct/{id:range(1, 3)}")]
        public HttpResponseMessage Get(int id)
        {
            var product = _productServices.GetProductById(id);
            if (product != null)
                return Request.CreateResponse(HttpStatusCode.OK, product);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found for this id");
        }

        // POST api/product
        [HttpPost]
        [Route("Create")]
        [Route("Register")]
        public int Post([FromBody] ProductEntity productEntity)
        {
            return _productServices.CreateProduct(productEntity);
        }

        // PUT api/product/5
        [HttpPut]
        [Route("Update/productid/{id}")]
        [Route("Modify/productid/{id}")]
        public bool Put(int id, [FromBody] ProductEntity productEntity)
        {
            if (id > 0)
            {
                return _productServices.UpdateProduct(id, productEntity) == 1 ? true : false;
            }
            return false;
        }

        // DELETE api/product/5
        [HttpDelete]
        [Route("remove/productid/{id}")]
        [Route("clear/productid/{id}")]
        [Route("delete/productid/{id}")]
        public bool Delete(int id)
        {
            if (id > 0)
                return _productServices.DeleteProduct(id.ToString()) == 1 ? true : false;
            return false;
        }
    }
}
