namespace checkBack.Services.Interfaces;

public record AuthResult(string Token, string Username, string Role);

public interface IAuthService
{
    AuthResult? Authenticate(string username, string password);
}
