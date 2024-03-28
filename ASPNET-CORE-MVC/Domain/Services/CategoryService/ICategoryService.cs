using Domain.Dtos.CategoryDtos;
using Domain.Dtos.UserDtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CategoryService
{
    public interface ICategoryService
    {
        ICollection<CategoryDto> getUserCategories(Guid UserId);

        Task<Category> createCategory(CategoryDto category);

        Task<Category> updateCategory(CategoryDto category);

        void removeCategory(CategoryDto category);
    }
}
