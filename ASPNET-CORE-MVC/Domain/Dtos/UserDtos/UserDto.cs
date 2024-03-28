using Domain.Dtos.CategoryDtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.UserDtos
{
    public class UserDto
    {
        public UserDto(User user)
        {
            this.Name = user.Name;
            this.Surname = user.Surname;
            this.Email = user.Email;
            this.BirthDate = user.BirthDate;
            this.Currency = user.Currency;
            this.Categories = new List<CategoryDto>();
            foreach (var category in user.Categories)
            {
                this.Categories.Add(new CategoryDto(category));
            }
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Currency { get; set; }

        public virtual ICollection<CategoryDto> Categories { get; set; }
    }
}
