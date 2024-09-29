using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace PetStore
{
    public interface IProductRepository
    {
        public void AddProduct(Product product);
        public Product GetProductById(string id);
        public List<Product> GetAllProducts();
    }
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context = new ProductContext();
        public void AddProduct(Product product)
        {
            _context.Add<Product>(product);
            _context.SaveChanges();
        }
        public Product GetProductById(string id)
        {
            Product product = _context.Products
                .Where(e => String.Equals(e.ProductId, id))
                .FirstOrDefault();
            return product;
        }
        public List<Product> GetAllProducts()
        {
            List<Product> products = new();
            foreach (Product product in _context.Products)
                products.Add(product);
            return products;
        }
    }
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public string DbPath { get; }
        public ProductContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "petshop.db");
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
    }
    public class Product
    {
        public string ProductId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
