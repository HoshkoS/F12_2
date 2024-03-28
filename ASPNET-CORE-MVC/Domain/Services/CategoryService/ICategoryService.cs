using Domain.Dtos.CategoryDtos;
using Domain.Models;

namespace Domain.Services.CategoryService;

public interface ICategoryService
{
    Task<ICollection<CategoryDto>> GetUserCategories(Guid UserId);

    Task<Category> CreateCategory(CategoryDto category);

    Task<Category> UpdateCategory(CategoryDto category);

    Task RemoveCategory(CategoryDto category);
}