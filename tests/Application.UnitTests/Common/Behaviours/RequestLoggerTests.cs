
using Application.Contract.Commands;
using Application.Contract.Common.Behaviours;
using Application.Contract.Common.Interfaces;
using Application.Contract.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace EShop.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateProductCommand>> _logger = null!;
    private Mock<ICurrentUserService> _currentUserService = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateProductCommand>>();
        _currentUserService = new Mock<ICurrentUserService>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserId).Returns(1);

        var requestLogger = new LoggingBehaviour<CreateProductCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateProductCommand { Name="کالا1",Type=Domain.Enums.ProductType.Normal,Price=10000,Benefit=1000 }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateProductCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateProductCommand { Name="کالا1",Type=Domain.Enums.ProductType.Normal,Price=10000,Benefit=1000 }, new CancellationToken());


        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<int>()), Times.Never);
    }
}
