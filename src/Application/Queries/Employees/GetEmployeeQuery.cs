using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using WEB_API_CQRS.src.Domain.Entities;
using WEB_API_CQRS.src.Infrastructure.Persistence;

namespace WEB_API_CQRS.src.Application.Queries.Employees
{
    public class GetEmployeeQuery : IRequest<Employee>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Employee>
    {
        private readonly IMongoDbContext _context;

        public GetEmployeeQueryHandler(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var productsCollection = _context.GetCollection<Employee>("Employees");

            return await productsCollection.Find(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
