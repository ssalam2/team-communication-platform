namespace CollabPlatform.Shared.DTOs.Auth;

public record AuthResponse(string Token, string Username, Guid UserId);
