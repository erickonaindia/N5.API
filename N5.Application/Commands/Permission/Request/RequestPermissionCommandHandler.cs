using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using N5.Domain.ElasticSearch;
using N5.Domain.Messaging;
using N5.Domain.Repository;
using System.Net;

namespace N5.Application.Commands.Permission.Request
{
    public class RequestPermissionCommandHandler : IRequestHandler<RequestPermissionCommand, RequestPermissionResponse>
    {
        private readonly ILogger<RequestPermissionCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Domain.Entities.Permission> _repository;
        private readonly IKafkaProducer _kafkaProducer;
        private readonly IElasticSearchRepository _elasticSearchRepository;

        public RequestPermissionCommandHandler(
            IUnitOfWork unitOfWork,
            IRepository<Domain.Entities.Permission> repository,
            ILogger<RequestPermissionCommandHandler> logger,
            IKafkaProducer kafkaProducer,
            IElasticSearchRepository elasticSearchRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _repository = repository;
            _kafkaProducer = kafkaProducer;
            _elasticSearchRepository = elasticSearchRepository;
        }

        public async Task<RequestPermissionResponse> Handle(
            RequestPermissionCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(@"-----   Permission validation on db with EmployeeName: {@request.EmployeeName}", request.EmployeeName);
            var permissionExist = await _repository.All.AnyAsync(x => x.EmployeeName == request.EmployeeName);

            if (permissionExist)
                throw new Exception();

            var permission = new Domain.Entities.Permission
            {
                PermissionTypeId = request.PermissionType,
                EmployeeLastName = request.EmployeeLastName,
                EmployeeName = request.EmployeeName,
                PermissionDate = request.PermissionDate,
            };

            _repository.Insert(permission);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation(@"-----   Permission inserted successfully - Id: {@permission.Id}", permission.Id);

            var message = new OperationDto
            {
                Id = Guid.NewGuid(),
                Name = "request"
            };

            _logger.LogInformation(@"-----   Kafka Operation Message for Permision Id: {@permissionExist.Id}", permission.Id);
            await _kafkaProducer.SendMessageAsync(message);

            _logger.LogInformation(@"-----   Elastic Search for Permision Id: {@permissionExist.Id}", permission.Id);
            await _elasticSearchRepository.IndexPermissionAsync(permission);

            return new RequestPermissionResponse
            {
                Id = permission.Id,
                EmployeeLastName = permission.EmployeeLastName,
                EmployeeName = permission.EmployeeName,
                PermissionDate = permission.PermissionDate,
                PermissionType = permission.PermissionTypeId,
            };
        }
    }
}
