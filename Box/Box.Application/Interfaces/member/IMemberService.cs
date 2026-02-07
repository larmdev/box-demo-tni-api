using Box.Application.Dtos;
using Box.Application.Common;

namespace Box.Application.Interfaces;

public interface IMemberService
{
    Task<SearchResponse<MemberResponseDto>> Search(
        int offset,
        int limit,
        string? search
        );
    Task<ApiResponse<MemberResponseDto>> Get(string id);
    Task<ApiResponse<string>> Create(MemberRequestDto request);
    Task<ApiResponse<string>> Update(MemberRequestDto request);
    Task<ApiResponse<string>> Delete(string id);

}