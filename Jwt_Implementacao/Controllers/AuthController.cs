using Jwt_Implementacao.DTOs;
using Jwt_Implementacao.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jwt_Implementacao.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthInterface _authInterface;

    public AuthController(IAuthInterface authInterface)
    {
        _authInterface = authInterface;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UsuarioCriacaoDto usuarioRegister)
    {
        var resposta = await _authInterface.Registrar(usuarioRegister);

        return Ok(resposta);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
    {
        var resposta = await _authInterface.Login(usuarioLoginDto);
        return Ok(resposta);
    }
}
