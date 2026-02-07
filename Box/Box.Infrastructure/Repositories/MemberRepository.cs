using Box.Domain.Entities;
using Box.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Box.Application.Interfaces;

namespace Box.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly AppDbContext _db;

    public MemberRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<(List<Member> Items, int Total)> Search(
        int offset,
        int limit,
        string? search
        )
    {
        try
        {
            var query = _db.Members
                .AsNoTracking()
                .Where(i => !i.IsDeleted);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(i => i.FullName.Contains(search) || i.Email.Contains(search));
            }

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(i => i.CreatedAt)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }
        catch (DbUpdateException ex)
        {
            // ดึง error จาก PostgreSQL
            var inner = ex.InnerException?.Message;
            throw new Exception(inner ?? ex.Message);
        }
    }

    public async Task<Member?> Get(Guid id)
    {
        var query = _db.Members
            .AsNoTracking()
            .Where(i => i.MemberId == id);

        var item = await query.FirstOrDefaultAsync();

        if (item == null) return null;

        return item;
    }

    public async Task<bool> IsEmail(string email)
    {
        try
        {
            var query = _db.Members
                .AsNoTracking()
                .Where(i => i.Email == email);

            return await query.AnyAsync(); ;
        }
        catch (DbUpdateException ex)
        {
            // ดึง error จาก PostgreSQL
            var inner = ex.InnerException?.Message;
            throw new Exception(inner ?? ex.Message);
        }
    }

    public async Task Create(Member member)
    {
        try
        {
            await _db.Members.AddAsync(member);
            await _db.SaveChangesAsync();
            return;
        }
        catch (DbUpdateException ex)
        {
            // ดึง error จาก PostgreSQL
            var inner = ex.InnerException?.Message;
            throw new Exception(inner ?? ex.Message);
        }
    }

    public async Task Update(Member member)
    {
        try
        {
            _db.Members.Update(member);
            await _db.SaveChangesAsync();
            return;
        }
        catch (DbUpdateException ex)
        {
            // ดึง error จาก PostgreSQL
            var inner = ex.InnerException?.Message;
            throw new Exception(inner ?? ex.Message);
        }
    }

    public async Task Delete(Guid memberId)
    {
        try
        {
            var member = await _db.Members
                .FirstOrDefaultAsync(i => i.MemberId == memberId);

            if (member == null)
                throw new Exception("Member not found");

            // soft delete
            member.IsDeleted = true;

            // _db.Members.Remove(member);
            await _db.SaveChangesAsync();
            return;
        }
        catch (DbUpdateException ex)
        {
            // ดึง error จาก PostgreSQL
            var inner = ex.InnerException?.Message;
            throw new Exception(inner ?? ex.Message);
        }
    }

}

