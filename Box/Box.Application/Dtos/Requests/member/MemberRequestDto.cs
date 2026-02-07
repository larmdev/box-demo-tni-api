using System.ComponentModel.DataAnnotations;

public class MemberRequestDto
{
    public Guid? MemberId { get; set; }

    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = default!;

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = default!;

    [Required]
    [RegularExpression(@"^\+?[0-9]{9,15}$")]
    public string Phone { get; set; } = default!;

    [Required]
    [MaxLength(100)]
    public string Position { get; set; } = default!;

    [Required]
    public string BirthdayStr { get; set; } = default!;

    [Required]
    public string Status { get; set; } = default!;
}

