using System.Text;
using Domain.Dtos.CategoryDtos;
using Domain.Dtos.UserDtos;
using Domain.Models;
using Domain.Repositories;
using Domain.Services.CategoryService;
using Domain.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Serilog;
using ThinkTwice.Controllers;

namespace Tests;

public class HomeControllerTests
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly ICategoryService _categoryService;
    private readonly IHttpContextAccessor _contextAccessor;

    public HomeControllerTests()
    {
        // Arrange
        _logger = Substitute.For<ILogger>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _userService = Substitute.For<IUserService>();
        _categoryService = Substitute.For<ICategoryService>();
        _contextAccessor = Substitute.For<IHttpContextAccessor>();
    }

    [Fact]
    public async Task Settings_ReturnsViewWithCurrentUser()
    {
        // Arrange
        var controller = new HomeController(_logger, null, _userService, _categoryService, _contextAccessor);
        var expectedUserId = Guid.NewGuid();
        var user = new User()
        {
            Id = expectedUserId, // Use the expected user ID here
            Currency = "UAH",
            BirthDate = DateTime.Now,
            Email = "emain@gmail.com",
            Name = "name",
            Password = "123132",
            Surname = "user"
        };
        var currentUser = new UserDto(user); // Assuming this is your UserDto model

        var userIdBytes = Encoding.UTF8.GetBytes(expectedUserId.ToString());
        var session = Substitute.For<ISession>();
        session.TryGetValue(Arg.Any<string>(), out Arg.Any<byte[]>()).Returns(args =>
        {
            args[1] = userIdBytes;
            return true;
        });
        var httpContext = new DefaultHttpContext();
        httpContext.Session = session;
        _contextAccessor.HttpContext.Returns(httpContext);

        _userService.GetUser(expectedUserId).Returns(Task.FromResult(currentUser)); // Use expectedUserId here
        _categoryService.GetUserCategories(expectedUserId).Returns(new List<CategoryDto>());

        // Act
        var result = await controller.Settings() as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(currentUser, result.Model);
        await _userService.Received(1).GetUser(expectedUserId); // Use expectedUserId here
    }

    [Fact]
    public async Task CreateCategory_RedirectsToSettings_WhenModelStateIsValid()
    {
        // Arrange
        var controller = new HomeController(_logger, _unitOfWork, _userService, _categoryService, _contextAccessor);
        var categoryDto = new CategoryDto();

        // Act
        var result = await controller.CreateCategory(categoryDto) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Settings", result.ActionName);
        Assert.Equal("Home", result.ControllerName);
        await _categoryService.Received(1).CreateCategory(Arg.Any<CategoryDto>());
    }

    [Fact]
    public async Task UpdateCategory_RedirectsToSettings()
    {
        // Arrange
        var controller = new HomeController(_logger, _unitOfWork, _userService, _categoryService, _contextAccessor);
        var categoryDto = new CategoryDto();

        // Act
        var result = await controller.UpdateCategory(categoryDto) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Settings", result.ActionName);
        Assert.Equal("Home", result.ControllerName);
        await _categoryService.Received(1).UpdateCategory(Arg.Any<CategoryDto>());
    }

    [Fact]
    public async Task DeleteCategory_RedirectsToSettings()
    {
        // Arrange
        var controller = new HomeController(_logger, _unitOfWork, _userService, _categoryService, _contextAccessor);
        var categoryDto = new CategoryDto();

        // Act
        var result = await controller.RemoveCategory(categoryDto) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Settings", result.ActionName);
        Assert.Equal("Home", result.ControllerName);
        await _categoryService.Received(1).RemoveCategory(Arg.Any<CategoryDto>());
    }
}