namespace CashFlow.Communication.Responses.User;
public class ResponseUserRegistered
{
    public ResponseUserRegistered(string email, string token)
    {
        Email = email;
        Token = token;
    }

    public string Email { get; set; } = string.Empty;
    public string Token { get; set; }

}
