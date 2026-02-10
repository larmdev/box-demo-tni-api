using Box.Application.Dtos;
using Box.Application.Common;

namespace Box.Application.Interfaces;

public interface IProductService
{
    Task<SearchResponse<ProductResponseDto>> Search(
        int offset,
        int limit,
        string? search
        );

    Task<ApiResponse<string>> CheckOut(List<CheckOutRequestDto> request);

    // Task<ApiResponse<List<CartResponseDto>>> Get();
    // Task<ApiResponse<string>> Create(MemberRequestDto request);
    // Task<ApiResponse<string>> Update(MemberRequestDto request);
    // Task<ApiResponse<string>> Delete(string id);

}