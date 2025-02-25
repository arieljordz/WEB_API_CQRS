using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using WEB_API_CQRS.src.Domain.Entities;
using WEB_API_CQRS.src.Infrastructure.Persistence;

namespace WEB_API_CQRS.src.Application.Commands
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IMongoDbContext _context;

        public UpdateProductCommandHandler(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productsCollection = _context.GetCollection<Product>("Products");

            var updateDefinition = Builders<Product>.Update
                .Set(p => p.Name, request.Name)
                .Set(p => p.Price, request.Price);

            var updatedProduct = await productsCollection.FindOneAndUpdateAsync(
                p => p.Id == request.Id,
                updateDefinition,
                new FindOneAndUpdateOptions<Product> { ReturnDocument = ReturnDocument.After },
                cancellationToken);

            return updatedProduct;
        }
    }
}
