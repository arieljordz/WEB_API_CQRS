using MediatR;
using MongoDB.Driver;
using WEB_API_CQRS.src.Domain.Entities;
using WEB_API_CQRS.src.Infrastructure.Persistence;

namespace WEB_API_CQRS.src.Application.Queries.Employees
{
    public class GetAllEmployeesQuery : IRequest<List<Employee>> { }

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<Employee>>
    {
        private readonly IMongoDbContext _context;

        public GetAllEmployeesQueryHandler(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employeesCollection = _context.GetCollection<Employee>("Employees");
            return await employeesCollection.Find(_ => true).ToListAsync(cancellationToken);
        }
    }
}
