using Domain.Dtos.CategoryDtos;
using Domain.Models;
using Domain.Services.CategoryService;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Serilog;

namespace Infrastructure.Services.CategoryService;

using ILogger = ILogger;

public class CategoryService : ICategoryService
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

    public async Task<ICollection<CategoryDto>> GetUserCategories(Guid UserId)
    {
        try
        {
            var categories = await _categoryRepository.Find(c => c.UserId == UserId || c.IsGeneral);
            if (categories == null)
            {
                _logger.Error($"Categories for user with userID {UserId} not found.");
                throw new Exception($"Categories for user with ID {UserId} not found.");
            }

            var result = new List<CategoryDto>();
            foreach (var category in categories)
            {
                result.Add(new CategoryDto(category));
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while getting category for user with ID {UserId}: {ex.Message}");
            throw;
        }
    }

    public async Task<Category> CreateCategory(CategoryDto category)
    {
        var newCategory = new Category
        {
            Title = category.Title,
            Type = category.Type,
            PercentageAmount = category.PercentageAmount,
            UserId = category.UserId,
            IsGeneral = category.IsGeneral
        };
        _categoryRepository.Add(newCategory);
        return await Task.FromResult(newCategory);
    }

    public async Task<Category> UpdateCategory(CategoryDto category)
    {
        var existingCategory =
            await _categoryRepository.FirstOrDefault(c => c.Id == category.Id && c.UserId == category.UserId);
        if (existingCategory == null)
        {
            _logger.Error($"Category with title {category.Title} not found.");
            throw new Exception($"Category with title {category.Title} not found.");
        }

        existingCategory.Title = category.Title;
        existingCategory.Type = category.Type;
        existingCategory.PercentageAmount = category.PercentageAmount;

        await _categoryRepository.Update(existingCategory);
        return existingCategory;
    }

    public async Task RemoveCategory(CategoryDto category)
    {
        try
        {
            var categoryToRemove =
                await _categoryRepository.FirstOrDefault(c => c.Id == category.Id && c.UserId == category.UserId);
            if (categoryToRemove == null)
            {
                _logger.Error($"Category with title {category.Title} not found.");
                throw new Exception($"Category with title {category.Title} not found.");
            }

            await _categoryRepository.Remove(categoryToRemove);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error occurred while getting category with title {category.Title}: {ex.Message}");
            throw;
        }
    }
}