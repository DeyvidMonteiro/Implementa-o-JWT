using Jwt_Implementacao.Models;

namespace Jwt_Implementacao.Services.SenhaService;

public interface ISenhaInterface
{
    void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
    bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);
    string CriarToken(UsuarioModel usuario);
}

