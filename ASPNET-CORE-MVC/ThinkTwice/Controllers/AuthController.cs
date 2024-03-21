using System.Diagnostics;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using ThinkTwice.Models;
using ILogger = Serilog.ILogger;
using ThinkTwice.Dtos;
using Domain.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

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

    public IActionResult SignUp()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    // GET: SignUpController/Details/5
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
            Currency = "UAH"
        };
        _unitOfWork.Users.Add(newUser);
        _unitOfWork.Complete();
        _logger.Error(user.Surname);
        return RedirectToAction("Login", "Auth");
    }

    // POST: SignUpController/Create
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

    // GET: SignUpController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: SignUpController/Edit/5
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

    // GET: SignUpController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: SignUpController/Delete/5
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