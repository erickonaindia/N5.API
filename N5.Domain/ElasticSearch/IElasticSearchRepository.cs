using N5.Domain.Entities;

namespace N5.Domain.ElasticSearch
{
    public interface IElasticSearchRepository
    {
        Task IndexPermissionAsync(Permission permission);
    }
}
