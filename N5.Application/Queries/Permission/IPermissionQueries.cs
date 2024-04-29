using N5.Application.Queries.Permission.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5.Application.Queries.Permission
{
    public interface IPermissionQueries
    {
        Task<List<PermissionsDto>> GetPermissionsAsync(CancellationToken cancellationToken = default);
    }
}
