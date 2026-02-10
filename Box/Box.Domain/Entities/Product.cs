using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Box.Domain.Entities;

[Index(nameof(ProductId))]
public class Product
{
    [Key]
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public string? Code { get; set; }
    [ForeignKey(nameof(Code))]
    public Stock? Stock { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

}
