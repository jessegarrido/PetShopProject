using FluentValidation;
using PetStore;
using System.Reflection;
using System.Text.Json;

namespace PetShop
{
    public interface IProductLogic {
        public void AddNewProduct();
        public List<Product> GetAllProducts();
        public Product GetProductById(int id);
        public void AddNewOrder();
        public Order GetOrderById(int id);
        //  public List<string> GetOnlyInStockProducts();
        //  public decimal GetTotalPriceOfInventory();
        //public void DefineNewProduct(UILogic uilogic);

    }

    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        //private List<Product> _products { get; set; }
        //private Dictionary<string, DogLeash?> _dogLeash { get; set; }
        //private Dictionary<string, CatFood?> _catFood { get; set; }
        public ProductLogic(IProductRepository productRepo, IOrderRepository orderRepo) {
            _productRepository = productRepo;
            _orderRepository = orderRepo;
            //_dogLeash = new Dictionary<string, DogLeash?>();
            //_catFood = new Dictionary<string, CatFood?>();
            //_products = new List<Product>();
            //Product product = new Product()
            //{
            //    ProductId = "1",
            //    Name = "Catteriffic",
            //    Price = "$6.99",
            //    Quantity = "4",
            //    Description = "It's Tasty riffic, meow!"
            //    WeightPounds = 7,
            //    KittenFood = true
            //};
            //_repository.AddProduct(product);
            //_catFood.Add(_products[0].Name, _products[0] as CatFood);
            //_products.Add(new DogLeash()
            //{
            //    Name = "Leashomatic",
            //    Price = 4.55M,
            //    Quantity = 6,
            //    Description = "It's a leash but automatic",
            //    LengthInches = 27,
            //    Material = "nylon"
            //});
            //_dogLeash.Add(_products[1].Name, _products[1] as DogLeash);
            //_products.Add(new DogLeash()
            //{
            //    Name = "Leash Unleashed",
            //    Price = 99.55M,
            //    Quantity = 0,
            //    Description = "Don't let a short leash hold you back",
            //    LengthInches = 4,
            //    Material = "titanium"
            //});
            //_dogLeash.Add(_products[2].Name, _products[2] as DogLeash);
        }

        public void AddNewProduct()
        {
            Console.WriteLine("Enter a JSON string that describes the product:");
            string json = Console.ReadLine();
            Product product = JsonSerializer.Deserialize<Product>(json);  
            ProductValidator validator = new ProductValidator();
            validator.ValidateAndThrow(product);
            _productRepository.AddProduct(product);
        }
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }
        //public List<string> GetOnlyInStockProducts()
        //{
        //    return _products.InStock().Select(x => x.Name.ToString()).ToList();
        //}

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
            //if (_dogLeash.ContainsKey(name))
            //{
            //    return _dogLeash[name] as T;
            //}
            //else if (_catFood.ContainsKey(name))
            //{
            //    return _catFood[name] as T;
            //}
            //else
            //    return null;
          }
        public void AddNewOrder()
        {
            ProductContext context = new();
            var orderCount = context.Orders.Count();
            context.Dispose();
            Order order = new Order();
            order.OrderId = orderCount + 1;
            order.OrderDate = DateTime.Now;
            Console.Write($"Products ordered (as JSON): ");
            string json = Console.ReadLine();
            var products = JsonSerializer.Deserialize<OrderRoot>(json);
            order.Products = products as ICollection<Product>;
            orderValidator validator = new orderValidator();
            validator.ValidateAndThrow(order);
            _orderRepository.AddOrder(order);
        }
        //public List<Order> GetAllOrders()
        //{
        //    return _repository.GetAllOrders();
        //}
        public Order GetOrderById(int id)
        {
            return _orderRepository.GetOrderById(id);

        }
        //public decimal GetTotalPriceOfInventory(){
        //    return _products.InStock().Sum(x=>x.Price*x.Quantity) ?? 0;
        //}
        //    public void DefineNewProduct(UILogic uiLogic)
        //{
        //    // Console.WriteLine("Press 1 to add Cat Food or 2 to Add a Dog Leash");
        //    //  int typeSelection = uiLogic.GetValidUserSelection(new List<int> { 1, 2 });
        //   Console.WriteLine("Enter a JSON string that describes the product:");
        //    string json = Console.ReadLine();
        //    JsonSerializer.Deserialize<Product>(json);
        //    Product product = new Product();
        //   // PropertyInfo[] properties = typeof(Product).GetProperties();
        //   // foreach (PropertyInfo property in properties)
        //   // {
        //    //    Console.Write($"Enter new product {property.Name}: ");
        //   //     property.SetValue(product,Console.ReadLine());
        //   // }
        //    ProductValidator validator = new ProductValidator();
        //    validator.ValidateAndThrow(product);
        //    AddNewProduct(product);
        //switch (typeSelection)
        //{
        //    case 1:
        //        CatFood catFood = JsonSerializer.Deserialize<CatFood>(json);
        //        validator.ValidateAndThrow(catFood);
        //        this.AddProductToDictionary(catFood);
        //        break;
        //    case 2:
        //        DogLeash dogLeash = JsonSerializer.Deserialize<DogLeash>(json);
        //        validator.ValidateAndThrow(dogLeash);
        //        this.AddProductToDictionary(dogLeash);
        //        break;
        //    default:
        //        break;
        //}

        //}
    }
}