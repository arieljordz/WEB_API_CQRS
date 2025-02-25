using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_API_CQRS.src.Application.Commands;
using WEB_API_CQRS.src.Application.Queries;
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
                         .ReturnsAsync((Product)null);

            var result = await _controller.GetById("1");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            var product = new Product { Id = "1" };
            _mockMediator.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(product);

            var result = await _controller.Create(new CreateProductCommand());

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task Update_IdMismatch_ReturnsBadRequest()
        {
            var result = await _controller.Update("1", new UpdateProductCommand { Id = "2" });

            Assert.IsType<BadRequestObjectResult>(result);
        }

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
