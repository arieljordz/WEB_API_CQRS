using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using WEB_API_CQRS.src.Domain.Entities;
using WEB_API_CQRS.src.Infrastructure.Persistence;

namespace WEB_API_CQRS.src.Application.Commands.Employees
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;   
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly IMongoDbContext _context;

        public CreateEmployeeCommandHandler(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var collection = _context.GetCollection<Employee>("Employees");

            var newEmployee = new Employee
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Email = request.Email,
            };

            await collection.InsertOneAsync(newEmployee, cancellationToken: cancellationToken);
            return newEmployee;
        }
    }
}
