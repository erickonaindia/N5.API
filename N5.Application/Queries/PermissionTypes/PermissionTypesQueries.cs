using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using N5.Application.Queries.PermissionTypes.Dtos;
using N5.Domain.Repository;

namespace N5.Application.Queries.PermissionTypes
{
    public class PermissionTypesQueries : IPermissionTypesQueries
    {
        private IRepository<Domain.Entities.PermissionType> _repository;
        private readonly ILogger<PermissionTypesQueries> _logger;

        public PermissionTypesQueries(
            IRepository<Domain.Entities.PermissionType> repository,
            ILogger<PermissionTypesQueries> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<List<PermissionTypeDto>> GetPermissionTypesAsync(CancellationToken cancellationToken = default)
        {
            var data = await _repository.All.OrderBy(x => x.Description)
                .ToListAsync(cancellationToken);
            var result = new List<PermissionTypeDto>();

            foreach (var item in data)
            {
                result.Add(new PermissionTypeDto
                {
                    Id = item.Id,
                    Description = item.Description,
                });
            }
            
            return result;

        }
    }
}
