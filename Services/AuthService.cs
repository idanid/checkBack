using checkBack.Services.Interfaces;

namespace checkBack.Services;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthService> _logger;

    // username → password  (in-memory store, no DB)
    // 1 admin, 3 regular users
    private readonly Dictionary<string, (string Password, string Role)> _users = new()
    {
        { "admin",  ("Admin123!",  "admin") },
        { "alice",  ("Alice123!",  "user")  },
        { "bob",    ("Bob123!",    "user")  },
        { "charlie",("Charlie123!","user")  }
    };

    public AuthService(IJwtService jwtService, ILogger<AuthService> logger)
    {
        _jwtService = jwtService;
        _logger = logger;
    }

    public AuthResult? Authenticate(string username, string password)
    {
        var key = username.ToLowerInvariant();
        if (!_users.TryGetValue(key, out var entry) || entry.Password != password)
        {
            _logger.LogWarning("Failed login attempt for username: {Username}", username);
            return null;
        }

        _logger.LogInformation("Successful login for username: {Username}, role: {Role}", username, entry.Role);
        var token = _jwtService.GenerateToken(key, entry.Role);
        return new AuthResult(token, key, entry.Role);
    }
}
