using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using WEB_API_CQRS.src.Domain.Entities;
using WEB_API_CQRS.src.Infrastructure.Persistence;

namespace WEB_API_CQRS.src.Application.Commands.Products
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IMongoDbContext _context;

        public DeleteProductCommandHandler(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var collection = _context.GetCollection<Product>("Products");

            var result = await collection.DeleteOneAsync(p => p.Id == request.Id, cancellationToken);
            return result.DeletedCount > 0;
        }
    }
}
