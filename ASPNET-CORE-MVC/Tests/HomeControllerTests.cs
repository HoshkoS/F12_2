using Domain.Dtos.CategoryDtos;
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