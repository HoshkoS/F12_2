using System.Diagnostics;
using Domain.Dtos.CategoryDtos;
using Domain.Dtos.UserDtos;
using Domain.Repositories;
using Domain.Services.CategoryService;
using Domain.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public HomeController(ILogger logger, IUnitOfWork unitOfWork, IUserService userService, ICategoryService categoryService)
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

    public IActionResult Settings()
    {
        CurrentUserId = Guid.Parse("3ABAA456-0B8E-49E0-A6E9-1B79DBA2E38F");
        var currentUser = _userService.getUser(CurrentUserId);

        return View(currentUser);
    }

    [HttpPost]
    public IActionResult? CreateCategory(CategoryDto category)
    {
        category.UserId = CurrentUserId;

        if (ModelState.IsValid)
        {
            _categoryService.createCategory(category);
            _logger.Error(category.Title);
            return RedirectToAction("Settings", "Home");
        }

        TempData["ErrorMessage"] = ModelState.ErrorCount;

        return RedirectToAction("Settings", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}