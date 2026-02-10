using Box.Domain.Entities;

namespace Box.Application.Interfaces;

public interface IProductRepository
{
    Task<(List<Product> Items, int Total)> Search(
        int offset,
        int limit,
        string? search
    );

    Task CheckOut(List<CheckOutRequestDto> req);

    // Task<Member?> Get(Guid id);
    // Task<bool> IsEmail(string email);
    // Task Update(Member req);
    // Task Delete(Guid id);

}