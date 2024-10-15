using Moq;
using PetShop;

namespace PetShop.Tests
{
    [TestClass]
    public class ProductLogicTests
    {
        [TestMethod]
        public void GetProductById_CallsRepo()
        {
        // Arrange 
            Mock<IProductRepository> _productRepositoryMock = new();
            Mock<IOrderRepository> _orderRepositoryMock = new();
            ProductLogic _productLogic = new ProductLogic(_productRepositoryMock.Object, _orderRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.GetProductById(10))
                .Returns(new Product { ProductId = 10, Name = "test product" });
         // Act 
            _productLogic.GetProductById(10);
         // Assert
            _productRepositoryMock.Verify(x => x.GetProductById(10), Times.Once);
        }
    }
}