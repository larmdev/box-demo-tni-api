using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Box.Application.Interfaces;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    public ProductsController(
        IProductService service
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

    [HttpPost]
    [Route("checkout")]
    public async Task<IActionResult> CheckOut([FromBody] CheckOutRequestItemsDto req)
    {
        var result = await _service.CheckOut(req.Items!);
        return Ok(result);
    }
}

