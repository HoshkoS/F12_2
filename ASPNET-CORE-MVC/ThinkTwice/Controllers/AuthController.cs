using Domain.Dtos.UserDtos;
using Domain.Repositories;
using Domain.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace ThinkTwice.Controllers;

public class AuthController : Controller
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public AuthController(ILogger logger, IUnitOfWork unitOfWork, IUserService userService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    public ActionResult Index()
    {
        return View();
    }

    public ActionResult SignUp()
    {
        return View();
    }

    public ActionResult Login()
    {
        return View();
    }

    public ActionResult Details(int id)
    {
        return View();
    }

    [HttpPost]
    public ActionResult CreateUser(RegisterUserDto user)
    {
        _userService.CreateUser(user);
        return RedirectToAction("Login", "Auth");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public ActionResult Edit(int id)
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public ActionResult Delete(int id)
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}