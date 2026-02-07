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
    private readonly IAuthService _service;

    public MembersController(
        IAuthService service
        )
    {
        _service = service;
    }

    [HttpPost("")]
    public async Task<IActionResult> Register([FromBody] AuthRequestDto request)
    {
        var result = await _service.RegisterAsync(request);
        return Ok(result);
    }
}

