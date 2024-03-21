using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using ThinkTwice.Dtos;
using ILogger = Serilog.ILogger;

namespace ThinkTwice.Controllers;

public class AuthController : Controller
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public AuthController(ILogger logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public ActionResult Index()
    {
        return View();
    }

    public ActionResult SignUp()
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
        User newUser = new User
        {
            Email = user.Email,
            Password = user.Password,
            Name = user.Name,
            Surname = user.Surname,
            Currency = "UAH",
        };
        _unitOfWork.Users.Add(newUser);
        _unitOfWork.Complete();
        _logger.Error(user.Surname);
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