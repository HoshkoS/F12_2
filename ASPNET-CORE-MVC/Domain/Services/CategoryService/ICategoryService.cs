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
        Task<Category> createCategory(CategoryDto category);

        ICollection<Category> getUserCategories(Guid UserId);

        void removeCategory(Guid CategoryId);
    }
}
