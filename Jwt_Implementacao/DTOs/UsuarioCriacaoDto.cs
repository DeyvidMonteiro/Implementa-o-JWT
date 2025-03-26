using Jwt_Implementacao.Enum;
using System.ComponentModel.DataAnnotations;

namespace Jwt_Implementacao.DTOs
{
    public class UsuarioCriacaoDto
    {
        [Required(ErrorMessage = "O campo usuario é obrigatório")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório"), EmailAddress(ErrorMessage = "Email Invalido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não coincidem")]
        public string ConfirmaSenha { get; set; }

        [Required(ErrorMessage = "O campo Cargo é obrigatório")]
        public CargoEnum cargo { get; set; }
    }
}
