using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PromoTrack.Application.Dtos;
using PromoTrack.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PromoTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthController(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginRequest)
    {
        // 1. Find the user by email
        var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);

        // --- THIS IS THE CRITICAL FIX ---
        // First, check if the user even exists.
        if (user == null)
        {
            // If user is null, immediately return Unauthorized.
            return Unauthorized("Invalid credentials.");
        }

        // Only if the user exists, then check the password.
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid credentials.");
        }
        // --- END OF FIX ---


        // 3. Generate the JWT
        var token = GenerateJwtToken(user);

        // 4. Return the response DTO
        var response = new LoginResponseDto
        {
            Token = token,
            UserId = user.UserId,
            Email = user.Email,
            FirstName = user.FirstName,
            Role = user.Role
        };

        return Ok(response);
    }

    private string GenerateJwtToken(Domain.User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("firstName", user.FirstName)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}