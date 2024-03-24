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
            this.Categories = user.Categories;
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Currency { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
