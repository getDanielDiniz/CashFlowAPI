namespace CashFlow.Communication.Responses.User;
public class ResponseUserRegistered
{
    public ResponseUserRegistered(string email, Guid token = new Guid())
    {
        Email = email;
        Token = token;
    }

    public string Email { get; set; } = string.Empty;
    public Guid Token { get; set; }

}
