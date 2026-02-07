using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Box.Application.Interfaces;

[ApiController]
[Route("api/members")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _service;

    public MembersController(
        IMemberService service
        )
    {
        _service = service;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Search(
        int offset = 0,
        int limit = 10,
        string? search = null)
    {
        var result = await _service.Search(offset, limit, search);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _service.Get(id);
        return Ok(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Create(MemberRequestDto req)
    {
        var result = await _service.Create(req);
        return Ok(result);
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> Update(MemberRequestDto req)
    {
        var result = await _service.Update(req);
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _service.Delete(id);
        return Ok(result);
    }
}

