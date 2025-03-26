using Jwt_Implementacao.Data;
using Jwt_Implementacao.DTOs;
using Jwt_Implementacao.Models;
using Jwt_Implementacao.Services.SenhaService;
using Microsoft.EntityFrameworkCore;

namespace Jwt_Implementacao.Services.AuthService;

public class AuthServices : IAuthInterface
{
    private readonly AppDbContext _context;
    private readonly ISenhaInterface _senhaInterface;

    public AuthServices(AppDbContext context, ISenhaInterface senhaInterface)
    {
        _context = context;
        _senhaInterface = senhaInterface;
    }

    public async Task<Response<UsuarioCriacaoDto>> Registrar(UsuarioCriacaoDto usuarioRegistro)
    {
        Response<UsuarioCriacaoDto> respostaServico = new Response<UsuarioCriacaoDto>();
        try
        {
            if (!VerificaSeEmaileUsuarioExistem(usuarioRegistro))
            {
                respostaServico.Dados = null;
                respostaServico.Mensagens = "Email/Usuario já cadastrados";
                respostaServico.Status = false;
                return respostaServico;
            }

            _senhaInterface.CriarSenhaHash(usuarioRegistro.Senha, out byte[] senhaHash, out byte[] SenhaSalt);

            UsuarioModel usuario = new()
            {
                Usuario = usuarioRegistro.Usuario,
                Email = usuarioRegistro.Email,
                Cargo = usuarioRegistro.cargo,
                SenhaHash = senhaHash,
                SenhaSalt = SenhaSalt
            };

            _context.Add(usuario);
            await _context.SaveChangesAsync();

            respostaServico.Mensagens = "Usuário Criado com sucesso!";


        }
        catch (Exception ex)
        {
            respostaServico.Dados = null;
            respostaServico.Mensagens = ex.Message;
            respostaServico.Status = false;
        }

        return respostaServico;
    }

    public async Task<Response<string>> Login (UsuarioLoginDto usuarioLogin)
    {
        Response<string> respostaServico = new();

        try
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(userBanco => userBanco.Email == usuarioLogin.Email);

            if(usuario == null)
            {
                respostaServico.Mensagens = "Credenciais Invalidas!";
                respostaServico.Status = false;
                return respostaServico;
            }

            if(!_senhaInterface.VerificaSenhaHash(usuarioLogin.Senha, usuario.SenhaHash, usuario.SenhaSalt))
            {
                respostaServico.Mensagens = "Credenciais Invalidas!";
                respostaServico.Status = false;
                return respostaServico;
            }

            var token = _senhaInterface.CriarToken(usuario);

            respostaServico.Dados = token;
            respostaServico.Mensagens = "Usuário logado com sucesso!";

        }
        catch(Exception ex)
        {
            respostaServico.Dados = null;
            respostaServico.Mensagens = ex.Message;
            respostaServico.Status = false;
        }

        return respostaServico;
    }

    public bool VerificaSeEmaileUsuarioExistem(UsuarioCriacaoDto usuarioRegistro)
    {
        var usuario = _context.Usuario.FirstOrDefault(userBanco =>
                            userBanco.Email == usuarioRegistro.Email ||
                            userBanco.Usuario == usuarioRegistro.Usuario);

        if (usuario != null)
            return false;

        return true;
    }

}
