﻿using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Domain.Dtos.CategoryDtos;

public class CategoryDto
{
    public CategoryDto()
    {
    }

    public CategoryDto(Category category)
    {
        Id = category.Id;
        UserId = category.UserId;
        Title = category.Title;
        IsGeneral = category.IsGeneral;
        PercentageAmount = category.PercentageAmount;
        Type = category.Type;
    }

    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    [Required(ErrorMessage = "The Title field is required.")]
    public string Title { get; set; } = null!;

    public bool IsGeneral { get; set; }

    [Range(0, 100, ErrorMessage = "Percentage Amount must be between 0 and 100.")]
    public decimal PercentageAmount { get; set; }

    public string Type { get; set; } = null!;

    //public virtual ICollection<Transaction> TransactionFromCategoryNavigations { get; set; } = new List<Transaction>();

    //public virtual ICollection<Transaction> TransactionToCategoryNavigations { get; set; } = new List<Transaction>();
}