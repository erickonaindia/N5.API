using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5.Application.Commands.Permission;
using N5.Application.Commands.Permission.Request;
using N5.Application.Commons.Dtos;
using N5.Application.Queries.Permission;
using N5.Application.Queries.Permission.Dtos;
using System.Net;
using System.Net.Mime;

namespace N5.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPermissionQueries _permissionQueries;

        public PermissionsController(
            IMediator mediator,
            IPermissionQueries permissionQueries)
        {
            _mediator = mediator;
            _permissionQueries = permissionQueries;
        }


        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(RequestPermissionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResponseDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RequestPermissionResponse>> RequestPermission(
            [FromBody] RequestPermissionCommand command,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ModifyPermissionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResponseDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ModifyPermissionResponse>> ModifyPermission(
            int id,
            ModifyPermissionCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PermissionsDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResponseDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<PermissionsDto>>> GetPermissions(
            CancellationToken cancellationToken)
        {
            return await _permissionQueries.GetPermissionsAsync(cancellationToken);
        }
    }
}
