using System.Diagnostics;
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
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    //public IActionResult SignUp()
    //{
    //    return View();
    //}

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}