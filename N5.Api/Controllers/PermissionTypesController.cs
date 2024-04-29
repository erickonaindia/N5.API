using Microsoft.AspNetCore.Mvc;
using System.Net;
using N5.Application.Queries.PermissionTypes;
using N5.Application.Queries.PermissionTypes.Dtos;
using N5.Application.Commons.Dtos;

namespace N5.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionTypesController : ControllerBase
    {
        private readonly IPermissionTypesQueries _permissionTypesQueries;

        public PermissionTypesController(
            IPermissionTypesQueries permissionTypesQueries)
        {
            _permissionTypesQueries = permissionTypesQueries;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PermissionTypeDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResponseDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<PermissionTypeDto>>> GetPermissions(
            CancellationToken cancellationToken)
        {
            return await _permissionTypesQueries.GetPermissionTypesAsync(cancellationToken);
        }
    }
}
