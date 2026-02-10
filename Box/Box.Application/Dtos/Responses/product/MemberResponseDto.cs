using System.ComponentModel.DataAnnotations;

public class ProductResponseDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Remain { get; set; }

}


