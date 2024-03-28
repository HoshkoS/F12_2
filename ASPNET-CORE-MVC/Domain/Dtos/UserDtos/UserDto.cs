using Domain.Dtos.CategoryDtos;
using Domain.Models;

namespace Domain.Dtos.UserDtos;

public class UserDto
{
    public UserDto(User user)
    {
        Name = user.Name;
        Surname = user.Surname;
        Email = user.Email;
        BirthDate = user.BirthDate;
        Currency = user.Currency;
        Categories = new List<CategoryDto>();

        foreach (var category in user.Categories)
        {
            Categories.Add(new CategoryDto(category));
        }
    }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public DateTime? BirthDate { get; set; }

    public string Currency { get; set; }

    public virtual ICollection<CategoryDto> Categories { get; set; }
}