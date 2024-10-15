using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static PetShop.ProductRepository;
namespace PetShop
{
    public interface IProductRepository
    {
       public void AddProduct(Product product);
       public Product GetProductById(int id);
       public List<Product> GetAllProducts();
       public Task<bool> SaveChangesAsync();
    }
    public interface IOrderRepository
    {
        public void AddOrder(Order order);
        public Order GetOrderById(int id);
     //   public List<Order> GetAllProducts();
    }
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context = new ProductContext();

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void AddProduct(Product product)
        {
            _context.Add<Product>(product);
            _context.SaveChanges();
        }
        public Product GetProductById(int id)
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
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public string DbPath { get; }
        public ProductContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "petshop.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(e => e.Products)
                .WithOne(e => e.Order);
        }
    }
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Order? Order { get; set; } = null!;
    }
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
        public class OrderRoot
    {
        public List<Product> Products { get; set; }
    }
    public class OrderRepository : IOrderRepository
    {
        public ProductContext _context = new ProductContext();
        public void AddOrder(Order order)
        {
            _context.Add<Order>(order);
            _context.SaveChanges();
        }
        public Order GetOrderById(int id)
        {
            Order order = _context.Orders
                .Where(e => (e.OrderId == id))
                .Include(e => e.Products)
                .FirstOrDefault();
            return order;
        }
    }
}