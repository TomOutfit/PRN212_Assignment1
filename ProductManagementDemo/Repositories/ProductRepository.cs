// Repositories/ProductRepository.cs
using System.Collections.Generic;
using BusinessObjects;
using DataAccessLayer;
using System; // Thêm dòng này

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(Product p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p), "Product cannot be null for deletion.");
            ProductDAO.DeleteProduct(p);
        }

        public void SaveProduct(Product p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p), "Product cannot be null for saving.");
            ProductDAO.SaveProduct(p);
        }

        public void UpdateProduct(Product p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p), "Product cannot be null for update.");
            ProductDAO.UpdateProduct(p);
        }

        public List<Product> GetProducts()
        {
            return ProductDAO.GetProducts() ?? new List<Product>();
        }

        public Product? GetProductById(int id)
        {
            return ProductDAO.GetProductById(id);
        }
    }
}