using System.Globalization;
using Box.Application.Interfaces;
using Box.Domain.Entities;
using Box.Application.Dtos;
using Box.Application.Common;

namespace Box.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly ICurrentUserService _currentUser;

    public ProductService(
        IProductRepository repo,
        ICurrentUserService currentUser
        )
    {
        _repo = repo;
        _currentUser = currentUser;

    }

    public async Task<SearchResponse<ProductResponseDto>> Search(
        int offset,
        int limit,
        string? search
        )
    {
        try
        {
            var (products, total) = await _repo.Search(offset, limit, search);

            if (products == null) return SearchResponse<ProductResponseDto>.Error(404, "Users is not found!");

            var items = products.Select(s => new ProductResponseDto()
            {
                Code = s.Code!,
                Name = s.Name,
                Price = s.Price,
                Remain = s.Stock!.Amount
            }).ToList();

            return SearchResponse<ProductResponseDto>.Success(
                items,
                total,
                offset,
                limit
            );
        }
        catch (Exception ex)
        {
            return SearchResponse<ProductResponseDto>.Error(ex.Message);
        }
    }

    public async Task<ApiResponse<string>> CheckOut(List<CheckOutRequestDto> req)
    {
        try
        {
            await _repo.CheckOut(req);
            return ApiResponse<string>.Success();
        }
        catch (Exception ex)
        {
            return ApiResponse<string>.Error(ex.Message);
        }
    }


}
