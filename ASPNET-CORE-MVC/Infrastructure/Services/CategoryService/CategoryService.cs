using Domain.Dtos.CategoryDtos;
using Domain.Dtos.UserDtos;
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
        private readonly CategoryRepository _categoryRepository;
        private readonly ILogger _logger;
        private readonly ServerDbContext _context;

        public CategoryService(ServerDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _categoryRepository = new CategoryRepository(_context);
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
            _categoryRepository.Add(newCategory);
            return await Task.FromResult(newCategory);
        }

        public ICollection<Category> getUserCategories(Guid UserId)
        {
            try
            {
                var categories = _categoryRepository.Find(c => c.UserId == UserId || c.IsGeneral);
                if (categories == null)
                {
                    _logger.Error($"Categories for user with userID {UserId} not found.");
                    throw new Exception($"Categories for user with ID {UserId} not found.");
                }

                //var userDto = new UserDto(user);

                //return userDto;
                return _categoryRepository.GetAll().ToList();

            }
            catch (Exception ex)
            {
                _logger.Error($"Error occurred while getting category for user with ID {UserId}: {ex.Message}");
                throw;
            }
        }

        public void removeCategory(Guid CategoryId)
        {
            try
            {
                var category = _categoryRepository.FirstOrDefault(c => c.Id == CategoryId);
                if (category != null)
                {
                    _logger.Error($"Category with ID {CategoryId} not found.");
                    throw new Exception($"Category with ID {CategoryId} not found.");
                }
                _categoryRepository.Remove(category);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occurred while getting category with ID {CategoryId}: {ex.Message}");
                throw;
            }
        }
    }
}
