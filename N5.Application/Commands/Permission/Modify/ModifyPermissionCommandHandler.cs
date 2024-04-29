using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using N5.Domain.ElasticSearch;
using N5.Domain.Entities;
using N5.Domain.Messaging;
using N5.Domain.Repository;
using System.Net;

namespace N5.Application.Commands.Permission
{
    public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand, ModifyPermissionResponse>
    {
        private readonly ILogger<ModifyPermissionCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Domain.Entities.Permission> _repository;
        private readonly IKafkaProducer _kafkaProducer;
        private readonly IElasticSearchRepository _elasticSearchRepository;

        public ModifyPermissionCommandHandler(
            IUnitOfWork unitOfWork,
            IRepository<Domain.Entities.Permission> repository,
            ILogger<ModifyPermissionCommandHandler> logger,
            IKafkaProducer kafkaProducer,
            IElasticSearchRepository elasticSearchRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _repository = repository;
            _kafkaProducer = kafkaProducer;
            _elasticSearchRepository = elasticSearchRepository;
        }

        public async Task<ModifyPermissionResponse> Handle(
            ModifyPermissionCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(@"-----   Permission validation on db with EmployeeName: {@request.Id}", request.Id);
            var permissionExist = await _repository.Find(request.Id);

            permissionExist.PermissionDate = request.PermissionDate;
            permissionExist.PermissionTypeId = request.PermissionType;
            permissionExist.EmployeeName = request.EmployeeName;
            permissionExist.EmployeeLastName = request.EmployeeLastName;

            _repository.Update(permissionExist);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation(@"-----   Permission updated successfully - Id: {@permissionExist.Id}", permissionExist.Id);

            var message = new OperationDto
            {
                Id = Guid.NewGuid(),
                Name = "modify"
            };

            _logger.LogInformation(@"-----   Kafka Operation Message for Permision Id: {@permissionExist.Id}", permissionExist.Id);
            await _kafkaProducer.SendMessageAsync(message);

            _logger.LogInformation(@"-----   Elastic Search for Permision Id: {@permissionExist.Id}", permissionExist.Id);
            await _elasticSearchRepository.IndexPermissionAsync(permissionExist);

            return new ModifyPermissionResponse
            {
                Id = permissionExist.Id,
                EmployeeLastName = permissionExist.EmployeeLastName,
                EmployeeName = permissionExist.EmployeeName,
                PermissionDate = permissionExist.PermissionDate,
                PermissionType = permissionExist.PermissionTypeId,
            };
        }
    }
}
