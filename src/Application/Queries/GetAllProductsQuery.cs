using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WEB_API_CQRS.src.Domain.Entities;
using WEB_API_CQRS.src.Infrastructure.Persistence;

namespace WEB_API_CQRS.src.Application.Queries
{
    public class GetAllProductsQuery : IRequest<List<Product>> { }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IMongoDbContext _context;

        public GetAllProductsQueryHandler(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productsCollection = _context.GetCollection<Product>("Products");
            return await productsCollection.Find(_ => true).ToListAsync(cancellationToken);
        }
    }
}
