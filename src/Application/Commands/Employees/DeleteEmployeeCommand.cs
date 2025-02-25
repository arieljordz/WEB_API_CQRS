using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using WEB_API_CQRS.src.Domain.Entities;
using WEB_API_CQRS.src.Infrastructure.Persistence;

namespace WEB_API_CQRS.src.Application.Commands.Employees
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IMongoDbContext _context;

        public DeleteEmployeeCommandHandler(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var collection = _context.GetCollection<Employee>("Employees");

            var result = await collection.DeleteOneAsync(p => p.Id == request.Id, cancellationToken);
            return result.DeletedCount > 0;
        }
    }
}
