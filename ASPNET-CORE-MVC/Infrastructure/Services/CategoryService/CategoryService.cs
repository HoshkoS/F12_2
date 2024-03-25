using Domain.Dtos.CategoryDtos;
using Domain.Models;
using Domain.Repositories;
using Domain.Services.CategoryService;
using Infrastructure.Database;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;


namespace Infrastructure.Services.CategoryService
{
    public class CategoryService: ICategoryService
    {
        private readonly CategoryRepository categoryRepository;
        private readonly ILogger _logger;
        private readonly ServerDbContext _context;

        public CategoryService(ServerDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            categoryRepository = new CategoryRepository(_context);
        }

        public async Task<Category> createCategory(CategoryDto category)
        {
            Category newCategory = new Category
            {
                Title = category.Title,
                Type = category.Type,
                PercentageAmount = category.PercentageAmount,
                UserId = category.UserId,
                IsGeneral = category.IsGeneral,
            };
            categoryRepository.Add(newCategory);
            return await Task.FromResult(newCategory);
        }
    }
}
