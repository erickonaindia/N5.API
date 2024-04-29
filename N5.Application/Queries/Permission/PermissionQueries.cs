using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using N5.Application.Queries.Permission.Dtos;
using N5.Domain.ElasticSearch;
using N5.Domain.Messaging;
using N5.Domain.Repository;

namespace N5.Application.Queries.Permission
{
    public class PermissionQueries : IPermissionQueries
    {
        private IRepository<Domain.Entities.Permission> _repository;
        private readonly ILogger<PermissionQueries> _logger;
        private readonly IKafkaProducer _kafkaProducer;
        private readonly IElasticSearchRepository _elasticSearchRepository;

        public PermissionQueries(
            IRepository<Domain.Entities.Permission> repository,
            ILogger<PermissionQueries> logger,
            IKafkaProducer kafkaProducer,
            IElasticSearchRepository elasticSearchRepository)
        {
            _logger = logger;
            _repository = repository;
            _kafkaProducer = kafkaProducer;
            _elasticSearchRepository = elasticSearchRepository;
        }

        public async Task<List<PermissionsDto>> GetPermissionsAsync(CancellationToken cancellationToken = default)
        {
            var data = await _repository.All.OrderBy(x => x.EmployeeName)
                .ToListAsync(cancellationToken);
            var result = new List<PermissionsDto>();

            foreach (var item in data)
            {
                result.Add(new PermissionsDto
                {
                    EmployeeName = item.EmployeeName,
                    EmployeeLastName = item.EmployeeLastName,
                    Id = item.Id,
                    PermissionDate = item.PermissionDate,
                    PermissionType = item.PermissionTypeId
                });
            }

            var message = new OperationDto
            {
                Id = Guid.NewGuid(),
                Name = "get"
            };

            _logger.LogInformation(@"-----   Kafka Operation Message for Get Permisions");
            await _kafkaProducer.SendMessageAsync(message);

            _logger.LogInformation(@"-----   Elastic Search for Get Permisions");

            var singlePermission = result.Select(x => new Domain.Entities.Permission
            {
                EmployeeLastName = x.EmployeeLastName,
                Id = x.Id,
                EmployeeName = x.EmployeeName,
                PermissionDate = x.PermissionDate,
                PermissionTypeId = x.PermissionType,
            }).FirstOrDefault();

            if (singlePermission is not null)
            {
                await _elasticSearchRepository.IndexPermissionAsync(singlePermission!);
            }
            

            return result;

        }
    }
}
