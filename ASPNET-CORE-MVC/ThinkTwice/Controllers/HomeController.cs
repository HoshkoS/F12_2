using System.Diagnostics;
using Domain.Dtos.CategoryDtos;
using Domain.Repositories;
using Domain.Services.CategoryService;
using Domain.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using ThinkTwice.Models;
using ILogger = Serilog.ILogger;

namespace ThinkTwice.Controllers;

public class HomeController : Controller
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly ICategoryService _categoryService;
    public static Guid CurrentUserId;

    public HomeController(ILogger logger, IUnitOfWork unitOfWork, IUserService userService,
        ICategoryService categoryService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userService = userService;
        _categoryService = categoryService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Settings()
    {
        CurrentUserId = Guid.Parse("3ABAA456-0B8E-49E0-A6E9-1B79DBA2E38F");
        var currentUser = await _userService.GetUser(CurrentUserId);
        currentUser.Categories = await _categoryService.GetUserCategories(CurrentUserId);
        return View(currentUser);
    }

    [HttpPost]
    public async Task<IActionResult?> CreateCategory(CategoryDto category)
    {
        category.UserId = CurrentUserId;

        if (ModelState.IsValid)
        {
            await _categoryService.CreateCategory(category);
            _logger.Error(category.Title);
            return RedirectToAction("Settings", "Home");
        }

        TempData["ErrorMessage"] = ModelState.ErrorCount;

        return RedirectToAction("Settings", "Home");
    }

    [HttpPost, ActionName("UpdateCategory")]
    public async Task<ActionResult> UpdateCategory(CategoryDto category)
    {
        await _categoryService.UpdateCategory(category);
        _logger.Error(category.Title);
        return RedirectToAction("Settings", "Home");
    }

    [HttpPost, ActionName("DeleteCategory")]
    public async Task<ActionResult> RemoveCategory(CategoryDto category)
    {
        await _categoryService.RemoveCategory(category);
        _logger.Error(category.Title);
        return RedirectToAction("Settings", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}