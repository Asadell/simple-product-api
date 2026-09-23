using System.ComponentModel.DataAnnotations;

namespace SimpleApi.DTOs.Auth;

public record RegisterRequest(
    [Required, MinLength(3), MaxLength(100)] string Username,
    [Required, MinLength(6)] string Password
);

public record LoginRequest(
    [Required] string Username,
    [Required] string Password
);

public record AuthResponse(
    string AccessToken,
    DateTime ExpiresAt,
    string Username
);
