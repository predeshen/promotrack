namespace PromoTrack.Application.Dtos;

/// <summary>
/// Data Transfer Object for creating a new user.
/// </summary>
public class CreateUserDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // Plain-text password from the client
    public string Role { get; set; } = string.Empty;
}