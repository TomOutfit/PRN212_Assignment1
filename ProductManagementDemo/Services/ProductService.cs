using System.Collections.Generic;
using BusinessObjects;
using DataAccessLayer;

namespace Services
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
        {
            return ProductDAO.GetProducts() ?? new List<Product>();
        }

        public void SaveProduct(Product product)
        {
            ProductDAO.SaveProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            ProductDAO.UpdateProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            ProductDAO.DeleteProduct(product);
        }

        public Product GetProductById(int id)
        {
            return ProductDAO.GetProductById(id);
        }
    }
}