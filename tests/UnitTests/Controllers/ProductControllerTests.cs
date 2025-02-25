using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API_CQRS.src.Application.Commands.Products;
using WEB_API_CQRS.src.Application.Queries.Products;
using WEB_API_CQRS.src.Domain.Entities;
using WEB_API_CQRS.src.WebApi.Controllers;
using Xunit;

namespace WEB_API_CQRS.tests.UnitTests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new ProductController(_mockMediator.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new List<Product>());

            var result = await _controller.GetAll();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ProductExists_ReturnsOkResult()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new Product());

            var result = await _controller.GetById("1");

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_ProductNotFound_ReturnsNotFoundResult()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((Product?)null);

            var result = await _controller.GetById("1");

            Assert.IsType<NotFoundResult>(result);
        }

        //[Fact]
        //public async Task Create_ShouldReturn_CreatedAtActionResult()
        //{
        //    // Arrange
        //    var command = new CreateProductCommand
        //    {
        //        Name = "Test Product",
        //        Price = 100
        //    };

        //    var expectedProduct = new Product
        //    {
        //        Id = "1",
        //        Name = command.Name,
        //        Price = command.Price
        //    };

        //    _mockMediator.Setup(m => m.Send(It.Is<CreateProductCommand>(c => c.Name == command.Name), It.IsAny<CancellationToken>()))
        //                 .ReturnsAsync(expectedProduct);

        //    // Act
        //    var result = await _controller.Create(command);

        //    // Assert
        //    var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
        //    Assert.Equal(nameof(_controller.GetById), createdAtResult.ActionName);
        //    Assert.Equal(expectedProduct.Id, createdAtResult.RouteValues["id"]);
        //    Assert.Equal(expectedProduct, createdAtResult.Value);
        //}

        //[Fact]
        //public async Task Update_WithIdMismatch_ShouldReturn_BadRequest()
        //{
        //    // Arrange
        //    var command = new UpdateProductCommand { Id = "2", Name = "Updated Product", Price = 150 };

        //    // Act
        //    var result = await _controller.Update("1", command);

        //    // Assert
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //    Assert.Equal("ID in URL and request body must match.", badRequestResult.Value);

        //    // Ensure the mediator is NOT called when IDs don't match
        //    _mockMediator.Verify(m => m.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        //}

        [Fact]
        public async Task Delete_ProductExists_ReturnsNoContentResult()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            var result = await _controller.Delete("1");

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ProductNotFound_ReturnsNotFoundResult()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(false);

            var result = await _controller.Delete("1");

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
