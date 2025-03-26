using Jwt_Implementacao.DTOs;
using Jwt_Implementacao.Models;

namespace Jwt_Implementacao.Services.AuthService
{
    public interface IAuthInterface
    {
        Task<Response<UsuarioCriacaoDto>> Registrar(UsuarioCriacaoDto usuarioRegistro);
        Task<Response<string>> Login(UsuarioLoginDto usuarioLogin);
    }
}
