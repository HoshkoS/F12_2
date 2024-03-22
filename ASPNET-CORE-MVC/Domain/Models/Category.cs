﻿namespace Domain.Models;

public class Category
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string Title { get; set; } = null!;

    public bool IsGeneral { get; set; }

    public decimal PercentageAmount { get; set; }

    public string Type { get; set; } = null!;

    public virtual User? User { get; set; }

    public virtual ICollection<Transaction> TransactionFromCategoryNavigations { get; set; } = new List<Transaction>();

    public virtual ICollection<Transaction> TransactionToCategoryNavigations { get; set; } = new List<Transaction>();

}