using System.Globalization;
using Box.Application.Interfaces;
using Box.Domain.Entities;
using Box.Application.Dtos;
using Box.Application.Common;

namespace Box.Application.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _repo;
    private readonly ICurrentUserService _currentUser;

    public MemberService(
        IMemberRepository repo,
        ICurrentUserService currentUser
        )
    {
        _repo = repo;
        _currentUser = currentUser;

    }

    public async Task<SearchResponse<MemberResponseDto>> Search(
        int offset,
        int limit,
        string? search
        )
    {
        try
        {
            var (members, total) = await _repo.Search(offset, limit, search);

            if (members == null) return SearchResponse<MemberResponseDto>.Error(404, "Users is not found!");

            var items = members.Select(s => new MemberResponseDto()
            {
                MemberId = s.MemberId,
                FullName = s.FullName,
                Email = s.Email,
                Phone = s.Phone,
                Position = s.Position,
                Birthday = s.Birthday,
                Status = s.Status
            }).ToList();

            return SearchResponse<MemberResponseDto>.Success(
                items,
                total,
                offset,
                limit
            );
        }
        catch (Exception ex)
        {
            return SearchResponse<MemberResponseDto>.Error(ex.Message);
        }
    }


    public async Task<ApiResponse<MemberResponseDto>> Get(string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id)) return ApiResponse<MemberResponseDto>.Error(404, "MemberId is not found!");
            Guid memberId = Guid.Parse(id);

            var member = await _repo.Get(memberId);

            if (member == null) return ApiResponse<MemberResponseDto>.Error(404, "User is not found!");

            var item = new MemberResponseDto()
            {
                MemberId = member.MemberId,
                FullName = member.FullName,
                Email = member.Email,
                Phone = member.Phone,
                Position = member.Position,
                Birthday = member.Birthday,
                Status = member.Status
            };

            return ApiResponse<MemberResponseDto>.Success(item);
        }
        catch (Exception ex)
        {
            return ApiResponse<MemberResponseDto>.Error(ex.Message);
        }
    }

    public async Task<ApiResponse<string>> Create(MemberRequestDto req)
    {
        try
        {
            var email = await _repo.IsEmail(req.Email);
            if (email) return ApiResponse<string>.Error(404, "Email is Duplicated!");

            var item = new Member()
            {
                FullName = req.FullName,
                Email = req.Email,
                Phone = req.Phone,
                Position = req.Position,
                Birthday = 19990510,
                Status = req.Status
            };

            await _repo.Create(item);

            return ApiResponse<string>.Success();
        }
        catch (Exception ex)
        {
            return ApiResponse<string>.Error(ex.Message);
        }
    }

    public async Task<ApiResponse<string>> Update(MemberRequestDto req)
    {
        try
        {
            if (req.MemberId == null || req.MemberId == Guid.Empty) return ApiResponse<string>.Error(500, "MemberId is not found!");
            Guid memberId = req.MemberId ?? Guid.NewGuid();

            var member = await _repo.Get(memberId);
            if (member == null) return ApiResponse<string>.Error(404, "User is not found!");

            var date = DateTime.ParseExact(
                req.BirthdayStr,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture
            );
            
            var birthday = int.Parse(date.ToString("yyyyMMdd"));

            var item = new Member()
            {
                MemberId = memberId,
                FullName = req.FullName,
                Email = req.Email,
                Phone = req.Phone,
                Position = req.Position,
                Birthday = birthday,
                Status = req.Status,
                CreatedAt = member.CreatedAt
            };

            await _repo.Update(item);

            return ApiResponse<string>.Success();
        }
        catch (Exception ex)
        {
            return ApiResponse<string>.Error(ex.Message);
        }
    }

    public async Task<ApiResponse<string>> Delete(string id)
    {
        try
        {
            if (id == null || id == "") return ApiResponse<string>.Error(404, "MemberId is not found!");

            Guid memberId = Guid.Parse(id);

            await _repo.Delete(memberId);

            return ApiResponse<string>.Success();
        }
        catch (Exception ex)
        {
            return ApiResponse<string>.Error(ex.Message);
        }
    }

}
