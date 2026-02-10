using System.ComponentModel.DataAnnotations;

public class CheckOutRequestDto
{
    public string Code { get; set; } = string.Empty;
    public int Amount { get; set; }
}

public class CheckOutRequestItemsDto
{
    public List<CheckOutRequestDto>? Items { get; set; }
}


