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
    static public User currentUser = new User
    {
        Id = Guid.Parse("3ABAA456-0B8E-49E0-A6E9-1B79DBA2E38F"),
        Email = "olena@lnu.edu",
        Password = "123456789",
        Name = "Olena",
        Surname = "Hoshko",
        BirthDate = DateTime.Now,
        Currency = "UAH",
        Categories = new Category[]
        {
            new Category
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("3ABAA456-0B8E-49E0-A6E9-1B79DBA2E38F"),
                Title = "Salary",
                IsGeneral = false,
                PercentageAmount = 0,
                Type = "Income",
            },

            new Category
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("3ABAA456-0B8E-49E0-A6E9-1B79DBA2E38F"),
                Title = "Food",
                IsGeneral = false,
                PercentageAmount = 0,
                Type = "Expences",
            },
        },
    };

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

    public IActionResult Settings()
    {
        return View(currentUser);
    }

    //public IActionResult SignUp()
    //{
    //    return View();
    //}


    [HttpPost]
    public ActionResult CreateCategory(string Title, string Type, decimal Percentage)
    {
        Category cat = new Category
        {
            Title = Title,
            Type = Type,
            PercentageAmount = Percentage,
            UserId = currentUser.Id,
        };
        _unitOfWork.Categories.Add(cat);
        _unitOfWork.Complete();
        _logger.Error(cat.Title);
        return RedirectToAction("Settings", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}