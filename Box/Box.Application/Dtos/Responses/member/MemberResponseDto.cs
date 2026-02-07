using System.ComponentModel.DataAnnotations;

public class MemberResponseDto
{
    public Guid MemberId { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Position { get; set; } = default!;

    // เช่น 19990510
    public int Birthday { get; set; }

    // แปลงเป็น "10/05/1999"
    public string BirthdayStr
    {
        get
        {
            // parse yyyyMMdd
            var str = Birthday.ToString();
            if (str.Length != 8)
                return string.Empty;

            if (DateTime.TryParseExact(
                    str,
                    "yyyyMMdd",
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out var dt))
            {
                return dt.ToString("dd/MM/yyyy");
            }

            return string.Empty;
        }
    }

    public string Status { get; set; } = default!;
}


