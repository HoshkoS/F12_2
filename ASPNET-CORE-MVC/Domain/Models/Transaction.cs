namespace Domain.Models;

public class Transaction
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? FromCategory { get; set; }

    public Guid? ToCategory { get; set; }

    public decimal Amount { get; set; }

    public DateTime? Date { get; set; }

    public string? Details { get; set; }

    public bool Planned { get; set; }
}