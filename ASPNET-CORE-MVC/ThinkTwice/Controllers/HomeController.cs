using System.Diagnostics;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using ThinkTwice.Models;
using ILogger = Serilog.ILogger;

namespace ThinkTwice.Controllers;

public class HomeController : Controller
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var user = new User()
        {
            Name = "Andrii", Email = "andrii2353@gmail.com", Currency = "UAH", Password = "password123",
            Surname = "Savka"
        };
        _unitOfWork.Users.Add(user);
        _logger.Error("This is serilog to seq demo.");
        _unitOfWork.Complete();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}