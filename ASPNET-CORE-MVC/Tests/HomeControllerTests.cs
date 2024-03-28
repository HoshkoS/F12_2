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
        var controller = new HomeController(_logger, _unitOfWork, _userService, _categoryService);
        var user = new User();
        var currentUser = new UserDto(user);

        _userService.GetUser(Arg.Any<Guid>()).Returns(Task.FromResult(currentUser));
        _categoryService.GetUserCategories(Arg.Any<Guid>())
            .Returns(Array.Empty<CategoryDto>());

        // Act
        var result = await controller.Settings() as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(currentUser, result.Model);
    }

    [Fact]
    public async Task CreateCategory_RedirectsToSettings_WhenModelStateIsValid()
    {
        var controller = new HomeController(_logger, _unitOfWork, _userService, _categoryService);
        var categoryDto = new CategoryDto();

        // Act
        var result = await controller.CreateCategory(categoryDto) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Settings", result.ActionName);
        Assert.Equal("Home", result.ControllerName);
    }
}