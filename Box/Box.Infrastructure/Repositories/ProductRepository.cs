using Box.Domain.Entities;
using Box.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Box.Application.Interfaces;
using System.Text.Json;

namespace Box.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<(List<Product> Items, int Total)> Search(
        int offset,
        int limit,
        string? search
        )
    {
        try
        {
            var query = _db.Products
                .Include(i => i.Stock)
                .AsNoTracking();

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(i => i.Code)
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

    public async Task CheckOut(List<CheckOutRequestDto> items)
    {
        try
        {
            var codes = items.Select(x => x.Code).ToList();

            var stocks = await _db.Stocks
                .Where(s => codes.Contains(s.Code))
                .ToListAsync();

            foreach (var stock in stocks)
            {
                var req = items.First(x => x.Code == stock.Code);

                if (stock.Amount >= req.Amount)
                {
                    stock.Amount -= req.Amount;
                }
            }

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

