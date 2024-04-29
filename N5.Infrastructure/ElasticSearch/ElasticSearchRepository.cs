using N5.Domain.ElasticSearch;
using N5.Domain.Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5.Infrastructure.ElasticSearch
{
    public class ElasticSearchRepository : IElasticSearchRepository
    {
        private readonly IElasticClient _client;

        public ElasticSearchRepository(Uri elasticsearchUri)
        {
            var settings = new ConnectionSettings(elasticsearchUri);
            _client = new ElasticClient(settings);
        }

        public async Task IndexPermissionAsync(Permission permission)
        {
            var document = new
            {
                id = permission.Id,
                employeeName = permission.EmployeeName,
                employeeLastname = permission.EmployeeLastName,
                permissionTypeId = permission.PermissionType,
                permissionDate = permission.PermissionDate.ToString("yyyy-MM-dd")
            };

            await _client.IndexAsync(document, idx => idx.Index("permissions"));
        }
    }
}
