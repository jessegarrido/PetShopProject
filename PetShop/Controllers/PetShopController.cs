using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
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
                var allProducts = _productRepository.GetAllProducts();
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
                var allOrders = _orderRepository.GetAllOrders();
                return Ok(allOrders);
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
        public async Task<ActionResult<Order>> AddOrder(ProductsRoot products)
        {
            ProductContext tmpcontext = new();
            int orderCount = tmpcontext.Orders.Count();
            Order order = new Order();
            order.OrderId = orderCount + 1;
            order.OrderDate = DateTime.Now;
            
            //string json = Console.ReadLine();
           // var stringproducts = JsonSerializer.Serialize<ProductsRoot>(products);
           // Console.Write($"Products ordered (as JSON): {stringproducts}");
            order.Products = products.Products;
            orderValidator validator = new orderValidator();
            validator.ValidateAndThrow(order);
            _orderRepository.AddOrder(order);
            await _productRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }
    }
}

