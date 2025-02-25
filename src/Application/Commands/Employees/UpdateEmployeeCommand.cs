using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using WEB_API_CQRS.src.Domain.Entities;
using WEB_API_CQRS.src.Infrastructure.Persistence;

namespace WEB_API_CQRS.src.Application.Commands.Employees
{
    public class UpdateEmployeeCommand : IRequest<Employee>
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
    {
        private readonly IMongoDbContext _context;

        public UpdateEmployeeCommandHandler(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var collection = _context.GetCollection<Employee>("Employees");

            var updateDefinition = Builders<Employee>.Update
                .Set(p => p.FirstName, request.FirstName)
                .Set(p => p.MiddleName, request.MiddleName)
                .Set(p => p.LastName, request.LastName)
                .Set(p => p.Email, request.Email);

            var updatedEmployee = await collection.FindOneAndUpdateAsync(
                p => p.Id == request.Id,
                updateDefinition,
                new FindOneAndUpdateOptions<Employee> { ReturnDocument = ReturnDocument.After },
                cancellationToken);

            return updatedEmployee;
        }
    }
}
