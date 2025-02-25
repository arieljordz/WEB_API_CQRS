using MediatR;
using MongoDB.Driver;
using WEB_API_CQRS.src.Domain.Entities;
using WEB_API_CQRS.src.Infrastructure.Persistence;

namespace WEB_API_CQRS.src.Application.Queries.Products
{
    public class GetProductQuery : IRequest<Product>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly IMongoDbContext _context;

        public GetProductQueryHandler(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var productsCollection = _context.GetCollection<Product>("Products");

            return await productsCollection.Find(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
