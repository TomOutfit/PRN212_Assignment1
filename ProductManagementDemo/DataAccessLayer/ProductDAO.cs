using BusinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private static readonly List<Product> listProducts;
        private static readonly JsonRoot dataRoot;

        static ProductDAO()
        {
            dataRoot = DataLoader.LoadFromJson() ?? new JsonRoot();
            listProducts = dataRoot.Products ?? new List<Product>();
        }

        public static List<Product> GetProducts()
        {
            return listProducts.Select(p => new Product
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                CategoryId = p.CategoryId,
                Category = dataRoot.Categories.FirstOrDefault(c => c.CategoryId == p.CategoryId) // Populate Category
            }).ToList();
        }

        public static void SaveProduct(Product p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            lock (listProducts)
            {
                var productToSave = new Product
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    UnitPrice = p.UnitPrice,
                    UnitsInStock = p.UnitsInStock,
                    CategoryId = p.CategoryId
                };
                listProducts.Add(productToSave);
                DataLoader.SaveToJson(dataRoot);
            }
        }

        public static void UpdateProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            lock (listProducts)
            {
                var existing = listProducts.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (existing != null)
                {
                    existing.ProductName = product.ProductName;
                    existing.UnitPrice = product.UnitPrice;
                    existing.UnitsInStock = product.UnitsInStock;
                    existing.CategoryId = product.CategoryId;
                    DataLoader.SaveToJson(dataRoot);
                }
                else
                {
                    throw new InvalidOperationException($"Product with ID {product.ProductID} not found.");
                }
            }
        }

        public static void DeleteProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            lock (listProducts)
            {
                var existing = listProducts.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (existing != null)
                {
                    listProducts.Remove(existing);
                    DataLoader.SaveToJson(dataRoot);
                }
                else
                {
                    throw new InvalidOperationException($"Product with ID {product.ProductID} not found.");
                }
            }
        }

        public static Product GetProductById(int id)
        {
            var product = listProducts.FirstOrDefault(p => p.ProductID == id);
            if (product == null) return null;
            return new Product
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                CategoryId = product.CategoryId,
                Category = dataRoot.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId) // Populate Category
            };
        }
    }
}