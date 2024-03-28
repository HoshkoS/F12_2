using Domain.Dtos.CategoryDtos;
using Domain.Dtos.UserDtos;
using Domain.Models;
using Domain.Repositories;
using Domain.Services.CategoryService;
using Domain.Services.UserService;
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

    public HomeControllerTests()
    {
        // Arrange
        _logger = Substitute.For<ILogger>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _userService = Substitute.For<IUserService>();
        _categoryService = Substitute.For<ICategoryService>();
    }

    [Fact]
    public async Task Settings_ReturnsViewWithCurrentUser()
    {
        // Arrange
        var controller = new HomeController(_logger, null, _userService, _categoryService);
        var expectedUserId = Guid.Parse("3ABAA456-0B8E-49E0-A6E9-1B79DBA2E38F");
        var user = new User()
        {
            Id = Guid.NewGuid(), Currency = "UAH", BirthDate = DateTime.Now, Email = "emain@gmail.com", Name = "name",
            Password = "123132", Surname = "user"
        };
        var currentUser = new UserDto(user); // Assuming this is your UserDto model

        _userService.GetUser(expectedUserId).Returns(Task.FromResult(currentUser));
        _categoryService.GetUserCategories(expectedUserId).Returns([]);

        // Act
        var result = await controller.Settings() as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(currentUser, result.Model);
        await _userService.Received(1).GetUser(Arg.Any<Guid>());
    }

    [Fact]
    public async Task CreateCategory_RedirectsToSettings_WhenModelStateIsValid()
    {
        // Arrange
        var controller = new HomeController(_logger, _unitOfWork, _userService, _categoryService);
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
        var controller = new HomeController(_logger, _unitOfWork, _userService, _categoryService);
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
        var controller = new HomeController(_logger, _unitOfWork, _userService, _categoryService);
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