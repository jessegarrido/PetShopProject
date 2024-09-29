using PetShop.Validators;
using FluentValidation;
using PetStore;
using System.Reflection;

namespace PetShop
{
    public interface IProductLogic {
        public void AddNewProduct(Product product);
        public List<Product> GetAllProducts();
        public Product GetProductById(string id);
      //  public List<string> GetOnlyInStockProducts();
      //  public decimal GetTotalPriceOfInventory();
        public void DefineNewProduct(UILogic uilogic);

    }

    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository _repository;
        //private List<Product> _products { get; set; }
        //private Dictionary<string, DogLeash?> _dogLeash { get; set; }
        //private Dictionary<string, CatFood?> _catFood { get; set; }
        public ProductLogic(IProductRepository repo) {
            _repository = repo;
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

        public void AddNewProduct(Product product)
        {
            _repository.AddProduct(product);
            //_products.Add(product);
            //if (product is DogLeash)
            //{
            //    _dogLeash.Add(product.Name, product as DogLeash);
            //}
            //else if (product is CatFood)
            //{
            //    _catFood.Add(product.Name, product as CatFood);
            //}

        }
        public List<Product> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }
        //public List<string> GetOnlyInStockProducts()
        //{
        //    return _products.InStock().Select(x => x.Name.ToString()).ToList();
        //}

        public Product GetProductById(string id)
        {
            return _repository.GetProductById(id);
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
        //public decimal GetTotalPriceOfInventory(){
        //    return _products.InStock().Sum(x=>x.Price*x.Quantity) ?? 0;
        //}
        public void DefineNewProduct(UILogic uiLogic)
        {
            // Console.WriteLine("Press 1 to add Cat Food or 2 to Add a Dog Leash");
            //  int typeSelection = uiLogic.GetValidUserSelection(new List<int> { 1, 2 });
            //Console.WriteLine("Enter a JSON string that describes the product:");
            //string json = Console.ReadLine();
           // JsonSerializer.Deserialize<Product>(json);
            Product product = new Product();
            PropertyInfo[] properties = typeof(Product).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Console.Write($"Enter new product {property.Name}: ");
                property.SetValue(product,Console.ReadLine());
            }
            ProductValidator validator = new ProductValidator();
            validator.ValidateAndThrow(product);
            AddNewProduct(product);
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

        }
    }
}