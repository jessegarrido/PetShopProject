using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static PetShop.ProductRepository;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
namespace PetShop
{
    public interface IProductRepository
    {
       public Task AddProductAsync(Product product);
       public Task<Product> GetProductByIdAsync(int id);
       public Task<List<Product>> GetAllProductsAsync();
       public Task<bool> SaveChangesAsync();
    }
    public interface IOrderRepository
    {
        public Task AddOrderAsync(Order order);
        public Task<Order> GetOrderByIdAsync(int id);
        public Task<List<Order>> GetAllOrdersAsync();
    }
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context = new ProductContext();

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task AddProductAsync(Product product)
        {
            var task = Task.Run(() =>
            {
                _context.Add<Product>(product);
                _context.SaveChanges();
            });
            await task;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = _context.Products
                  .Where(e => (e.ProductId == id))
                  .FirstOrDefault();
            return product;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = new();
            foreach (Product product in _context.Products)
                products.Add(product);
            return products;
        }

    }
    public class OrderRepository : IOrderRepository
    {
        public ProductContext _context = new ProductContext();
        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };
        public async Task AddOrderAsync(Order order)
        {
            var task = Task.Run(() =>
            {
                _context.Add<Order>(order);
                _context.SaveChanges();
            });
            await task;
        }
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            Order order = _context.Orders
                .Where(e => (e.OrderId == id))
                .Include(e => e.Products)
                .FirstOrDefault();
            return order;
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            List<Order> orders = new();
            foreach (Order order in _context.Orders)
                orders.Add(order);
            return orders;
        }
    }
    public class ProductContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public string DbPath { get; }
        public ProductContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "petshop.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite($"Data Source={DbPath}")
            .LogTo(Console.WriteLine, new[] {DbLoggerCategory.Database.Command.Name },
                LogLevel.Information)
            .EnableSensitiveDataLogging();
    }
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Order? Order { get; set; } = null!;
        public int? OrderId { get; set; }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public class ProductsRoot
    {
        public List<Product> Products { get; set; }
    }

}