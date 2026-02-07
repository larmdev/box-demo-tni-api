using Box.Domain.Entities;

namespace Box.Application.Interfaces;

public interface IMemberRepository
{
    Task<(List<Member> Items, int Total)> Search(
        int offset,
        int limit,
        string? search
    );

    Task<Member?> Get(Guid id);
    Task<bool> IsEmail(string email);
    Task Create(Member req);
    Task Update(Member req);
    Task Delete(Guid id);

}