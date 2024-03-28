namespace Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string Name { get; set; }

    public required string Surname { get; set; }

    public required DateTime BirthDate { get; set; }

    public required string Currency { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}