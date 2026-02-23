using CollabPlatform.Shared.DTOs.Auth;

namespace CollabPlatform.API.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}
