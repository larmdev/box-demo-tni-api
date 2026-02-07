using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Box.Domain.Entities;

[Index(nameof(MemberId))]
public class Member
{
    [Key]
    public Guid MemberId { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Position { get; set; } = default!;
    public int Birthday { get; set; }
    public string Status { get; set; } = default!;
    public bool IsDeleted { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
