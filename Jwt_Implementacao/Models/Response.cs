namespace Jwt_Implementacao.Models;

public class Response<T>
{
    public T? Dados { get; set; }
    public string Mensagens { get; set; }
    public bool Status { get; set; } = true;
}
