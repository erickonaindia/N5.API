using N5.Application.Queries.Permission.Dtos;
using N5.Application.Queries.PermissionTypes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5.Application.Queries.PermissionTypes
{
    public interface IPermissionTypesQueries
    {
        Task<List<PermissionTypeDto>> GetPermissionTypesAsync(CancellationToken cancellationToken = default);
    }
}
