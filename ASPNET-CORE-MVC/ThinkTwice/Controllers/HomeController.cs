using System.Diagnostics;
using System.Text;
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
    public static Guid currentUserId;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(ILogger logger, IUnitOfWork unitOfWork, IUserService userService,
        ICategoryService categoryService, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userService = userService;
        _categoryService = categoryService;
        _httpContextAccessor = httpContextAccessor;
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
        var userIdBytes = _httpContextAccessor.HttpContext.Session.Get("UserId");
        if (userIdBytes != null)
        {
            var userIdString = Encoding.UTF8.GetString(userIdBytes);
            var currentUser = await _userService.GetUser(currentUserId);
            currentUser.Categories = await _categoryService.GetUserCategories(currentUserId);
            return View(currentUser);
        }

        return Unauthorized();
    }

    [HttpPost]
    public async Task<IActionResult?> CreateCategory(CategoryDto category)
    {
        category.UserId = currentUserId;

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