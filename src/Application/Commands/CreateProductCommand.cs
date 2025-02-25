using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using WEB_API_CQRS.src.Domain.Entities;
using WEB_API_CQRS.src.Infrastructure.Persistence;

namespace WEB_API_CQRS.src.Application.Commands
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IMongoDbContext _context;

        public CreateProductCommandHandler(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productsCollection = _context.GetCollection<Product>("Products");

            var newProduct = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            await productsCollection.InsertOneAsync(newProduct, cancellationToken: cancellationToken);
            return newProduct;
        }
    }
}
