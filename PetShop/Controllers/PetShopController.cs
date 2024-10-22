using FluentValidation;
using Microsoft.AspNetCore.Mvc;
namespace PetShop
{
    [ApiController]
    [Route("api/{action}")]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
        ProductValidator validator = new ProductValidator();

        public ProductController(IProductRepository productRepo, IOrderRepository orderRepo)
        {
            _productRepository = productRepo;
            _orderRepository = orderRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var allProducts = await _productRepository.GetAllProductsAsync();
                return Ok(allProducts);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var allOrders = await _orderRepository.GetAllOrdersAsync();
                return Ok(allOrders);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return Ok(await _productRepository.GetProductByIdAsync(id));
        }
        [HttpGet("{id:int}")]
        public async Task<Order> GetOrder(int id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            await _productRepository.AddProductAsync(product);
            await _productRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product); 
        }
        public async Task<ActionResult<Order>> AddOrder(ProductsRoot products)
        {
            ProductContext tmpcontext = new();
            int orderCount = tmpcontext.Orders.Count();
            Order order = new Order();
            order.OrderId = orderCount + 1;
            order.OrderDate = DateTime.Now;
            order.Products = products.Products;
            orderValidator validator = new orderValidator();
            validator.ValidateAndThrow(order);
            await _orderRepository.AddOrderAsync(order);
            await _productRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }
    }
}

