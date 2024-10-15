using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace PetShop
{ 
    [ApiController]
    [Route("api/{action}")]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
        
        public ProductController(IProductRepository productRepo, IOrderRepository orderRepo)
        {
            _productRepository = productRepo;
            _orderRepository = orderRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var allproducts = _productRepository.GetAllProducts();
                return Ok(allproducts);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id:int}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(_productRepository.GetProductById(id));
        }
        [HttpGet("{id:int}")]
        public Order GetOrder(int id)
        {
            return _orderRepository.GetOrderById(id);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
            await _productRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product); 
        }
        public async Task<ActionResult<Order>> AddOrder(Order order)
        {
            _orderRepository.AddOrder(order);
            await _productRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }
    }
}

