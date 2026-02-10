using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Box.Domain.Entities;

[Index(nameof(Code))]
public class Stock
{
    [Key]
    public string Code { get; set; } = string.Empty;
    public int Amount { get; set; }
}
