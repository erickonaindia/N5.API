using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using N5.Api.Controllers;
using N5.Application.Commands.Permission;
using N5.Application.Commands.Permission.Request;
using N5.Application.Queries.Permission;
using N5.Application.Queries.Permission.Dtos;
using N5.Domain.ElasticSearch;
using N5.Domain.Messaging;
using N5.Domain.Repository;
using System.Linq.Expressions;

namespace N5.TestProject
{
    public class PermissionsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        private readonly Mock<IPermissionQueries> _permissionQueriesMock = new Mock<IPermissionQueries>();

        [Fact(Skip = "TODO - Needs more Mocking")]
        public async Task RequestPermission_Returns_OkResult_With_Valid_Request()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var repositoryMock = new Mock<IRepository<Domain.Entities.Permission>>();
            var mediatorMock = new Mock<IMediator>();
            var elasticSearchRepositoryMock = new Mock<IElasticSearchRepository>();
            var kafkaProducerMock = new Mock<IKafkaProducer>();

            var handler = new RequestPermissionCommandHandler(
                unitOfWorkMock.Object,
                repositoryMock.Object,
                NullLogger<RequestPermissionCommandHandler>.Instance,
                kafkaProducerMock.Object,
                elasticSearchRepositoryMock.Object);

            var command = new RequestPermissionCommand
            {
                EmployeeName = "John",
                EmployeeLastName = "Doe",
                PermissionType = 1,
                PermissionDate = DateTime.Now
            };

            repositoryMock.Setup(x => x.All)
                .Returns(new List<Domain.Entities.Permission>().AsQueryable());

            repositoryMock.Setup(x => x.All.Any(It.IsAny<Expression<Func<Domain.Entities.Permission, bool>>>()))
                .Returns(false);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<RequestPermissionResponse>(response);
        }

        [Fact(Skip = "TODO - Needs more Mocking")]
        public async Task ModifyPermission_Returns_OkResult_With_Valid_Request()
        {
            // Arrange
            var controller = new PermissionsController(_mediatorMock.Object, _permissionQueriesMock.Object);
            var id = 1;
            var request = new ModifyPermissionCommand();
            var expectedResponse = new ModifyPermissionResponse();

            _mediatorMock.Setup(m => m.Send(It.IsAny<ModifyPermissionCommand>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.ModifyPermission(id, request, CancellationToken.None);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.Value.Should().Be(expectedResponse);
        }

        [Fact(Skip = "TODO - Needs more Mocking")]
        public async Task GetPermissions_Returns_OkResult_With_Valid_Permissions()
        {
            // Arrange
            var controller = new PermissionsController(_mediatorMock.Object, _permissionQueriesMock.Object);
            var expectedPermissions = new List<PermissionsDto>();

            _permissionQueriesMock.Setup(p => p.GetPermissionsAsync(default)).ReturnsAsync(expectedPermissions);

            // Act
            var result = await controller.GetPermissions(CancellationToken.None);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.Value.Should().Be(expectedPermissions);
        }
    }
}